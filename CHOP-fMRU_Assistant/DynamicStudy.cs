using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHOP_fMRU_Assistant
{
    public struct DynamicStudyImageKey : IComparable
    {
        public String seriesUID;
        public float time;
        public float position;
        public int CompareTo(object k)
        {
            DynamicStudyImageKey other = (DynamicStudyImageKey)k;
            if (other.seriesUID != seriesUID) { return String.Compare(seriesUID, other.seriesUID); }
            if (other.position != position) { return Math.Sign(other.position-position); }
            if (other.time != time) { return Math.Sign(other.time - time); }
            return 0;
        }
    }
    public class DynamicStudy
    {
        //private SortedList<String, SortedList<float, SortedList<float, String>>> outerlist = new SortedList<String, SortedList<float, SortedList<float, String>>>();
        //private SortedList<float, SortedList<float, String>> middlelist;
        //private SortedList<float, String> innerlist;
        private SortedList<DynamicStudyImageKey, String> images = new SortedList<DynamicStudyImageKey, String>();
        private SortedList<String, String> seriesnames = new SortedList<String, String>();
        private System.IO.DirectoryInfo studydirectory;
        private System.IO.DirectoryInfo exclusiondirectory;
        //Innermost sortedlist is "acquisition time" as key and "file name" as value
        //Middle sortedlist is "slice position" as key and the innermost sortedlist as value
        //Outer sortedlist is "series UID" as key and middle sortedlist as value
        public DynamicStudy(String directory)
        {
            studydirectory = new System.IO.DirectoryInfo(directory);
            exclusiondirectory = new System.IO.DirectoryInfo(directory + "\\excluded");
            if (!exclusiondirectory.Exists) { exclusiondirectory.Create(); }
        }
        public void Clear()
        {
            //outerlist.Clear();
            images.Clear();
        }
        public void Add(String seriesUID, String seriesdescription, float acquisitiontime, float sliceposition, String filename)
        {
            //study.Add(seriesUID, acquisitiontime, sliceposition, filename);
            
            /*
            if (!outerlist.TryGetValue(seriesUID, out middlelist))
            {
                middlelist = new SortedList<float, SortedList<float, String>>();
                outerlist.Add(seriesUID, middlelist);
            }

            if (!middlelist.TryGetValue(sliceposition, out innerlist))
            {
                innerlist = new SortedList<float, String>();
                middlelist.Add(sliceposition, innerlist);
            }
            if(!innerlist.ContainsKey(acquisitiontime)){
                innerlist.Add(acquisitiontime, filename);
            }
             */
            DynamicStudyImageKey k;
            k.seriesUID = seriesUID;
            k.time = acquisitiontime;
            k.position = sliceposition;

            if (!images.ContainsKey(k)) { images.Add(k, filename); }
            if(!seriesnames.Keys.Contains(k.seriesUID)){
                seriesnames.Add(k.seriesUID,seriesdescription);
            }
        }
        public String SeriesDescription(String seriesUID)
        {
            String retval = "";
            if (seriesUID == null) { return retval; }
            seriesnames.TryGetValue(seriesUID, out retval);
            return retval;
        }
        public List<String> SeriesUIDs()
        {
            List<String> retval = new List<String>();
            /*
            foreach (String series in outerlist.Keys)
            {
                retval.Add(series);
            }
             */
            foreach(DynamicStudyImageKey k in images.Keys)
            {
                if (!retval.Contains(k.seriesUID)) { retval.Add(k.seriesUID); }
            }
            retval.Sort();
            return retval;
        }
        public List<float> SlicePositions(String seriesUID)
        {
            List<float> retval = new List<float>();
            /*
            if (outerlist.TryGetValue(seriesUID, out middlelist))
            {
                foreach (float sliceposition in middlelist.Keys)
                {
                    retval.Add(sliceposition);
                }
            }
             */
            foreach (DynamicStudyImageKey k in images.Keys)
            {
                if (k.seriesUID==seriesUID && !retval.Contains(k.position)) { retval.Add(k.position); }
            }
            retval.Sort();
            return retval;
        }
        public List<float> AcquisitionTimes(String seriesUID)
        {
            List<float> retval = new List<float>();
            if (seriesUID == null) { return retval; }
            /*
            if (outerlist.TryGetValue(seriesUID, out middlelist))
            {
                foreach (float sliceposition in middlelist.Keys)
                {
                    if (middlelist.TryGetValue(sliceposition, out innerlist))
                    {
                        foreach (float acquisitiontime in innerlist.Keys)
                        {
                            if (!retval.Contains(acquisitiontime))
                            {
                                retval.Add(acquisitiontime);
                            }
                        }
                    }
                }
            }
             */
            foreach (DynamicStudyImageKey k in images.Keys)
            {
                if (k.seriesUID == seriesUID && !retval.Contains(k.time)) { retval.Add(k.time); }
            }
            retval.Sort();
            return retval;
        }
        public bool ImageExists(String seriesUID, float sliceposition, float acquisitiontime)
        {
            /*
            if (outerlist.TryGetValue(seriesUID, out middlelist))
            {
                if (middlelist.TryGetValue(sliceposition, out innerlist))
                {
                    String resultingval;
                    if (innerlist.TryGetValue(acquisitiontime, out resultingval))
                    {
                        return true;
                    }
                }
            }
             return false;
             */
            DynamicStudyImageKey k;
            k.seriesUID = seriesUID;
            k.time = acquisitiontime;
            k.position = sliceposition;
            return images.Keys.Contains(k);
            
        }
        public String ImageFile(String seriesUID, float sliceposition, float acquisitiontime)
        {
            /*
            if (outerlist.TryGetValue(seriesUID, out middlelist))
            {
                if (middlelist.TryGetValue(sliceposition, out innerlist))
                {
                    String resultingval;
                    if (innerlist.TryGetValue(acquisitiontime, out resultingval))
                    {
                        return resultingval;
                    }
                }
            }
            */
            String retval;
            DynamicStudyImageKey k;
            k.seriesUID = seriesUID;
            k.time = acquisitiontime;
            k.position = sliceposition;
            images.TryGetValue(k, out retval);
            return retval;
        }
        private void ExcludeImage(DynamicStudyImageKey k)
        {
            String file;
            images.TryGetValue(k, out file);
            System.IO.FileInfo f = new System.IO.FileInfo(file);
            String dest = exclusiondirectory.FullName + "\\" + f.Name;
            System.Diagnostics.Debug.WriteLine("Moving " + f.FullName + " to " + dest);
            f.MoveTo(exclusiondirectory.FullName + "\\" + f.Name);
            images.Remove(k);
        }
        public void ExcludeSeries(String seriesUID)
        {
            /*
            if(outerlist.TryGetValue(seriesUID, out middlelist))
            {
                foreach(SortedList<float,String>il in middlelist.Values)
                {
                    foreach (String file in il.Values)
                    {
                        System.IO.FileInfo f = new System.IO.FileInfo(file);
                        String dest = exclusiondirectory.FullName + "\\" + f.Name;
                        System.Diagnostics.Debug.WriteLine("Moving " + f.FullName + " to " + dest);
                        f.MoveTo(exclusiondirectory.FullName+"\\"+f.Name);
                    }
                    il.Clear();
                }
                middlelist.Clear();
                outerlist.Remove(seriesUID);
            }
             * */
            foreach(DynamicStudyImageKey k in images.Keys.ToArray())
            {
                if (k.seriesUID == seriesUID) { ExcludeImage(k);}
            }
        }
        public void ExcludeBeforeTime(float time)
        {
            foreach (DynamicStudyImageKey k in images.Keys.ToArray())
            {
                if (k.time < time) { ExcludeImage(k); }
            }
        }
        public void ExcludeAfterTime(float time)
        {
            foreach (DynamicStudyImageKey k in images.Keys.ToArray())
            {
                if (k.time > time) { ExcludeImage(k); }
            }
        }
        public void ExcludeBeforePosition(float position)
        {
            foreach (DynamicStudyImageKey k in images.Keys.ToArray())
            {
                if (k.position < position) { ExcludeImage(k); }
            }
        }
        public void ExcludeAfterPosition(float position)
        {
            foreach (DynamicStudyImageKey k in images.Keys.ToArray())
            {
                if (k.position > position) { ExcludeImage(k); }
            }
        }
        public void UndoExclusions()
        {
            foreach(System.IO.FileInfo f in exclusiondirectory.GetFiles())
            {
                String dest = studydirectory.FullName + "\\" + f.Name;
                System.Diagnostics.Debug.WriteLine("Moving " + f.FullName + " to " + dest);
                f.MoveTo(dest);
                Add(dest);                
            }
        }
        public void Add(String filename)
        {
            gdcm.ImageReader reader = new gdcm.ImageReader();
            reader.SetFileName(filename);
            //reader.Read();

            gdcm.TagSetType tst = new gdcm.TagSetType();
            tst.Add(GRD_Utils.Tags.tag_seriesInstanceUID);
            tst.Add(GRD_Utils.Tags.tag_acquisitiontime);
            tst.Add(GRD_Utils.Tags.tag_sliceposition);
            tst.Add(GRD_Utils.Tags.tag_seriesDescription);
            reader.ReadSelectedTags(tst);

            gdcm.DataSet ds = reader.GetFile().GetDataSet();

            Props p = new Props();
            p.seriesUID = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_seriesInstanceUID));
            p.seriesdesc = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_seriesDescription));
            //p.studyUID = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_studyInstanceUID));
            //p.triggertime = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_triggertime));
            p.acquisitiontime = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_acquisitiontime));
            //p.accessionnum = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_studyaccessionnumber));
            p.sliceposition = (String)GRD_Utils.DataElementInterpreter.interpretDE(ds.GetDataElement(GRD_Utils.Tags.tag_sliceposition));
            //p.transfersyntax = reader.GetImage().GetTransferSyntax().toString();

            reader.Dispose();

            if (p.seriesUID != null && p.acquisitiontime != null && p.sliceposition != null)
            {
                float sliceposition = float.Parse(p.sliceposition);
                float acquisitiontime = float.Parse(p.acquisitiontime);
                Add(p.seriesUID, p.seriesdesc, acquisitiontime, sliceposition, filename);
            }
        }
    }
}
