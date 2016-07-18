using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CHOP_fMRU_Assistant
{
    public partial class Inspect_DICOM_Files : Form
    {
        public Inspect_DICOM_Files()
        {
            InitializeComponent();
            listView1.Columns.Clear();
            
            listView1.Columns.Add("Study UID");
            listView1.Columns.Add("UID");
            listView1.Columns.Add("Series Description");
            listView1.Columns.Add("Accession Number");
            listView1.Columns.Add("Trigger Time");
            listView1.Columns.Add("Acquisition Time");
            listView1.Columns.Add("Slice Position");
            listView1.Columns.Add("Counted Images");
            listView1.Columns.Add("Transfer Syntax");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public ListView lv(){
            return this.listView1;
        }
        public void Autosize()
        {
            this.listView1.AutoResizeColumns(System.Windows.Forms.ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
