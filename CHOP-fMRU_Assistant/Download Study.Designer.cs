namespace CHOP_fMRU_Assistant
{
    partial class Download_Study
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Download_Study));
            this.button3 = new System.Windows.Forms.Button();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.header_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.header_MRN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.header_DOB = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(658, 400);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(235, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "Download Selected Series";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // listView3
            // 
            this.listView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader7});
            this.listView3.FullRowSelect = true;
            this.listView3.GridLines = true;
            this.listView3.Location = new System.Drawing.Point(15, 269);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(881, 125);
            this.listView3.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView3.TabIndex = 23;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Series Instance UID";
            this.columnHeader6.Width = 116;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 586;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Images";
            this.columnHeader3.Width = 95;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Modality";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Series Number";
            this.columnHeader7.Width = 97;
            // 
            // listView2
            // 
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader4});
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(12, 138);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(881, 125);
            this.listView2.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView2.TabIndex = 22;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            this.listView2.SelectedIndexChanged += new System.EventHandler(this.listView2_SelectedIndexChanged);
            // 
            // columnHeader0
            // 
            this.columnHeader0.Text = "Study Instance UID";
            this.columnHeader0.Width = 114;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 738;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(193, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Query";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header_Name,
            this.header_MRN,
            this.header_DOB});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(211, 28);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(682, 104);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 20;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged_1);
            // 
            // header_Name
            // 
            this.header_Name.Text = "Name";
            this.header_Name.Width = 172;
            // 
            // header_MRN
            // 
            this.header_MRN.Text = "MRN";
            this.header_MRN.Width = 314;
            // 
            // header_DOB
            // 
            this.header_DOB.Text = "Date of Birth";
            this.header_DOB.Width = 130;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Medical Record Number";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 83);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(193, 20);
            this.textBox2.TabIndex = 17;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 44);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(190, 20);
            this.textBox1.TabIndex = 16;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 431);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(905, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(557, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            // 
            // Download_Study
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 453);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Download_Study";
            this.Text = "Download Study";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader0;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader header_Name;
        private System.Windows.Forms.ColumnHeader header_MRN;
        private System.Windows.Forms.ColumnHeader header_DOB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}