using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;


namespace VhdDirectorApp.BcdDirector
{
    public partial class BcdList : Form
    {
        public BcdList()
        {
            InitializeComponent();
        }
        // http://msdn.microsoft.com/en-us/library/windows/desktop/dd405463%28v=vs.85%29.aspx

        private void BcdList_Load(object sender, EventArgs e)
        {
            //USE IMPERSONATE LEVEL

            ConnectionOptions connectionOptions = new ConnectionOptions();
            connectionOptions.Impersonation = ImpersonationLevel.Impersonate;
            connectionOptions.EnablePrivileges = true;

            ManagementScope managementScope = new ManagementScope(@"root\WMI",
                                        connectionOptions);

            // SYSTEM STORE GUID = {9dea862c-5cdd-4e70-acc1-f32b344d4795}

            BcdObject systemObject = new BcdObject(managementScope,
                        "{9dea862c-5cdd-4e70-acc1-f32b344d4795}", "");

            // GET LIST OF OS

            ManagementBaseObject mboOut;
            bool success = systemObject.GetElement((uint)
                BCDConstants.BcdBootMgrElementTypes.BcdBootMgrObjectList_DisplayOrder, out mboOut);

            if (success)
            {
                string[] osIdList = (string[])mboOut.GetPropertyValue("Ids");
                // LOOP FOR ALL OS Ids (GUID)

                foreach (string osGuid in osIdList)
                {
                    BcdObject osObj = new BcdObject(managementScope, osGuid, "");
                    // GET DESCRIPTION STRING

                    ManagementBaseObject[] elements;
                    success = osObj.EnumerateElements(out elements);
                    if (success) {
                        object oid = null;
                        uint typeid = 0;
                        string laststring = string.Empty;
                        object lastvalue = null;

                        List<BcdOsDetailModel> models = new List<BcdOsDetailModel>();
                        foreach (ManagementBaseObject mbo in elements)
                        {

                            // success = osObj.GetElement((uint) BCDConstants.BcdLibraryElementTypes.BcdLibraryString_Description, out mboOut);
                            // if (success)
                            // {
                            // USE A CLASS TO KEEP OS OBJECT AND NAME FOR LATER USE

                            //OS myOS = new OS();
                            //myOS.Name = mboOut.GetPropertyValue("String").ToString();
                            //myOS.GUID = osGuid;
                            //bcdListBox.Items.Add(myOS);
                            BcdOsDetailModel model = new BcdOsDetailModel();

                            foreach (PropertyData prop in mbo.Properties)
                            {
                                // 12000002
                                if (prop.Value == null)
                                    continue;
                                if (prop.Value.ToString().Length > 0)
                                {
                                    //Device: System.Management.ManagementBaseObject
                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //Type: 285212673

                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //String: \Windows\system32\winload.exe
                                    //'VHD Director.vshost.exe' (Managed (v2.0.50727)): Loaded 'C:\Windows\assembly\GAC_MSIL\System.Configuration\2.0.0.0__b03f5f7f11d50a3a\System.Configuration.dll', Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
                                    //Type: 301989890

                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //String: Windows Server 2008 R2
                                    //Type: 301989892

                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //String: en-US
                                    //Type: 301989893

                                    //Ids: System.String[]
                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //Type: 335544326

                                    //Ids: System.String[]
                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //Type: 335544328

                                    //Boolean: True
                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //Type: 369098761

                                    //Device: System.Management.ManagementBaseObject
                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //Type: 553648129

                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //String: \Windows
                                    //Type: 570425346

                                    //Id: {3a8b7412-72d2-11e0-a9fa-f1b50c8caab1}
                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //Type: 587202563

                                    //Integer: 1
                                    //ObjectId: {3a8b7413-72d2-11e0-a9fa-f1b50c8caab1}
                                    //Type: 620757024

                                    // System.Console.WriteLine("{0}: {1}", prop.Name, prop.Value);

                                    if (prop.Name.Equals("ObjectId"))
                                    {
                                        oid = prop.Value;
                                    }
                                    else if (prop.Name.Equals("Type"))
                                    {
                                        typeid = (uint)prop.Value;
                                        if (typeid == 0x12000004 && oid != null)
                                        {
                                            GuidItem gi;
                                            Guid g;

                                            g = new Guid((string)oid);
                                            if (!BcdGuidList.TryGetValue(g, out gi))
                                            {
                                                BcdGuidList.Add(g, laststring, laststring);
                                            }
                                        }
                                            
                                        string niceEnum = Enum.GetName(typeof(BCDConstants.BcdLibraryElementTypes), prop.Value);
                                        if (niceEnum == null || niceEnum.Length < 1)
                                        {
                                            niceEnum = Enum.GetName(typeof(BCDConstants.BcdOSLoaderElementTypes), prop.Value);
                                            if (niceEnum == null || niceEnum.Length < 1)
                                            {

                                                niceEnum = "0x" + ((UInt32)prop.Value).ToString("X");
                                            }
                                        }
                                        model.Add(niceEnum, lastvalue.ToString());
                                        Console.WriteLine("{0}: {1}", niceEnum, lastvalue.ToString());
                                        models.Add(model);
                                        model = new BcdOsDetailModel();

                                        // bcdListBox.Controls.Add(new BcdOsSummaryView(model));

                                        // bcdListBox.Items.Add(String.Format("{0}: {1}\r\n", prop.Name, niceEnum));
                                        // bcdListBox.Items.Add(String.Format("\r\n", prop.Name, prop.Value));

                                    }                                        else
                                    {
                                        lastvalue = prop.Value;
                                        if (prop.Value is Array)
                                        {
                                            foreach (Object o in prop.Value as Array)
                                            {
                                                // bcdListBox.Items.Add(String.Format("{0}: {1}\r\n", prop.Name, TranslateAnyGuids(o)));
                                                // model.Add(prop.Name, TranslateAnyGuids(o).ToString());
                                                lastvalue = TranslateAnyGuids(o).ToString();
                                            }
                                        }
                                        else if (prop.Value is ManagementBaseObject)
                                        {
                                            String quicklist = String.Empty;
                                            foreach (PropertyData prop2 in ((ManagementBaseObject)(prop.Value)).Properties)
                                            {
                                                quicklist += String.Format("{0}={1}, ", prop2.Name, prop2.Value.ToString());
                                            }
                                            lastvalue = quicklist;
                                        }
                                        else
                                        {
                                           // lastvalue = String.Format("({1}) {0}", prop.Value.ToString(), prop.Value.GetType().ToString());
                                            lastvalue = String.Format("{0}", prop.Value.ToString(), prop.Value.GetType().ToString());
                                        }
                                        if (prop.Name.Equals("String"))
                                        {
                                            laststring = prop.Value.ToString();
                                        }
                                        
                                    }
                                }
                            }

                           
                        }

                        //BcdOsDetailView dview = new BcdOsDetailView(models);
                        //dview.Dock = DockStyle.Fill;
                        //dview.AddToPanel(panelDetail);
                        //panelDetail.Controls.Add(dview);

                        bcdOsDetailView1.Models = models;
                    }
                }
            }   
        }

        private object TranslateAnyGuids(object o)
        {
            return o.ToString();
            if (o is string)
            {
                Guid g;
                try
                {
                    g = new Guid((string)o);
                    o = g;
                }
                catch
                {
                }
            }

            if (o is Guid)
            {
                GuidItem gi;
                if (BcdGuidList.TryGetValue((Guid)o, out gi))
                {
                    return gi.ToString();
                }
                return "{" + o.ToString() + "}";
            }

            return o.ToString();
        }
    }
}
