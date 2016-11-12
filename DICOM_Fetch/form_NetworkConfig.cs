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
    public partial class form_NetworkConfig : Form
    {
        private DICOM_NetworkConfigurations configs;
        public form_NetworkConfig(DICOM_NetworkConfigurations Configurations)
        {
            InitializeComponent();
            this.configs = Configurations;
            set_combobox_values();
            settextboxvalues();
        }

        private void set_combobox_values()
        {
            comboBox1.Items.Clear();
            if (configs.Count() > 0) { comboBox1.Items.AddRange(configs.AllKeys()); }
            comboBox1.SelectedText = configs.Current().Label;
            comboBox1.Text = configs.Current().Label;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Delete Config
            configs.remove(configs.Current());
            set_combobox_values();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //New config
            form_NewNetworkConfig nnc = new form_NewNetworkConfig();
            nnc.ShowDialog();
            if (nnc.DialogResult == DialogResult.OK)
            {
                configs.add(nnc.config);
                set_combobox_values();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            configs.setcurrent(comboBox1.Text);
            settextboxvalues();
        }

        private void settextboxvalues()
        {
            tb_Label.Text = configs.Current().Label;
            tb_ServerAdd.Text = configs.Current().server_address;
            tb_ServerAETitle.Text = configs.Current().server_AETitle;
            tb_ClientAETitle.Text = configs.Current().client_AETitle;
            tb_ServerPort.Text = configs.Current().server_port.ToString();
            tb_ClientPort.Text = configs.Current().client_port.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Network_Configuration testconfig;
            testconfig.Label = "Test DICOM Server";
            testconfig.server_address = "www.dicomserver.co.uk";
            testconfig.server_port = 104;
            testconfig.server_AETitle = "TEST";
            testconfig.client_AETitle = "ANY-SCP";
            testconfig.client_port = 104;
            configs.add(testconfig);
            configs.setcurrent(testconfig);
            settextboxvalues();
        }
    }
}
