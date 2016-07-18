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
        public MainForm()
        {
            InitializeComponent();
            Properties.Settings.Default.Reload();
            this.Load+=new EventHandler(formloaded);
        }
        private void formloaded(object sender, System.EventArgs e){
            changecurrentstudy(Properties.Settings.Default.CurrentStudy);
        }
        public void changecurrentstudy(String currentstudy)
        {
            Properties.Settings.Default.CurrentStudy = currentstudy;
            Properties.Settings.Default.Save();
            change_SS_text();
        }
        public void change_SS_text()
        {
            SS_text = "Current study: " + Properties.Settings.Default.CurrentStudy;
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

            Series_Editor se = new Series_Editor(study,this);
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public bool DatabaseDefined()
        {
            bool defined= (new System.IO.DirectoryInfo(Properties.Settings.Default.DataDirectory)).Exists;
            if (!defined) { System.Windows.Forms.MessageBox.Show("Database directory not defined. Please go to settings."); }
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
            if (importthread != null)
            {
                if(importthread.IsAlive)
                {
                    import_cont = false;
                    if(!importthread.Join(2000))
                    {
                        importthread.Abort();
                    }
                }
            }
            importthread = new System.Threading.Thread(ImportPatient);
            importthread.SetApartmentState(System.Threading.ApartmentState.STA);
            importthread.Start();
        }
        public String RawDataSubdirName = "\\input_files";
        public String ImageToDICOMSubdirName = "\\output_files";

        private bool import_cont = false;
        private void ImportPatient()
        {
            import_cont = true;
            if (!DatabaseDefined()) { return; }
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog()==DialogResult.OK)
            {
                //Do sort and check right away, putting the resulting files in the right place in the destination directory.
                System.IO.DirectoryInfo basedir = new System.IO.DirectoryInfo(Properties.Settings.Default.DataDirectory);
                if (!basedir.Exists) { basedir.Create(); }
                
                GRD_Utils.FileIterator fi = new GRD_Utils.FileIterator(fbd.SelectedPath);
                GRD_Utils.TSConvert tsc = new GRD_Utils.TSConvert(new gdcm.TransferSyntax(gdcm.TransferSyntax.TSType.ExplicitVRLittleEndian));
                String newname;
                while (import_cont && fi.MoveNext())
                {
                    change_SS_text("Converting: " + fi.Current.Name);
                    gdcm.Reader reader = new gdcm.Reader();
                    reader.SetFileName(fi.Current.FullName);

                    if (reader.CanRead()){
                        gdcm.TagSetType tst = new gdcm.TagSetType();
                        tst.Add(GRD_Utils.Tags.tag_patientname);
                        tst.Add(GRD_Utils.Tags.tag_studyInstanceUID);
                        tst.Add(GRD_Utils.Tags.tag_studyaccessionnumber);

                        reader.ReadSelectedTags(tst);
                        //reader.Read();
                        gdcm.File f;
                        f = reader.GetFile();
                        gdcm.DataSet ds = f.GetDataSet();

                        String patname=(String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_patientname));
                        patname = fixedfilename(patname);

                        String study="";
                        try
                        {
                            study = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_studyaccessionnumber));
                        }
                        catch(Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("Accession number read error.");
                        }
                        
                        if (study == null || study == "")
                        {
                            study = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_studyInstanceUID));
                            study = fixedfilename(study);
                        }

                        tst.Dispose();

                        System.IO.DirectoryInfo patdir = new System.IO.DirectoryInfo(basedir + "\\" + patname);
                        if (!patdir.Exists) { patdir.Create(); }
                        System.IO.DirectoryInfo studydir = new System.IO.DirectoryInfo(patdir.FullName + "\\" + study);
                        if (!studydir.Exists) {
                            studydir.Create();
                            changecurrentstudy(studydir.FullName);
                        }
                        System.IO.DirectoryInfo dicomrawdir = new System.IO.DirectoryInfo(studydir.FullName + RawDataSubdirName);
                        if (!dicomrawdir.Exists) { dicomrawdir.Create(); }

                        newname = dicomrawdir.GetFiles().Count<System.IO.FileInfo>().ToString();
                        newname = newname.PadLeft(8, '0');
                        tsc.convert_transfer_syntax(fi.Current, dicomrawdir, newname);
                    }
                    reader.Dispose();
                    change_SS_prog(fi.totalfiles() - fi.filesleft(), 0, fi.totalfiles());
                }
            }
            change_SS_text();
            change_SS_prog(0, 0, 1);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings f_settings = new Settings();
            f_settings.ShowDialog();
        }

        private void changePatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DatabaseDefined()) { return; }
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
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Properties.Settings.Default.CurrentStudy);
            if (!di.Exists)
            {
                System.Windows.Forms.MessageBox.Show("No patient selected.");
                return;
            }
            if (System.Windows.Forms.MessageBox.Show("Are you sure you want to delete this patient's data? This cannot be undone.","",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                di.Delete();
            }
        }

        private gdcm.File GetCurrentStudyFile()
        {
            GRD_Utils.FileIterator fi = new GRD_Utils.FileIterator(Properties.Settings.Default.CurrentStudy+RawDataSubdirName);
            while (fi.MoveNext())
            {
                gdcm.Reader reader = new gdcm.Reader();
                reader.SetFileName(fi.Current.FullName);

                if (reader.CanRead())
                {
                    reader.Read();
                    gdcm.File f = reader.GetFile();
                    return f;
                }
            }
            return null;
        }

        private System.Threading.Thread convertthread;
        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Properties.Settings.Default.CurrentStudy);
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
                    if (!convertthread.Join(1000))
                    {
                        convertthread.Interrupt();
                    }
                }
                
                convertthread = new System.Threading.Thread(imageconvert);
                convertthread.Start();
            }
        }
        private bool continueconversion = false;
        private string[] imagestoconvert;
        private void imageconvert()
        {
            change_SS_text("Converting images.");
            continueconversion = true;
            System.IO.DirectoryInfo di_out = new System.IO.DirectoryInfo(Properties.Settings.Default.CurrentStudy + ImageToDICOMSubdirName);
            if (!di_out.Exists) { di_out.Create(); }
            int n = 0;
            String seriesUID = new gdcm.UIDGenerator().Generate();
            String studyUID = new gdcm.UIDGenerator().Generate();
            foreach (String f in imagestoconvert)
            {
                if (!continueconversion) { return; }
                change_SS_text("Converting images. Current file: " + f);
                n += 1;
                System.IO.FileInfo fi = new System.IO.FileInfo(f);
                System.IO.FileInfo fi_new = new System.IO.FileInfo(di_out.FullName + "\\" + fixedfilename(fi.Name.Substring(0, fi.Name.LastIndexOf('.'))));
                if (fi_new.Exists) { fi_new.Delete(); }
                GRD_Utils.DICOM_Functions.Convert_to_DICOM(fi.FullName, fi_new.FullName, GetCurrentStudyFile(), this.checkBox1.Checked, studyUID, seriesUID, imagestoconvert.Count(), n);
                change_SS_prog(n, 1, imagestoconvert.Count());
            }
            change_SS_text("Finished conversion.");
            System.Threading.Thread.Sleep(2000);
            change_SS_prog(0, 0, 1);
            change_SS_text();
        }
    }
}
