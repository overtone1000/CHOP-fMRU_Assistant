using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    public static class DataElementInterpreter
    {
        static private gdcm.VR vr;
        static private gdcm.VL vl;
        static private gdcm.ByteValue bv;
        public static object interpretDE(gdcm.DataElement de)
        {
            object retval = null;
           // vr = new gdcm.VR(gdcm.VR.VRType.TM);
            vr = de.GetVR();
            vl = de.GetVL();
            bv = de.GetByteValue();

            uint len = vl.GetValueLength();
            if (len > 0)
            {
                byte[] b = new byte[len];
                bv.GetBuffer(b, len);
                switch (vr.toString()) //raw data is a character array 2 bytes, so this is most efficient
                {
                    case "??":
                    case "UI":
                    case "TM":
                    case "DS": //decimal string
                    case "SH":
                    case "LO":
                    case "IS": //integer string
                    case "PN": //person name
                        retval = (String)System.Text.Encoding.Default.GetString(b);
                        break;
                    case "US":
                        retval = BitConverter.ToUInt16(b,0);
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Unknown Data Element: " + vr.toString());
                        break;
                }
            }

            vr.Dispose();
            vl.Dispose();

            return retval;
        }
    }
}
