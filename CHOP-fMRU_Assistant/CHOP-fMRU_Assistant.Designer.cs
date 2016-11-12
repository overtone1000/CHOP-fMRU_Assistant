namespace CHOP_fMRU_Assistant
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.patientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importStudyFromPACSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePatientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.networkSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(141, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Study Editor";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 225);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(807, 22);
            this.statusStrip1.TabIndex = 23;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(792, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patientToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(807, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // patientToolStripMenuItem
            // 
            this.patientToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPatientToolStripMenuItem,
            this.importStudyFromPACSToolStripMenuItem,
            this.changePatientToolStripMenuItem,
            this.deletePatientToolStripMenuItem});
            this.patientToolStripMenuItem.Name = "patientToolStripMenuItem";
            this.patientToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.patientToolStripMenuItem.Text = "Study";
            // 
            // newPatientToolStripMenuItem
            // 
            this.newPatientToolStripMenuItem.Name = "newPatientToolStripMenuItem";
            this.newPatientToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.newPatientToolStripMenuItem.Text = "Import Study from Disc";
            this.newPatientToolStripMenuItem.Click += new System.EventHandler(this.newPatientToolStripMenuItem_Click);
            // 
            // importStudyFromPACSToolStripMenuItem
            // 
            this.importStudyFromPACSToolStripMenuItem.Name = "importStudyFromPACSToolStripMenuItem";
            this.importStudyFromPACSToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.importStudyFromPACSToolStripMenuItem.Text = "Import Study from PACS";
            this.importStudyFromPACSToolStripMenuItem.Click += new System.EventHandler(this.importStudyFromPACSToolStripMenuItem_Click);
            // 
            // changePatientToolStripMenuItem
            // 
            this.changePatientToolStripMenuItem.Name = "changePatientToolStripMenuItem";
            this.changePatientToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.changePatientToolStripMenuItem.Text = "Change Study";
            this.changePatientToolStripMenuItem.Click += new System.EventHandler(this.changePatientToolStripMenuItem_Click);
            // 
            // deletePatientToolStripMenuItem
            // 
            this.deletePatientToolStripMenuItem.Name = "deletePatientToolStripMenuItem";
            this.deletePatientToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.deletePatientToolStripMenuItem.Text = "Delete Study";
            this.deletePatientToolStripMenuItem.Click += new System.EventHandler(this.deletePatientToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseDirectoryToolStripMenuItem,
            this.networkSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // databaseDirectoryToolStripMenuItem
            // 
            this.databaseDirectoryToolStripMenuItem.Name = "databaseDirectoryToolStripMenuItem";
            this.databaseDirectoryToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.databaseDirectoryToolStripMenuItem.Text = "Database Directory";
            this.databaseDirectoryToolStripMenuItem.Click += new System.EventHandler(this.databaseDirectoryToolStripMenuItem_Click);
            // 
            // networkSettingsToolStripMenuItem
            // 
            this.networkSettingsToolStripMenuItem.Name = "networkSettingsToolStripMenuItem";
            this.networkSettingsToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.networkSettingsToolStripMenuItem.Text = "Network Settings";
            this.networkSettingsToolStripMenuItem.Click += new System.EventHandler(this.networkSettingsToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Images to DICOM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(159, 60);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(130, 17);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "Anonymize for Testing";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip2.Location = new System.Drawing.Point(0, 203);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(807, 22);
            this.statusStrip2.TabIndex = 27;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(690, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 85);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 23);
            this.button2.TabIndex = 29;
            this.button2.Text = "Send Results to PACS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 247);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "CHOP-fMRU Assistant";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePatientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripMenuItem databaseDirectoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem networkSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importStudyFromPACSToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button button2;
    }
}

