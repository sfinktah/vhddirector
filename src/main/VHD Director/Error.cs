using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;

namespace VHD_Director
{
    public partial class Error : Form
    {
        protected Dictionary<string, string> data = new Dictionary<string, string>();
        public static bool showing { get; set; }
        public Error()
        {
            Init();
            
        }

        protected String ScreenshotFullPath;
        public bool Init()
        {
            if (showing)
            {
                this.InError = true;
                return false;
            }
            showing = true;

            UnhandledExceptionManager.ExceptionToScreenshot();
            if (!String.IsNullOrEmpty(UnhandledExceptionManager._strScreenshotFullPath))
            {
                ScreenshotFullPath = UnhandledExceptionManager._strScreenshotFullPath;
            }
            InitializeComponent();
           
            Random r = new Random();
            authorTextBox.Text = App.Prefs.GetPreference("User.ExceptionAuthorName", "anonymous" + r.Next());
            App.Prefs.SetPreference("User.ExceptionAuthorName", authorTextBox.Text);

            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;
            int ww = this.Bounds.Width;
            int wh = this.Bounds.Height;
            this.Location = new Point((sw >> 1) - (ww >> 1), (sh >> 1) - (ww >> 1));
            Color bg = System.Drawing.SystemColors.Control;

            return true;
        }


        private void Error_FormClosed(object sender, FormClosedEventArgs e)
        {
            showing = false;
        }

        // http://www.codeproject.com/KB/exception/ExceptionHandling.aspx

        public static bool ShowError(Exception ex) {
            return ShowError("Unhandled Exception", ex);
        }
        public static bool ShowError(String ErrorHeading, Exception ex)
        {
            Error error = new Error(ErrorHeading, ex);
            if (error.InError)
            {
                return false;
            }

            return true;
        }

        public Error(Exception ex) : this("Unhandled Exception", ex) { }

        // ex.Data.Add("Cause", "Idiocy");
        // ex.Source = "You are the source";
        // ex.Message;
        // ex.StackTrace;
        // ex.TargetSite;
        // ex.InnerException

        public Error(String ErrorHeading, Exception ex)
        {
            if (!Init()) return;
            this.Ex = ex;
            this.Name = ErrorHeading;
            this.ErrorHeading.Text = ex.GetType().FullName;
            this.ErrorSubHeading.Text = ex.Message;

            

            SetDetail();

            pictureBox1.ImageLocation = UnhandledExceptionManager._strScreenshotFullPath;


            ShowDialog();
            showing = false;
        }

        private string GetVersions()
        {
            /*
             * Operating System: Microsoft Windows NT 6.1.7601 Service Pack 1 (6.1.7601.65536)
             * System Version: Microsoft Windows Server 2008 R2 Standard Edition Service Pack 1 (build 7601), 64-bit
             * CLR Version 2.0.50727.5448
             * Application VHD Director, Version 1.0.0.0
             * VHD Director, Version 1.0.0.0
             * */
            String output = string.Empty;
            OperatingSystem os = Environment.OSVersion;
            Version ver = os.Version;
            data.Add("OS1", String.Format("OS1: {0} ({1})\r\n", os.VersionString, ver.ToString()));
            data.Add("OS2", String.Format("OS2: {0}\r\n", SystemVersionInterop.GetText()));

            output += String.Format("OS1: {0} ({1})\r\n", os.VersionString, ver.ToString());
            output += String.Format("OS2: {0}\r\n", SystemVersionInterop.GetText());
            ver = Environment.Version;

            data.Add("CLR Version", ver.ToString());
            data.Add("64-bit OS", Wow.Is64BitOperatingSystem.ToString());
            data.Add("64-bit process", Wow.Is64BitProcess.ToString());

            output += String.Format("CLR Version: {0}\r\n", ver.ToString());
            output += String.Format("64-bit operating system: {0}\r\n", Wow.Is64BitOperatingSystem);
            output += String.Format("64-bit process: {0}\r\n", Wow.Is64BitProcess);

            Assembly assem = Assembly.GetEntryAssembly();
            AssemblyName assemName = assem.GetName();
            ver = assemName.Version;

            data.Add("Application", assemName.Name);
            data.Add("Application Version", ver.ToString());
#if DEBUG
            data.Add("Build", "DEBUG");
#else
            data.Add("Build", "RELEASE");
#endif

            output += String.Format("Application: {0}, Version {1}\r\n", assemName.Name, ver.ToString());



            // Get the version of a specific assembly.
            //string filename = @".\StringLibrary.dll";
            // assem = Assembly.ReflectionOnlyLoadFrom(filename);
            //AssemblyName assemName = assem.GetName();
            // ver = assemName.Version;
            //output += String.Format("{0}, Version {1}\r\n", assemName.Name, ver.ToString());

            return output;

        }

