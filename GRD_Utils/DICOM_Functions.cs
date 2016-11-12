using System;
using System.Windows.Media.Imaging;
using gdcm;
using System.Windows.Forms;

namespace GRD_Utils
{
    public static class DICOM_Functions
    {
        private static gdcm.ImageChangeTransferSyntax cts = new gdcm.ImageChangeTransferSyntax();        
        private static void setdataelement(ref DataElement de, String value)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(value);
            VL len = new VL((uint)bytes.Length);
            de.SetByteValue(bytes, len);
        }
        public static void Convert_to_DICOM(String inputf, string outputf, string metadataf, bool anonymize, String StudyUID, String SeriesUID, int numberofimages, int imagenumber) 
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(inputf);
            System.IO.FileStream fs = fi.OpenRead();
            JpegBitmapDecoder decoder = new JpegBitmapDecoder(fs, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            BitmapSource bitmapSource = decoder.Frames[0];
            fs.Close();

            ImageReader r = new ImageReader();
            gdcm.Image image = r.GetImage();
            image.SetNumberOfDimensions(2);
            DataElement pixeldata = new DataElement(GRD_Utils.Tags.tag_pixeldata);
            string file1 = inputf;

            System.IO.FileStream infile =
                new System.IO.FileStream(file1, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //uint fsize = gdcm.PosixEmulation.FileSize(file1);

            //byte[] jstream = new byte[fsize];
            //infile.Read(jstream, 0, jstream.Length);

            byte[] jstream = System.IO.File.ReadAllBytes(file1);
            uint fsize = (uint)jstream.Length;

            infile.Close();

            SmartPtrFrag sq = SequenceOfFragments.New();
            Fragment frag = new Fragment();
            frag.SetByteValue(jstream, new gdcm.VL((uint)jstream.Length));
            sq.AddFragment(frag);
            pixeldata.SetValue(sq.__ref__());

            // insert:
            image.SetDataElement(pixeldata);


            //PhotometricInterpretation pi = new PhotometricInterpretation(PhotometricInterpretation.PIType.MONOCHROME2); //1
            //PhotometricInterpretation pi = new PhotometricInterpretation(PhotometricInterpretation.PIType.RGB); //2
            PhotometricInterpretation pi = new PhotometricInterpretation(PhotometricInterpretation.PIType.YBR_FULL);
            image.SetPhotometricInterpretation(pi);

            // FIXME hardcoded:
            PixelFormat pixeltype = new PixelFormat(PixelFormat.ScalarType.UINT8); //1,2
            //pixeltype.SetSamplesPerPixel(3); //2
            pixeltype.SetSamplesPerPixel(3);
            image.SetPixelFormat(pixeltype);

            //TransferSyntax ts = new TransferSyntax(TransferSyntax.TSType.JPEGBaselineProcess1); //1
            //TransferSyntax ts = new TransferSyntax(TransferSyntax.TSType.JPEGLosslessProcess14_1); //2
            //TransferSyntax ts = new TransferSyntax(TransferSyntax.TSType.ExplicitVRLittleEndian);
            TransferSyntax ts = new TransferSyntax(TransferSyntax.TSType.JPEGLosslessProcess14_1);
            image.SetTransferSyntax(ts);

            image.SetDimension(0, (uint)bitmapSource.PixelWidth);
            image.SetDimension(1, (uint)bitmapSource.PixelHeight);

            Reader reader = new Reader();
            reader.SetFileName(metadataf);
            reader.Read();

            ImageWriter writer = new ImageWriter();
            File writerfile = writer.GetFile();
            writerfile.SetDataSet(reader.GetFile().GetDataSet());
            
            TransferSyntax targetsyntax = new TransferSyntax(TransferSyntax.TSType.ExplicitVRLittleEndian);
            
            cts.SetTransferSyntax(targetsyntax);
            cts.SetInput(image);
            
            if (cts.Change())
            {
                System.Diagnostics.Debug.WriteLine("Transfer syntax change successful.");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Transfer syntax change failed.");
            }



            FileDerivation fd = new FileDerivation();
            fd.SetFile(writerfile);
            if (true)
            {
                // Step 2: DERIVED object
                
                // For the pupose of this execise we will pretend that this image is referencing
                // two source image (we need to generate fake UID for that).
                string ReferencedSOPClassUID = "1.2.840.10008.5.1.4.1.1.7"; // Secondary Capture
                fd.AddReference(ReferencedSOPClassUID, reader.GetFile().GetDataSet().GetDataElement(GRD_Utils.Tags.tag_SOPInstanceUID).toString());

                // Again for the purpose of the exercise we will pretend that the image is a
                // multiplanar reformat (MPR):
                // CID 7202 Source Image Purposes of Reference
                // {"DCM",121322,"Source image for image processing operation"},
                fd.SetPurposeOfReferenceCodeSequenceCodeValue(121322);
                // CID 7203 Image Derivation
                // { "DCM",113072,"Multiplanar reformatting" },
                fd.SetDerivationCodeSequenceCodeValue(113072);
                
                // If all Code Value are ok the filter will execute properly
                if (!fd.Derive())
                {
                    System.Diagnostics.Debug.WriteLine("Derive failed.");
                }
            }

            if (anonymize)
            {
                System.Diagnostics.Debug.WriteLine("Anonymizing.");
                GRD_Utils.Anonymizer.anonymize(writerfile, "CHOPfMRUTest", StudyUID); //This can't be done on the final File object instance before the dataset object instance manipulation. Just do it on the input metadata file first, and this works.
            }

            gdcm.DataSet ds = writerfile.GetDataSet();

            string SOPinstanceUID = new gdcm.UIDGenerator().Generate();
            //ds.Replace(metadatafile.GetDataSet().GetDataElement(GRD_Utils.Tags.tag_patientMRN));
            //ds.Replace(metadatafile.GetDataSet().GetDataElement(GRD_Utils.Tags.tag_patientname));
            //ds.Replace(metadatafile.GetDataSet().GetDataElement(GRD_Utils.Tags.tag_studyaccessionnumber));
            //ds.Replace(metadatafile.GetDataSet().GetDataElement(GRD_Utils.Tags.tag_studyDate));
            //ds.Replace(metadatafile.GetDataSet().GetDataElement(GRD_Utils.Tags.tag_studyTime));
            //ds.Replace(metadatafile.GetDataSet().GetDataElement(GRD_Utils.Tags.tag_studyInstanceUID));
            DataElement seriesInstanceUID = new DataElement(GRD_Utils.Tags.tag_seriesInstanceUID);
            DataElement mediaSOPClassUID = new DataElement(GRD_Utils.Tags.tag_mediastorageSOPClassUID);
            DataElement mediaSOPInstanceUID = new DataElement(GRD_Utils.Tags.tag_mediastorageSOPInstanceUID);
            DataElement SOPClassUID = new DataElement(GRD_Utils.Tags.tag_SOPClassUID);
            DataElement SOPInstanceUID = new DataElement(GRD_Utils.Tags.tag_SOPInstanceUID);
            DataElement seriesDescription = new DataElement(GRD_Utils.Tags.tag_seriesDescription);
            setdataelement(ref seriesInstanceUID, SeriesUID);
            String UIDClass = "1.2.840.10008.5.1.4.1.1.7"; //Secondary Capture Image Storage
            String UIDInstance = new gdcm.UIDGenerator().Generate();
            setdataelement(ref mediaSOPClassUID, UIDClass);
            setdataelement(ref mediaSOPInstanceUID, UIDInstance);
            setdataelement(ref SOPClassUID, UIDClass);
            setdataelement(ref SOPInstanceUID, UIDInstance);

            String date = DateTime.Now.ToString("yyyy-MM-dd");
            String time = DateTime.Now.ToString("HH:mm");
            setdataelement(ref seriesDescription, "fMRU Results " + date + " " + time);
            //if (ds.FindDataElement(GRD_Utils.Tags.tag_SOPClassUID)) { ds.Remove(GRD_Utils.Tags.tag_SOPClassUID); }
            if (ds.FindDataElement(GRD_Utils.Tags.tag_SOPInstanceUID)) { ds.Remove(GRD_Utils.Tags.tag_SOPInstanceUID); }
            //ds.Insert(SOPClassUID);
            //ds.Insert(SOPInstanceUID); //gdcm examples say imagewriter will generate one.
            ds.Replace(SOPClassUID);
            ds.Replace(seriesInstanceUID);
            ds.Replace(mediaSOPClassUID);
            ds.Replace(mediaSOPInstanceUID);

            ds.Replace(seriesDescription);


            date = DateTime.Now.ToString("yyyyMMdd");
            time = DateTime.Now.ToString("HHmmss");
            DataElement seriesdate = new DataElement(GRD_Utils.Tags.tag_seriesDate);
            setdataelement(ref seriesdate, date);
            ds.Replace(seriesdate);
            DataElement seriestime = new DataElement(GRD_Utils.Tags.tag_seriesTime);
            setdataelement(ref seriestime, time);
            ds.Replace(seriestime);
            DataElement acquisitiondate = new DataElement(GRD_Utils.Tags.tag_acquisitionDate);
            setdataelement(ref acquisitiondate, date);
            ds.Replace(acquisitiondate);
            DataElement acquisitiontime = new DataElement(GRD_Utils.Tags.tag_acquisitiontime);
            setdataelement(ref acquisitiontime, time);
            ds.Replace(acquisitiontime);
            DataElement contentDate = new DataElement(GRD_Utils.Tags.tag_contentDate);
            setdataelement(ref contentDate, date);
            ds.Replace(contentDate);

            DataElement seriesNumber = new DataElement(GRD_Utils.Tags.tag_seriesNumber);
            setdataelement(ref seriesNumber, "6001");
            ds.Replace(seriesNumber);
            DataElement totalimages = new DataElement(GRD_Utils.Tags.tag_seriesnumberofrelatedinstances);
            setdataelement(ref totalimages, numberofimages.ToString());
            ds.Replace(totalimages);
            DataElement imageNumber = new DataElement(GRD_Utils.Tags.tag_instanceNumber);
            setdataelement(ref imageNumber, imagenumber.ToString());
            ds.Replace(imageNumber);

            FileExplicitFilter fef = new FileExplicitFilter();
            if (targetsyntax.IsExplicit())
            {
                fef.SetFile(writerfile);
                if (!fef.Change())
                {
                    MessageBox.Show("Couldn't change to explicit TS");
                    return;
                }

            }

            System.Diagnostics.Debug.WriteLine(writerfile.GetDataSet().toString());

            writer.SetImage(cts.GetOutput());
            writer.SetFileName(outputf);
            bool ret = writer.Write();
            
            if (ret)
            {
                System.Diagnostics.Debug.WriteLine("Successful write @ " + DateTime.Now.TimeOfDay.ToString());
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Failed write.");
            }

            writerfile.Dispose();
            writer.Dispose();
            reader.Dispose();
            r.Dispose();
            ds.Dispose();
            infile.Dispose();
            fs.Dispose();
            image.Dispose();
            fef.Dispose();
        }
    }
}
