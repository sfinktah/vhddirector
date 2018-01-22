using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp.BcdDirector
{
    public class BcdOsDetailModel
    {
        public Dictionary<string, string> Contents = new Dictionary<string, string>();
        internal void Add(string p, string niceEnum)
        {
            System.Console.WriteLine("Add: {0}: {1}", p, niceEnum);
            Contents.Add(p, niceEnum);
        }
        public string Get(string item)
        {
            String value;
            if (Contents.TryGetValue(item, out value))
            {
                return value;
            }

            return "Empty";
        }
    }

    //BcdLibraryString_ApplicationPath: \Windows\system32\winload.exe
    //BcdLibraryString_Description: Windows Server 2008 R2
    //BcdLibraryString_PreferredLocale: en-US
    //BcdLibraryObjectList_InheritedObjects: {bootloadersettings}
    //BcdLibraryObjectList_RecoverySequence: {3a8b7414-72d2-11e0-a9fa-f1b50c8caab1}
    //BcdLibraryBoolean_AutoRecoveryEnabled: True
    //BcdOSLoaderDevice_OSDevice: System.Management.ManagementBaseObject
    //BcdOSLoaderString_SystemRoot: \Windows
    //BcdOSLoaderObject_AssociatedResumeObject: {3a8b7412-72d2-11e0-a9fa-f1b50c8caab1}
    //BcdOSLoaderInteger_NxPolicy: 1
}
