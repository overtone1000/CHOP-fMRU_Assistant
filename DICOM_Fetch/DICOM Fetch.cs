using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DICOM_Fetch
{
    public partial class MainForm : Form
    {
        private DICOM_NetworkConfigurations configs = new DICOM_NetworkConfigurations();
        private form_NetworkConfig ncf;

        private DICOM_ListView_Manager lv_patients_man;
        private DICOM_ListView_Manager lv_studies_man;
        private DICOM_ListView_Manager lv_series_man;

        public MainForm()
        {
            InitializeComponent();

            lv_patients_man = new DICOM_ListView_Manager(listView1);
            lv_studies_man = new DICOM_ListView_Manager(listView2);
            lv_series_man = new DICOM_ListView_Manager(listView3);

            lv_patients_man.AddShownValue(GRD_Utils.Tags.tag_patientname);
            lv_patients_man.AddShownValue(GRD_Utils.Tags.tag_patientMRN);
            lv_patients_man.AddShownValue(GRD_Utils.Tags.tag_patientDOB);

            lv_studies_man.AddShownValue(GRD_Utils.Tags.tag_studyInstanceUID);
            lv_studies_man.AddShownValue(GRD_Utils.Tags.tag_studyDate);
            lv_studies_man.AddShownValue(GRD_Utils.Tags.tag_studyDescription);
            lv_studies_man.AddShownValue(GRD_Utils.Tags.tag_patientname);
            //lv_studies_man.AddShownValue(tag_patientMRN);

            lv_series_man.AddShownValue(GRD_Utils.Tags.tag_seriesInstanceUID);
            lv_series_man.AddShownValue(GRD_Utils.Tags.tag_seriesDescription);
            lv_series_man.AddShownValue(GRD_Utils.Tags.tag_seriesnumberofrelatedinstances);
            lv_series_man.AddShownValue(GRD_Utils.Tags.tag_seriesModality);
            lv_series_man.AddShownValue(GRD_Utils.Tags.tag_seriesNumber);
                       

            toolStripProgressBar1.Minimum = 0;
            toolStripProgressBar1.Maximum = 1000;

            configs.ConfigurationChanged += new EventHandler(configchange);
            this.listView1.ColumnClick += new ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView2.ColumnClick += new ColumnClickEventHandler(this.listView2_ColumnClick);
            this.listView3.ColumnClick += new ColumnClickEventHandler(this.listView3_ColumnClick);

            this.Shown += new EventHandler(formshown);

            if(System.IO.Directory.Exists(Properties.Settings.Default.CMove_dir))
            {
                changetargetdir(Properties.Settings.Default.CMove_dir);
            }
        }

        private void formshown(object sender, EventArgs e)
        {
            configchange(this, null);
        }
         
        CEcho configtest;
        DICOMNetworkEventHandler_Status configtesthandler;
        private void configchange(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Selected Network Configuration: " + configs.Current().Label;
            if (configtest != null) { 
                configtest.StatusChange -= configtesthandler; 
            }
            configtest = new CEcho(configs.Current());
            configtesthandler = new DICOMNetworkEventHandler_Status(statuschange_CEcho);
            configtest.StatusChange += configtesthandler;
            configtest.Execute();
        }

        CFind Cfind_patients;
        DICOMNetworkEventHandler_Status Cfind_patient_eventhandler;
        private void CFind_patients(gdcm.BaseRootQuery theQuery)
        {
            if (Cfind_patients != null)
            {
                Cfind_patients.StatusChange -= Cfind_patient_eventhandler;
            }
            Cfind_patients = new CFind(configs.Current(), theQuery);
            Cfind_patient_eventhandler = new DICOMNetworkEventHandler_Status(statuschange_CFind_patients);
            Cfind_patients.StatusChange += Cfind_patient_eventhandler;
            Cfind_patients.Execute();
        }

        CFind Cfind_studies;
        DICOMNetworkEventHandler_Status Cfind_studies_eventhandler;
        private void CFind_studies(gdcm.BaseRootQuery theQuery)
        {
            if (Cfind_studies != null)
            {
                Cfind_studies.StatusChange -= Cfind_studies_eventhandler;
            }
            Cfind_studies = new CFind(configs.Current(), theQuery);
            Cfind_studies_eventhandler = new DICOMNetworkEventHandler_Status(statuschange_CFind_studies);
            Cfind_studies.StatusChange += Cfind_studies_eventhandler;
            Cfind_studies.Execute();
        }

        CFind Cfind_series;
        DICOMNetworkEventHandler_Status Cfind_series_eventhandler;
        private void CFind_series(gdcm.BaseRootQuery theQuery)
        {
            if (Cfind_series != null)
            {
                Cfind_series.StatusChange -= Cfind_series_eventhandler;
            }
            Cfind_series = new CFind(configs.Current(), theQuery);
            Cfind_series_eventhandler = new DICOMNetworkEventHandler_Status(statuschange_CFind_series);
            Cfind_series.StatusChange += Cfind_series_eventhandler;
            Cfind_series.Execute();
        }
        //CMove Cmove_series;
        movescu CMove_series_movescu;
        DICOMNetworkEventHandler_Status Cmove_series_eventhandler;
        private void CMove_series(gdcm.DataSetArrayType dsa)
        {
            /*
            if (Cmove_series != null)
            {
                Cmove_series.StatusChange -= Cmove_series_eventhandler;
            }
                        
            Cmove_series = new CMove(configs.Current(), dsa, targetdir.FullName);
            Cmove_series_eventhandler = new DICOMNetworkEventHandler_Status(statuschange_CMove_series);
            Cmove_series.StatusChange += Cmove_series_eventhandler;
            Cmove_series.Execute();
            */

            if (CMove_series_movescu !=null)
            {
                CMove_series_movescu.StatusChange -= Cmove_series_eventhandler;
            }

            CMove_series_movescu = new movescu(configs.Current(), dsa, targetdir.FullName);
            Cmove_series_eventhandler = new DICOMNetworkEventHandler_Status(statuschange_CMove_series);
            CMove_series_movescu.StatusChange += Cmove_series_eventhandler;
            CMove_series_movescu.Execute();
        }

        private void statuschange_CEcho(DICOMNetworkQueryStatus status)
        {
            echostatus = status;
            this.BeginInvoke(new delegate_ChangeStatusStrip1(ChangeStatusStrip1));
        }

        private void statuschange_CFind_patients(DICOMNetworkQueryStatus status)
        {
            findstatus = status;
            this.BeginInvoke(new delegate_ChangeStatusStrip2(ChangeStatusStrip2));
            if (status == DICOMNetworkQueryStatus.Success)
            {
                lv_patients_man.UpdateListview(Cfind_patients.GetResults());
            }
        }

        private void statuschange_CFind_studies(DICOMNetworkQueryStatus status)
        {
            findstatus = status;
            this.BeginInvoke(new delegate_ChangeStatusStrip2(ChangeStatusStrip2));
            if (status == DICOMNetworkQueryStatus.Success)
            {
                lv_studies_man.UpdateListview(Cfind_studies.GetResults());
            }
        }

        private void statuschange_CFind_series(DICOMNetworkQueryStatus status)
        {
            findstatus = status;
            this.BeginInvoke(new delegate_ChangeStatusStrip2(ChangeStatusStrip2));
            if (status == DICOMNetworkQueryStatus.Success)
            {
                lv_series_man.UpdateListview(Cfind_series.GetResults());
            }
        }

        private void statuschange_CMove_series(DICOMNetworkQueryStatus status)
        {
            movestatus = status;
            this.BeginInvoke(new delegate_ChangeCMove(ChangeCMove));
        }

        private delegate void delegate_ChangeStatusStrip1();
        private DICOMNetworkQueryStatus echostatus;
        private void ChangeStatusStrip1()
        {
            switch (echostatus)
            {
                case DICOMNetworkQueryStatus.Waiting:
                    toolStripStatusLabel1.Text = configs.Current().Label + ": attempting communication with server.";
                    toolStripStatusLabel1.BackColor = Color.Yellow;
                    break;
                case DICOMNetworkQueryStatus.Success:
                    toolStripStatusLabel1.Text = configs.Current().Label + ": connection established.";
                    toolStripStatusLabel1.BackColor = Color.LightGreen;
                    break;
                case DICOMNetworkQueryStatus.Failure:
                    toolStripStatusLabel1.Text = configs.Current().Label + ": connection failure.";
                    toolStripStatusLabel1.BackColor = Color.Red;
                    break;
            }
        }

        private delegate void delegate_ChangeStatusStrip2();
        private DICOMNetworkQueryStatus findstatus;
        private void ChangeStatusStrip2()
        {
            switch (findstatus)
            {
                case DICOMNetworkQueryStatus.Waiting:
                    toolStripStatusLabel2.Text = "Query in progress.";
                    toolStripStatusLabel2.BackColor = Color.Yellow;
                    break;
                case DICOMNetworkQueryStatus.Receiving:
                    toolStripStatusLabel2.Text = "Receiving.";
                    toolStripStatusLabel2.BackColor = Color.LightBlue;
                    break;
                case DICOMNetworkQueryStatus.Success:
                    toolStripStatusLabel2.Text = "Query complete.";
                    toolStripStatusLabel2.BackColor = Color.LightGreen;
                    break;
                case DICOMNetworkQueryStatus.Failure:
                    toolStripStatusLabel2.Text = "Query failed.";
                    toolStripStatusLabel2.BackColor = Color.Red;
                    break;
            }
        }

        private delegate void delegate_ChangeCMove();
        private DICOMNetworkQueryStatus movestatus;
        private void ChangeCMove()
        {
            switch (movestatus)
            {
                case DICOMNetworkQueryStatus.Waiting:
                case DICOMNetworkQueryStatus.Receiving:
                    toolStripStatusLabel2.Text = "Move in progress.";
                    toolStripStatusLabel2.BackColor = Color.LightBlue;
                    toolStripProgressBar1.Value = (int)((float)(toolStripProgressBar1.Maximum - toolStripProgressBar1.Minimum) * CMove_series_movescu.Progress + toolStripProgressBar1.Minimum);
                    break;
                case DICOMNetworkQueryStatus.Success:
                    toolStripStatusLabel2.Text = "Move complete.";
                    toolStripStatusLabel2.BackColor = Color.LightGreen;
                    toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
                    break;
                case DICOMNetworkQueryStatus.Failure:
                    toolStripStatusLabel2.Text = "Move failed.";
                    toolStripStatusLabel2.BackColor = Color.Red;
                    toolStripProgressBar1.Value = toolStripProgressBar1.Minimum;
                    break;
            }
        }

        private void networkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ncf = new form_NetworkConfig(configs);
            ncf.ShowDialog();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }
        private void copyselecteddataset(DICOM_ListView_Manager lv, gdcm.DataSet ds)
        {
            foreach (gdcm.Tag t in lv.ShownTags())
            {
                if(lv.SelectedDataSet().FindDataElement(t))
                {
                    ds.Insert(lv.SelectedDataSet().GetDataElement(t));
                }
            }
        }
        private void copyselecteddataset(DICOM_ListView_Manager lv, int index, gdcm.DataSet ds)
        {
            foreach (gdcm.Tag t in lv.ShownTags())
            {
                if (lv.SelectedDataSet(index).FindDataElement(t))
                {
                    ds.Insert(lv.SelectedDataSet(index).GetDataElement(t));
                }
            }
        }

        private void addtagstodataset(DICOM_ListView_Manager lv, gdcm.DataSet ds)
        {
            foreach (gdcm.Tag t in lv.ShownTags())
            {
                ds.Insert(new gdcm.DataElement(t));
            }
        }
        
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) { return; }
            listView2.Items.Clear();
            listView3.Items.Clear();

            gdcm.DataSet ds = new gdcm.DataSet();
            copyselecteddataset(lv_patients_man, ds);
            addtagstodataset(lv_studies_man, ds);

            gdcm.BaseRootQuery theQuery = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.ePatientRootType, gdcm.EQueryLevel.eStudy, ds);
            theQuery.InitializeDataSet(gdcm.EQueryLevel.eStudy);
            theQuery.ValidateQuery(true);
            CFind_studies(theQuery);
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count <= 0) { return; }
            listView3.Items.Clear();

            gdcm.DataSet ds = new gdcm.DataSet();
            copyselecteddataset(lv_patients_man, ds);
            copyselecteddataset(lv_studies_man, ds);
            addtagstodataset(lv_series_man, ds);

            gdcm.BaseRootQuery theQuery = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.eStudyRootType, gdcm.EQueryLevel.eSeries, ds);
            theQuery.InitializeDataSet(gdcm.EQueryLevel.eSeries);
            theQuery.ValidateQuery(true);
            CFind_series(theQuery);
        }

        private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            listView1.ListViewItemSorter = new ListViewItemComparer(e.Column);
            listView1.Sort();
        }

        private void listView2_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            listView2.ListViewItemSorter = new ListViewItemComparer(e.Column);
            listView2.Sort();
        }

        private void listView3_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            listView3.ListViewItemSorter = new ListViewItemComparer(e.Column);
            listView3.Sort();
        }


        class ListViewItemComparer : System.Collections.IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                ((ListViewItem)y).SubItems[col].Text);
                return returnVal;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1 is name, textBox2 is MRN
            
            gdcm.DataSet ds = new gdcm.DataSet();

            if (textBox1.Text == "" && textBox2.Text == "") { return; }

            String name = textBox1.Text;
            String MRN = textBox2.Text;
            String DOB = "";

            gdcm.DataElement de = new gdcm.DataElement(GRD_Utils.Tags.tag_patientname);
            byte[] valasbytes = System.Text.Encoding.ASCII.GetBytes(name);
            if (valasbytes.Length > 0)
            {
                gdcm.VL len = new gdcm.VL((uint)valasbytes.Length);
                de.SetByteValue(valasbytes, len);
            }
            ds.Insert(de);

            de = new gdcm.DataElement(GRD_Utils.Tags.tag_patientMRN);
            valasbytes = System.Text.Encoding.ASCII.GetBytes(MRN);
            if (valasbytes.Length > 0)
            {
                gdcm.VL len = new gdcm.VL((uint)valasbytes.Length);
                de.SetByteValue(valasbytes, len);
            }
            ds.Insert(de);
            
            de = new gdcm.DataElement(GRD_Utils.Tags.tag_patientDOB);
            valasbytes = System.Text.Encoding.ASCII.GetBytes(DOB);
            if (valasbytes.Length > 0)
            {
                gdcm.VL len = new gdcm.VL((uint)valasbytes.Length);
                de.SetByteValue(valasbytes, len);
            }
            ds.Insert(de);
            
            if (ds.IsEmpty()) { return; }

            gdcm.EQueryLevel ert = gdcm.EQueryLevel.ePatient;
            if (radioButton1.Checked) { ert = gdcm.EQueryLevel.ePatient; }
            if (radioButton2.Checked) { ert = gdcm.EQueryLevel.eStudy; }

            gdcm.BaseRootQuery theQuery = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.ePatientRootType, ert, ds);

            CFind_patients(theQuery);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private System.IO.DirectoryInfo targetdir;
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog()==DialogResult.OK)
            {
                changetargetdir(fbd.SelectedPath);
            }
        }

        private void changetargetdir(String dir)
        {
            textBox3.Text = dir;
            Properties.Settings.Default.CMove_dir = dir;
            Properties.Settings.Default.Save();
            targetdir = new System.IO.DirectoryInfo(dir);
            if (targetdir.GetDirectories().Count<System.IO.DirectoryInfo>() > 0 || targetdir.GetFiles().Count<System.IO.FileInfo>() > 0)
            {
                System.Windows.Forms.MessageBox.Show("The selected directory is not empty. An empty directory is recommended, but not mandatory.");
                textBox3.BackColor = Color.Yellow;
            }
            else
            {
                textBox3.BackColor = Color.LightGreen;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count <= 0) { return; }

            gdcm.DataSetArrayType dsa = new gdcm.DataSetArrayType();

            for (int n = 0; n < lv_series_man.SelectedDataSets.Count;n++ )
            {
                gdcm.DataSet ds = new gdcm.DataSet();
                copyselecteddataset(lv_patients_man, ds);
                copyselecteddataset(lv_studies_man, ds);
                copyselecteddataset(lv_series_man, n, ds);
                dsa.Add(ds);

            }
            CMove_series(dsa);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
