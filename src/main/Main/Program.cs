using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Remoting;

namespace Main
{
    static class Program
    {
        static public System.Windows.Forms.Form PrimaryForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // This is where we use dynamic binding to instantiate our main form, which is stored as an LZMA compressed DLL in an Embedded Resource.
                // We have also removed it from the list of "References", just because we could. The .NET CLR has never heard of our target form, or it's namespace.
            
                ObjectHandle objHandle = currentDomain.CreateInstance("VhdDirectorApp", "VhdDirectorApp.DiskUsageForm");  // Activator.CreateInstance(null, fullname);
                
                // If we use static binding, it will attempt to load the DLL before our AssemblyResolver is installed
                // VhdDirectorApp.DiskUsageForm form = (VhdDirectorApp.DiskUsageForm)objHandle.Unwrap();

                // So we just do this instead.
                Form form = (Form)objHandle.Unwrap();

                Application.Run(form);

            }
            catch (System.MissingMethodException mex)
            {
                //new Error(mex);
                MessageBox.Show(mex.Message, "DLL is missing method");
            }
            catch (System.DllNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Missing DLL");
            }

            // AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            // MessageBox.Show(string.Join("\r\n", Assembly.GetExecutingAssembly().GetManifestResourceNames()), "Available Resources");
            // Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            // AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            
        }

        private static Assembly MyResolveEventHandler(object sender,ResolveEventArgs args)
            {
                /*
                Assembly MyAssembly, objExecutingAssemblies;
                string strTempAssmbPath = "";
                objExecutingAssemblies = Assembly.GetExecutingAssembly();
                AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

                //Loop through the array of referenced assembly names.
                foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
                {
                    //Check for the assembly names that have raised the "AssemblyResolve" event.
                    if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                    {
                        //Build the path of the assembly from where it has to be loaded.				
                        strTempAssmbPath = "C:\\Myassemblies\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                        break;
                    }

                }

                //Load the assembly from the specified path. 					
                MyAssembly = Assembly.LoadFrom(strTempAssmbPath);

                //Return the loaded assembly.
                return MyAssembly;	
                */

                Console.WriteLine("AssemblyResolve: {0}", args.Name);
                String resourceName; //  = "vhddirector.dll." + new AssemblyName(args.Name).Name + ".dll";
                String assemblyName = new AssemblyName(args.Name).Name;
                MessageBox.Show("Looking for Embedded Assembly " + assemblyName, "Looking for Assembly");

                AppDomain currentDomain = AppDomain.CurrentDomain;
                //Assembly execAssembly = Assembly.GetExecutingAssembly();  // Main
                //Assembly callAssembly = Assembly.GetCallingAssembly();    // mscorlib
                //Assembly entryAssembly = Assembly.GetEntryAssembly();     // Main

            
                string[] resources; //  = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                Assembly[] assemblies = currentDomain.GetAssemblies();
                foreach (Assembly assembly in assemblies)
                {
                    string thisAssemblyName = assembly.GetName().Name;

                    if (!thisAssemblyName.StartsWith("Microsoft.") && !thisAssemblyName.StartsWith("System"))
                    {
                        resources = assembly.GetManifestResourceNames();
                        resourceName = thisAssemblyName + ".Embedded." + assemblyName + ".dll";
                       
                        bool has7z = Array.IndexOf(resources, resourceName + ".7z") > -1;
                        bool has = Array.IndexOf(resources, resourceName) > -1;
                       
                        if (has)
                        {
                            using (var stream = assembly.GetManifestResourceStream(resourceName))
                            {
                                Byte[] assemblyData = new Byte[stream.Length];
                                stream.Read(assemblyData, 0, assemblyData.Length);
                                return Assembly.Load(assemblyData);
                            }
                        }

                        if (has7z)
                        {
                            using (var stream = assembly.GetManifestResourceStream(resourceName + ".7z"))
                            {
                                Byte[] assemblyData = My7Zip.DecompressLzmaStream(stream);
                                return Assembly.Load(assemblyData);
                            }
                        }
                    }

                }

               


                MessageBox.Show("Couldn't find Embedded Resource " + args.Name, "Missing Embedded Resource");
                // MessageBox.Show(string.Join("\r\n", resources), "Available Resources");
                return null;
                 
            }

    }
}

