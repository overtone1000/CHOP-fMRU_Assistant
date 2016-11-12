using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DICOM_Fetch
{
    public enum DICOMNetworkQueryStatus
    {
        Waiting,
        Receiving,
        Success,
        Failure
    }

    public delegate void DICOMNetworkEventHandler_Status(DICOMNetworkQueryStatus e);

    public class NetworkOperation{
        public event DICOMNetworkEventHandler_Status StatusChange;
        protected bool cancelled = false;
        protected bool finished = false;
        public NetworkOperation(){}
        public void Cancel()
        {
            cancelled = true;
        }
        public bool Finished()
        {
            return finished;
        }
        protected System.Threading.ThreadStart ts;
        protected Network_Configuration config;
        protected virtual void RaiseStatusChange(DICOMNetworkQueryStatus e)
        {
            DICOMNetworkEventHandler_Status handler = StatusChange;
            if (handler != null)
            {
                handler(e);
            }
        }
        public void Execute()
        {
            System.Threading.Thread t = new System.Threading.Thread(ts);
            t.Start();
        }
    }

    public class CEcho : NetworkOperation{
        public CEcho(Network_Configuration config)
        {
            this.config = config;
            this.ts = new System.Threading.ThreadStart(CEcho_thread);
        }
        
        private void CEcho_thread()
        {
            base.RaiseStatusChange(DICOMNetworkQueryStatus.Waiting);
            bool CEchobool = gdcm.CompositeNetworkFunctions.CEcho(config.server_address, config.server_port, config.client_AETitle, config.server_AETitle);
            if (cancelled) { finished = true; return; }
            if (!CEchobool) { 
                System.Diagnostics.Debug.WriteLine("CEcho failed.");
                base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
            }
            else
            {
                base.RaiseStatusChange(DICOMNetworkQueryStatus.Success);
            }
            finished = true;
        }
    }

    public class CFind:NetworkOperation{
        private gdcm.DataSetArrayType returnquery;
        private gdcm.BaseRootQuery query;
        public CFind(Network_Configuration config, gdcm.BaseRootQuery query)
        {
            this.config = config;
            this.query = query;
            this.ts = new System.Threading.ThreadStart(CFind_thread);
        }
        private void CFind_thread()
        {
            gdcm.DataSetArrayType returnquery_patient = new gdcm.DataSetArrayType();
            base.RaiseStatusChange(DICOMNetworkQueryStatus.Waiting);
            returnquery = new gdcm.DataSetArrayType();
            //bool CFindbool = gdcm.CompositeNetworkFunctions.CFind(config.server_address, config.server_port, query, returnquery, config.client_AETitle, config.server_AETitle); //works test and OHSU
            bool CFindbool = gdcm.CompositeNetworkFunctions.CFind(config.server_address, config.server_port, query, returnquery, config.client_AETitle, config.server_AETitle); //also works at OHSU
            if (cancelled) { finished = true; return; }
            if (CFindbool)
            {
                System.Diagnostics.Debug.WriteLine("CFind succeeded.");
                base.RaiseStatusChange(DICOMNetworkQueryStatus.Success);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CFind failed.");
                base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
            }
        }
        public gdcm.DataSetArrayType GetResults()
        {
            return returnquery;
        }
    }
    public class movescu:NetworkOperation
    {
        private String outdir;
        private gdcm.DataSetArrayType searchdatasets;
        private Double progress = 0;
        private int images;
        public movescu(Network_Configuration config, gdcm.DataSetArrayType searchdatasets, String outdir)
        {
            this.config = config;
            this.outdir = outdir;
            this.searchdatasets = searchdatasets;
            this.ts = new System.Threading.ThreadStart(thread);
        }
        private bool string_starts_with_test(ref string str, string teststr)
        {
            if (str.Length >= teststr.Length)
            {
                if (str.Substring(0, teststr.Length) == teststr)
                {
                    str = str.Substring(teststr.Length, str.Length - teststr.Length);
                    return true;
                }
            }
            return false;
        }
        private void thread()
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(outdir);
            if (di.Exists)
            {
                try
                {
                    di.Delete(true);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            di.Create();

            System.Net.NetworkInformation.IPGlobalProperties ipGlobalProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();
            System.Net.NetworkInformation.TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            System.Net.IPEndPoint[] tcpListeners = ipGlobalProperties.GetActiveTcpListeners();
            foreach (System.Net.NetworkInformation.TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == config.client_port)
                {
                    base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
                    System.Windows.Forms.MessageBox.Show("Port " + config.client_port + " is in use. CMove cannot be attempted with the current configuration. Please close the application using this port and try again.");
                    return;
                }
            }
            foreach (System.Net.IPEndPoint ep in tcpListeners)
            {
                if (ep.Port == config.client_port)
                {
                    base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
                    System.Windows.Forms.MessageBox.Show("Port " + config.client_port + " is in use. CMove cannot be attempted with the current configuration. Please close the application using this port and try again.");
                    return;
                }
            }
            
            base.RaiseStatusChange(DICOMNetworkQueryStatus.Waiting);
            gdcm.DataSetArrayType returnquery_images = new gdcm.DataSetArrayType();
            string remaining = "I: Remaining Suboperations       : ";
            string completed = "I: Completed Suboperations       : ";
            string status = "I: DIMSE Status                  : ";
            string status_pending = "0xff00: Pending";
            string status_success = "0x0000: Success";
            string status_error = "Error";
            int remaining_count = 0;
            int completed_count = 0;
            foreach (gdcm.DataSet ds in searchdatasets)
            {
                ds.Insert(new gdcm.DataElement(GRD_Utils.Tags.tag_SOPInstanceUID));
                gdcm.BaseRootQuery query_images = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.eStudyRootType, gdcm.EQueryLevel.eImage, ds);
                gdcm.CompositeNetworkFunctions.CFind(config.server_address, config.server_port, query_images, returnquery_images, config.client_AETitle, config.server_AETitle); //also works at OHSU
                System.Diagnostics.Debug.WriteLine("Number of images matching this query: " + returnquery_images.Count);
                images = returnquery_images.Count;

                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                //psi.FileName = "cmd.exe";
                psi.FileName = "movescu.exe";
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.Arguments = get_command_string(ds);
                StringBuilder output = new StringBuilder();
                StringBuilder err = new StringBuilder();
                try
                {
                    System.Diagnostics.Process exeProcess = new System.Diagnostics.Process();

                    exeProcess.OutputDataReceived += (sender, args) =>
                    {
                        output.AppendLine(args.Data);
                        System.Diagnostics.Debug.WriteLine(args.Data);

                        if (args.Data != null)
                        {
                            string str = args.Data;
                            if(string_starts_with_test(ref str, remaining))
                            {
                                if (str == "none")
                                {
                                    remaining_count = 0;
                                }
                                else
                                {
                                    remaining_count = Int32.Parse(str);
                                }
                            }
                            else if(string_starts_with_test(ref str, completed))
                            {
                                if (str == "none")
                                {
                                    completed_count = 0;
                                }
                                else
                                {
                                    completed_count = Int32.Parse(str);
                                }
                            }
                            else if(string_starts_with_test(ref str, status))
                            {
                                if (string_starts_with_test(ref str, status_pending))
                                {
                                    progress = (float)completed_count/(float)(remaining_count + completed_count);
                                    if (progress > 1.0) { progress = 1.0; }
                                    if(progress < 0.0) { progress = 0.0; }
                                    if(Double.IsNaN(progress)) { progress = 0.0; }
                                    base.RaiseStatusChange(DICOMNetworkQueryStatus.Receiving);
                                }
                                else if (string_starts_with_test(ref str, status_success))
                                {
                                    progress = 1.0;
                                }
                                else if (str.Contains(status_error))
                                {
                                    progress = 0.0;
                                    base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
                                    System.Windows.Forms.MessageBox.Show("Error from PACS server: " + str);
                                }
                            }
                        }
                    };
                    exeProcess.ErrorDataReceived += (sender, args) =>
                    {
                        err.AppendLine(args.Data);
                    };

                    exeProcess.StartInfo = psi;
                    exeProcess.Start();

                    exeProcess.BeginOutputReadLine();
                    exeProcess.BeginErrorReadLine();

                    exeProcess.WaitForExit();

                    base.RaiseStatusChange(DICOMNetworkQueryStatus.Success);

                    //System.Diagnostics.Debug.WriteLine("Output: ");
                    //System.Diagnostics.Debug.WriteLine(output);
                    System.Diagnostics.Debug.WriteLine("Error: ");
                    System.Diagnostics.Debug.WriteLine(err);
                }
                catch(Exception e)
                {
                    base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    System.Diagnostics.Debug.WriteLine(err);
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
        }
        private String get_command_string(gdcm.DataSet ds)
        {
            String command = "";
            //command += "\"" + AppDomain.CurrentDomain.BaseDirectory;
            //command += "movescu.exe\"";
            command += " -v -S"; //-S sets to study root
            command += " -aet " + config.client_AETitle;
            command += " -aec " + config.server_AETitle;
            command += " -aem " + config.client_AETitle;
            command += " -xe";
            command += " +P " + config.client_port;
            command += " -od \"" + outdir + "\"";
            command += tag_as_argument(new gdcm.Tag(0x0008, 0x0052), "SERIES");
            //command += tag_as_argument(new gdcm.Tag(0x0008, 0x0060), "MR");
            command += tag_as_argument(GRD_Utils.Tags.tag_seriesModality, ds);
            command += tag_as_argument(GRD_Utils.Tags.tag_patientMRN, ds);
            command += tag_as_argument(GRD_Utils.Tags.tag_studyInstanceUID, ds);
            command += tag_as_argument(GRD_Utils.Tags.tag_seriesNumber, ds);
            command += tag_as_argument(GRD_Utils.Tags.tag_seriesInstanceUID, ds);
            command += " " + this.config.server_address + " " + this.config.server_port;
            System.Diagnostics.Debug.WriteLine(command);
            return command;
        }
        private String tag_as_argument(gdcm.Tag tag, gdcm.DataSet ds)
        {
            String retval;
            retval = " -k \"" + tagstring(tag) + "=";
            retval +=(String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds.GetDataElement(tag));
            while (retval.Last<char>() == '\0' || retval.Last<char>() == ' ') {
                retval = retval.Substring(0, retval.Length - 1);
            }
            retval += "\"";
            return retval;
        }
        private String tag_as_argument(gdcm.Tag tag, String value)
        {
            String retval;
            retval = " -k \"" + tagstring(tag) + "=";
            retval += value;
            retval += "\"";
            return retval;
        }
        private String tagstring(gdcm.Tag tag)
        {
            return tag.GetGroup().ToString("X4") + "," + tag.GetElement().ToString("X4");
        }
        public double Progress
        {
            get
            {
                return progress;
            }
        }
    }
    public class CMove : NetworkOperation
    {
        private gdcm.DataSetArrayType dsa;
        private String outdir;
        private double progress=0;
        private System.Threading.ThreadStart ts_prog;
        public double Progress
        {
            get {
                return progress;
            }
        }
        public CMove(Network_Configuration config, gdcm.DataSetArrayType datasets, String outdir)
        {
            this.config = config;
            this.dsa = datasets;
            this.outdir = outdir;
            this.ts = new System.Threading.ThreadStart(CMove_byseries_thread);
            this.ts_prog = new System.Threading.ThreadStart(CMove_byseries_progress_thread);
        }
        gdcm.DataSetArrayType allimages = new gdcm.DataSetArrayType();
        private void CMove_byseries_thread()
        {
            allimages.Clear();
            base.RaiseStatusChange(DICOMNetworkQueryStatus.Waiting);

            gdcm.DataSetArrayType returnquery_series = new gdcm.DataSetArrayType();
            gdcm.DataSetArrayType returnquery_images = new gdcm.DataSetArrayType();

            foreach(gdcm.DataSet ds_series in dsa)
            {
                /*
                //Not actually used
                gdcm.BaseRootQuery query_series = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.eStudyRootType, gdcm.EQueryLevel.eSeries, ds_series);
                gdcm.CompositeNetworkFunctions.CFind(config.server_address, config.server_port, query_series, returnquery_series, config.client_AETitle, config.server_AETitle); //also works at OHSU
                System.Diagnostics.Debug.WriteLine("Number of series matching this query: " + returnquery_series.Count);
                numberofseries += returnquery_series.Count;
                */

                //For progress, find out total number of images
                ds_series.Insert(new gdcm.DataElement(GRD_Utils.Tags.tag_SOPInstanceUID));
                gdcm.BaseRootQuery query_images = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.eStudyRootType, gdcm.EQueryLevel.eImage, ds_series);
                gdcm.CompositeNetworkFunctions.CFind(config.server_address, config.server_port, query_images, returnquery_images, config.client_AETitle, config.server_AETitle); //also works at OHSU
                System.Diagnostics.Debug.WriteLine("Number of images matching this query: " + returnquery_images.Count);
                allimages.AddRange(returnquery_images);
            }
            System.Threading.Thread t_prog = new System.Threading.Thread(ts_prog);
            t_prog.Start();
            foreach (gdcm.DataSet ds_series in dsa)
            {
                gdcm.DataSet newds = new gdcm.DataSet();

                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_studyInstanceUID));
                newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_seriesInstanceUID));
                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_SOPInstanceUID));
                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_patientMRN));
                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_specificcharacterset));
                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_patientname));

                newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_seriesNumber));
                newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_seriesModality));
                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_seriesrelatedinstances));

                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_studyDate));
                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_studyTime));
                //newds.Insert(ds_series.GetDataElement(GRD_Utils.Tags.tag_studyaccessionnumber));

                String la;
                System.Diagnostics.Debug.WriteLine("Query built:");
                la = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(newds.GetDataElement(GRD_Utils.Tags.tag_studyInstanceUID));
                System.Diagnostics.Debug.WriteLine("Study UID = " + la);
                la = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(newds.GetDataElement(GRD_Utils.Tags.tag_seriesInstanceUID));
                System.Diagnostics.Debug.WriteLine("Series UID = " + la);

                gdcm.BaseRootQuery query_series = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.eStudyRootType, gdcm.EQueryLevel.eSeries, newds);
                //gdcm.BaseRootQuery query_series = gdcm.CompositeNetworkFunctions.ConstructQuery(gdcm.ERootType.ePatientRootType, gdcm.EQueryLevel.eSeries, newds);

                gdcm.DataSetArrayType returnquery = new gdcm.DataSetArrayType();
                bool CFindbool = gdcm.CompositeNetworkFunctions.CFind(config.server_address, config.server_port, query_series, returnquery, config.client_AETitle, config.server_AETitle); //also works at OHSU
                System.Diagnostics.Debug.WriteLine("Number of images matching the exact move query: " + returnquery_images.Count);

                //bool CMovebool = gdcm.CompositeNetworkFunctions.CMove(config.server_address, config.server_port, query_series, config.client_port, config.client_AETitle, config.server_AETitle, outdir); //works with test server, not OHSU
                bool CMovebool = gdcm.CompositeNetworkFunctions.CMove(config.server_address, config.server_port, query_series, config.client_port, config.server_AETitle, config.client_AETitle, outdir);
                System.Diagnostics.Debug.WriteLine("CMovebool = " + CMovebool);
                if (cancelled)
                {
                    cancelprogressthread = true;
                    return;
                }
                if (!CMovebool)
                {
                    cancelprogressthread = true;
                    return;
                }
            }
        }
        private bool cancelprogressthread=false;
        private void CMove_byseries_progress_thread()
        {
            System.Diagnostics.Stopwatch sw_timeout = new System.Diagnostics.Stopwatch();
            sw_timeout.Start();

            System.Diagnostics.Stopwatch sw_checkagain = new System.Diagnostics.Stopwatch();
            sw_checkagain.Start();

            double timeout_seconds = 60;
            double filesprocessed = 0;
            double lastfilesprocessed = 0;
            progress = 0;
            while (sw_timeout.Elapsed.TotalSeconds < timeout_seconds)
            {
                if (cancelprogressthread) {
                    System.Diagnostics.Debug.WriteLine("CMove failed - progress thread cancelled");
                    base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
                    return;
                }
                if (sw_checkagain.Elapsed.TotalSeconds >= 1)
                {
                    filesprocessed+=process_files(allimages, outdir);
                    if (lastfilesprocessed < filesprocessed)
                    {
                        System.Diagnostics.Debug.WriteLine("CMove receiving.");
                        lastfilesprocessed = filesprocessed;
                        progress = (double)filesprocessed / (double)allimages.Count;
                        base.RaiseStatusChange(DICOMNetworkQueryStatus.Receiving);
                        sw_timeout.Reset();
                        sw_timeout.Start(); 
                    }
                    System.Diagnostics.Debug.WriteLine("Progress = " + progress);
                    if (progress >= 1.0)
                    {
                        System.Diagnostics.Debug.WriteLine("CMove Success.");
                        base.RaiseStatusChange(DICOMNetworkQueryStatus.Success);
                        finished = true;
                        return;
                    }
                    sw_checkagain.Reset();
                    sw_checkagain.Start();
                }
            }
            System.Diagnostics.Debug.WriteLine("CMove Failed - timed out waiting for files.");
            base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
            finished = true;
            return;
        }
        private GRD_Utils.TSConvert tsc = new GRD_Utils.TSConvert(new gdcm.TransferSyntax(gdcm.TransferSyntax.TSType.ExplicitVRLittleEndian));
        private String patient_directory = null;
        private int process_files(gdcm.DataSetArrayType dsa, String outdir)
        {
            int retval=0;
            String imageSOP_ds;

            foreach(System.IO.FileInfo fi in new System.IO.DirectoryInfo(outdir).GetFiles())
            {
                gdcm.TagSetType tst = new gdcm.TagSetType();
                tst.Add(GRD_Utils.Tags.tag_SOPInstanceUID);
                tst.Add(GRD_Utils.Tags.tag_seriesDescription);
                tst.Add(GRD_Utils.Tags.tag_seriesInstanceUID);
                tst.Add(GRD_Utils.Tags.tag_patientname);
                tst.Add(GRD_Utils.Tags.tag_patientMRN);

                String imageSOP_file = "";
                String series_name = null;
                String patient_name = null;

                gdcm.Reader reader = new gdcm.Reader();
                reader.SetFileName(fi.FullName);

                if (reader.CanRead())
                {
                    reader.ReadSelectedTags(tst);
                    //reader.Read();
                    gdcm.File f;
                    f = reader.GetFile();
                    gdcm.DataSet ds_file = f.GetDataSet();

                    imageSOP_file = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds_file.GetDataElement(GRD_Utils.Tags.tag_SOPInstanceUID));
                    series_name = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds_file.GetDataElement(GRD_Utils.Tags.tag_seriesDescription));
                    if (series_name == null || series_name == "") { series_name = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds_file.GetDataElement(GRD_Utils.Tags.tag_seriesInstanceUID)); }
                    if (series_name == null || series_name == "") { series_name = "Unknown Series"; }
                    patient_name = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds_file.GetDataElement(GRD_Utils.Tags.tag_patientname));
                    if (patient_name == null || patient_name == "") { patient_name = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds_file.GetDataElement(GRD_Utils.Tags.tag_patientMRN)); }
                    if (patient_name == null || patient_name == "") { patient_name = "Unknown Patient"; }

                    if (patient_directory == null) { patient_directory = outdir + '\\' + patient_name; }

                    f.Dispose();
                    reader.Dispose();
                    ds_file.Dispose();
                }

                reader.Dispose();

                foreach (gdcm.DataSet ds in dsa)
                {
                    imageSOP_ds = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds.GetDataElement(GRD_Utils.Tags.tag_SOPInstanceUID));
                    if(imageSOP_file==imageSOP_ds)
                    {
                        retval += 1;
                        //String fd = patient_directory + '\\' + series_name;
                        String fd = patient_directory;
                        foreach(char c in System.IO.Path.GetInvalidPathChars())
                        {
                            fd = fd.Replace(c,'-');
                        }
                        System.IO.DirectoryInfo finaloutdir = new System.IO.DirectoryInfo(fd);
                        if(!finaloutdir.Exists){finaloutdir.Create();}
                        //String newname = finaloutdir.GetFiles().Count<System.IO.FileInfo>().ToString();
                        //newname=newname.PadLeft(8, '0');
                        if (tsc.convert_transfer_syntax(fi, finaloutdir))
                        {
                            fi.Delete();
                        }
                        else
                        {
                            fi.MoveTo(finaloutdir.FullName + '\\' + fi.Name);
                        }
                    }
                }
            }
            
            return retval;
        }
        
    }

    public class CStore:NetworkOperation
    {
        System.IO.FileInfo[] files;
        public CStore(Network_Configuration config, System.IO.FileInfo[] files_to_send)
        {
            this.config = config;
            this.files = files_to_send;
            this.ts = new System.Threading.ThreadStart(CStore_thread);
        }
        private void CStore_thread()
        {
            base.RaiseStatusChange(DICOMNetworkQueryStatus.Waiting);
            gdcm.FilenamesType filenames = new gdcm.FilenamesType();
            foreach(System.IO.FileInfo f in files)
            {
                filenames.Add(f.FullName);
            }
            bool CStorebool = gdcm.CompositeNetworkFunctions.CStore(config.server_address, config.server_port, filenames, config.client_AETitle, config.server_AETitle);
            if (cancelled) { finished = true; return; }
            if (CStorebool)
            {
                System.Diagnostics.Debug.WriteLine("CStore succeeded.");
                base.RaiseStatusChange(DICOMNetworkQueryStatus.Success);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("CStore failed.");
                base.RaiseStatusChange(DICOMNetworkQueryStatus.Failure);
            }
        }
    }
}