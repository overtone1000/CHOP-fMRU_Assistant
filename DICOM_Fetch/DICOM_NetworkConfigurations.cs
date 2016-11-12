using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Windows.Forms;

namespace DICOM_Fetch
{
    [Serializable]
    public struct Network_Configuration : ISerializable
    {
        public String Label;

        public String server_address;
        public ushort server_port;
        public String server_AETitle;

        public ushort client_port;
        public String client_AETitle;

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values.
            info.AddValue("Label", Label, typeof(String));
            info.AddValue("scp_address", server_address, typeof(String));
            info.AddValue("scp_port", server_port, typeof(ushort));
            info.AddValue("scp_AETitle", server_AETitle, typeof(String));
            info.AddValue("scu_port", client_port, typeof(ushort));
            info.AddValue("scu_AETitle", client_AETitle, typeof(String));
        }

        public Network_Configuration(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            Label = info.GetString("Label");
            server_address = info.GetString("scp_address");
            server_port = info.GetUInt16("scp_port");
            server_AETitle = info.GetString("scp_AETitle");
            client_port = info.GetUInt16("scu_port");
            client_AETitle = info.GetString("scu_AETitle");
        }
    }

    public class DICOM_NetworkConfigurations
    {
        public event EventHandler ConfigurationChanged;

        private String filename_configs = "NetworkConfigurations.dat";
        private SortedDictionary<String,Network_Configuration> netconfigs;
        private Network_Configuration current_config;
        public DICOM_NetworkConfigurations()
        {
            load();
        }

        public void setcurrent(Network_Configuration configuration)
        {
            current_config = configuration;
            add(configuration);
            currentset();
        }
        public void setcurrent(String configurationlabel)
        {
            if (netconfigs.TryGetValue(configurationlabel, out current_config))
            {
                currentset();
            }
        }
        private void currentset()
        {  
            save();
            ConfigurationChanged(this, new EventArgs());
        }

        public Network_Configuration Current()
        {
            return current_config;
        }

        public String[] AllKeys()
        {
            if (netconfigs.Count > 0)
            {
                return netconfigs.Keys.ToArray();
            }
            else{return null;}
        }

        private void save(){
            try
            {
                System.IO.FileInfo f = new System.IO.FileInfo(filename_configs);
                if (f.Exists) { f.Delete(); }
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename_configs, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, current_config);
                formatter.Serialize(stream, netconfigs.Count);
                foreach (Network_Configuration n in netconfigs.Values)
                {
                    formatter.Serialize(stream, n);
                }
                stream.Close();
            }
            catch (Exception e)
            {
                String result = "Network Configuration Save Error:";
                result += '\n' + e.Message;
                System.Windows.Forms.MessageBox.Show(result);
            }
        }

        private void load()
        {
            System.IO.FileInfo f = new System.IO.FileInfo(filename_configs);
            netconfigs = new SortedDictionary<String, Network_Configuration>();
            if (f.Exists)
            {
                Stream str=null;
                try{
                    IFormatter formatter = new BinaryFormatter();
                    str = new FileStream(filename_configs, FileMode.Open, FileAccess.Read, FileShare.None);
                    current_config = (Network_Configuration)formatter.Deserialize(str);
                    int count = (int)formatter.Deserialize(str);
                    for (int n = 0; n < count; n++)
                    {
                        Network_Configuration newconfig=(Network_Configuration)formatter.Deserialize(str);
                        netconfigs.Add(newconfig.Label,newconfig);
                    }
                    str.Close();
                }
                catch(Exception e){
                    if (!(str == null)) { str.Close(); }
                    System.Diagnostics.Debug.WriteLine("Error reading configuration file. Deleting.");
                    f.Delete();
                }
            }
        }

        public int Count()
        {
            return netconfigs.Count;
        }

        public void add(Network_Configuration nc){
            //try{
                if (netconfigs.Keys.Contains(nc.Label)) { remove(nc); }
                netconfigs.Add(nc.Label, nc);
                current_config = nc;
                save();
            /*
            }
            catch(Exception e)
            {
                MessageBox.Show("Configuration Add Error");
            }
             */
        }
        public void remove(Network_Configuration nc)
        {
            if (!netconfigs.Keys.Contains<String>(nc.Label)) { return; }
            bool needsreset=false;
            if (nc.Label == current_config.Label) { needsreset = true; }
            netconfigs.Remove(nc.Label);

            if (needsreset) { current_config = netconfigs.Values.First<Network_Configuration>(); }
            save();
        }
    }
}
