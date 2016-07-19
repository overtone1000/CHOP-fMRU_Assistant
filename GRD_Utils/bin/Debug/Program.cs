using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    class Program
    {
        //static String basedir= "C:\\Users\\Tyler Moore\\Desktop\\MRU for Hopkins\\dcmmkdir\\2";
        //static String basedir = "C:\\Users\\Tyler Moore\\Desktop\\MRU for Hopkins\\dcmmkdir\\";
        //static String basedir = "C:\\Users\\Tyler Moore\\Desktop\\MRU for Hopkins\\dcmmkdir\\Data Trimmed";
        static String basedir = @"C:\Users\Tyler Moore\Desktop\New folder\Hans^Hansen\1.2.208.154.1.17485.21432.33309.17379.46322.53037.44486.44865-";
        //static string outputdir = "C:\\Users\\Tyler Moore\\Desktop\\MRU for Hopkins\\dcmmkdir\\Programmatic_Output";
        static string anondir = "C:\\Users\\Tyler Moore\\Desktop\\MRU for Hopkins\\dcmmkdir\\Data_Anonymized";
        //static string TSdir = "C:\\Users\\Tyler Moore\\Desktop\\MRU for Hopkins\\dcmmkdir\\Data_TS-ImplicitVRLittleEndian";
        static string TSdir = @"C:\Users\Tyler Moore\Desktop\New folder\Hans^Hansen\1.2.208.154.1.17485.21432.33309.17379.46322.53037.44486.44865-\TS\";
        static string TS_tag_dir = "C:\\Users\\Tyler Moore\\Desktop\\MRU for Hopkins\\dcmmkdir\\Data_TS-ImplicitVRLittleEndian-ModifiedTags";
        static FileIterator fileit;
        static void Main(string[] args)
        {
            System.IO.DirectoryInfo outputdir;

            TSConvert c = new TSConvert(new gdcm.TransferSyntax(gdcm.TransferSyntax.TSType.ExplicitVRLittleEndian));
            TagQuery tq = new TagQuery();
            //CreateDICOMDIR d = new CreateDICOMDIR();
            
            System.Collections.Generic.SortedList<String, int> list = new System.Collections.Generic.SortedList<String, int>();

            outputdir = new System.IO.DirectoryInfo(anondir);
            if (!outputdir.Exists) { outputdir.Create(); }

            if (false)
            {
                fileit = new FileIterator(basedir);
                while (fileit.MoveNext())
                {
                    System.Diagnostics.Debug.WriteLine("Current file: " + fileit.Current.FullName);
                    Anonymizer.anonymize(fileit.Current.FullName, outputdir.FullName + "\\" + fileit.Current.Name);
                    //d.Create(basedir);


                    String name = tq.queryseriesname(fileit.Current);
                    if (list.ContainsKey(name))
                    {
                        int thisval;
                        list.TryGetValue(name, out thisval);
                        thisval += 1;
                        list.Remove(name);
                        list.Add(name, thisval);
                    }
                    else
                    {
                        list.Add(name, (int)1);
                    }

                    if (name == "8ML MULTI/ 3D_DYNAMIC_T1_UROGRAM 5 min delay")
                    {
                        fileit.Current.Delete();
                    }
                }
            }

            if (true)
            {
                outputdir = new System.IO.DirectoryInfo(TSdir);
                if (!outputdir.Exists) { outputdir.Create(); }
                fileit = new FileIterator(basedir);
                while (fileit.MoveNext())
                {
                    c.convert_transfer_syntax(fileit.Current, outputdir);
                }
            }

            c.Dispose();
            tq.Dispose();
            //d.Dispose();
        }
    }
}
