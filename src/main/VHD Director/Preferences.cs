using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    static public class App
    {
        static public Preferences Prefs;
        static App()
        {
            Prefs = new Preferences();
        }
    }

    public class Preferences
    {
        protected object plist;
        protected string filename = string.Empty;
 
        public Preferences()
        {
            Read();
        }

        public Preferences(string filename)
        {
            this.filename = filename;
        }

        public static string GetAppConfigurationFile()
        {
            // Get the application path needed to obtain
            // the application configuration file.
            string applicationName =
                Environment.GetCommandLineArgs()[0];

            if (!applicationName.Contains(".exe"))
                applicationName =
                  String.Concat(applicationName, ".exe");


            string exePath = System.IO.Path.Combine(
                Environment.CurrentDirectory, applicationName.Replace(".exe", ".plist"));

            return exePath;
        }


        protected void Read()
        {
            if (string.IsNullOrEmpty(filename))
            {
                filename = GetAppConfigurationFile();
            }
            try
            {
                plist = PList.Read(filename);
            }
            catch (System.IO.FileNotFoundException)
            {
                plist = new Hashtable();
            }
        }

        protected void Write()
        {
            PList.Write(filename, plist);
        }

        public string GetPreference(string path, string defaultValue) {
            string[] pathArray = path.Split(new char[] { '.' }, StringSplitOptions.None);
            if (pathArray.Length == 0) {
                throw new Exception("Invalid or empty preference path");
            }

            object o = plist;
            int count = pathArray.Length;
            for (int i = 0; i < count; i++)
            {
                if (!(o as Hashtable).Contains(pathArray[i]))
                {
                    return defaultValue;
                }

                if (i < count - 1)
                {
                    o = (o as Hashtable)[pathArray[i]];
                }
                else
                {
                    return (string)(o as Hashtable)[pathArray[i]];
                }
            }

            // This point never reached 

            return defaultValue;
                        
        }

        public void SetPreference(string path, string value)
        {
            string[] pathArray = path.Split(new char[] { '.' }, StringSplitOptions.None);
            if (pathArray.Length == 0)
            {
                throw new Exception("Invalid or empty preference path");
            }

            object o = plist;
            int count = pathArray.Length - 1;
            for (int i = 0; i < count; i++)
            {
                Hashtable h = (o as Hashtable);
                string k = pathArray[i];
                if (!h.Contains(k))
                {
                    o = new Hashtable();
                    h.Add(k, o);

                }
                else if (h.Contains(k) && h[k] is Hashtable)
                {
                    o = h[k];
                }
                else
                {
                    throw new Exception("Tried to convert a preference value into a hashtable");
                }
            }

            // If we don't {} this, we get stupid warning about redeclaring locally scoped variables
            {
                Hashtable h = (o as Hashtable);
                string k = pathArray[count];

                if (h.Contains(k))
                {
                    if (h[k] is Hashtable)
                    {
                        throw new Exception("Tried to convert a preference hashtable into a value");
                    }
                    else
                    {
                        h[k] = value;
                    }
                }
                else
                {
                    h.Add(k, value);
                }
            }

            Write();
        }


        public void AddEditForm(string p, Type type)
        {
            SetPreference(p + ".EditForm", type.FullName);
            //string name = type.Name;
            //string fullname = type.FullName;
            //object o = Activator.CreateInstance(type);
            //System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstance(null, fullname);
            //System.Windows.Forms.Form dynamicForm = (System.Windows.Forms.Form)oh.Unwrap();


            //dynamicForm.Show();
        }
    }
}
