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
    public struct Props
    {
        public String seriesUID;
        public String studyUID;
        public String seriesdesc;
        public String accessionnum;
        public String triggertime;
        public String sliceposition;
        public String acquisitiontime;
        public int countedimages;
        public String transfersyntax;
    }

    public partial class MainForm : Form
    {
        DICOM_Fetch.form_NetworkConfig ncf;
        DICOM_Fetch.DICOM_NetworkConfigurations configs = new DICOM_Fetch.DICOM_NetworkConfigurations();
        Download_Study dsf;
        public MainForm()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload();
            this.Load+=new EventHandler(formloaded);
            configs.ConfigurationChanged += new EventHandler(configchange);
        }


        private void formloaded(object sender, System.EventArgs e){
            changecurrentstudy(Properties.Settings.Default.CurrentStudy);
            configchange(this, null);
        }

        DICOM_Fetch.CEcho configtest;
        DICOM_Fetch.DICOMNetworkEventHandler_Status configtesthandler;
        private void configchange(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "Selected Network Configuration: " + configs.Current().Label;
            if (configtest != null)
            {
                configtest.StatusChange -= configtesthandler;
            }
            configtest = new DICOM_Fetch.CEcho(configs.Current());
            configtesthandler = new DICOM_Fetch.DICOMNetworkEventHandler_Status(statuschange_CEcho);
            configtest.StatusChange += configtesthandler;
            configtest.Execute();
        }
        private delegate void delegate_ChangeStatusStrip1();
        private DICOM_Fetch.DICOMNetworkQueryStatus echostatus;
        private void statuschange_CEcho(DICOM_Fetch.DICOMNetworkQueryStatus status)
        {
            echostatus = status;
            this.BeginInvoke(new delegate_ChangeStatusStrip1(ChangeStatusStrip1));
        }
        private void ChangeStatusStrip1()
        {
            switch (echostatus)
            {
                case DICOM_Fetch.DICOMNetworkQueryStatus.Waiting:
                    toolStripStatusLabel2.Text = configs.Current().Label + ": attempting communication with server.";
                    toolStripStatusLabel2.BackColor = Color.Yellow;
                    break;
                case DICOM_Fetch.DICOMNetworkQueryStatus.Success:
                    toolStripStatusLabel2.Text = configs.Current().Label + ": connection established.";
                    toolStripStatusLabel2.BackColor = Color.LightGreen;
                    break;
                case DICOM_Fetch.DICOMNetworkQueryStatus.Failure:
                    toolStripStatusLabel2.Text = configs.Current().Label + ": connection failure.";
                    toolStripStatusLabel2.BackColor = Color.Red;
                    break;
            }
        }

        public void changecurrentstudy(String currentstudy)
        {
            Properties.Settings.Default.CurrentStudy = currentstudy;
            Properties.Settings.Default.Save();
            change_SS_text();
        }
        public void change_SS_text()
        {
            String s = Properties.Settings.Default.CurrentStudy;
            if (s != "") { SS_text = "Current study: " + Properties.Settings.Default.CurrentStudy; }
            else { SS_text = "No study selected."; }
            this.BeginInvoke(new delegate_SS(invokeSS_text));
        }
        public void change_SS_text(String status)
        {
            SS_text = status;
            this.BeginInvoke(new delegate_SS(invokeSS_text));
        }
        public void change_SS_prog(int current, int first, int last)
        {
            SS_prog_cur = current;
            SS_prog_first = first;
            SS_prog_last = last;
            this.BeginInvoke(new delegate_SS(invokeSS_prog));
        }
        private delegate void delegate_SS();
        private String SS_text = "";
        private int SS_prog_cur = 0;
        private int SS_prog_first = 0;
        private int SS_prog_last = 0;
        private void invokeSS_text(){
            this.toolStripStatusLabel1.Text = SS_text;
        }
        private void invokeSS_prog()
        {
            this.toolStripProgressBar1.Minimum = SS_prog_first;
            this.toolStripProgressBar1.Maximum = SS_prog_last;
            this.toolStripProgressBar1.Value = SS_prog_cur;
        }
        
        //private SortedList<String, SortedList<String, Props>> serieslist = new SortedList<String, SortedList<String, Props>>();
        DynamicStudy study;
        System.Threading.Thread analysis_thread;
        private void button3_Click(object sender, EventArgs e){
            if (analysis_thread != null)
            {
                if (analysis_thread.IsAlive)
                {
                    analyze_cont = false;
                    if (!analysis_thread.Join(2000))
                    {
                        analysis_thread.Abort();
                    }
                }
            }
            analysis_thread = new System.Threading.Thread(analyze);
            analysis_thread.Start();
        }
        private bool analyze_cont=false;
        private void analyze(){
            analyze_cont = true;
            //serieslist.Clear();
            study = new DynamicStudy(Properties.Settings.Default.CurrentStudy + RawDataSubdirName);
            //GRD_Utils.FileIterator fi = new GRD_Utils.FileIterator(Properties.Settings.Default.CurrentStudy + RawDataSubdirName);
            //while (fi.MoveNext() && analyze_cont)
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Properties.Settings.Default.CurrentStudy + RawDataSubdirName);
            System.IO.FileInfo[] files = di.GetFiles();
            int filecount = files.Count();
            int filesprocessed = 0;
            foreach (System.IO.FileInfo fi in files)
            {
                filesprocessed += 1;
                if (!analyze_cont) { break; }
                change_SS_text("Loading: " + fi.FullName);

                study.Add(fi.FullName);
                
                /*
                SortedList<String, Props> positionlist;
                if (serieslist.Keys.Contains<String>(p.seriesUID))
                {
                    serieslist.TryGetValue(p.seriesUID, out positionlist);
                    if (positionlist.Keys.Contains<String>(p.sliceposition))
                    {
                        Props lastp;
                        positionlist.TryGetValue(p.sliceposition, out lastp);
                        p.countedimages = lastp.countedimages + 1;
                        //if (lastp.seriesUID != p.seriesUID) { p.seriesUID = "Varies"; }
                        if (lastp.seriesdesc != p.seriesdesc) { p.seriesdesc = "Varies"; }
                        if (lastp.studyUID != p.studyUID) { p.studyUID = "Varies"; }
                        if (lastp.triggertime != p.triggertime) { p.triggertime = "Varies"; }
                        if (lastp.acquisitiontime != p.acquisitiontime) { p.acquisitiontime = "Varies"; }
                        if (lastp.accessionnum != p.accessionnum) { p.accessionnum = "Varies"; }
                        if (lastp.transfersyntax != p.transfersyntax) { p.transfersyntax = "Varies"; }
                        positionlist.Remove(lastp.sliceposition);
                    }
                    else
                    {
                        p.countedimages = 1;
                    }
                }
                else
                {
                    positionlist = new SortedList<string, Props>();
                    serieslist.Add(p.seriesUID, positionlist);
                }
                positionlist.Add(p.sliceposition, p);
                 */
                


                change_SS_prog(filesprocessed,0,filecount);
            }

            Study_Editor se = new Study_Editor(study,this);
            //this.BeginInvoke(new delegate_SS(se.Autosize));
            this.BeginInvoke(new delegate_SS(se.Show));

            /*
            Inspect_DICOM_Files idf = new Inspect_DICOM_Files();

            foreach (SortedList<String, Props> propertieslist in serieslist.Values)
            {
                foreach (Props p in propertieslist.Values)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = p.studyUID;
                    lvi.SubItems.Add(p.seriesUID);
                    lvi.SubItems.Add(p.seriesdesc);
                    lvi.SubItems.Add(p.accessionnum);
                    lvi.SubItems.Add(p.triggertime);
                    lvi.SubItems.Add(p.acquisitiontime);
                    lvi.SubItems.Add(p.sliceposition);
                    lvi.SubItems.Add(p.countedimages.ToString());
                    lvi.SubItems.Add(p.transfersyntax);
                    idf.lv().Items.Add(lvi);
                }
            }
            
            this.BeginInvoke(new delegate_SS(idf.Autosize));
            this.BeginInvoke(new delegate_SS(idf.Show));
            */
             
            change_SS_text();
            change_SS_prog(0, 0, 1);
        }     

        public bool DatabaseDefined()
        {
            bool defined= (new System.IO.DirectoryInfo(Properties.Settings.Default.DataDirectory)).Exists;
            if (!defined) {
                changecurrentstudy("");
                MessageBox.Show("Database directory not defined. Please go to settings.");
            }
            return defined;
        }

        private String fixedfilename(String filename)
        {
            if (filename == null) { return null; }
            string invalid = new string(System.IO.Path.GetInvalidFileNameChars()) + new string(System.IO.Path.GetInvalidPathChars()) + "_";
            foreach (char c in invalid)
            {
                filename = filename.Replace(c.ToString(), "");
            }
            return filename;
        }

        private System.Threading.Thread importthread;
        private void newPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                StartImport(fbd.SelectedPath, false);
            }
        }
        private void StartImport(string import_directory, bool deletedirectoryafter)
        {
            this.import_directory = import_directory;
            this.deletedirectoryafter = deletedirectoryafter;
            if (importthread != null)
            {
                if (importthread.IsAlive)
                {
                    import_cont = false;
                    if (!importthread.Join(2000))
                    {
                        importthread.Abort();
                    }
                }
            }
            importthread = new System.Threading.Thread(ImportStudy);
            importthread.SetApartmentState(System.Threading.ApartmentState.STA);
            importthread.Start();
        }

        public String RawDataSubdirName = "\\input_files";
        public String ImageToDICOMSubdirName = "\\output_files";

        private bool import_cont = false;
        private string import_directory;
        private bool deletedirectoryafter;
        private void ImportStudy()
        {
            try
            {
                import_cont = true;
                if (!DatabaseDefined()) { return; }

                //Do sort and check right away, putting the resulting files in the right place in the destination directory.
                System.IO.DirectoryInfo basedir = new System.IO.DirectoryInfo(Properties.Settings.Default.DataDirectory);
                if (!basedir.Exists) { basedir.Create(); }

                GRD_Utils.FileIterator fi = new GRD_Utils.FileIterator(import_directory);
                gdcm.TransferSyntax ts = new gdcm.TransferSyntax(gdcm.TransferSyntax.TSType.ExplicitVRLittleEndian);
                //gdcm.TransferSyntax ts = new gdcm.TransferSyntax(gdcm.TransferSyntax.TSType.ImplicitVRLittleEndian);
                //gdcm.TransferSyntax ts = new gdcm.TransferSyntax(gdcm.TransferSyntax.TSType.ImplicitVRBigEndianPrivateGE); //just used to check that transfer syntax conversion works
                GRD_Utils.TSConvert tsc = new GRD_Utils.TSConvert(ts);
                String newname;

                while (import_cont && fi.MoveNext())
                {
                    change_SS_text("Converting: " + fi.Current.Name);
                    gdcm.Reader reader = new gdcm.Reader();
                    reader.SetFileName(fi.Current.FullName);

                    if (reader.CanRead())
                    {
                        //For some reason, this must be initialized every time...
                        gdcm.TagSetType tst = new gdcm.TagSetType();
                        tst.Add(GRD_Utils.Tags.tag_patientname);
                        tst.Add(GRD_Utils.Tags.tag_studyInstanceUID);
                        tst.Add(GRD_Utils.Tags.tag_studyaccessionnumber);

                        reader.ReadSelectedTags(tst);
                        //reader.Read();
                        gdcm.File f;
                        f = reader.GetFile();
                        gdcm.DataSet ds = f.GetDataSet();

                        String patname = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds.GetDataElement(GRD_Utils.Tags.tag_patientname));
                        patname = fixedfilename(patname);

                        String study = "";
                        try
                        {
                            study = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds.GetDataElement(GRD_Utils.Tags.tag_studyaccessionnumber));
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("Accession number read error.");
                        }

                        if (study == null || study == "")
                        {
                            study = (String)GRD_Utils.DataElementInterpreter.interpretDE<String>(ds.GetDataElement(GRD_Utils.Tags.tag_studyInstanceUID));
                            study = fixedfilename(study);
                        }

                        System.IO.DirectoryInfo patdir = new System.IO.DirectoryInfo(basedir + "\\" + patname);
                        if (!patdir.Exists) { patdir.Create(); }
                        System.IO.DirectoryInfo studydir = new System.IO.DirectoryInfo(patdir.FullName + "\\" + study);
                        if (!studydir.Exists)
                        {
                            studydir.Create();
                            changecurrentstudy(studydir.FullName);
                        }
                        System.IO.DirectoryInfo dicomrawdir = new System.IO.DirectoryInfo(studydir.FullName + RawDataSubdirName);
                        if (!dicomrawdir.Exists) { dicomrawdir.Create(); }

                        newname = dicomrawdir.GetFiles().Count<System.IO.FileInfo>().ToString();
                        newname = newname.PadLeft(8, '0');
                        tsc.convert_transfer_syntax(fi.Current, dicomrawdir, newname);

                        tst.Dispose();
                    }

                    reader.Dispose();
                    change_SS_prog(fi.totalfiles() - fi.filesleft(), 0, fi.totalfiles());
                }
                change_SS_text();
                change_SS_prog(0, 0, 1);
                if(deletedirectoryafter)
                {
                    change_SS_text("Deleting temporary directory.");
                    System.IO.DirectoryInfo di_temp = new System.IO.DirectoryInfo(import_directory);
                    di_temp.Delete(true);
                }
                change_SS_text();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changePatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DatabaseDefined()) {return;}
            Patients pf = new Patients(this);
            pf.ShowDialog();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void deletePatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DatabaseDefined()) { return; }
            if (!System.IO.Directory.Exists(Properties.Settings.Default.CurrentStudy)) { return; }
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Properties.Settings.Default.CurrentStudy);
            if (!di.Exists)
            {
                System.Windows.Forms.MessageBox.Show("No patient selected.");
                return;
            }
            if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this study? This cannot be undone.","",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    di.Parent.Delete(true);
                }
                catch(Exception e2)
                {
                    System.Windows.Forms.MessageBox.Show(e2.Message);
                }
                changecurrentstudy("");
            }
        }

        private string GetCurrentStudyFile()
        {
            GRD_Utils.FileIterator fi = new GRD_Utils.FileIterator(Properties.Settings.Default.CurrentStudy+RawDataSubdirName);
            while (fi.MoveNext())
            {
                gdcm.Reader reader = new gdcm.Reader();
                reader.SetFileName(fi.Current.FullName);

                if (reader.CanRead())
                {
                    reader.Dispose();
                    return fi.Current.FullName;
                }
            }
            return null;
        }

        private System.Threading.Thread convertthread;
        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di;
            try
            { di = new System.IO.DirectoryInfo(Properties.Settings.Default.CurrentStudy); }
            catch(Exception e2)
            { MessageBox.Show(e2.Message);return; }
            if (!di.Exists)
            {
                System.Windows.Forms.MessageBox.Show("No patient selected.");
                return;
            }
            OpenFileDialog fbd = new OpenFileDialog();
            fbd.Multiselect = true;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                imagestoconvert = fbd.FileNames;
                if (convertthread != null)
                {
                    continueconversion = false;
                    if (convertthread.IsAlive)
                    {
                        if (!convertthread.Join(5000))
                        {
                            convertthread.Interrupt();
                        }
                    }
                }
                
                convertthread = new System.Threading.Thread(imageconvert);
                convertthread.Start();
            }
        }
        private System.IO.DirectoryInfo get_output_dir()
        {
            return new System.IO.DirectoryInfo(Properties.Settings.Default.CurrentStudy + ImageToDICOMSubdirName);
        }
        private bool continueconversion = false;
        private string[] imagestoconvert;
        private void imageconvert()
        {
            change_SS_text("Converting images.");
            continueconversion = true;
            System.IO.DirectoryInfo di_out = get_output_dir();
            if (!di_out.Exists) { di_out.Create(); }
            int n = 0;
            gdcm.UIDGenerator uidgen = new gdcm.UIDGenerator();
            String seriesUID = uidgen.Generate();
            String studyUID = uidgen.Generate();
            uidgen.Dispose();

            string filefile = GetCurrentStudyFile();
            try
            {
                foreach (String f in imagestoconvert)
                {
                    if (!continueconversion) { return; }
                    change_SS_text("Converting images. Current file: " + f);
                    n += 1;
                    System.IO.FileInfo fi = new System.IO.FileInfo(f);
                    if (fi.Extension.Equals(".jpg", StringComparison.CurrentCultureIgnoreCase))
                    {
                        System.IO.FileInfo fi_new = new System.IO.FileInfo(di_out.FullName + "\\" + fixedfilename(fi.Name.Substring(0, fi.Name.LastIndexOf('.'))));
                        GRD_Utils.DICOM_Functions.Convert_to_DICOM(fi.FullName, fi_new.FullName, filefile, this.checkBox1.Checked, studyUID, seriesUID, imagestoconvert.Count(), n);
                    }
                    change_SS_prog(n, 1, imagestoconvert.Count());
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            change_SS_text("Finished conversion.");
            System.Threading.Thread.Sleep(1000);
            change_SS_prog(0, 0, 1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void databaseDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f_settings = new Settings();
            f_settings.ShowDialog();
        }

        private void networkSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ncf = new DICOM_Fetch.form_NetworkConfig(configs);
            ncf.ShowDialog();
        }

        private void importStudyFromPACSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dsf == null) { dsf = new Download_Study(); }
            if (dsf.ShowDialog() == DialogResult.OK)
            {
                StartImport(Download_Study.tempdir(), false);
            }
            else
            {
                System.IO.DirectoryInfo di_temp = new System.IO.DirectoryInfo(Download_Study.tempdir());
                if (di_temp.Exists) { di_temp.Delete(true); }
            }
        }
        DICOM_Fetch.DICOMNetworkEventHandler_Status storehandler;
        private void button2_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Send output files to PACS? This cannot be undone. Be sure you have the correct patient selected.", "Confirm PACS send.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DICOM_Fetch.CStore cs = new DICOM_Fetch.CStore(configs.Current(), get_output_dir().GetFiles());
                storehandler = new DICOM_Fetch.DICOMNetworkEventHandler_Status(statuschange_CStore);
                cs.StatusChange += storehandler;
                cs.Execute();
            }
        }
        private delegate void delegate_ChangeStatusStrip2();
        private DICOM_Fetch.DICOMNetworkQueryStatus storestatus;
        private void statuschange_CStore(DICOM_Fetch.DICOMNetworkQueryStatus status)
        {
            storestatus = status;
            this.BeginInvoke(new delegate_ChangeStatusStrip2(ChangeStatusStrip2));
        }
        private void ChangeStatusStrip2()
        {
            switch (storestatus)
            {
                case DICOM_Fetch.DICOMNetworkQueryStatus.Waiting:
                    toolStripStatusLabel2.Text = configs.Current().Label + ": CStore pending.";
                    toolStripStatusLabel2.BackColor = Color.Yellow;
                    break;
                case DICOM_Fetch.DICOMNetworkQueryStatus.Success:
                    toolStripStatusLabel2.Text = configs.Current().Label + ": CStore succeeded.";
                    toolStripStatusLabel2.BackColor = Color.LightGreen;
                    break;
                case DICOM_Fetch.DICOMNetworkQueryStatus.Failure:
                    toolStripStatusLabel2.Text = configs.Current().Label + ": CStore failed.";
                    toolStripStatusLabel2.BackColor = Color.Red;
                    break;
            }
        }
    }
}
