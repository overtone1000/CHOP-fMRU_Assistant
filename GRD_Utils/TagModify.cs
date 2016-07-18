using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    class TagModify:IDisposable
    {
        gdcm.VR vr;
        gdcm.VL vl;
        gdcm.ByteValue bv;
        
        public TagModify()
        {
            
        }        
        
        public void query(System.IO.FileInfo file)
        {
            gdcm.Tag t1 = new gdcm.Tag();
            gdcm.Tag t2 = new gdcm.Tag();
            gdcm.TagSetType tst = new gdcm.TagSetType();

            //Slice location, DS
            t1.SetGroup((ushort)0X0020);
            t1.SetElement((ushort)0X1041);

            //Aquisition time, TM //hmm...series time is 0008,0031...
            t2.SetGroup((ushort)0X0008);
            t2.SetElement((ushort)0X0032);

            tst.Add(t1);
            tst.Add(t2);

            gdcm.Reader reader = new gdcm.Reader();
            reader.SetFileName(file.FullName);

            if (reader.CanRead())
            {
                reader.ReadSelectedTags(tst);
                //reader.Read();
                gdcm.File f;
                f = reader.GetFile();
                gdcm.DataSet ds = f.GetDataSet();

                interpretDE(ds.GetDataElement(t1));
                interpretDE(ds.GetDataElement(t2));
            }
            reader.Dispose();
            tst.Dispose();
            t1.Dispose();
            t2.Dispose();
        }

        
        private object interpretDE(gdcm.DataElement de){
            object retval=null;
            vr = new gdcm.VR(gdcm.VR.VRType.TM);
            vl=de.GetVL();
            bv = de.GetByteValue();
                                
            uint len=vl.GetValueLength();
            if (len > 0) { 
                byte[] b=new byte[len];
                bv.GetBuffer(b,len);
                switch (vr.toString()) //raw data is a character array 2 bytes, so this is most efficient
                {
                    case "TM":
                    case "DS":
                        retval= (String)System.Text.Encoding.Default.GetString(b);
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Unknown Data Element");
                        break;
                }
            }
            return retval;
        }

        public void Dispose()
        {
            vl.Dispose();
            vr.Dispose();
            bv.Dispose();
        }
    }
}
