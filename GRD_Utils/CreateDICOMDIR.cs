using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    public class CreateDICOMDIR:IDisposable
    {
        private gdcm.DICOMDIRGenerator gen = new gdcm.DICOMDIRGenerator();
        private gdcm.FilenamesType fnt = new gdcm.FilenamesType();
        public void Create(String directory)
        {
            //Creates the DICOMDIR that will contain all files added with AddFile
            gen.SetRootDirectory(directory);
            gen.SetFilenames(fnt);
            if (gen.Generate()){
                System.Diagnostics.Debug.WriteLine("DICOMDIR Generation succeeded");
            }
            else{
                System.Diagnostics.Debug.WriteLine("DICOMDIR Generation failed");
            }
        }

        public void AddFile(System.IO.FileInfo fi)
        {
            fnt.Add(fi.FullName);
        }



        public void Dispose()
        {
            gen.Dispose();
            fnt.Dispose();
        }
    }
}
