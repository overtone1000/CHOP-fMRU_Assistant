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
    public partial class form_NewNetworkConfig : Form
    {
        public Network_Configuration config;
        public form_NewNetworkConfig()
        {
            InitializeComponent();
            button1.DialogResult = DialogResult.Cancel;
            button2.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Save button
            config.Label = tb_Label.Text;
            config.server_address = tb_ServerAdd.Text;
            config.server_AETitle = tb_ServerAETitle.Text;
            config.client_AETitle = tb_ClientAETitle.Text;

            ushort scp_port_parsed;
            ushort scu_port_parsed;
            ushort.TryParse(tb_ServerPort.Text, out scp_port_parsed);
            ushort.TryParse(tb_ClientPort.Text, out scu_port_parsed);

            config.server_port = scp_port_parsed;
            config.client_port = scu_port_parsed;

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
