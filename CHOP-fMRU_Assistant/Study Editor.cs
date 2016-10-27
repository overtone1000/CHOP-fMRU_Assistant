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
    public partial class Study_Editor : Form
    {
        DynamicStudy study;
        MainForm owner;
        private delegate void deleg();
        public Study_Editor(DynamicStudy studyinterogated, MainForm parent)
        {
            InitializeComponent();
            owner = parent;
            this.study = studyinterogated;
            owner.BeginInvoke(new deleg(PopulateCombobox));
            owner.BeginInvoke(new deleg(PopulateDatabox));

            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanges);   
        }
        private void PopulateCombobox()
        {
            comboBox1.Items.Clear();
            foreach (String series in study.SeriesUIDs())
            {
                comboBox1.Items.Add(series);
            }
            foreach(object i in comboBox1.Items)
            {
                String s = (string)i;
                if (seriesUID == s) {
                    comboBox1.SelectedItem = i;
                    return;
                }
            }
            if (comboBox1.Items.Count > 0) { comboBox1.SelectedItem = comboBox1.Items[0]; }
        }
        private void PopulateDatabox()
        {
            PopulateDatabox(false);
        }
        private void PopulateDatabox(bool preserveselection)
        {
            label2.Text = study.SeriesDescription(seriesUID);
            label2.Refresh();

            //imagechangeenabled = false;
            int oldrowindex=0;
            int oldcolindex=0;
            if(dataGridView1.SelectedCells.Count>0)
            {
                oldrowindex= dataGridView1.SelectedCells[0].RowIndex;
                oldcolindex = dataGridView1.SelectedCells[0].ColumnIndex;
            }

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            if (comboBox1.SelectedItem == null) { return; }
            List<float> acquisitiontimes = study.AcquisitionTimes(seriesUID);

            foreach (float acquisitiontime in acquisitiontimes)
            {
                DataGridViewColumn col = new DataGridViewColumn();
                col.Name = acquisitiontime.ToString();
                col.HeaderText = "";
                col.Width = 3;
                dataGridView1.Columns.Add(col);
            }
            List<DataGridViewRow> rows=new List<DataGridViewRow>();
            foreach (float sliceposition in study.SlicePositions(seriesUID))
            {
                DataGridViewRow row = new DataGridViewRow();
                //row.HeaderCell.Value=sliceposition.ToString();
                row.HeaderCell.Value = "";
                row.Height = 3;
                foreach (float acquisitiontime in acquisitiontimes)
                {
                    DataGridViewTextBoxCell cell= new DataGridViewTextBoxCell();
                    int images = study.ImagesThatExists(seriesUID, sliceposition, acquisitiontime);
                    if (images==1)
                    {
                        cell.Style.BackColor = Color.Green;
                    }
                    else if(images==0)
                    {
                        cell.Style.BackColor = Color.Red;
                    }
                    else if(images>1)
                    {
                        cell.Style.BackColor = Color.Orange;
                    }
                    else
                    {
                        cell.Style.BackColor = Color.Black;
                    }
                    cell.Value="";
                    row.Cells.Add(cell);
                }
                rows.Add(row);
                
            }

            dataGridView1.SuspendLayout();
            dataGridView1.Rows.AddRange(rows.ToArray());
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.Rows[0].Cells[0].Selected = true;
            dataGridView1.ResumeLayout();

            if (preserveselection) { 
                if (dataGridView1.Rows.Count > 0 && dataGridView1.Columns.Count > 0)
                {
                    if (dataGridView1.Rows.Count > oldrowindex && dataGridView1.Columns.Count > oldcolindex) { dataGridView1.CurrentCell = dataGridView1.Rows[oldrowindex].Cells[oldcolindex]; }
                }
            }
            //imagechangeenabled = true;
        }
        
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.BeginInvoke(new deleg(PopulateDatabox));
            this.seriesUID = comboBox1.SelectedItem.ToString();
            PopulateDatabox(true);
        }

        private String seriesUID;
        private float sliceposition;
        private float acquisitiontime;
        //private bool imagechangeenabled = true;
        private void dataGridView1_SelectionChanges(object sender, EventArgs e)
        {
            //if (!imagechangeenabled) { return; }

            if (dataGridView1.SelectedCells.Count<= 0) {
                pictureBox1.Image = null;
                pictureBox1.Refresh();
                label1.Text = "";
                label1.Refresh();
                return;
            }
            sliceposition=study.SlicePositions(seriesUID)[dataGridView1.SelectedCells[0].RowIndex];
            acquisitiontime = study.AcquisitionTimes(seriesUID)[dataGridView1.SelectedCells[0].ColumnIndex];

            String l="Slice position: " + sliceposition;
            l+=", Acquisition time: " + acquisitiontime;
            label1.Text = l;
            label1.Refresh();

            string[] files = (study.ImageFiles(seriesUID, sliceposition, acquisitiontime));
            if (files == null)
            {
                pictureBox1.Image = null;
                pictureBox1.Refresh();
                return;
            }
            string file = files[0]; //Just show the first one
            gdcm.ImageReader reader = new gdcm.ImageReader();
            reader.SetFileName(file);
            reader.Read();
            gdcm.DataSet ds = reader.GetFile().GetDataSet();
            ushort rows = (ushort)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_imagerows));
            ushort cols = (ushort)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_imagecols));
            gdcm.Image image = reader.GetImage();
            
            //int width = (int)image.G(0);
            //int height = (int)image.GetDimension(1);
            //Bitmap bmp = new Bitmap(width,height);
            //System.Windows.Forms.MessageBox.Show("Unanticipated pixel format: " + image.GetPixelFormat().GetHighBit());
            float wcent = float.Parse((string)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_windowceneter)));
            float wwidth = float.Parse((string)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_windowwidth)));
            System.Diagnostics.Debug.WriteLine("rows: " + rows); //should be 11
            System.Diagnostics.Debug.WriteLine("columns: " + cols); //should be 11
            System.Diagnostics.Debug.WriteLine("pixel format: " + image.GetPixelFormat().GetHighBit()); //should be 11
            System.Diagnostics.Debug.WriteLine("photometric interpretation: " + image.GetPhotometricInterpretation().toString()); //should be monochrome2
            System.Diagnostics.Debug.WriteLine("pixel representation: " + image.GetPixelFormat().GetPixelRepresentation().ToString()); //should be 0
            System.Diagnostics.Debug.WriteLine("window center: " + wcent); //should be 0
            System.Diagnostics.Debug.WriteLine("window width: " + wwidth); //should be 0

            Bitmap bmp = new Bitmap(cols, rows);
            ushort[] pixels=new ushort[rows*cols];
            if (image.GetArray(pixels))
            {
                System.Diagnostics.Debug.WriteLine("Generating Image.");
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        ushort pixelval = pixels[row * cols + col];
                        if (pixelval > byte.MaxValue) { pixelval = byte.MaxValue; }
                        if (pixelval < byte.MinValue) { pixelval = byte.MinValue; }
                        /*
                        float p = (float)(0.5 + ((float)pixelval - wcent) / wwidth);
                        if (p > 1.0) { p=1.0F;}
                        if (p < 0.0) { p = 0.0F; }
                        int i = (int)(p * byte.MaxValue);
                        System.Diagnostics.Debug.WriteLine("Pixel value = " + i.ToString());
                         */
                        Color color = Color.FromArgb(pixelval, pixelval, pixelval);
                        bmp.SetPixel(col, row, color);
                    }
                }

                this.pictureBox1.Image = bmp;
                this.pictureBox1.Refresh();
                System.Diagnostics.Debug.WriteLine("Image generation done.");

                reader.Dispose();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            study.ExcludeSeries(seriesUID);
            PopulateCombobox();
            PopulateDatabox(true);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            study.UndoExclusions();
            PopulateCombobox();
            PopulateDatabox(true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            study.ExcludeBeforeTime(acquisitiontime);
            PopulateCombobox();
            PopulateDatabox(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            study.ExcludeAfterTime(acquisitiontime);
            PopulateCombobox();
            PopulateDatabox(true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            study.ExcludeBeforePosition(sliceposition);
            PopulateCombobox();
            PopulateDatabox(true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            study.ExcludeAfterPosition(sliceposition);
            PopulateCombobox();
            PopulateDatabox(true);
        }

        private void Series_Editor_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            study.ExcludeTime(acquisitiontime);
            PopulateCombobox();
            PopulateDatabox(true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            study.ExcludePosition(sliceposition);
            PopulateCombobox();
            PopulateDatabox(true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            study.ExcludeAllButThisSeries(seriesUID);
            PopulateCombobox();
            PopulateDatabox(true);
        }
    }
}