        private void SetDetail()
        {
            data.Add("Name", this.Name);
            data.Add("ErrorHeading", this.ErrorHeading.Text);
            data.Add("ErrorSubHeading", this.ErrorSubHeading.Text);
            data.Add("EnhancedStackTrace", UnhandledExceptionManager.EnhancedStackTrace(Ex));

            this.StackTrace = UnhandledExceptionManager.SysInfoToString(true);
            string ErrorDetail = "";
            ErrorDetail +=
                "Error: " + this.Name + Environment.NewLine
                + "ErrorHeading: " + this.ErrorHeading.Text + Environment.NewLine
                + "ErrorSubHeading: " + this.ErrorSubHeading.Text + Environment.NewLine
                + "TargetSite: " + this.Ex.TargetSite.ToString() + Environment.NewLine
                + GetVersions()
                + Environment.NewLine + this.StackTrace + Environment.NewLine;

            this.ErrorDetail.Text = ErrorDetail;

        }

        private Error(String ErrorSubHeading, String ErrorDetail, Exception ex)
        {
            if (!Init()) return;


            if (showing) return;
            showing = true;

            Init();
            // this.Name = ErrorHeading;
            this.ErrorHeading.Text = ex.GetType().FullName;
            this.ErrorSubHeading.Text = ex.Message;
            this.StackTrace = UnhandledExceptionManager.EnhancedStackTrace(ex);

            this.ErrorSubHeading.Text = ErrorSubHeading;
            this.ErrorDetail.Text = ErrorSubHeading + "\r\n\r\n" + ErrorDetail;
            // Report(ErrorSubHeading, ErrorDetail);

            Show();
        }


        private void okButton_Click(object sender, EventArgs e)
        {
            if (checkBoxReport.Checked)
            {
                foreach (var pair in data)
                {
                    try
                    {
                        this.Ex.Data.Add(pair.Key, pair.Value);
                        
                    }
                     catch (Exception) { }
                        // Just incase there are any silly duplicates or casting errors or something
                }
                if (!String.IsNullOrEmpty(this.authorTextBox.Text))
                {
                    App.Prefs.SetPreference("User.ExceptionAuthorName", authorTextBox.Text);

                    try { this.Ex.Data.Add("Reported By", this.authorTextBox.Text); }
                    catch (Exception) { }
                }

                if (!String.IsNullOrEmpty(this.userComments.Text))
                {
                    try { this.Ex.Data.Add("User Comments", this.userComments.Text); }
                    catch (Exception) { }
                }
                if (!String.IsNullOrEmpty(this.ScreenshotFullPath))
                {
                    try
                    {
                        this.Ex.Data.Add("Screenshot", "file://" + this.ScreenshotFullPath);
                    }
                    catch (Exception) { }
                }
                CSharp.cc.ReportException.url = "http://feedback.nt4.com/exception.php";
                CSharp.cc.ReportException.WebReport(this.Ex);
            }
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            userComments.Visible = checkBoxReport.Checked;
            checkBoxScreenshot.Checked = checkBoxReport.Checked;
            checkBoxScreenshot.Enabled = checkBoxReport.Checked;
        }

