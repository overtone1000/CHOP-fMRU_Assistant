﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRD_Utils
{
    public class TSConvert:IDisposable
    {
        private static List<gdcm.Tag> tags_to_keep()
        {
            List<gdcm.Tag> retval=new List<gdcm.Tag>();
            retval.Capacity = 50;
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
            retval.Add(new gdcm.Tag(0x0008, 0x1030));
            retval.Add(new gdcm.Tag(0x0008, 0x103E));
            retval.Add(new gdcm.Tag(0x0008, 0x1040));
            retval.Add(new gdcm.Tag(0x0008, 0x1050));
            retval.Add(new gdcm.Tag(0x0008, 0x1060));
            retval.Add(new gdcm.Tag(0x0008, 0x1070));
            retval.Add(new gdcm.Tag(0x0008, 0x1080));
            retval.Add(new gdcm.Tag(0x0008, 0x1090));
            retval.Add(new gdcm.Tag(0x0010, 0x0010));
            retval.Add(new gdcm.Tag(0x0010, 0x0020));
            retval.Add(new gdcm.Tag(0x0010, 0x0030));
            retval.Add(new gdcm.Tag(0x0010, 0x0032));
            retval.Add(new gdcm.Tag(0x0010, 0x0040));
            retval.Add(new gdcm.Tag(0x0010, 0x1010));
            retval.Add(new gdcm.Tag(0x0018, 0x0015));
            retval.Add(new gdcm.Tag(0x0018, 0x0084));
            retval.Add(new gdcm.Tag(0x0018, 0x1060));
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
            gdcm.TagSetType retval=new gdcm.TagSetType();
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
        public bool convert_transfer_syntax(System.IO.FileInfo f, System.IO.DirectoryInfo output_location,String newname)
        {
            if (!output_location.Exists) { output_location.Create(); }
            return processdicomfile(f.FullName, output_location.FullName + '\\' + newname);
        }
        gdcm.ImageChangeTransferSyntax cts = new gdcm.ImageChangeTransferSyntax();

        private bool processdicomfile(String inputf, String outputf)
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(outputf);
            if(fi.Exists)
            {
                fi.Delete();
            }

            bool retval = false;
            //Tried resusing these objects but "Read" function was not replacing old values from prior iteration
            gdcm.FileExplicitFilter fef = new gdcm.FileExplicitFilter(); ;
            gdcm.ImageReader ireader = new gdcm.ImageReader();
            
            System.Diagnostics.Debug.WriteLine(inputf + " is a DICOM file.");
            ireader.SetFileName(inputf);
            if (ireader.CanRead())
            {
                //gdcm.Reader reader = new gdcm.Reader();
                gdcm.ImageWriter imagewriter = new gdcm.ImageWriter();
                //gdcm.Writer writer = new gdcm.Writer();

                System.Diagnostics.Debug.WriteLine(inputf + " is also an image file. Converting.");
                System.Diagnostics.Debug.WriteLine("IRead success=" + ireader.Read());

                /*
                reader.SetFileName(inputf);
                if(!reader.Read())
                {
                    System.Diagnostics.Debug.WriteLine("Couldn't read DICOM file.");
                    return false;
                }
                */

                gdcm.Anonymizer anon=new gdcm.Anonymizer();
                anon.SetFile(ireader.GetFile());
                //anon.RemoveGroupLength();
                anon.RemovePrivateTags();
                anon.RemoveRetired();

                //anon.GetFile().GetHeader().SetDataSetTransferSyntax(targetsyntax);
                //System.Diagnostics.Debug.WriteLine(anon.GetFile().GetDataSet().toString());

                //Don't just copy if transfer syntax is right. Bogus tags need to be removed.
                //if (!(image.GetTransferSyntax().GetString() == targetsyntax.GetString()))
                //{
                ireader.SetFile(anon.GetFile());
                //cts.SetTransferSyntax(ireader.GetImage().GetTransferSyntax());
                
                try
                {
                    if (ireader.GetImage().GetTransferSyntax().IsImplicit() && targetsyntax.IsExplicit())
                    {
                        fef.SetFile(ireader.GetFile());
                        if (!fef.Change())
                        {
                            MessageBox.Show("Couldn't change to explicit TS");
                            return false;
                        }
                    }

                    cts.SetTransferSyntax(targetsyntax);
                    cts.SetInput(ireader.GetImage());
                    cts.SetForce(true);

                    //switch(true)
                    switch (cts.Change())
                    {
                        case true:

                            imagewriter.SetImage(cts.GetOutput());
                            imagewriter.SetFile(ireader.GetFile());
                            imagewriter.SetFileName(outputf);
                            
                            switch(imagewriter.Write())
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
                            System.Diagnostics.Debug.WriteLine("Transfer syntax conversion failed.");
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
            return retval;
        }

        public void Dispose()
        {
            targetsyntax.Dispose();
            cts.Dispose();
            
        }

    }
}
