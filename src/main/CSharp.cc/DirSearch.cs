using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharp.cc
{
    public class DirSearch
    {

        public delegate void CompleteHandler(object source, object eventArgument);
        public event CompleteHandler completed;
        public delegate void FoundHandler(object source, object eventArgument);
        public event FoundHandler found;
        public int MaxDepth = 0;
        public List<string> lstFilesFound = new List<string>();
        // protected List<string> extensions = new List<string>();

        public DirSearch() { }

        public void StartDirSearch(string startingDir, string match, int depth = 0)
        {
            if (depth > MaxDepth)
            {
                return;
            }
            string ext;

            try
            {
                foreach (string f in Directory.GetFiles(startingDir))
                {
                    ext = Path.GetExtension(f);

                    if (ext.ToLower().Equals(match))
                    {
                        lstFilesFound.Add(f);
                        if (found != null)
                        {
                            found(this, f);
                        }
                    }
                }
            }
            catch { }


            if (depth <= MaxDepth)
            {
                foreach (string d in Directory.GetDirectories(startingDir))
                {
                    StartDirSearch(d, match, depth + 1);

                }
            }

            if (depth == 0)
            {
                completed(this, null);
            }
        }
    }
}
