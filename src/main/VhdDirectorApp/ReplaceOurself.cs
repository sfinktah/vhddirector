using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace VhdDirectorApp
{
    // http://stackoverflow.com/questions/616584/how-do-i-get-the-name-of-the-current-executable-in-c
    // http://themech.net/2008/05/adding-check-for-update-option-in-csharp/
    // http://themech.net/2008/09/check-for-updates-how-to-download-and-install-a-new-version-of-your-csharp-application/
    // http://stackoverflow.com/questions/2335755/how-to-update-an-assembly-for-a-running-c-sharp-process-aka-hot-deploy/8775175#8775175

    class Ourself
    {
        public static string FileName() {
            Assembly _objParentAssembly;

            if (Assembly.GetEntryAssembly() == null)
                _objParentAssembly = Assembly.GetCallingAssembly();
            else
                _objParentAssembly = Assembly.GetEntryAssembly();

            if (_objParentAssembly.CodeBase.StartsWith("http://"))
                throw new IOException("Deployed from URL");

            if (File.Exists(_objParentAssembly.Location))
                return _objParentAssembly.Location;
            if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName))
                return System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
            if (File.Exists(Assembly.GetExecutingAssembly().Location))
                return Assembly.GetExecutingAssembly().Location;

            throw new IOException("Assembly not found");
        }

        public static bool Rename()
        {
            string currentName = FileName();
            string newName = FileName() + ".ori";
            if (File.Exists(newName))
            {
                File.Delete(newName);
            }
            File.Move(currentName, newName);
            return true;
        }

        public static bool ReLaunch()
        {
            System.Diagnostics.Process.Start(FileName());
            return true;
        }
    }
}
