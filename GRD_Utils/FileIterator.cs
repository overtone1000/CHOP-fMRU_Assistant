using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRD_Utils
{
    public class FileIterator:IEnumerator<System.IO.FileInfo>
    {
        public FileIterator(String directory)
        {
            this.initialize(new System.IO.DirectoryInfo(directory));
        }
        public FileIterator(System.IO.DirectoryInfo directory)
        {
            this.initialize(directory);
        }
        public void reset()
        {
            this.initialize(basedir);
        }
        System.IO.DirectoryInfo basedir;
        System.Collections.Generic.Queue<System.IO.FileInfo> files=new System.Collections.Generic.Queue<System.IO.FileInfo>();
        System.IO.FileInfo currentfile;
        private void initialize(System.IO.DirectoryInfo directory){
            if (directory.Exists)
            {
                iteratedirectories(directory);
            }
            else
            {
                files.Clear();
            }
            currentfile = null;
            totalfilecount = files.Count;
        }
        private void iteratedirectories(System.IO.DirectoryInfo d)
        {
            basedir = d;
            IEnumerator<System.IO.DirectoryInfo> en_dir = d.EnumerateDirectories().GetEnumerator();
            while (en_dir.MoveNext())
            {
                iteratedirectories(en_dir.Current); //Call recursively to get through the directory tree
            }
            iteratefiles(d); //And go through the files in each directory before leaving it
        }

        private void iteratefiles(System.IO.DirectoryInfo d)
        {
            IEnumerator<System.IO.FileInfo> en_file = d.EnumerateFiles().GetEnumerator();
            while (en_file.MoveNext())
            {
                files.Enqueue(en_file.Current);
            }
        }

        public System.IO.FileInfo Current
        {
            get { return currentfile; }
        }

        public void Dispose()
        {
            files = null;
            files = null;
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this.Current; }
        }

        private int totalfilecount = 0;
        public int totalfiles()
        {
            return totalfilecount;
        }
        public int filesleft()
        {
            return files.Count;
        }

        public bool MoveNext()
        {
            if (files.Count > 0) { 
                currentfile = files.Dequeue(); 
                return true;
            }
            return false;
        }

        public void Reset()
        {
            initialize(basedir);
        }
    }
}
