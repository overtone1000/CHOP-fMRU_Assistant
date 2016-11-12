namespace DICOM_Fetch
{
    partial class form_NetworkConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Label = new System.Windows.Forms.TextBox();
            this.tb_ServerAdd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_ServerPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_ServerAETitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_ClientPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ClientAETitle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Configuration Name";
            // 
            // tb_Label
            // 
            this.tb_Label.Location = new System.Drawing.Point(118, 71);
            this.tb_Label.Name = "tb_Label";
            this.tb_Label.ReadOnly = true;
            this.tb_Label.Size = new System.Drawing.Size(154, 20);
            this.tb_Label.TabIndex = 2;
            // 
            // tb_ServerAdd
            // 
            this.tb_ServerAdd.Location = new System.Drawing.Point(118, 101);
            this.tb_ServerAdd.Name = "tb_ServerAdd";
            this.tb_ServerAdd.ReadOnly = true;
            this.tb_ServerAdd.Size = new System.Drawing.Size(154, 20);
            this.tb_ServerAdd.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Server Address";
            // 
            // tb_ServerPort
            // 
            this.tb_ServerPort.Location = new System.Drawing.Point(118, 131);
            this.tb_ServerPort.Name = "tb_ServerPort";
            this.tb_ServerPort.ReadOnly = true;
            this.tb_ServerPort.Size = new System.Drawing.Size(154, 20);
            this.tb_ServerPort.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Server Port";
            // 
            // tb_ServerAETitle
            // 
            this.tb_ServerAETitle.Location = new System.Drawing.Point(118, 161);
            this.tb_ServerAETitle.Name = "tb_ServerAETitle";
            this.tb_ServerAETitle.ReadOnly = true;
            this.tb_ServerAETitle.Size = new System.Drawing.Size(154, 20);
            this.tb_ServerAETitle.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Server AETitle";
            // 
            // tb_ClientPort
            // 
            this.tb_ClientPort.Location = new System.Drawing.Point(118, 191);
            this.tb_ClientPort.Name = "tb_ClientPort";
            this.tb_ClientPort.ReadOnly = true;
            this.tb_ClientPort.Size = new System.Drawing.Size(154, 20);
            this.tb_ClientPort.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Client Port";
            // 
            // tb_ClientAETitle
            // 
            this.tb_ClientAETitle.Location = new System.Drawing.Point(118, 221);
            this.tb_ClientAETitle.Name = "tb_ClientAETitle";
            this.tb_ClientAETitle.ReadOnly = true;
            this.tb_ClientAETitle.Size = new System.Drawing.Size(154, 20);
            this.tb_ClientAETitle.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Client AETitle";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 26);
            this.button1.TabIndex = 13;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 254);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 26);
            this.button2.TabIndex = 14;
            this.button2.Text = "New";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(260, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 39);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(260, 26);
            this.button3.TabIndex = 16;
            this.button3.Text = "Set to Test Server";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // form_NetworkConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 292);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_ClientAETitle);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_ClientPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_ServerAETitle);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tb_ServerPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_ServerAdd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_Label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "form_NetworkConfig";
            this.Text = "Network Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Label;
        private System.Windows.Forms.TextBox tb_ServerAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_ServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_ServerAETitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_ClientPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_ClientAETitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button3;
    }
}