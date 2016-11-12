using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DICOM_Fetch
{
    public class DICOM_ListView_Manager
    {
        ListView lv;
        
        public DICOM_ListView_Manager(ListView listview)
        {
            lv = listview;
        }

        public ListView ManagedListView
        {
            get { return lv; }
        }

        public gdcm.DataSet SelectedDataSet()
        {
            return GetDataSetFromListViewItem(lv.SelectedItems[0]);
        }

        public gdcm.DataSetArrayType SelectedDataSets
        {
            get
            {
                gdcm.DataSetArrayType dsa=new gdcm.DataSetArrayType();
                foreach (ListViewItem lvi in lv.SelectedItems)
                {
                    dsa.Add(GetDataSetFromListViewItem(lvi));
                }
                return dsa;
            }
        }

        public gdcm.DataSet SelectedDataSet(int index)
        {
            if (index > lv.Items.Count - 1) { return null; }
            return GetDataSetFromListViewItem(lv.SelectedItems[index]);
        }

        private bool ListViewItems_AreTheSame(ListViewItem lvi1, ListViewItem lvi2)
        {
            if(lvi1.Text!=lvi2.Text){return false;}
            if(lvi1.SubItems.Count!=lvi2.SubItems.Count){return false;}
            for (int n = 0; n < lvi1.SubItems.Count; n++)
            {
                if (lvi1.SubItems[n].Text != lvi2.SubItems[n].Text) { return false; }
            }
            return true;
        }

        public void UpdateListview(gdcm.DataSetArrayType values)
        {
            lv_items.Clear();
            foreach (gdcm.DataSet ds in values)
            {
                ListViewItem newitem = patientlistviewitem(ds);
                bool skip = false;
                if (lv_items.Keys.Count > 0)
                {
                    foreach(ListViewItem lvi in lv_items.Keys){
                        if (ListViewItems_AreTheSame(newitem, lvi))
                        {
                            skip = true;
                            break;
                        }
                    }
                }
                if (!skip) { lv_items.Add(newitem, ds); }
            }
            lv.Parent.BeginInvoke(new delegate_ChangelistView1(ChangelistView1));
        }

        public gdcm.DataSet GetDataSetFromListViewItem(ListViewItem listviewitem)
        {
            gdcm.DataSet retval;
            lv_items.TryGetValue(listviewitem, out retval);
            return retval;
        }

        private delegate void delegate_ChangelistView1();
        private System.Collections.Generic.Dictionary<ListViewItem, gdcm.DataSet> lv_items = new System.Collections.Generic.Dictionary<ListViewItem, gdcm.DataSet>();
        private void ChangelistView1()
        {
            lv.Items.Clear();
            lv.Items.AddRange(lv_items.Keys.ToArray<ListViewItem>());
        }
        public gdcm.TagArrayType ShownTags()
        {
            gdcm.TagArrayType retval = new gdcm.TagArrayType();
            foreach (gdcm.Tag t in showntags)
            {
                retval.Add(t);
            }
            return retval;
        }
        private System.Collections.Generic.List<gdcm.Tag> showntags = new System.Collections.Generic.List<gdcm.Tag>();
        public void AddShownValue(gdcm.Tag tag)
        {
            showntags.Add(tag);
        }
        private ListViewItem patientlistviewitem(gdcm.DataSet ds)
        {
            ListViewItem retval = new ListViewItem();
            for (int n = 0; n < showntags.Count; n++)
            {
                String thisval = "";
                if (ds.FindDataElement(showntags[n]))
                {
                    gdcm.DataElement de = ds.GetDataElement(showntags[n]);
                    if (!de.IsEmpty()) {
                        thisval = de.GetValue().toString(); 
                    }
                }
                if (n == 0)
                {
                    retval.Text = thisval;
                }
                else
                {
                    retval.SubItems.Add(thisval);
                }
            }
            return retval;
        }
    }
}
