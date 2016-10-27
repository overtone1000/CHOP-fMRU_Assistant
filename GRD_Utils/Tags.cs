using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    public static class Tags
    
    {
        public static gdcm.Tag tag_mediastorageSOPClassUID = new gdcm.Tag(0x2, 0x2);
        public static gdcm.Tag tag_mediastorageSOPInstanceUID = new gdcm.Tag(0x2, 0x3);
        public static gdcm.Tag tag_transfersyntax = new gdcm.Tag(0x2, 0x10);

        public static gdcm.Tag tag_specificcharacterset = new gdcm.Tag(0x8, 0x5);
        public static gdcm.Tag tag_instancecreationdate = new gdcm.Tag(0x8, 0x12);
        public static gdcm.Tag tag_instancecreationtime = new gdcm.Tag(0x8, 0x13);
        public static gdcm.Tag tag_instancecreationUID = new gdcm.Tag(0x8, 0x14);
        public static gdcm.Tag tag_SOPClassUID = new gdcm.Tag(0x8, 0x16);
        public static gdcm.Tag tag_SOPInstanceUID = new gdcm.Tag(0x8, 0x18);
        public static gdcm.Tag tag_studyDate = new gdcm.Tag(0x8, 0x20);
        public static gdcm.Tag tag_seriesDate = new gdcm.Tag(0x8, 0x21);
        public static gdcm.Tag tag_acquisitionDate = new gdcm.Tag(0x8, 0x22);
        public static gdcm.Tag tag_contentDate = new gdcm.Tag(0x8, 0x23);
        public static gdcm.Tag tag_studyTime = new gdcm.Tag(0x8, 0x30);
        public static gdcm.Tag tag_seriesTime= new gdcm.Tag(0x8, 0x31);
        public static gdcm.Tag tag_acquisitiontime = new gdcm.Tag(0x8, 0x32);
        public static gdcm.Tag tag_studyaccessionnumber = new gdcm.Tag(0x8, 0x50);
        public static gdcm.Tag tag_seriesModality = new gdcm.Tag(0x8, 0x60);
        public static gdcm.Tag tag_institutionname = new gdcm.Tag(0x8, 0x80);
        public static gdcm.Tag tag_referringphysician = new gdcm.Tag(0x8, 0x90);
        public static gdcm.Tag tag_studyDescription = new gdcm.Tag(0x8, 0x1030);
        public static gdcm.Tag tag_seriesDescription = new gdcm.Tag(0x8, 0x103E);
        public static gdcm.Tag tag_institutiondepartmentname = new gdcm.Tag(0x8, 0x1040);
        public static gdcm.Tag tag_performingphysician = new gdcm.Tag(0x8, 0x1050);
        public static gdcm.Tag tag_readingphysician = new gdcm.Tag(0x8, 0x1060);

        public static gdcm.Tag tag_patientgrouplength = new gdcm.Tag(0x10, 0x0);
        public static gdcm.Tag tag_patientname = new gdcm.Tag(0x10, 0x10);
        public static gdcm.Tag tag_patientMRN = new gdcm.Tag(0x10, 0x20);
        public static gdcm.Tag tag_patientDOB = new gdcm.Tag(0x10, 0x30);
        public static gdcm.Tag tag_patientsex = new gdcm.Tag(0x10, 0x40);
        public static gdcm.Tag tag_patientinsurancecode = new gdcm.Tag(0x10, 0x50);
        public static gdcm.Tag tag_patientOtherIDs = new gdcm.Tag(0x10, 0x1000);
        public static gdcm.Tag tag_patientOtherNames = new gdcm.Tag(0x10, 0x1001);
        public static gdcm.Tag tag_patientOtherIDSequences = new gdcm.Tag(0x10, 0x1002);
        public static gdcm.Tag tag_patientbirthname = new gdcm.Tag(0x10, 0x1005);
        public static gdcm.Tag tag_patientage = new gdcm.Tag(0x10, 0x1010);
        public static gdcm.Tag tag_patientsize = new gdcm.Tag(0x10, 0x1020);
        public static gdcm.Tag tag_patientweight = new gdcm.Tag(0x10, 0x1030);
        public static gdcm.Tag tag_patientaddress = new gdcm.Tag(0x10, 0x1040);
        public static gdcm.Tag tag_patientinsuranceID = new gdcm.Tag(0x10, 0x1050);
        public static gdcm.Tag tag_patientmotherbirthname = new gdcm.Tag(0x10, 0x1060);
        public static gdcm.Tag tag_patientmilitaryrank = new gdcm.Tag(0x10, 0x1080);
        public static gdcm.Tag tag_patientmedreclocator = new gdcm.Tag(0x10, 0x1090);
        public static gdcm.Tag tag_patienttelephone = new gdcm.Tag(0x10, 0x2154);
        public static gdcm.Tag tag_patientoccupation = new gdcm.Tag(0x10, 0x2180);
        public static gdcm.Tag tag_patientadditionalhistory = new gdcm.Tag(0x10, 0x21B0);
        public static gdcm.Tag tag_patientpregnancystatus = new gdcm.Tag(0x10, 0x21C0);
        
        public static gdcm.Tag tag_patientdataremoved = new gdcm.Tag(0x12, 0x62);

        public static gdcm.Tag tag_operatorname = new gdcm.Tag(0x8, 0x1070);
        public static gdcm.Tag tag_triggertime = new gdcm.Tag(0x18, 0x1060);

        //public static gdcm.Tag tag_seriesImages = new gdcm.Tag(0x20, 0x1001); //this is something else...
        public static gdcm.Tag tag_seriesnumberofrelatedinstances = new gdcm.Tag(0x20, 0x1209); //number of images
        public static gdcm.Tag tag_seriesNumber = new gdcm.Tag(0x20, 0x11);
        public static gdcm.Tag tag_instanceNumber = new gdcm.Tag(0x20, 0x13);
        public static gdcm.Tag tag_seriesInstanceUID = new gdcm.Tag(0x20, 0xE);
        public static gdcm.Tag tag_studyInstanceUID = new gdcm.Tag(0x20, 0xD);
        public static gdcm.Tag tag_sliceposition = new gdcm.Tag(0x20, 0x1041);
        public static gdcm.Tag tag_seriesrelatedinstances = new gdcm.Tag(0x20, 0x1209);

        public static gdcm.Tag tag_requestingphysician = new gdcm.Tag(0x32, 0x1032);
        public static gdcm.Tag tag_requestingservice = new gdcm.Tag(0x32, 0x1033);

        public static gdcm.Tag tag_pixeldata = new gdcm.Tag(0x7fe0, 0x10);
        public static gdcm.Tag tag_imagerows = new gdcm.Tag(0x28, 0x10);
        public static gdcm.Tag tag_imagecols = new gdcm.Tag(0x28, 0x11);
        public static gdcm.Tag tag_windowceneter = new gdcm.Tag(0x28, 0x1050);
        public static gdcm.Tag tag_windowwidth = new gdcm.Tag(0x28, 0x1051);
    }
}