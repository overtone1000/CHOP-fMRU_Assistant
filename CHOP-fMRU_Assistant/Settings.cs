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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            this.textBox1.Text = Properties.Settings.Default.DataDirectory;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DataDirectory = this.textBox1.Text;
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog()==DialogResult.OK)
            {
                this.textBox1.Text = fbd.SelectedPath.ToString();
            }
            
        }
    }
}
