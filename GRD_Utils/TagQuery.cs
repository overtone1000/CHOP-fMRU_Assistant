using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    class TagQuery:IDisposable
    {
        gdcm.VR vr;
        gdcm.VL vl;
        gdcm.ByteValue bv;
        
        public TagQuery()
        {
            
        }

        public String queryseriesname(System.IO.FileInfo file)
        {
            gdcm.Tag t = new gdcm.Tag();
            gdcm.TagSetType tst = new gdcm.TagSetType();
            t.SetGroup((ushort)0X0008);
            t.SetElement((ushort)0X103E);

            tst.Add(t);

            String retval="";

            //Series Description
            

            gdcm.Reader reader = new gdcm.Reader();
            reader.SetFileName(file.FullName);

            if (reader.CanRead())
            {
                reader.ReadSelectedTags(tst);
                //reader.Read();
                gdcm.File f;
                f = reader.GetFile();
                gdcm.DataSet ds = f.GetDataSet();

                retval=(String)DataElementInterpreter.interpretDE(ds.GetDataElement(t));
            }
            reader.Dispose();
            tst.Dispose();
            t.Dispose();
            return retval;
        }
        
        
        public void query(System.IO.FileInfo file)
        {
            gdcm.Tag t1 = new gdcm.Tag();
            gdcm.Tag t2 = new gdcm.Tag();
            gdcm.TagSetType tst = new gdcm.TagSetType();

            t1.SetGroup((ushort)0X0020);
            t1.SetElement((ushort)0X1041);

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

                DataElementInterpreter.interpretDE(ds.GetDataElement(t1));
                DataElementInterpreter.interpretDE(ds.GetDataElement(t2));
            }
            reader.Dispose();
            tst.Dispose();
            t1.Dispose();
            t2.Dispose();
        }

        
        public void Dispose()
        {
            if (vl != null) { vl.Dispose(); }
            if (vr != null) { vr.Dispose();}
            if (bv != null) { bv.Dispose(); }
        }
    }
}
