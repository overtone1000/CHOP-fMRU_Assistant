using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    public class TSConvert:IDisposable
    {
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
                reader.Read();
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
