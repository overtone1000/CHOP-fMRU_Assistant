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
        public static ExpectedType interpretDE<ExpectedType>(gdcm.DataElement de)
        {
            ExpectedType retval = default(ExpectedType);
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
                        if (typeof(ExpectedType) == typeof(String) || typeof(ExpectedType) == typeof(string))
                        {
                            retval = (ExpectedType)(object)System.Text.Encoding.Default.GetString(b);
                        }
                        else if (typeof(ExpectedType) == typeof(ushort))
                        {
                            retval = (ExpectedType)(object)BitConverter.ToUInt16(b, 0);
                        }
                        else
                        {
                            retval = (ExpectedType)(object)b;
                        }
                        break;
                    case "TM":
                    case "DS": //decimal string
                    case "SH":
                    case "LO":
                    case "IS": //integer string
                    case "PN": //person name
                        retval = (ExpectedType)(object)System.Text.Encoding.Default.GetString(b);
                        break;
                    case "US":
                        retval = (ExpectedType)(object)BitConverter.ToUInt16(b,0);
                        break;
                    case "UI": //can have a trailing null character
                        retval= (ExpectedType)(object)System.Text.Encoding.Default.GetString(b);
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Unexpected Data Element: " + vr.toString());
                        break;
                }
            }

            vr.Dispose();
            vl.Dispose();

            return retval;
        }
    }
}
