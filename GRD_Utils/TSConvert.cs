using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    public class TSConvert:IDisposable
    {
        private static gdcm.TagSetType selected_tags()
        {
            gdcm.TagSetType retval=new gdcm.TagSetType();
            retval.Add(new gdcm.Tag(0x0002, 0x0001));
            retval.Add(new gdcm.Tag(0x0002, 0x0002));
            retval.Add(new gdcm.Tag(0x0002, 0x0003));
            retval.Add(new gdcm.Tag(0x0002, 0x0010));
            retval.Add(new gdcm.Tag(0x0002, 0x0012));
            retval.Add(new gdcm.Tag(0x0002, 0x0013));
            retval.Add(new gdcm.Tag(0x0008, 0x0005));
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
            retval.Add(new gdcm.Tag(0x0008, 0x0081));
            retval.Add(new gdcm.Tag(0x0008, 0x1030));
            retval.Add(new gdcm.Tag(0x0008, 0x103E));
            retval.Add(new gdcm.Tag(0x0010, 0x0010));
            retval.Add(new gdcm.Tag(0x0010, 0x0020));
            retval.Add(new gdcm.Tag(0x0018, 0x1060));
            retval.Add(new gdcm.Tag(0x0020, 0x000D));
            retval.Add(new gdcm.Tag(0x0020, 0x000E));
            retval.Add(new gdcm.Tag(0x0020, 0x0010));
            retval.Add(new gdcm.Tag(0x0020, 0x0011));
            retval.Add(new gdcm.Tag(0x0020, 0x0012));
            retval.Add(new gdcm.Tag(0x0020, 0x0013));
            retval.Add(new gdcm.Tag(0x0020, 0x0032));
            retval.Add(new gdcm.Tag(0x0020, 0x0037));
            retval.Add(new gdcm.Tag(0x0020, 0x0052));
            retval.Add(new gdcm.Tag(0x0020, 0x1041));
            retval.Add(new gdcm.Tag(0x0028, 0x0002));
            retval.Add(new gdcm.Tag(0x0028, 0x0010));
            retval.Add(new gdcm.Tag(0x0028, 0x0011));
            retval.Add(new gdcm.Tag(0x0028, 0x0030));
            retval.Add(new gdcm.Tag(0x0028, 0x0100));
            retval.Add(new gdcm.Tag(0x0028, 0x0101));
            retval.Add(new gdcm.Tag(0x0028, 0x0102));
            retval.Add(new gdcm.Tag(0x0028, 0x0103));
            retval.Add(new gdcm.Tag(0x0028, 0x1050));
            retval.Add(new gdcm.Tag(0x0028, 0x1051));
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
            bool retval = false;
            //Tried resusing these objects but "Read" function was not replacing old values from prior iteration
            gdcm.Reader reader = new gdcm.Reader();
            gdcm.ImageReader ireader = new gdcm.ImageReader();
            gdcm.ImageWriter iwriter = new gdcm.ImageWriter();
            gdcm.Image image;

            bool convert=false;

            System.Diagnostics.Debug.WriteLine(inputf + " is a DICOM file.");
            ireader.SetFileName(inputf);
            if (ireader.CanRead()) { 
                convert = true;
                System.Diagnostics.Debug.WriteLine(inputf + " is also an image file. Converting.");
                System.Diagnostics.Debug.WriteLine("IRead success=" + ireader.Read());
                reader.SetFileName(inputf);
                reader.ReadSelectedTags(selected_tags(), true);
            }
            else { 
                System.Diagnostics.Debug.WriteLine(inputf + " is not an image file."); 
            }

            if (convert)
            {
                image = ireader.GetImage();
                if (!(image.GetTransferSyntax().GetString() == targetsyntax.GetString()))
                {
                    cts.SetTransferSyntax(targetsyntax);
                    cts.SetInput(image);
                    try
                    {
                        switch (cts.Change())
                        {
                            case true:
                                iwriter.SetCheckFileMetaInformation(true);
                                iwriter.SetFile(reader.GetFile());
                                iwriter.SetImage(cts.GetOutput());
                                iwriter.SetFileName(outputf);
                                
                                switch (iwriter.Write())
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
                    catch(Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("Error.");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Transfer syntax is already correct. Just copying.");
                    System.IO.FileInfo inf = new System.IO.FileInfo(inputf);
                    inf.CopyTo(outputf, true);
                    retval = true;
                }

                reader.Dispose();
                ireader.Dispose();
                iwriter.Dispose();

            }
            return retval;
        }

        public void Dispose()
        {
            targetsyntax.Dispose();
            cts.Dispose();
            
        }

    }
}
