using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    static class Anonymizer
    {
        public static void anonymize(String inputf, String outputf)
        {
            gdcm.Reader r = new gdcm.Reader();
            gdcm.Writer w = new gdcm.Writer();
        
            r.SetFileName(inputf);
            r.Read();
            gdcm.File f = r.GetFile();
            anonymize(f);

            w.SetFile(f);
            w.SetFileName(outputf);
            if (!w.Write()) { System.Diagnostics.Debug.WriteLine("Error writing anonymized file."); }

            r.Dispose();
            w.Dispose();
        }
        public static void anonymize(gdcm.File f)
        {
            anonymize(f, "Anonymized");
        }
        public static void anonymize(gdcm.File f, String anonstring)
        {
            anonymize(f, anonstring, new gdcm.UIDGenerator().Generate());
        }
        public static void anonymize(gdcm.File f, String anonstring, String studyUID)
        {
            gdcm.Anonymizer anon = new gdcm.Anonymizer();

            DateTime now=System.DateTime.Now;
            String date=now.ToString("yyyyMMdd");
            String time=now.ToString("HHmmss");
            String anonGUID = new gdcm.UIDGenerator().Generate();

            anon.SetFile(f);
            anon.Replace(Tags.tag_patientdataremoved, "1");
            anon.Replace(Tags.tag_instancecreationdate, date);
            anon.Replace(Tags.tag_instancecreationtime, time);
            anon.Replace(Tags.tag_instancecreationUID, anonGUID);
            anon.Replace(Tags.tag_studyInstanceUID, studyUID);
            anon.Replace(Tags.tag_studyDate, date);
            anon.Replace(Tags.tag_studyTime, time);
            anon.Replace(Tags.tag_seriesDate, date);
            anon.Replace(Tags.tag_acquisitionDate, date);
            anon.Replace(Tags.tag_contentDate, date);
            anon.Replace(Tags.tag_studyaccessionnumber, "0123456789");
            anon.Replace(Tags.tag_institutionname, anonstring);
            anon.Replace(Tags.tag_referringphysician, anonstring);
            anon.Replace(Tags.tag_institutiondepartmentname, anonstring);
            anon.Replace(Tags.tag_performingphysician, anonstring);
            anon.Replace(Tags.tag_readingphysician, anonstring);
            anon.Replace(Tags.tag_operatorname, anonstring);
            anon.Replace(Tags.tag_patientgrouplength, anonstring);
            anon.Replace(Tags.tag_patientname, anonstring);
            anon.Replace(Tags.tag_patientMRN, "0123456789");
            anon.Replace(Tags.tag_patientDOB, date);
            anon.Replace(Tags.tag_patientsex, "UNKNOWN");
            anon.Replace(Tags.tag_patientinsurancecode, anonstring);
            anon.Replace(Tags.tag_patientOtherIDs,anonstring);
            anon.Replace(Tags.tag_patientOtherNames,anonstring);
            anon.Replace(Tags.tag_patientOtherIDSequences,anonstring);
            anon.Replace(Tags.tag_patientbirthname,anonstring);
            anon.Replace(Tags.tag_patientage,"001Y");
            anon.Replace(Tags.tag_patientsize,"1");
            anon.Replace(Tags.tag_patientweight,"1");
            anon.Replace(Tags.tag_patientaddress,anonstring);
            anon.Replace(Tags.tag_patientinsuranceID, "0123456789");
            anon.Replace(Tags.tag_patientmotherbirthname,anonstring);
            anon.Replace(Tags.tag_patientmilitaryrank,anonstring);
            anon.Replace(Tags.tag_patientmedreclocator,anonstring);
            anon.Replace(Tags.tag_patienttelephone, "0123456789");
            anon.Replace(Tags.tag_patientoccupation,anonstring);
            anon.Replace(Tags.tag_patientadditionalhistory,anonstring);
            anon.Replace(Tags.tag_patientpregnancystatus,anonstring);
            anon.Replace(Tags.tag_requestingphysician,anonstring);
            anon.Replace(Tags.tag_requestingservice,anonstring);
            
            anon.RemovePrivateTags();
            anon.RemoveGroupLength();
            anon.RemoveRetired();

            anon.Dispose();
        }
    }
}
