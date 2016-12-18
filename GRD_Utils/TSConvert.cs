using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRD_Utils
{
    public class TSConvert : IDisposable
    {
        static List<gdcm.Tag> legal_tags = new List<gdcm.Tag>();
        static TSConvert()
        {
        }

        private static void define_legal_tags()
        {
            //This takes forever to execute. Need to access dictionary.
            uint element = 0;
            uint group = 0;
            gdcm.Tag t = new gdcm.Tag();
            while (group <= ushort.MaxValue)
            {
                while (element <= ushort.MaxValue)
                {
                    t.SetElement((ushort)element);
                    if (!t.IsIllegal())
                    {
                        legal_tags.Add(t);
                    }
                    element++;
                }
                t.SetGroup((ushort)group);
                group++;
                element = 0;
            }
            System.Diagnostics.Debug.WriteLine(legal_tags.Count + " legal types defined.");
        }

        private static List<gdcm.Tag> tags_to_keep()
        {
            List<gdcm.Tag> retval = new List<gdcm.Tag>();
            retval.Capacity = 50;
            retval.Add(new gdcm.Tag(0x0002, 0x0000));
            retval.Add(new gdcm.Tag(0x0002, 0x0001));
            retval.Add(new gdcm.Tag(0x0002, 0x0002));
            retval.Add(new gdcm.Tag(0x0002, 0x0003));
            retval.Add(new gdcm.Tag(0x0002, 0x0010));
            retval.Add(new gdcm.Tag(0x0002, 0x0012));
            retval.Add(new gdcm.Tag(0x0002, 0x0013));
            retval.Add(new gdcm.Tag(0x0002, 0x0016));
            retval.Add(new gdcm.Tag(0x0008, 0x0005));
            retval.Add(new gdcm.Tag(0x0008, 0x0008));
            retval.Add(new gdcm.Tag(0x0008, 0x0012));
            retval.Add(new gdcm.Tag(0x0008, 0x0013));
            retval.Add(new gdcm.Tag(0x0008, 0x0014));
            retval.Add(new gdcm.Tag(0x0008, 0x0016));
            retval.Add(new gdcm.Tag(0x0008, 0x0018));
            retval.Add(new gdcm.Tag(0x0008, 0x0020));
            retval.Add(new gdcm.Tag(0x0008, 0x0021));
            retval.Add(new gdcm.Tag(0x0008, 0x0022));
            retval.Add(new gdcm.Tag(0x0008, 0x0023));
            retval.Add(new gdcm.Tag(0x0008, 0x0030));
            retval.Add(new gdcm.Tag(0x0008, 0x0031));
            retval.Add(new gdcm.Tag(0x0008, 0x0032));
            retval.Add(new gdcm.Tag(0x0008, 0x0033));
            retval.Add(new gdcm.Tag(0x0008, 0x0050));
            retval.Add(new gdcm.Tag(0x0008, 0x0060));
            retval.Add(new gdcm.Tag(0x0008, 0x0064));
            retval.Add(new gdcm.Tag(0x0008, 0x0070));
            retval.Add(new gdcm.Tag(0x0008, 0x0080));
            retval.Add(new gdcm.Tag(0x0008, 0x0090));
            retval.Add(new gdcm.Tag(0x0008, 0x0081));
            retval.Add(new gdcm.Tag(0x0008, 0x0100)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x0102)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x0104)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x1010)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x1030));
            retval.Add(new gdcm.Tag(0x0008, 0x1032)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x103E));
            retval.Add(new gdcm.Tag(0x0008, 0x1040));
            retval.Add(new gdcm.Tag(0x0008, 0x1050));
            retval.Add(new gdcm.Tag(0x0008, 0x1060));
            retval.Add(new gdcm.Tag(0x0008, 0x1070));
            retval.Add(new gdcm.Tag(0x0008, 0x1080));
            retval.Add(new gdcm.Tag(0x0008, 0x1090));
            retval.Add(new gdcm.Tag(0x0008, 0x1110)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x1111)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x1120)); //added empirically
            retval.Add(new gdcm.Tag(0x0008, 0x1140)); //added empirically
            retval.Add(new gdcm.Tag(0x0010, 0x0010));
            retval.Add(new gdcm.Tag(0x0010, 0x0020));
            retval.Add(new gdcm.Tag(0x0010, 0x0030));
            retval.Add(new gdcm.Tag(0x0010, 0x0032));
            retval.Add(new gdcm.Tag(0x0010, 0x0040));
            retval.Add(new gdcm.Tag(0x0010, 0x1010));
            retval.Add(new gdcm.Tag(0x0010, 0x1030));//added empirically
            retval.Add(new gdcm.Tag(0x0010, 0x1080));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0015));
            retval.Add(new gdcm.Tag(0x0018, 0x0020));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0021));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0022));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0023));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0024));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0025));//added empirically
            //retval.Add(new gdcm.Tag(0x0018, 0x0050));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0080));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0081));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0083));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0084));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0085));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0086));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0087));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0088));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0089));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0091));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0093));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0094));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x0095));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x1030));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x1060));
            retval.Add(new gdcm.Tag(0x0018, 0x1310));//added empirically
            retval.Add(new gdcm.Tag(0x0018, 0x2010));
            retval.Add(new gdcm.Tag(0x0018, 0x9073));
            retval.Add(new gdcm.Tag(0x0020, 0x000D));
            retval.Add(new gdcm.Tag(0x0020, 0x000E));
            retval.Add(new gdcm.Tag(0x0020, 0x0010));
            retval.Add(new gdcm.Tag(0x0020, 0x0011));
            retval.Add(new gdcm.Tag(0x0020, 0x0012));
            retval.Add(new gdcm.Tag(0x0020, 0x0013));
            retval.Add(new gdcm.Tag(0x0020, 0x0020));
            retval.Add(new gdcm.Tag(0x0020, 0x0032));
            retval.Add(new gdcm.Tag(0x0020, 0x0037));
            retval.Add(new gdcm.Tag(0x0020, 0x0052));
            retval.Add(new gdcm.Tag(0x0020, 0x0100));
            retval.Add(new gdcm.Tag(0x0020, 0x0105));
            retval.Add(new gdcm.Tag(0x0020, 0x1040));
            retval.Add(new gdcm.Tag(0x0020, 0x1041));
            retval.Add(new gdcm.Tag(0x0028, 0x0002));
            retval.Add(new gdcm.Tag(0x0028, 0x0004));
            retval.Add(new gdcm.Tag(0x0028, 0x0010));
            retval.Add(new gdcm.Tag(0x0028, 0x0011));
            retval.Add(new gdcm.Tag(0x0028, 0x0030));
            retval.Add(new gdcm.Tag(0x0028, 0x0100));
            retval.Add(new gdcm.Tag(0x0028, 0x0101));
            retval.Add(new gdcm.Tag(0x0028, 0x0102));
            retval.Add(new gdcm.Tag(0x0028, 0x0103));
            retval.Add(new gdcm.Tag(0x0028, 0x1050));
            retval.Add(new gdcm.Tag(0x0028, 0x1051));
            retval.Add(new gdcm.Tag(0x0028, 0x1052));
            retval.Add(new gdcm.Tag(0x0028, 0x1053));
            retval.Add(new gdcm.Tag(0x0028, 0x1054));
            retval.Add(new gdcm.Tag(0x0040, 0x9096));//added empirically
            retval.Add(new gdcm.Tag(0x2050, 0x0020));//added empirically
            //retval.Add(new gdcm.Tag(0x7FE0, 0010)); //Maybe leave image data to ireader?
            return retval;
        }
        private static List<gdcm.Tag> tags_image()
        {
            List<gdcm.Tag> retval = new List<gdcm.Tag>();
            retval.Capacity = 50;
            retval.Add(new gdcm.Tag(0x0028, 0x0002));
            retval.Add(new gdcm.Tag(0x0028, 0x0004));
            retval.Add(new gdcm.Tag(0x0028, 0x0010));
            retval.Add(new gdcm.Tag(0x0028, 0x0011));
            retval.Add(new gdcm.Tag(0x0028, 0x0030));
            retval.Add(new gdcm.Tag(0x0028, 0x0100));
            retval.Add(new gdcm.Tag(0x0028, 0x0101));
            retval.Add(new gdcm.Tag(0x0028, 0x0102));
            retval.Add(new gdcm.Tag(0x0028, 0x0103));
            retval.Add(new gdcm.Tag(0x7FE0, 0x0010));
            return retval;
        }
        private static gdcm.TagSetType selected_tags()
        {
            gdcm.TagSetType retval = new gdcm.TagSetType();
            List<gdcm.Tag> tags = tags_to_keep();
            foreach (gdcm.Tag t in tags)
            {
                retval.Add(t);
            }
            return retval;
        }

        gdcm.TransferSyntax targetsyntax;
        public TSConvert(gdcm.TransferSyntax targetsyntax)
        {
            this.targetsyntax = targetsyntax;
        }
        public bool convert_transfer_syntax(System.IO.FileInfo f, System.IO.DirectoryInfo output_location)
        {
            if (!output_location.Exists) { output_location.Create(); }
            return processdicomfile(f.FullName, output_location.FullName + '\\' + f.Name);
        }
        public bool convert_transfer_syntax(System.IO.FileInfo f, System.IO.DirectoryInfo output_location, String newname)
        {
            if (!output_location.Exists) { output_location.Create(); }
            return processdicomfile(f.FullName, output_location.FullName + '\\' + newname);
        }
        gdcm.ImageChangeTransferSyntax cts = new gdcm.ImageChangeTransferSyntax();

        private bool processdicomfile(String inputf, String outputf)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(outputf);
            if (fi.Exists)
            {
                fi.Delete();
            }

            bool retval = false;
            //Tried resusing these objects but "Read" function was not replacing old values from prior iteration
            gdcm.FileExplicitFilter fef = new gdcm.FileExplicitFilter(); ;
            gdcm.ImageReader ireader = new gdcm.ImageReader();
            gdcm.ImageReader ireader_imageonly = new gdcm.ImageReader();
            System.Diagnostics.Debug.WriteLine(inputf + " is a DICOM file.");
            ireader.SetFileName(inputf);
            if (ireader.CanRead())
            {
                if(true)
                {
                    System.IO.File.Copy(inputf, outputf);
                    return true;
                }

                ireader_imageonly.SetFileName(inputf);
                //gdcm.Reader reader = new gdcm.Reader();
                gdcm.ImageWriter imagewriter = new gdcm.ImageWriter();
                //gdcm.Writer writer = new gdcm.Writer();

                System.Diagnostics.Debug.WriteLine(inputf + " is also an image file. Converting.");


                List<gdcm.Tag> tags_to_read = new List<gdcm.Tag>();
                tags_to_read.AddRange(tags_to_keep());
                tags_to_read.AddRange(tags_image());

                gdcm.TagSetType tst = new gdcm.TagSetType();
                foreach (gdcm.Tag tg in tags_to_read)
                {
                    if (!tst.Contains(tg)) { tst.Add(tg); }
                }



                Boolean read_success;
                read_success = ireader.Read();
                //read_success = ireader.ReadSelectedTags(tst);
                ireader_imageonly.Read();
                //System.Diagnostics.Debug.WriteLine(ireader.GetFile().GetHeader().toString());
                //System.Diagnostics.Debug.WriteLine(ireader.GetFile().GetDataSet().toString());

                System.Diagnostics.Debug.WriteLine("IRead success=" + read_success);

                gdcm.Anonymizer anon = new gdcm.Anonymizer();
                anon.SetFile(ireader.GetFile());
                //anon.RemoveGroupLength();
                anon.RemovePrivateTags();
                anon.RemoveRetired();

                
                gdcm.DataSet dataSet = ireader.GetFile().GetDataSet();
                //gdcm.DataElement de;
                foreach (gdcm.Tag t in tags_to_read)
                {
                    if (dataSet.FindDataElement(t))
                    {
                        //Leave empty elements. It appears CHOP-fMRU needs them.
                        /*
                        System.Diagnostics.Debug.WriteLine("Tag found.");
                        if (t == null) { System.Diagnostics.Debug.WriteLine("t is null"); }
                        if (dataSet == null) { System.Diagnostics.Debug.WriteLine("dataSet is null"); }
                        de = dataSet.GetDataElement(t);
                        System.Diagnostics.Debug.WriteLine("DE found.");
                        if (de == null) { System.Diagnostics.Debug.WriteLine("de is null"); }
                        if (de.IsEmpty() || (de.GetSequenceOfFragments()!=null && de.GetSequenceOfFragments().GetNumberOfFragments() == 0))
                        {
                            System.Diagnostics.Debug.WriteLine("Removing " + t.toString());
                            System.Diagnostics.Debug.WriteLine(de.toString());
                            dataSet.Remove(t);
                        }
                        */
                    }
                    else
                    {
                        //System.Diagnostics.Debug.WriteLine("Creating new de.");
                        gdcm.DataElement newde = new gdcm.DataElement();
                        newde.SetTag(t);
                        newde.Empty();
                        //System.Diagnostics.Debug.WriteLine("Inserting new de. VR: " + newde.GetVR().toString() + ", VL: " + newde.GetVL().toString());
                        //dataSet.Insert(newde);
                    }
                }
                

                //Don't just copy if transfer syntax is right. Bogus tags need to be removed.
                //if (!(image.GetTransferSyntax().GetString() == targetsyntax.GetString()))
                //{

                try
                {

                    if (targetsyntax.IsExplicit())
                    {
                        fef.SetFile(ireader.GetFile());
                        fef.SetChangePrivateTags(true);
                        fef.SetRecomputeItemLength(true);
                        fef.SetRecomputeSequenceLength(true);
                        fef.SetUseVRUN(true);
                        if (!fef.Change())
                        {
                            MessageBox.Show("Couldn't change to explicit TS");
                            return false;
                        }
                    }

                    cts.SetTransferSyntax(targetsyntax);
                    cts.SetInput(ireader_imageonly.GetImage());
                    cts.SetForce(true);


                    //switch(true)
                    //switch (false)
                    switch (cts.Change())
                    {
                        case true:
                            ireader.GetFile().GetHeader().SetDataSetTransferSyntax(targetsyntax);
                            imagewriter.SetFile(fef.GetFile());
                            imagewriter.SetImage(cts.GetOutput());
                            imagewriter.SetFileName(outputf);

                            switch (imagewriter.Write())
                            {
                                case true:
                                    System.Diagnostics.Debug.WriteLine("Converted to " + outputf);
                                    retval = true;
                                    break;
                                case false:
                                    System.Diagnostics.Debug.WriteLine("ImageWriter failed");
                                    break;
                            }

                            break;
                        case false:
                            System.Diagnostics.Debug.WriteLine("Transfer syntax conversion failed. Copying without converted transfer syntax.");
                            imagewriter.SetFile(fef.GetFile());
                            imagewriter.SetImage(ireader_imageonly.GetImage());
                            imagewriter.SetFileName(outputf);
                            switch (imagewriter.Write())
                            {
                                case true:
                                    System.Diagnostics.Debug.WriteLine("Simple write successful.");
                                    retval = true;
                                    break;
                                case false:
                                    System.Diagnostics.Debug.WriteLine("ImageWriter failed");
                                    break;
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error.");
                }
                //}
                //else
                //{
                //    System.Diagnostics.Debug.WriteLine("Transfer syntax is already correct. Just copying.");
                //    System.IO.FileInfo inf = new System.IO.FileInfo(inputf);
                //    inf.CopyTo(outputf, true);
                //    retval = true;
                //}
                //reader.Dispose();
                //writer.Dispose();
                //f.Dispose();
                //ds.Dispose();
                //tst.Dispose();
                //newds.Dispose();
                imagewriter.Dispose();
                anon.Dispose();
                fef.Dispose();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(inputf + " is not an image file.");
            }
            ireader.Dispose();
            ireader_imageonly.Dispose();
            return retval;
        }

        public void Dispose()
        {
            targetsyntax.Dispose();
            cts.Dispose();

        }

    }
}
