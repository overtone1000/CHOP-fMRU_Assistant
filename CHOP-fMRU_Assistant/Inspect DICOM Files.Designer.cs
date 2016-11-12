namespace CHOP_fMRU_Assistant
{
    partial class Inspect_DICOM_Files
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inspect_DICOM_Files));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Study UID";
            this.columnHeader8.Width = 77;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Series Description";
            this.columnHeader1.Width = 178;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Accession Number";
            this.columnHeader2.Width = 122;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Trigger Time";
            this.columnHeader3.Width = 99;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Acquisition Time";
            this.columnHeader4.Width = 110;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Expected Images";
            this.columnHeader5.Width = 99;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Counted Images";
            this.columnHeader6.Width = 93;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Transfer Syntax";
            this.columnHeader7.Width = 185;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(968, 410);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 24;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // Inspect_DICOM_Files
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 434);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Inspect_DICOM_Files";
            this.Text = "Inspect_DICOM_Files";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ListView listView1;


    }
}