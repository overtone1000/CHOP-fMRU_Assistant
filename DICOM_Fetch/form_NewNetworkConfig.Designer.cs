namespace DICOM_Fetch
{
    partial class form_NewNetworkConfig
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_ClientAETitle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_ClientPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_ServerAETitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_ServerPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_ServerAdd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Label = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 195);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 26);
            this.button2.TabIndex = 29;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 195);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 26);
            this.button1.TabIndex = 28;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_ClientAETitle
            // 
            this.tb_ClientAETitle.Location = new System.Drawing.Point(118, 162);
            this.tb_ClientAETitle.Name = "tb_ClientAETitle";
            this.tb_ClientAETitle.Size = new System.Drawing.Size(154, 20);
            this.tb_ClientAETitle.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Client AETitle";
            // 
            // tb_ClientPort
            // 
            this.tb_ClientPort.Location = new System.Drawing.Point(118, 132);
            this.tb_ClientPort.Name = "tb_ClientPort";
            this.tb_ClientPort.Size = new System.Drawing.Size(154, 20);
            this.tb_ClientPort.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Client Port";
            // 
            // tb_ServerAETitle
            // 
            this.tb_ServerAETitle.Location = new System.Drawing.Point(118, 102);
            this.tb_ServerAETitle.Name = "tb_ServerAETitle";
            this.tb_ServerAETitle.Size = new System.Drawing.Size(154, 20);
            this.tb_ServerAETitle.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Server AETitle";
            // 
            // tb_ServerPort
            // 
            this.tb_ServerPort.Location = new System.Drawing.Point(118, 72);
            this.tb_ServerPort.Name = "tb_ServerPort";
            this.tb_ServerPort.Size = new System.Drawing.Size(154, 20);
            this.tb_ServerPort.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Server Port";
            // 
            // tb_ServerAdd
            // 
            this.tb_ServerAdd.Location = new System.Drawing.Point(118, 42);
            this.tb_ServerAdd.Name = "tb_ServerAdd";
            this.tb_ServerAdd.Size = new System.Drawing.Size(154, 20);
            this.tb_ServerAdd.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Server Address";
            // 
            // tb_Label
            // 
            this.tb_Label.Location = new System.Drawing.Point(118, 12);
            this.tb_Label.Name = "tb_Label";
            this.tb_Label.Size = new System.Drawing.Size(154, 20);
            this.tb_Label.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Configuration Name";
            // 
            // form_NewNetworkConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 232);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "form_NewNetworkConfig";
            this.Text = "form_NewNetworkConfig";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_ClientAETitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_ClientPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_ServerAETitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_ServerPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_ServerAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Label;
        private System.Windows.Forms.Label label1;
    }
}