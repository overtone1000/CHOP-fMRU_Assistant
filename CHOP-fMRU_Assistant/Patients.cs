using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CHOP_fMRU_Assistant
{
    public partial class Patients : Form
    {
        private MainForm parent;
        public Patients(MainForm parent)
        {
            InitializeComponent();

            bool closenow = false;

            this.parent = parent;
            DirectoryInfo pd = new DirectoryInfo(Properties.Settings.Default.DataDirectory);
            if (!pd.Exists)
            {
                System.Windows.Forms.MessageBox.Show("No database directory selected. Please edit settings.");
                closenow = true;
            }
            else
            {
                foreach (DirectoryInfo pat in new DirectoryInfo(Properties.Settings.Default.DataDirectory).GetDirectories())
                {
                    try
                    {
                        TreeNode patnode = new TreeNode();
                        patnode.Text = pat.Name;
                        this.treeView1.Nodes.Add(patnode);
                        foreach (DirectoryInfo stud in pat.GetDirectories())
                        {
                            TreeNode studnode = new TreeNode();
                            studnode.Text = stud.Name;
                            patnode.Nodes.Add(studnode);
                        }
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }

                if (this.treeView1.Nodes.Count <= 0)
                {
                    closenow = true;
                    System.Windows.Forms.MessageBox.Show("No patients in database. Please import a study.");
                }

                if (closenow) { this.Load += new EventHandler(closeonshown); }
            }
        }

        private void closeonshown(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.Print("Autoclose.");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null || treeView1.SelectedNode.Nodes.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("Invalid selection. Select a study or hit cancel.");
            }
            else
            {
                String par=treeView1.SelectedNode.Parent.Text;
                String study=treeView1.SelectedNode.Text;
                parent.changecurrentstudy(Properties.Settings.Default.DataDirectory + "\\" + par + "\\" + study);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
