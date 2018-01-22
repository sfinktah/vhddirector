using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharp.cc
{
    public class Files
    {
        static public bool DoubleCheckIfFileIsBeingUsed(string fileName)
        {
            return CheckIfFileIsBeingUsed(fileName) && CheckIfFileIsBeingUsed(fileName);
        }

        static public bool CheckIfFileIsBeingUsed(string fileName)
        {

            try
            {
                using (FileStream fs = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    fs.Close();
                }
            }

            catch (IOException ex)
            {
                LastErrorMessage = ex.Message;
                return true;
            }

            return false;
        }

        public static string LastErrorMessage { get; set; }
    }
}