        private void checkBoxScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = checkBoxReport.Checked;
        }

        private void checkBoxScreenshot_MouseHover(object sender, EventArgs e)
        {

        }

        public string StackTrace { get; set; }
        public Exception Ex { get; set; }


        public bool InError { get; set; }
    }

    public static class PainfulPort
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public UInt16 wServicePackMajor;
            public UInt16 wServicePackMinor;
            public UInt16 wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }
        [DllImport("Kernel32.dll")]
        internal static extern bool GetProductInfo(
            int osMajorVersion,
            int osMinorVersion,
            int spMajorVersion,
            int spMinorVersion,
            out int ReturnedProductType);

        // Use this when you want to pass it by-value even though the unmanaged API
        // expects a pointer to a structure.  Being a class adds an extra level of indirection.
        [StructLayout(LayoutKind.Sequential)]
        class OSVERSIONINFO
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public Int16 wServicePackMajor;
            public Int16 wServicePackMinor;
            public Int16 wSuiteMask;
            public Byte wProductType;
            public Byte wReserved;
        }

        [DllImport("kernel32.dll")]
        internal static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo); // Returns os info. This api can be used in a x86 process to determine if the os supports 64bit. This api is only supported on XP and higher. On lower os versions use GetSystemInfo.

        [DllImport("kernel32")]
        static extern bool GetVersionEx(ref OSVERSIONINFOEX osvi);


        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int PGNSI(int numberToMultiply);

        [DllImport("coredll.dll")]
        static extern IntPtr GetModuleHandle(string module);

        static public void PainfulVersion()
        {
            OSVERSIONINFOEX osvi;
            SYSTEM_INFO si;
            bool bOsVersionInfoEx = false;
            uint dwType;
            // ZeroMemory(&si, sizeof(SYSTEM_INFO));
            // ZeroMemory(&osvi, sizeof(OSVERSIONINFOEX)); osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);
            // bOsVersionInfoEx = GetVersionEx(ref osvi);
            if (!bOsVersionInfoEx)
            {
                return;      // Call GetNativeSystemInfo if supported or GetSystemInfo otherwise.
            }

            GetProcAddress(GetModuleHandle("kernel32.dll"), "GetNativeSystemInfo");
        }
    }


    public static class Wow
    {
        public static bool Is64BitProcess
        {
            get { return IntPtr.Size == 8; }
        }

        public static bool Is64BitOperatingSystem
        {
            get
            {
                // Clearly if this is a 64-bit process we must be on a 64-bit OS.
                if (Is64BitProcess)
                    return true;
                // Ok, so we are a 32-bit process, but is the OS 64-bit?
                // If we are running under Wow64 than the OS is 64-bit.
                bool isWow64;
                return ModuleContainsFunction("kernel32.dll", "IsWow64Process") && IsWow64Process(GetCurrentProcess(), out isWow64) && isWow64;
            }
        }

        static bool ModuleContainsFunction(string moduleName, string methodName)
        {
            IntPtr hModule = GetModuleHandle(moduleName);
            if (hModule != IntPtr.Zero)
                return GetProcAddress(hModule, methodName) != IntPtr.Zero;
            return false;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        extern static bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern static IntPtr GetCurrentProcess();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        extern static IntPtr GetModuleHandle(string moduleName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        extern static IntPtr GetProcAddress(IntPtr hModule, string methodName);
    }
}




namespace CSharp411
{
    /// <summary>
    /// Provides detailed information about the host operating system.
    /// </summary>
    static public class OSInfo
    {
        #region BITS
        /// <summary>
        /// Determines if the current application is 32 or 64-bit.
        /// </summary>
        static public int Bits
        {
            get
            {
                return IntPtr.Size * 8;
            }
        }
        #endregion BITS

        #region EDITION
        static private string s_Edition;
        /// <summary>
        /// Gets the edition of the operating system running on this computer.
        /// </summary>
        static public string Edition
        {
            get
            {
                if (s_Edition != null)
                    return s_Edition;  //***** RETURN *****//

                string edition = String.Empty;

                OperatingSystem osVersion = Environment.OSVersion;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;
                    byte productType = osVersionInfo.wProductType;
                    short suiteMask = osVersionInfo.wSuiteMask;

                    #region VERSION 4
                    if (majorVersion == 4)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            // Windows NT 4.0 Workstation
                            edition = "Workstation";
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                            {
                                // Windows NT 4.0 Server Enterprise
                                edition = "Enterprise Server";
                            }
                            else
                            {
                                // Windows NT 4.0 Server
                                edition = "Standard Server";
                            }
                        }
                    }
                    #endregion VERSION 4

                    #region VERSION 5
                    else if (majorVersion == 5)
                    {
                        if (productType == VER_NT_WORKSTATION)
                        {
                            if ((suiteMask & VER_SUITE_PERSONAL) != 0)
                            {
                                // Windows XP Home Edition
                                edition = "Home";
                            }
                            else
                            {
                                // Windows XP / Windows 2000 Professional
                                edition = "Professional";
                            }
                        }
                        else if (productType == VER_NT_SERVER)
                        {
                            if (minorVersion == 0)
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows 2000 Datacenter Server
                                    edition = "Datacenter Server";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows 2000 Advanced Server
                                    edition = "Advanced Server";
                                }
                                else
                                {
                                    // Windows 2000 Server
                                    edition = "Server";
                                }
                            }
                            else
                            {
                                if ((suiteMask & VER_SUITE_DATACENTER) != 0)
                                {
                                    // Windows Server 2003 Datacenter Edition
                                    edition = "Datacenter";
                                }
                                else if ((suiteMask & VER_SUITE_ENTERPRISE) != 0)
                                {
                                    // Windows Server 2003 Enterprise Edition
                                    edition = "Enterprise";
                                }
                                else if ((suiteMask & VER_SUITE_BLADE) != 0)
                                {
                                    // Windows Server 2003 Web Edition
                                    edition = "Web Edition";
                                }
                                else
                                {
                                    // Windows Server 2003 Standard Edition
                                    edition = "Standard";
                                }
                            }
                        }
                    }
                    #endregion VERSION 5

                    #region VERSION 6
                    else if (majorVersion == 6)
                    {
                        int ed;
                        if (GetProductInfo(majorVersion, minorVersion,
                            osVersionInfo.wServicePackMajor, osVersionInfo.wServicePackMinor,
                            out ed))
                        {
                            switch (ed)
                            {
                                case PRODUCT_BUSINESS:
                                    edition = "Business";
                                    break;
                                case PRODUCT_BUSINESS_N:
                                    edition = "Business N";
                                    break;
                                case PRODUCT_CLUSTER_SERVER:
                                    edition = "HPC Edition";
                                    break;
                                case PRODUCT_DATACENTER_SERVER:
                                    edition = "Datacenter Server";
                                    break;
                                case PRODUCT_DATACENTER_SERVER_CORE:
                                    edition = "Datacenter Server (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE:
                                    edition = "Enterprise";
                                    break;
                                case PRODUCT_ENTERPRISE_N:
                                    edition = "Enterprise N";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER:
                                    edition = "Enterprise Server";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_CORE:
                                    edition = "Enterprise Server (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_CORE_V:
                                    edition = "Enterprise Server without Hyper-V (core installation)";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_IA64:
                                    edition = "Enterprise Server for Itanium-based Systems";
                                    break;
                                case PRODUCT_ENTERPRISE_SERVER_V:
                                    edition = "Enterprise Server without Hyper-V";
                                    break;
                                case PRODUCT_HOME_BASIC:
                                    edition = "Home Basic";
                                    break;
                                case PRODUCT_HOME_BASIC_N:
                                    edition = "Home Basic N";
                                    break;
                                case PRODUCT_HOME_PREMIUM:
                                    edition = "Home Premium";
                                    break;
                                case PRODUCT_HOME_PREMIUM_N:
                                    edition = "Home Premium N";
                                    break;
                                case PRODUCT_HYPERV:
                                    edition = "Microsoft Hyper-V Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT:
                                    edition = "Windows Essential Business Management Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING:
                                    edition = "Windows Essential Business Messaging Server";
                                    break;
                                case PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY:
                                    edition = "Windows Essential Business Security Server";
                                    break;
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS:
                                    edition = "Windows Essential Server Solutions";
                                    break;
                                case PRODUCT_SERVER_FOR_SMALLBUSINESS_V:
                                    edition = "Windows Essential Server Solutions without Hyper-V";
                                    break;
                                case PRODUCT_SMALLBUSINESS_SERVER:
                                    edition = "Windows Small Business Server";
                                    break;
                                case PRODUCT_STANDARD_SERVER:
                                    edition = "Standard Server";
                                    break;
                                case PRODUCT_STANDARD_SERVER_CORE:
                                    edition = "Standard Server (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER_CORE_V:
                                    edition = "Standard Server without Hyper-V (core installation)";
                                    break;
                                case PRODUCT_STANDARD_SERVER_V:
                                    edition = "Standard Server without Hyper-V";
                                    break;
                                case PRODUCT_STARTER:
                                    edition = "Starter";
                                    break;
                                case PRODUCT_STORAGE_ENTERPRISE_SERVER:
                                    edition = "Enterprise Storage Server";
                                    break;
                                case PRODUCT_STORAGE_EXPRESS_SERVER:
                                    edition = "Express Storage Server";
                                    break;
                                case PRODUCT_STORAGE_STANDARD_SERVER:
                                    edition = "Standard Storage Server";
                                    break;
                                case PRODUCT_STORAGE_WORKGROUP_SERVER:
                                    edition = "Workgroup Storage Server";
                                    break;
                                case PRODUCT_UNDEFINED:
                                    edition = "Unknown product";
                                    break;
                                case PRODUCT_ULTIMATE:
                                    edition = "Ultimate";
                                    break;
                                case PRODUCT_ULTIMATE_N:
                                    edition = "Ultimate N";
                                    break;
                                case PRODUCT_WEB_SERVER:
                                    edition = "Web Server";
                                    break;
                                case PRODUCT_WEB_SERVER_CORE:
                                    edition = "Web Server (core installation)";
                                    break;
                            }
                        }
                    }
                    #endregion VERSION 6
                }

                s_Edition = edition;
                return edition;
            }
        }
        #endregion EDITION

        #region NAME
        static private string s_Name;
        /// <summary>
        /// Gets the name of the operating system running on this computer.
        /// </summary>
        static public string Name
        {
            get
            {
                if (s_Name != null)
                    return s_Name;  //***** RETURN *****//

                string name = "unknown";

                OperatingSystem osVersion = Environment.OSVersion;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    int majorVersion = osVersion.Version.Major;
                    int minorVersion = osVersion.Version.Minor;

                    switch (osVersion.Platform)
                    {
                        case PlatformID.Win32Windows:
                            {
                                if (majorVersion == 4)
                                {
                                    string csdVersion = osVersionInfo.szCSDVersion;
                                    switch (minorVersion)
                                    {
                                        case 0:
                                            if (csdVersion == "B" || csdVersion == "C")
                                                name = "Windows 95 OSR2";
                                            else
                                                name = "Windows 95";
                                            break;
                                        case 10:
                                            if (csdVersion == "A")
                                                name = "Windows 98 Second Edition";
                                            else
                                                name = "Windows 98";
                                            break;
                                        case 90:
                                            name = "Windows Me";
                                            break;
                                    }
                                }
                                break;
                            }

                        case PlatformID.Win32NT:
                            {
                                byte productType = osVersionInfo.wProductType;

                                switch (majorVersion)
                                {
                                    case 3:
                                        name = "Windows NT 3.51";
                                        break;
                                    case 4:
                                        switch (productType)
                                        {
                                            case 1:
                                                name = "Windows NT 4.0";
                                                break;
                                            case 3:
                                                name = "Windows NT 4.0 Server";
                                                break;
                                        }
                                        break;
                                    case 5:
                                        switch (minorVersion)
                                        {
                                            case 0:
                                                name = "Windows 2000";
                                                break;
                                            case 1:
                                                name = "Windows XP";
                                                break;
                                            case 2:
                                                name = "Windows Server 2003";
                                                break;
                                        }
                                        break;
                                    case 6:
                                        switch (productType)
                                        {
                                            case 1:
                                                name = "Windows Vista";
                                                break;
                                            case 3:
                                                name = "Windows Server 2008";
                                                break;
                                        }
                                        break;
                                }
                                break;
                            }
                    }
                }

                s_Name = name;
                return name;
            }
        }
        #endregion NAME

        #region PINVOKE
        #region GET
        #region PRODUCT INFO
        [DllImport("Kernel32.dll")]
        internal static extern bool GetProductInfo(
            int osMajorVersion,
            int osMinorVersion,
            int spMajorVersion,
            int spMinorVersion,
            out int edition);
        #endregion PRODUCT INFO

        #region VERSION
        [DllImport("kernel32.dll")]
        private static extern bool GetVersionEx(ref OSVERSIONINFOEX osVersionInfo);
        #endregion VERSION
        #endregion GET

        #region OSVERSIONINFOEX
        [StructLayout(LayoutKind.Sequential)]
        private struct OSVERSIONINFOEX
        {
            public int dwOSVersionInfoSize;
            public int dwMajorVersion;
            public int dwMinorVersion;
            public int dwBuildNumber;
            public int dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public short wServicePackMajor;
            public short wServicePackMinor;
            public short wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }
        #endregion OSVERSIONINFOEX

        #region PRODUCT
        private const int PRODUCT_UNDEFINED = 0x00000000;
        private const int PRODUCT_ULTIMATE = 0x00000001;
        private const int PRODUCT_HOME_BASIC = 0x00000002;
        private const int PRODUCT_HOME_PREMIUM = 0x00000003;
        private const int PRODUCT_ENTERPRISE = 0x00000004;
        private const int PRODUCT_HOME_BASIC_N = 0x00000005;
        private const int PRODUCT_BUSINESS = 0x00000006;
        private const int PRODUCT_STANDARD_SERVER = 0x00000007;
        private const int PRODUCT_DATACENTER_SERVER = 0x00000008;
        private const int PRODUCT_SMALLBUSINESS_SERVER = 0x00000009;
        private const int PRODUCT_ENTERPRISE_SERVER = 0x0000000A;
        private const int PRODUCT_STARTER = 0x0000000B;
        private const int PRODUCT_DATACENTER_SERVER_CORE = 0x0000000C;
        private const int PRODUCT_STANDARD_SERVER_CORE = 0x0000000D;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE = 0x0000000E;
        private const int PRODUCT_ENTERPRISE_SERVER_IA64 = 0x0000000F;
        private const int PRODUCT_BUSINESS_N = 0x00000010;
        private const int PRODUCT_WEB_SERVER = 0x00000011;
        private const int PRODUCT_CLUSTER_SERVER = 0x00000012;
        private const int PRODUCT_HOME_SERVER = 0x00000013;
        private const int PRODUCT_STORAGE_EXPRESS_SERVER = 0x00000014;
        private const int PRODUCT_STORAGE_STANDARD_SERVER = 0x00000015;
        private const int PRODUCT_STORAGE_WORKGROUP_SERVER = 0x00000016;
        private const int PRODUCT_STORAGE_ENTERPRISE_SERVER = 0x00000017;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS = 0x00000018;
        private const int PRODUCT_SMALLBUSINESS_SERVER_PREMIUM = 0x00000019;
        private const int PRODUCT_HOME_PREMIUM_N = 0x0000001A;
        private const int PRODUCT_ENTERPRISE_N = 0x0000001B;
        private const int PRODUCT_ULTIMATE_N = 0x0000001C;
        private const int PRODUCT_WEB_SERVER_CORE = 0x0000001D;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT = 0x0000001E;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY = 0x0000001F;
        private const int PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING = 0x00000020;
        private const int PRODUCT_SERVER_FOR_SMALLBUSINESS_V = 0x00000023;
        private const int PRODUCT_STANDARD_SERVER_V = 0x00000024;
        private const int PRODUCT_ENTERPRISE_SERVER_V = 0x00000026;
        private const int PRODUCT_STANDARD_SERVER_CORE_V = 0x00000028;
        private const int PRODUCT_ENTERPRISE_SERVER_CORE_V = 0x00000029;
        private const int PRODUCT_HYPERV = 0x0000002A;
        #endregion PRODUCT

        #region VERSIONS
        private const int VER_NT_WORKSTATION = 1;
        private const int VER_NT_DOMAIN_CONTROLLER = 2;
        private const int VER_NT_SERVER = 3;
        private const int VER_SUITE_SMALLBUSINESS = 1;
        private const int VER_SUITE_ENTERPRISE = 2;
        private const int VER_SUITE_TERMINAL = 16;
        private const int VER_SUITE_DATACENTER = 128;
        private const int VER_SUITE_SINGLEUSERTS = 256;
        private const int VER_SUITE_PERSONAL = 512;
        private const int VER_SUITE_BLADE = 1024;
        #endregion VERSIONS
        #endregion PINVOKE

        #region SERVICE PACK
        /// <summary>
        /// Gets the service pack information of the operating system running on this computer.
        /// </summary>
        static public string ServicePack
        {
            get
            {
                string servicePack = String.Empty;
                OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();

                osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));

                if (GetVersionEx(ref osVersionInfo))
                {
                    servicePack = osVersionInfo.szCSDVersion;
                }

                return servicePack;
            }
        }
        #endregion SERVICE PACK

        #region VERSION
        #region BUILD
        /// <summary>
        /// Gets the build version number of the operating system running on this computer.
        /// </summary>
        static public int BuildVersion
        {
            get
            {
                return Environment.OSVersion.Version.Build;
            }
        }
        #endregion BUILD

        #region FULL
        #region STRING
        /// <summary>
        /// Gets the full version string of the operating system running on this computer.
        /// </summary>
        static public string VersionString
        {
            get
            {
                return Environment.OSVersion.Version.ToString();
            }
        }
        #endregion STRING

        #region VERSION
        /// <summary>
        /// Gets the full version of the operating system running on this computer.
        /// </summary>
        static public Version Version
        {
            get
            {
                return Environment.OSVersion.Version;
            }
        }
        #endregion VERSION
        #endregion FULL

        #region MAJOR
        /// <summary>
        /// Gets the major version number of the operating system running on this computer.
        /// </summary>
        static public int MajorVersion
        {
            get
            {
                return Environment.OSVersion.Version.Major;
            }
        }
        #endregion MAJOR

        #region MINOR
        /// <summary>
        /// Gets the minor version number of the operating system running on this computer.
        /// </summary>
        static public int MinorVersion
        {
            get
            {
                return Environment.OSVersion.Version.Minor;
            }
        }
        #endregion MINOR

        #region REVISION
        /// <summary>
        /// Gets the revision version number of the operating system running on this computer.
        /// </summary>
        static public int RevisionVersion
        {
            get
            {
                return Environment.OSVersion.Version.Revision;
            }
        }
        #endregion REVISION
        #endregion VERSION
    }
}