/*

System.IO.FileNotFoundException was unhandled
  Message=Could not load file or assembly 'VHD Director, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
  Source=Main
  FileName=VHD Director, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
  FusionLog==== Pre-bind state information ===
LOG: User = WIN-2008-DEV\Administrator
LOG: DisplayName = VHD Director, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 (Fully-specified)
LOG: Appbase = file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/
LOG: Initial PrivatePath = NULL
Calling assembly : Main, Version=1.0.8.26, Culture=neutral, PublicKeyToken=null.
===
LOG: This bind starts in default load context.
LOG: Using application configuration file: C:\Users\Administrator\Documents\Visual Studio 2010\Projects\VHD Director\Main\bin\Debug\Main.vshost.exe.config
LOG: Using machine configuration file from C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\machine.config.
LOG: Policy not being applied to reference at this time (private, custom, partial, or location-based assembly bind).
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VHD Director.DLL.
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VHD Director/VHD Director.DLL.
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VHD Director.EXE.
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VHD Director/VHD Director.EXE.

  StackTrace:
       at vhddirector.Program.Main()
       at System.AppDomain._nExecuteAssembly(Assembly assembly, String[] args)
       at System.AppDomain.ExecuteAssembly(String assemblyFile, Evidence assemblySecurity, String[] args)
       at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()
       at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
       at System.Threading.ThreadHelper.ThreadStart()
  InnerException: 
*/

/*
 * System.IO.FileNotFoundException was unhandled
  Message=Could not load file or assembly 'VhdDirectorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null' or one of its dependencies. The system cannot find the file specified.
  Source=Main
  FileName=VhdDirectorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
  FusionLog==== Pre-bind state information ===
LOG: User = WIN-2008-DEV\Administrator
LOG: DisplayName = VhdDirectorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
 (Fully-specified)
LOG: Appbase = file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/
LOG: Initial PrivatePath = NULL
Calling assembly : Main, Version=1.0.9.50, Culture=neutral, PublicKeyToken=null.
===
LOG: This bind starts in default load context.
LOG: Using application configuration file: C:\Users\Administrator\Documents\Visual Studio 2010\Projects\VHD Director\Main\bin\Debug\Main.vshost.exe.config
LOG: Using machine configuration file from C:\Windows\Microsoft.NET\Framework\v2.0.50727\config\machine.config.
LOG: Policy not being applied to reference at this time (private, custom, partial, or location-based assembly bind).
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VhdDirectorApp.DLL.
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VhdDirectorApp/VhdDirectorApp.DLL.
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VhdDirectorApp.EXE.
LOG: Attempting download of new URL file:///C:/Users/Administrator/Documents/Visual Studio 2010/Projects/VHD Director/Main/bin/Debug/VhdDirectorApp/VhdDirectorApp.EXE.

  StackTrace:
       at vhddirector.Program.Main()
       at System.AppDomain._nExecuteAssembly(Assembly assembly, String[] args)
       at System.AppDomain.ExecuteAssembly(String assemblyFile, Evidence assemblySecurity, String[] args)
       at Microsoft.VisualStudio.HostingProcess.HostProc.RunUsersAssembly()
       at System.Threading.ThreadHelper.ThreadStart_Context(Object state)
       at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
       at System.Threading.ThreadHelper.ThreadStart()
  InnerException: 

 * 
 * 
*/

/*System.TypeLoadException occurred
  Message=Could not load type 'DiskUtilForm' from assembly 'VhdDirectorApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.
  Source=mscorlib
  TypeName=DiskUtilForm
  StackTrace:
       at System.Reflection.Assembly._GetType(String name, Boolean throwOnError, Boolean ignoreCase)
       at System.Activator.CreateInstance(String assemblyName, String typeName, Boolean ignoreCase, BindingFlags bindingAttr, Binder binder, Object[] args, CultureInfo culture, Object[] activationAttributes, Evidence securityInfo, StackCrawlMark& stackMark)
       at System.Activator.CreateInstance(String assemblyName, String typeName)
       at System.AppDomain.CreateInstance(String assemblyName, String typeName)
       at vhddirector.Program.Main() in C:\Users\Administrator\Documents\Visual Studio 2010\Projects\VHD Director\Main\Program.cs:line 27
  InnerException: 
*/