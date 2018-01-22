using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VhdDirectorApp.BcdDirector
{
    public partial class BcdOsSummaryView : UserControl
    {
        private BcdOsDetailModel model;

        public BcdOsSummaryView()
        {
            InitializeComponent();
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

        public BcdOsSummaryView(BcdOsDetailModel _model)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.model = _model;
            this.labelTitle.Text = model.Get("BcdLibraryString_Description");
            this.labelSubTitle.Text = model.Get("BcdOSLoaderString_SystemRoot");
        }

        public override string ToString()
        {
            return this.labelTitle.Text;
        }


       // public Dictionary
    }
}
