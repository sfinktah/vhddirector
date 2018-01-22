// http://www.pinvoke.net/default.aspx/user32/EnumChildWindows.html
// http://msdn.microsoft.com/en-us/library/dd469351(VS.85).aspx 

using System;
using System.Collections.Generic;
using System.Drawing;
// using System.Linq;
using System.Text;
// using System.Reflection;
using System.Runtime.InteropServices;

namespace CSharp.cc.WinApi
{
    public class Code86
    {
        [DllImport("Code86.dll")]
        static public extern int Call(string Procedure, string Arguments);
    }

    public class SystemErrorCodes
    {
        static public String GetMessage(int code)
        {
            string errorMessage = new System.ComponentModel.Win32Exception(code).Message;
            return errorMessage;

        }
    }

    public class User32
    {


        [DllImport("user32.dll")]

        static public extern UInt32 GetClassLong(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll")]
        static public extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        static public extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static public extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        // ENUMERATION

        // declare the delegate
        public delegate bool WindowEnumDelegate(IntPtr hwnd,  int lParam);

        // declare the API function to enumerate child windows
        [DllImport("user32.dll")]
        static public extern int EnumChildWindows(IntPtr hwnd,
                                                  WindowEnumDelegate del,
                                                  int lParam);
        [DllImport("user32.dll")]
        static public extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("User32.dll")]
        static public extern int SetForegroundWindow(int hWnd);
        
        [DllImport("User32.dll")]
        static public extern int SetFocus(int hWnd);

        [DllImport("user32.dll")]
        static public extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static public extern int ShowWindow(int hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        static public extern int GetActiveWindow();

        [DllImport("user32.dll")]
        static public extern int SetActiveWindow(int hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static public extern IntPtr GetForegroundWindow();

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static public extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static public extern bool GetWindowRect(HandleRef hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static public extern int GetScrollPos(IntPtr hWnd, System.Windows.Forms.Orientation nBar);

		[DllImport("user32.dll")]
		static public extern int SetScrollPos(IntPtr hWnd, System.Windows.Forms.Orientation nBar, int nPos, bool bRedraw);



        static public string GetText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static public extern uint GetWindowModuleFileName(IntPtr hwnd,
           StringBuilder lpszFileName, uint cchFileNameMax);



        private void BringToFront(string className, string CaptionName)
        {
            SetForegroundWindow(FindWindow(className, CaptionName));
        }
        // [DllImport("coredll.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        // static public extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static public extern int SendMessage(
              IntPtr hWnd,    // HWND handle to destination window
              int Msg,     // UINT message
              int wParam,  // WPARAM first message parameter
              StringBuilder lParam   // LPARAM second message parameter
            );

        [DllImport("user32.dll")]
        static public extern int SendMessage(
              IntPtr hWnd,    // HWND handle to destination window
              int Msg,     // UINT message
              int wParam,  // WPARAM first message parameter
              string lParam   // LPARAM second message parameter
              );

        [DllImport("user32.dll")]
        static public extern int SendMessage(
              int hWnd,    // HWND handle to destination window
              int Msg,     // UINT message
              int wParam,  // WPARAM first message parameter
              int lParam   // LPARAM second message parameter
              );

        [DllImport("user32.dll")]
        static public extern int SendMessage(
              IntPtr hWnd,    // HWND handle to destination window
              int Msg,     // UINT message
              int wParam,  // WPARAM first message parameter
              int lParam   // LPARAM second message parameter
              );


        [DllImport("user32.dll")]
        static public extern int PostMessage(
              int hWnd,    // HWND handle to destination window
              int Msg,     // UINT message
              int wParam,  // WPARAM first message parameter
              int lParam   // LPARAM second message parameter
               );

        [DllImport("user32.dll")]
        static public extern int PostMessage(
              IntPtr hWnd,    // HWND handle to destination window
              int Msg,     // UINT message
              int wParam,  // WPARAM first message parameter
              int lParam   // LPARAM second message parameter
               );

		[DllImport("user32.dll")]
		static private extern bool PostMessageA(
				IntPtr hWnd, 
				int nBar, 
				int wParam, 
				int lParam
				);

        static public void SuspendDrawing(System.Windows.Forms.Control target)
        {
            IntPtr hwnd = target.Handle;
            SendMessage(hwnd, WM_SETREDRAW, 0, 0);
        }

        static public void ResumeDrawing(System.Windows.Forms.Control target)
        {
            ResumeDrawing(target, true);
        }
        static public void ResumeDrawing(System.Windows.Forms.Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }


        public const int MK_LBUTTON = 0x1;

        #region WindowsMessagingConstants
        /* GetWindowClass/Long */
        static public int GCL_MENUNAME = -8;
        static public int GCL_HBRBACKGROUND = -10;
        static public int GCL_HCURSOR = -12;
        static public int GCL_HICON = -14;
        static public int GCL_HMODULE = -16;
        static public int GCL_CBWNDEXTRA = -18;
        static public int GCL_CBCLSEXTRA = -20;
        static public int GCL_WNDPROC = -24;
        static public int GCL_STYLE = -26;
        static public int GCW_ATOM = -32;
        static public int GWL_WNDPROC = -4;
        static public int GWL_HINSTANCE = -6;
        static public int GWL_HWNDPARENT = -8;
        static public int GWL_STYLE = -16;
        static public int GWL_EXSTYLE = -20;
        static public int GWL_USERDATA = -21;
        static public int GWL_ID = -12;

        /* ShowWindow() Commands */
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_NORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_MAXIMIZE = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_MINIMIZE = 6;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;
        public const int SW_SHOWDEFAULT = 10;
        public const int SW_FORCEMINIMIZE = 11;
        public const int SW_MAX = 11;

        /* Animation Control */
        public const int ACM_OPENW = 1127;
        public const int ACM_OPENA = 1124;
        public const int ACM_PLAY = 1125;
        public const int ACM_STOP = 1126;
        public const int ACN_START = 1;
        public const int ACN_STOP = 2;

        /* Buttons */
        public const int BM_CLICK = 245;
        public const int BM_GETCHECK = 240;
        public const int BM_GETIMAGE = 246;
        public const int BM_GETSTATE = 242;
        public const int BM_SETCHECK = 241;
        public const int BM_SETIMAGE = 247;
        public const int BM_SETSTATE = 243;
        public const int BM_SETSTYLE = 244;
        public const int BN_CLICKED = 0;
        public const int BN_DBLCLK = 5;
        public const int BN_DISABLE = 4;
        public const int BN_DOUBLECLICKED = 5;
        public const int BN_HILITE = 2;
        public const int BN_KILLFOCUS = 7;
        public const int BN_PAINT = 1;
        public const int BN_PUSHED = 2;
        public const int BN_SETFOCUS = 6;
        public const int BN_UNHILITE = 3;
        public const int BN_UNPUSHED = 3;

        /* Combo Box */
        public const int CB_ADDSTRING = 323;
        public const int CB_DELETESTRING = 324;
        public const int CB_DIR = 325;
        public const int CB_FINDSTRING = 332;
        public const int CB_FINDSTRINGEXACT = 344;
        public const int CB_GETCOUNT = 326;
        public const int CB_GETCURSEL = 327;
        public const int CB_GETDROPPEDCONTROLRECT = 338;
        public const int CB_GETDROPPEDSTATE = 343;
        public const int CB_GETDROPPEDWIDTH = 351;
        public const int CB_GETEDITSEL = 320;
        public const int CB_GETEXTENDEDUI = 342;
        public const int CB_GETHORIZONTALEXTENT = 349;
        public const int CB_GETITEMDATA = 336;
        public const int CB_GETITEMHEIGHT = 340;
        public const int CB_GETLBTEXT = 328;
        public const int CB_GETLBTEXTLEN = 329;
        public const int CB_GETLOCALE = 346;
        public const int CB_GETTOPINDEX = 347;
        public const int CB_INITSTORAGE = 353;
        public const int CB_INSERTSTRING = 330;
        public const int CB_LIMITTEXT = 321;
        public const int CB_RESETCONTENT = 331;
        public const int CB_SELECTSTRING = 333;
        public const int CB_SETCURSEL = 334;
        public const int CB_SETDROPPEDWIDTH = 352;
        public const int CB_SETEDITSEL = 322;
        public const int CB_SETEXTENDEDUI = 341;
        public const int CB_SETHORIZONTALEXTENT = 350;
        public const int CB_SETITEMDATA = 337;
        public const int CB_SETITEMHEIGHT = 339;
        public const int CB_SETLOCALE = 345;
        public const int CB_SETTOPINDEX = 348;
        public const int CB_SHOWDROPDOWN = 335;

        /* Combo Box notifications */
        public const int CBN_CLOSEUP = 8;
        public const int CBN_DBLCLK = 2;
        public const int CBN_DROPDOWN = 7;
        public const int CBN_EDITCHANGE = 5;
        public const int CBN_EDITUPDATE = 6;
        public const int CBN_KILLFOCUS = 4;
        public const int CBN_SELCHANGE = 1;
        public const int CBN_SELENDCANCEL = 10;
        public const int CBN_SELENDOK = 9;
        public const int CBN_SETFOCUS = 3;

        /* Control Panel */

        /* Device messages */

        /* Drag list box */
        public const int DL_BEGINDRAG = 1157;
        public const int DL_CANCELDRAG = 1160;
        public const int DL_DRAGGING = 1158;
        public const int DL_DROPPED = 1159;

        /* Default push button */
        public const int DM_GETDEFID = 1024;
        public const int DM_REPOSITION = 1026;
        public const int DM_SETDEFID = 1025;

        /* RTF control */
        public const int EM_CANPASTE = 1074;
        public const int EM_CANUNDO = 198;
        public const int EM_CHARFROMPOS = 215;
        public const int EM_DISPLAYBAND = 1075;
        public const int EM_EMPTYUNDOBUFFER = 205;
        public const int EM_EXGETSEL = 1076;
        public const int EM_EXLIMITTEXT = 1077;
        public const int EM_EXLINEFROMCHAR = 1078;
        public const int EM_EXSETSEL = 1079;
        public const int EM_FINDTEXT = 1080;
        public const int EM_FINDTEXTEX = 1103;
        public const int EM_FINDWORDBREAK = 1100;
        public const int EM_FMTLINES = 200;
        public const int EM_FORMATRANGE = 1081;
        public const int EM_GETCHARFORMAT = 1082;
        public const int EM_GETEVENTMASK = 1083;
        public const int EM_GETFIRSTVISIBLELINE = 206;
        public const int EM_GETHANDLE = 189;
        public const int EM_GETLIMITTEXT = 213;
        public const int EM_GETLINE = 196;
        public const int EM_GETLINECOUNT = 186;
        public const int EM_GETMARGINS = 212;
        public const int EM_GETMODIFY = 184;
        public const int EM_GETIMECOLOR = 1129;
        public const int EM_GETIMEOPTIONS = 1131;
        public const int EM_GETOPTIONS = 1102;
        public const int EM_GETOLEINTERFACE = 1084;
        public const int EM_GETPARAFORMAT = 1085;
        public const int EM_GETPASSWORDCHAR = 210;
        public const int EM_GETPUNCTUATION = 1125;
        public const int EM_GETRECT = 178;
        public const int EM_GETSEL = 176;
        public const int EM_GETSELTEXT = 1086;
        public const int EM_GETTEXTRANGE = 1099;
        public const int EM_GETTHUMB = 190;
        public const int EM_GETWORDBREAKPROC = 209;
        public const int EM_GETWORDBREAKPROCEX = 1104;
        public const int EM_GETWORDWRAPMODE = 1127;
        public const int EM_HIDESELECTION = 1087;
        public const int EM_LIMITTEXT = 197;
        public const int EM_LINEFROMCHAR = 201;
        public const int EM_LINEINDEX = 187;
        public const int EM_LINELENGTH = 193;
        public const int EM_LINESCROLL = 182;
        public const int EM_PASTESPECIAL = 1088;
        public const int EM_POSFROMCHAR = 214;
        public const int EM_REPLACESEL = 194;
        public const int EM_REQUESTRESIZE = 1089;
        public const int EM_SCROLL = 181;
        public const int EM_SCROLLCARET = 183;
        public const int EM_SELECTIONTYPE = 1090;
        public const int EM_SETBKGNDCOLOR = 1091;
        public const int EM_SETCHARFORMAT = 1092;
        public const int EM_SETEVENTMASK = 1093;
        public const int EM_SETHANDLE = 188;
        public const int EM_SETIMECOLOR = 1128;
        public const int EM_SETIMEOPTIONS = 1130;
        public const int EM_SETLIMITTEXT = 197;
        public const int EM_SETMARGINS = 211;
        public const int EM_SETMODIFY = 185;
        public const int EM_SETOLECALLBACK = 1094;
        public const int EM_SETOPTIONS = 1101;
        public const int EM_SETPARAFORMAT = 1095;
        public const int EM_SETPASSWORDCHAR = 204;
        public const int EM_SETPUNCTUATION = 1124;
        public const int EM_SETREADONLY = 207;
        public const int EM_SETRECT = 179;
        public const int EM_SETRECTNP = 180;
        public const int EM_SETSEL = 177;
        public const int EM_SETTABSTOPS = 203;
        public const int EM_SETTARGETDEVICE = 1096;
        public const int EM_SETWORDBREAKPROC = 208;
        public const int EM_SETWORDBREAKPROCEX = 1105;
        public const int EM_SETWORDWRAPMODE = 1126;
        public const int EM_STREAMIN = 1097;
        public const int EM_STREAMOUT = 1098;
        public const int EM_UNDO = 199;

        /* Edit control */
        public const int EN_CHANGE = 768;
        public const int EN_CORRECTTEXT = 1797;
        public const int EN_DROPFILES = 1795;
        public const int EN_ERRSPACE = 1280;
        public const int EN_HSCROLL = 1537;
        public const int EN_IMECHANGE = 1799;
        public const int EN_KILLFOCUS = 512;
        public const int EN_MAXTEXT = 1281;
        public const int EN_MSGFILTER = 1792;
        public const int EN_OLEOPFAILED = 1801;
        public const int EN_PROTECTED = 1796;
        public const int EN_REQUESTRESIZE = 1793;
        public const int EN_SAVECLIPBOARD = 1800;
        public const int EN_SELCHANGE = 1794;
        public const int EN_SETFOCUS = 256;
        public const int EN_STOPNOUNDO = 1798;
        public const int EN_UPDATE = 1024;
        public const int EN_VSCROLL = 1538;

        /* File Manager extensions */

        /* File Manager extensions DLL events */

        /* Header control */
        public const int HDM_DELETEITEM = 4610;
        public const int HDM_GETITEMW = 4619;
        public const int HDM_INSERTITEMW = 4618;
        public const int HDM_SETITEMW = 4620;
        public const int HDM_GETITEMA = 4611;
        public const int HDM_INSERTITEMA = 4609;
        public const int HDM_SETITEMA = 4612;
        public const int HDM_GETITEMCOUNT = 4608;
        public const int HDM_HITTEST = 4614;
        public const int HDM_LAYOUT = 4613;

        /* Header control notifications */
        public const int HDN_BEGINTRACKW = -326;
        public const int HDN_DIVIDERDBLCLICKW = -325;
        public const int HDN_ENDTRACKW = -327;
        public const int HDN_ITEMCHANGEDW = -321;
        public const int HDN_ITEMCHANGINGW = -320;
        public const int HDN_ITEMCLICKW = -322;
        public const int HDN_ITEMDBLCLICKW = -323;
        public const int HDN_TRACKW = -328;
        public const int HDN_BEGINTRACKA = -306;
        public const int HDN_DIVIDERDBLCLICKA = -305;
        public const int HDN_ENDTRACKA = -307;
        public const int HDN_ITEMCHANGEDA = -301;
        public const int HDN_ITEMCHANGINGA = -300;
        public const int HDN_ITEMCLICKA = -302;
        public const int HDN_ITEMDBLCLICKA = -303;
        public const int HDN_TRACKA = -308;
        /* Hot key control */
        public const int HKM_GETHOTKEY = 1026;
        public const int HKM_SETHOTKEY = 1025;
        public const int HKM_SETRULES = 1027;

        /* List box */
        public const int LB_ADDFILE = 406;
        public const int LB_ADDSTRING = 384;
        public const int LB_DELETESTRING = 386;
        public const int LB_DIR = 397;
        public const int LB_FINDSTRING = 399;
        public const int LB_FINDSTRINGEXACT = 418;
        public const int LB_GETANCHORINDEX = 413;
        public const int LB_GETCARETINDEX = 415;
        public const int LB_GETCOUNT = 395;
        public const int LB_GETCURSEL = 392;
        public const int LB_GETHORIZONTALEXTENT = 403;
        public const int LB_GETITEMDATA = 409;
        public const int LB_GETITEMHEIGHT = 417;
        public const int LB_GETITEMRECT = 408;
        public const int LB_GETLOCALE = 422;
        public const int LB_GETSEL = 391;
        public const int LB_GETSELCOUNT = 400;
        public const int LB_GETSELITEMS = 401;
        public const int LB_GETTEXT = 393;
        public const int LB_GETTEXTLEN = 394;
        public const int LB_GETTOPINDEX = 398;
        public const int LB_INITSTORAGE = 424;
        public const int LB_INSERTSTRING = 385;
        public const int LB_ITEMFROMPOINT = 425;
        public const int LB_RESETCONTENT = 388;
        public const int LB_SELECTSTRING = 396;
        public const int LB_SELITEMRANGE = 411;
        public const int LB_SELITEMRANGEEX = 387;
        public const int LB_SETANCHORINDEX = 412;
        public const int LB_SETCARETINDEX = 414;
        public const int LB_SETCOLUMNWIDTH = 405;
        public const int LB_SETCOUNT = 423;
        public const int LB_SETCURSEL = 390;
        public const int LB_SETHORIZONTALEXTENT = 404;
        public const int LB_SETITEMDATA = 410;
        public const int LB_SETITEMHEIGHT = 416;
        public const int LB_SETLOCALE = 421;
        public const int LB_SETSEL = 389;
        public const int LB_SETTABSTOPS = 402;
        public const int LB_SETTOPINDEX = 407;

        /* List box notifications */
        public const int LBN_DBLCLK = 2;
        public const int LBN_ERRSPACE = -2;
        public const int LBN_KILLFOCUS = 5;
        public const int LBN_SELCANCEL = 3;
        public const int LBN_SELCHANGE = 1;
        public const int LBN_SETFOCUS = 4;

        /* List view control */
        public const int LVM_ARRANGE = 4118;
        public const int LVM_CREATEDRAGIMAGE = 4129;
        public const int LVM_DELETEALLITEMS = 4105;
        public const int LVM_DELETECOLUMN = 4124;
        public const int LVM_DELETEITEM = 4104;
        public const int LVM_ENSUREVISIBLE = 4115;
        public const int LVM_GETBKCOLOR = 4096;
        public const int LVM_GETCALLBACKMASK = 4106;
        public const int LVM_GETCOLUMNWIDTH = 4125;
        public const int LVM_GETCOUNTPERPAGE = 4136;
        public const int LVM_GETEDITCONTROL = 4120;
        public const int LVM_GETIMAGELIST = 4098;
        public const int LVM_EDITLABELW = 4214;
        public const int LVM_FINDITEMW = 4179;
        public const int LVM_GETCOLUMNW = 4191;
        public const int LVM_GETISEARCHSTRINGW = 4213;
        public const int LVM_GETITEMW = 4171;
        public const int LVM_GETITEMTEXTW = 4211;
        public const int LVM_GETSTRINGWIDTHW = 4183;
        public const int LVM_INSERTCOLUMNW = 4193;
        public const int LVM_INSERTITEMW = 4173;
        public const int LVM_SETCOLUMNW = 4192;
        public const int LVM_SETITEMW = 4172;
        public const int LVM_SETITEMTEXTW = 4212;
        public const int LVM_EDITLABELA = 4119;
        public const int LVM_FINDITEMA = 4109;
        public const int LVM_GETCOLUMNA = 4121;
        public const int LVM_GETISEARCHSTRINGA = 4148;
        public const int LVM_GETITEMA = 4101;
        public const int LVM_GETITEMTEXTA = 4141;
        public const int LVM_GETSTRINGWIDTHA = 4113;
        public const int LVM_INSERTCOLUMNA = 4123;
        public const int LVM_INSERTITEMA = 4103;
        public const int LVM_SETCOLUMNA = 4122;
        public const int LVM_SETITEMA = 4102;
        public const int LVM_SETITEMTEXTA = 4142;
        public const int LVM_GETITEMCOUNT = 4100;
        public const int LVM_GETITEMPOSITION = 4112;
        public const int LVM_GETITEMRECT = 4110;
        public const int LVM_GETITEMSPACING = 4147;
        public const int LVM_GETITEMSTATE = 4140;
        public const int LVM_GETNEXTITEM = 4108;
        public const int LVM_GETORIGIN = 4137;
        public const int LVM_GETSELECTEDCOUNT = 4146;
        public const int LVM_GETTEXTBKCOLOR = 4133;
        public const int LVM_GETTEXTCOLOR = 4131;
        public const int LVM_GETTOPINDEX = 4135;
        public const int LVM_GETVIEWRECT = 4130;
        public const int LVM_HITTEST = 4114;
        public const int LVM_REDRAWITEMS = 4117;
        public const int LVM_SCROLL = 4116;
        public const int LVM_SETBKCOLOR = 4097;
        public const int LVM_SETCALLBACKMASK = 4107;
        public const int LVM_SETCOLUMNWIDTH = 4126;
        public const int LVM_SETIMAGELIST = 4099;
        public const int LVM_SETITEMCOUNT = 4143;
        public const int LVM_SETITEMPOSITION = 4111;
        public const int LVM_SETITEMPOSITION32 = 4145;
        public const int LVM_SETITEMSTATE = 4139;
        public const int LVM_SETTEXTBKCOLOR = 4134;
        public const int LVM_SETTEXTCOLOR = 4132;
        public const int LVM_SORTITEMS = 4144;
        public const int LVM_UPDATE = 4138;

        /* List view control notifications */
        public const int LVN_BEGINDRAG = -109;
        public const int LVN_BEGINRDRAG = -111;
        public const int LVN_COLUMNCLICK = -108;
        public const int LVN_DELETEALLITEMS = -104;
        public const int LVN_DELETEITEM = -103;
        public const int LVN_BEGINLABELEDITW = -175;
        public const int LVN_ENDLABELEDITW = -176;
        public const int LVN_GETDISPINFOW = -177;
        public const int LVN_SETDISPINFOW = -178;
        public const int LVN_BEGINLABELEDITA = -105;
        public const int LVN_ENDLABELEDITA = -106;
        public const int LVN_GETDISPINFOA = -150;
        public const int LVN_SETDISPINFOA = -151;
        public const int LVN_INSERTITEM = -102;
        public const int LVN_ITEMCHANGED = -101;
        public const int LVN_ITEMCHANGING = -100;
        public const int LVN_KEYDOWN = -155;

        /* Control notification */
        public const int NM_CLICK = -2;
        public const int NM_DBLCLK = -3;
        public const int NM_KILLFOCUS = -8;
        public const int NM_OUTOFMEMORY = -1;
        public const int NM_RCLICK = -5;
        public const int NM_RDBLCLK = -6;
        public const int NM_RETURN = -4;
        public const int NM_SETFOCUS = -7;

        /* Power status */

        /* Progress bar control */
        public const int PBM_DELTAPOS = 1027;
        public const int PBM_SETPOS = 1026;
        public const int PBM_SETRANGE = 1025;
        public const int PBM_SETSTEP = 1028;
        public const int PBM_STEPIT = 1029;

        /* Property sheets */
        public const int PSM_ADDPAGE = 1127;
        public const int PSM_APPLY = 1134;
        public const int PSM_CANCELTOCLOSE = 1131;
        public const int PSM_CHANGED = 1128;
        public const int PSM_GETTABCONTROL = 1140;
        public const int PSM_GETCURRENTPAGEHWND = 1142;
        public const int PSM_ISDIALOGMESSAGE = 1141;
        public const int PSM_PRESSBUTTON = 1137;
        public const int PSM_QUERYSIBLINGS = 1132;
        public const int PSM_REBOOTSYSTEM = 1130;
        public const int PSM_REMOVEPAGE = 1126;
        public const int PSM_RESTARTWINDOWS = 1129;
        public const int PSM_SETCURSEL = 1125;
        public const int PSM_SETCURSELID = 1138;
        public const int PSM_SETFINISHTEXTW = 1145;
        public const int PSM_SETTITLEW = 1144;
        public const int PSM_SETFINISHTEXTA = 1139;
        public const int PSM_SETTITLEA = 1135;
        public const int PSM_SETWIZBUTTONS = 1136;
        public const int PSM_UNCHANGED = 1133;

        /* Property sheet notifications */
        public const int PSN_APPLY = -202;
        public const int PSN_HELP = -205;
        public const int PSN_KILLACTIVE = -201;
        public const int PSN_QUERYCANCEL = -209;
        public const int PSN_RESET = -203;
        public const int PSN_SETACTIVE = -200;
        public const int PSN_WIZBACK = -206;
        public const int PSN_WIZFINISH = -208;
        public const int PSN_WIZNEXT = -207;

        /* Status window */
        public const int SB_GETBORDERS = 1031;
        public const int SB_GETPARTS = 1030;
        public const int SB_GETRECT = 1034;
        public const int SB_GETTEXTW = 1037;
        public const int SB_GETTEXTLENGTHW = 1036;
        public const int SB_SETTEXTW = 1035;
        public const int SB_GETTEXTA = 1026;
        public const int SB_GETTEXTLENGTHA = 1027;
        public const int SB_SETTEXTA = 1025;
        public const int SB_SETMINHEIGHT = 1032;
        public const int SB_SETPARTS = 1028;
        public const int SB_SIMPLE = 1033;

        /* Scroll bar control */
        public const int SBM_ENABLE_ARROWS = 228;
        public const int SBM_GETPOS = 225;
        public const int SBM_GETRANGE = 227;
        public const int SBM_GETSCROLLINFO = 234;
        public const int SBM_SETPOS = 224;
        public const int SBM_SETRANGE = 226;
        public const int SBM_SETRANGEREDRAW = 230;
        public const int SBM_SETSCROLLINFO = 233;

        /* Static control */
        public const int STM_GETICON = 369;
        public const int STM_GETIMAGE = 371;
        public const int STM_SETICON = 368;
        public const int STM_SETIMAGE = 370;

        /* Static control notifications */
        public const int STN_CLICKED = 0;
        public const int STN_DBLCLK = 1;
        public const int STN_DISABLE = 3;
        public const int STN_ENABLE = 2;

        /* Toolbar control */
        public const int TB_ADDBITMAP = 1043;
        public const int TB_ADDBUTTONS = 1044;
        public const int TB_AUTOSIZE = 1057;
        public const int TB_BUTTONCOUNT = 1048;
        public const int TB_BUTTONSTRUCTSIZE = 1054;
        public const int TB_CHANGEBITMAP = 1067;
        public const int TB_CHECKBUTTON = 1026;
        public const int TB_COMMANDTOINDEX = 1049;
        public const int TB_CUSTOMIZE = 1051;
        public const int TB_DELETEBUTTON = 1046;
        public const int TB_ENABLEBUTTON = 1025;
        public const int TB_GETBITMAP = 1068;
        public const int TB_GETBITMAPFLAGS = 1065;
        public const int TB_GETBUTTON = 1047;
        public const int TB_ADDSTRINGW = 1101;
        public const int TB_GETBUTTONTEXTW = 1099;
        public const int TB_SAVERESTOREW = 1100;
        public const int TB_ADDSTRINGA = 1052;
        public const int TB_GETBUTTONTEXTA = 1069;
        public const int TB_SAVERESTOREA = 1050;
        public const int TB_GETITEMRECT = 1053;
        public const int TB_GETROWS = 1064;
        public const int TB_GETSTATE = 1042;
        public const int TB_GETTOOLTIPS = 1059;
        public const int TB_HIDEBUTTON = 1028;
        public const int TB_INDETERMINATE = 1029;
        public const int TB_INSERTBUTTON = 1045;
        public const int TB_ISBUTTONCHECKED = 1034;
        public const int TB_ISBUTTONENABLED = 1033;
        public const int TB_ISBUTTONHIDDEN = 1036;
        public const int TB_ISBUTTONINDETERMINATE = 1037;
        public const int TB_ISBUTTONPRESSED = 1035;
        public const int TB_PRESSBUTTON = 1027;
        public const int TB_SETBITMAPSIZE = 1056;
        public const int TB_SETBUTTONSIZE = 1055;
        public const int TB_SETCMDID = 1066;
        public const int TB_SETPARENT = 1061;
        public const int TB_SETROWS = 1063;
        public const int TB_SETSTATE = 1041;
        public const int TB_SETTOOLTIPS = 1060;

        /* Track bar control */
        public const int TBM_CLEARSEL = 1043;
        public const int TBM_CLEARTICS = 1033;
        public const int TBM_GETCHANNELRECT = 1050;
        public const int TBM_GETLINESIZE = 1048;
        public const int TBM_GETNUMTICS = 1040;
        public const int TBM_GETPAGESIZE = 1046;
        public const int TBM_GETPOS = 1024;
        public const int TBM_GETPTICS = 1038;
        public const int TBM_GETRANGEMAX = 1026;
        public const int TBM_GETRANGEMIN = 1025;
        public const int TBM_GETSELEND = 1042;
        public const int TBM_GETSELSTART = 1041;
        public const int TBM_GETTHUMBLENGTH = 1052;
        public const int TBM_GETTHUMBRECT = 1049;
        public const int TBM_GETTIC = 1027;
        public const int TBM_GETTICPOS = 1039;
        public const int TBM_SETLINESIZE = 1047;
        public const int TBM_SETPAGESIZE = 1045;
        public const int TBM_SETPOS = 1029;
        public const int TBM_SETRANGE = 1030;
        public const int TBM_SETRANGEMAX = 1032;
        public const int TBM_SETRANGEMIN = 1031;
        public const int TBM_SETSEL = 1034;
        public const int TBM_SETSELEND = 1036;
        public const int TBM_SETSELSTART = 1035;
        public const int TBM_SETTHUMBLENGTH = 1051;
        public const int TBM_SETTIC = 1028;
        public const int TBM_SETTICFREQ = 1044;

        /* Tool bar control notifications */
        public const int TBN_FIRST = -700;
        public const int TBN_LAST = -720;
        public const int TBN_BEGINADJUST = -703;
        public const int TBN_BEGINDRAG = -701;
        public const int TBN_CUSTHELP = -709;
        public const int TBN_ENDADJUST = -704;
        public const int TBN_ENDDRAG = -702;
        public const int TBN_GETBUTTONINFOW = -720;
        public const int TBN_GETBUTTONINFOA = -700;
        public const int TBN_QUERYDELETE = -707;
        public const int TBN_QUERYINSERT = -706;
        public const int TBN_RESET = -705;
        public const int TBN_TOOLBARCHANGE = -708;

        /* Tab control */
        public const int TCM_ADJUSTRECT = 4904;
        public const int TCM_DELETEALLITEMS = 4873;
        public const int TCM_DELETEITEM = 4872;
        public const int TCM_GETCURFOCUS = 4911;
        public const int TCM_GETCURSEL = 4875;
        public const int TCM_GETIMAGELIST = 4866;
        public const int TCM_GETITEMW = 4924;
        public const int TCM_INSERTITEMW = 4926;
        public const int TCM_SETITEMW = 4925;
        public const int TCM_GETITEMA = 4869;
        public const int TCM_INSERTITEMA = 4871;
        public const int TCM_SETITEMA = 4870;
        public const int TCM_GETITEMCOUNT = 4868;
        public const int TCM_GETITEMRECT = 4874;
        public const int TCM_GETROWCOUNT = 4908;
        public const int TCM_GETTOOLTIPS = 4909;
        public const int TCM_HITTEST = 4877;
        public const int TCM_REMOVEIMAGE = 4906;
        public const int TCM_SETCURFOCUS = 4912;
        public const int TCM_SETCURSEL = 4876;
        public const int TCM_SETIMAGELIST = 4867;
        public const int TCM_SETITEMEXTRA = 4878;
        public const int TCM_SETITEMSIZE = 4905;
        public const int TCM_SETPADDING = 4907;
        public const int TCM_SETTOOLTIPS = 4910;

        /* Tab control notifications */
        public const int TCN_KEYDOWN = -550;
        public const int TCN_SELCHANGE = -551;
        public const int TCN_SELCHANGING = -552;

        /* Tool tip control */
        public const int TTM_ACTIVATE = 1025;
        public const int TTM_ADDTOOLW = 1074;
        public const int TTM_DELTOOLW = 1075;
        public const int TTM_ENUMTOOLSW = 1082;
        public const int TTM_GETCURRENTTOOLW = 1083;
        public const int TTM_GETTEXTW = 1080;
        public const int TTM_GETTOOLINFOW = 1077;
        public const int TTM_HITTESTW = 1079;
        public const int TTM_NEWTOOLRECTW = 1076;
        public const int TTM_SETTOOLINFOW = 1078;
        public const int TTM_UPDATETIPTEXTW = 1081;
        public const int TTM_ADDTOOLA = 1028;
        public const int TTM_DELTOOLA = 1029;
        public const int TTM_ENUMTOOLSA = 1038;
        public const int TTM_GETCURRENTTOOLA = 1039;
        public const int TTM_GETTEXTA = 1035;
        public const int TTM_GETTOOLINFOA = 1032;
        public const int TTM_HITTESTA = 1034;
        public const int TTM_NEWTOOLRECTA = 1030;
        public const int TTM_SETTOOLINFOA = 1033;
        public const int TTM_UPDATETIPTEXTA = 1036;
        public const int TTM_GETTOOLCOUNT = 1037;
        public const int TTM_RELAYEVENT = 1031;
        public const int TTM_SETDELAYTIME = 1027;
        public const int TTM_WINDOWFROMPOINT = 1040;

        /* Tool tip control notification */
        public const int TTN_NEEDTEXTW = -530;
        public const int TTN_NEEDTEXTA = -520;
        public const int TTN_POP = -522;
        public const int TTN_SHOW = -521;

        /* Tree view control */
        public const int TVM_CREATEDRAGIMAGE = 4370;
        public const int TVM_DELETEITEM = 4353;
        public const int TVM_ENDEDITLABELNOW = 4374;
        public const int TVM_ENSUREVISIBLE = 4372;
        public const int TVM_EXPAND = 4354;
        public const int TVM_GETCOUNT = 4357;
        public const int TVM_GETEDITCONTROL = 4367;
        public const int TVM_GETIMAGELIST = 4360;
        public const int TVM_GETINDENT = 4358;
        public const int TVM_GETITEMRECT = 4356;
        public const int TVM_GETNEXTITEM = 4362;
        public const int TVM_GETVISIBLECOUNT = 4368;
        public const int TVM_HITTEST = 4369;
        public const int TVM_EDITLABELW = 4417;
        public const int TVM_GETISEARCHSTRINGW = 4416;
        public const int TVM_GETITEMW = 4414;
        public const int TVM_INSERTITEMW = 4402;
        public const int TVM_SETITEMW = 4415;
        public const int TVM_EDITLABELA = 4366;
        public const int TVM_GETISEARCHSTRINGA = 4375;
        public const int TVM_GETITEMA = 4364;
        public const int TVM_INSERTITEMA = 4352;
        public const int TVM_SETITEMA = 4365;
        public const int TVM_SELECTITEM = 4363;
        public const int TVM_SETIMAGELIST = 4361;
        public const int TVM_SETINDENT = 4359;
        public const int TVM_SORTCHILDREN = 4371;
        public const int TVM_SORTCHILDRENCB = 4373;

        /* Tree view control notification */
        public const int TVN_KEYDOWN = -412;
        public const int TVN_BEGINDRAGW = -456;
        public const int TVN_BEGINLABELEDITW = -459;
        public const int TVN_BEGINRDRAGW = -457;
        public const int TVN_DELETEITEMW = -458;
        public const int TVN_ENDLABELEDITW = -460;
        public const int TVN_GETDISPINFOW = -452;
        public const int TVN_ITEMEXPANDEDW = -455;
        public const int TVN_ITEMEXPANDINGW = -454;
        public const int TVN_SELCHANGEDW = -451;
        public const int TVN_SELCHANGINGW = -450;
        public const int TVN_SETDISPINFOW = -453;
        public const int TVN_BEGINDRAGA = -407;
        public const int TVN_BEGINLABELEDITA = -410;
        public const int TVN_BEGINRDRAGA = -408;
        public const int TVN_DELETEITEMA = -409;
        public const int TVN_ENDLABELEDITA = -411;
        public const int TVN_GETDISPINFOA = -403;
        public const int TVN_ITEMEXPANDEDA = -406;
        public const int TVN_ITEMEXPANDINGA = -405;
        public const int TVN_SELCHANGEDA = -402;
        public const int TVN_SELCHANGINGA = -401;
        public const int TVN_SETDISPINFOA = -404;

        /* Up/down control */
        public const int UDM_GETACCEL = 1132;
        public const int UDM_GETBASE = 1134;
        public const int UDM_GETBUDDY = 1130;
        public const int UDM_GETPOS = 1128;
        public const int UDM_GETRANGE = 1126;
        public const int UDM_SETACCEL = 1131;
        public const int UDM_SETBASE = 1133;
        public const int UDM_SETBUDDY = 1129;
        public const int UDM_SETPOS = 1127;
        public const int UDM_SETRANGE = 1125;

        /* Up/down control notification */
        public const int UDN_DELTAPOS = -722;

        /* Window messages */

        public const int WM_ACTIVATE = 6;
        public const int WM_ACTIVATEAPP = 28;
        public const int WM_ASKCBFORMATNAME = 780;
        public const int WM_CANCELJOURNAL = 75;
        public const int WM_CANCELMODE = 31;
        public const int WM_CAPTURECHANGED = 533;
        public const int WM_CHANGECBCHAIN = 781;
        public const int WM_CHAR = 258;
        public const int WM_CHARTOITEM = 47;
        public const int WM_CHILDACTIVATE = 34;
        public const int WM_CHOOSEFONT_GETLOGFONT = 1025;
        public const int WM_CHOOSEFONT_SETLOGFONT = 1125;
        public const int WM_CHOOSEFONT_SETFLAGS = 1126;
        public const int WM_CLEAR = 771;
        public const int WM_CLOSE = 16;
        public const int WM_COMMAND = 273;
        public const int WM_COMPACTING = 65;
        public const int WM_COMPAREITEM = 57;
        public const int WM_CONTEXTMENU = 123;
        public const int WM_COPY = 769;
        public const int WM_COPYDATA = 74;
        public const int WM_CREATE = 1;
        public const int WM_CTLCOLORBTN = 309;
        public const int WM_CTLCOLORDLG = 310;
        public const int WM_CTLCOLOREDIT = 307;
        public const int WM_CTLCOLORLISTBOX = 308;
        public const int WM_CTLCOLORMSGBOX = 306;
        public const int WM_CTLCOLORSCROLLBAR = 311;
        public const int WM_CTLCOLORSTATIC = 312;
        public const int WM_CUT = 768;
        public const int WM_DEADCHAR = 259;
        public const int WM_DELETEITEM = 45;
        public const int WM_DESTROY = 2;
        public const int WM_DESTROYCLIPBOARD = 775;
        public const int WM_DEVICECHANGE = 537;
        public const int WM_DEVMODECHANGE = 27;
        public const int WM_DISPLAYCHANGE = 126;
        public const int WM_DRAWCLIPBOARD = 776;
        public const int WM_DRAWITEM = 43;
        public const int WM_DROPFILES = 563;
        public const int WM_ENABLE = 10;
        public const int WM_ENDSESSION = 22;
        public const int WM_ENTERIDLE = 289;
        public const int WM_ENTERMENULOOP = 529;
        public const int WM_ENTERSIZEMOVE = 561;
        public const int WM_ERASEBKGND = 20;
        public const int WM_EXITMENULOOP = 530;
        public const int WM_EXITSIZEMOVE = 562;
        public const int WM_FONTCHANGE = 29;
        public const int WM_GETDLGCODE = 135;
        public const int WM_GETFONT = 49;
        public const int WM_GETHOTKEY = 51;
        public const int WM_GETICON = 127;
        public const int WM_GETMINMAXINFO = 36;
        public const int WM_GETTEXT = 13;
        public const int WM_GETTEXTLENGTH = 14;
        public const int WM_HELP = 83;
        public const int WM_HOTKEY = 786;
        public const int WM_HSCROLL = 276;
        public const int WM_HSCROLLCLIPBOARD = 782;
        public const int WM_ICONERASEBKGND = 39;
        public const int WM_IME_CHAR = 646;
        public const int WM_IME_COMPOSITION = 271;
        public const int WM_IME_COMPOSITIONFULL = 644;
        public const int WM_IME_CONTROL = 643;
        public const int WM_IME_ENDCOMPOSITION = 270;
        public const int WM_IME_KEYDOWN = 656;
        public const int WM_IME_KEYUP = 657;
        public const int WM_IME_NOTIFY = 642;
        public const int WM_IME_SELECT = 645;
        public const int WM_IME_SETCONTEXT = 641;
        public const int WM_IME_STARTCOMPOSITION = 269;
        public const int WM_INITDIALOG = 272;
        public const int WM_INITMENU = 278;
        public const int WM_INITMENUPOPUP = 279;
        public const int WM_INPUTLANGCHANGE = 81;
        public const int WM_INPUTLANGCHANGEREQUEST = 80;
        public const int WM_KEYDOWN = 256;
        public const int WM_KEYUP = 257;
        public const int WM_KILLFOCUS = 8;
        public const int WM_LBUTTONDBLCLK = 515;
        public const int WM_LBUTTONDOWN = 513;
        public const int WM_LBUTTONUP = 514;
        public const int WM_MBUTTONDBLCLK = 521;
        public const int WM_MBUTTONDOWN = 519;
        public const int WM_MBUTTONUP = 520;
        public const int WM_MDIACTIVATE = 546;
        public const int WM_MDICASCADE = 551;
        public const int WM_MDICREATE = 544;
        public const int WM_MDIDESTROY = 545;
        public const int WM_MDIGETACTIVE = 553;
        public const int WM_MDIICONARRANGE = 552;
        public const int WM_MDIMAXIMIZE = 549;
        public const int WM_MDINEXT = 548;
        public const int WM_MDIREFRESHMENU = 564;
        public const int WM_MDIRESTORE = 547;
        public const int WM_MDISETMENU = 560;
        public const int WM_MDITILE = 550;
        public const int WM_MEASUREITEM = 44;
        public const int WM_MENUCHAR = 288;
        public const int WM_MENUSELECT = 287;
        public const int WM_MOUSEACTIVATE = 33;
        public const int WM_MOUSEMOVE = 512;
        public const int WM_MOVE = 3;
        public const int WM_MOVING = 534;
        public const int WM_NCACTIVATE = 134;
        public const int WM_NCCALCSIZE = 131;
        public const int WM_NCCREATE = 129;
        public const int WM_NCDESTROY = 130;
        public const int WM_NCHITTEST = 132;
        public const int WM_NCLBUTTONDBLCLK = 163;
        public const int WM_NCLBUTTONDOWN = 161;
        public const int WM_NCLBUTTONUP = 162;
        public const int WM_NCMBUTTONDBLCLK = 169;
        public const int WM_NCMBUTTONDOWN = 167;
        public const int WM_NCMBUTTONUP = 168;
        public const int WM_NCMOUSEMOVE = 160;
        public const int WM_NCPAINT = 133;
        public const int WM_NCRBUTTONDBLCLK = 166;
        public const int WM_NCRBUTTONDOWN = 164;
        public const int WM_NCRBUTTONUP = 165;
        public const int WM_NEXTDLGCTL = 40;
        public const int WM_NOTIFY = 78;
        public const int WM_NOTIFYFORMAT = 85;
        public const int WM_NULL = 0;
        public const int WM_PAINT = 15;
        public const int WM_PAINTCLIPBOARD = 777;
        public const int WM_PAINTICON = 38;
        public const int WM_PALETTECHANGED = 785;
        public const int WM_PALETTEISCHANGING = 784;
        public const int WM_PARENTNOTIFY = 528;
        public const int WM_PASTE = 770;
        public const int WM_PENWINFIRST = 896;
        public const int WM_PENWINLAST = 911;
        public const int WM_POWER = 72;
        public const int WM_POWERBROADCAST = 536;
        public const int WM_PRINT = 791;
        public const int WM_PRINTCLIENT = 792;
        public const int WM_PSD_ENVSTAMPRECT = 1029;
        public const int WM_PSD_FULLPAGERECT = 1025;
        public const int WM_PSD_GREEKTEXTRECT = 1028;
        public const int WM_PSD_MARGINRECT = 1027;
        public const int WM_PSD_MINMARGINRECT = 1026;
        public const int WM_PSD_PAGESETUPDLG = 1024;
        public const int WM_PSD_YAFULLPAGERECT = 1030;
        public const int WM_QUERYDRAGICON = 55;
        public const int WM_QUERYENDSESSION = 17;
        public const int WM_QUERYNEWPALETTE = 783;
        public const int WM_QUERYOPEN = 19;
        public const int WM_QUEUESYNC = 35;
        public const int WM_QUIT = 18;
        public const int WM_RBUTTONDBLCLK = 518;
        public const int WM_RBUTTONDOWN = 516;
        public const int WM_RBUTTONUP = 517;
        public const int WM_RENDERALLFORMATS = 774;
        public const int WM_RENDERFORMAT = 773;
        public const int WM_SETCURSOR = 32;
        public const int WM_SETFOCUS = 7;
        public const int WM_SETFONT = 48;
        public const int WM_SETHOTKEY = 50;
        public const int WM_SETICON = 128;
        public const int WM_SETREDRAW = 11;
        public const int WM_SETTEXT = 12;
        public const int WM_SETTINGCHANGE = 26;
        public const int WM_SHOWWINDOW = 24;
        public const int WM_SIZE = 5;
        public const int WM_SIZECLIPBOARD = 779;
        public const int WM_SIZING = 532;
        public const int WM_SPOOLERSTATUS = 42;
        public const int WM_STYLECHANGED = 125;
        public const int WM_STYLECHANGING = 124;
        public const int WM_SYSCHAR = 262;
        public const int WM_SYSCOLORCHANGE = 21;
        public const int WM_SYSCOMMAND = 274;
        public const int WM_SYSDEADCHAR = 263;
        public const int WM_SYSKEYDOWN = 260;
        public const int WM_SYSKEYUP = 261;
        public const int WM_TCARD = 82;
        public const int WM_TIMECHANGE = 30;
        public const int WM_TIMER = 275;
        public const int WM_UNDO = 772;
        public const int WM_USER = 1024;
        public const int WM_USERCHANGED = 84;
        public const int WM_VKEYTOITEM = 46;
        public const int WM_VSCROLL = 277;
        public const int WM_VSCROLLCLIPBOARD = 778;
        public const int WM_WINDOWPOSCHANGED = 71;
        public const int WM_WINDOWPOSCHANGING = 70;
        public const int WM_WININICHANGE = 26;

        /* Window message ranges */
        public const int WM_KEYFIRST = 256;
        public const int WM_KEYLAST = 264;
        public const int WM_MOUSEFIRST = 512;
        public const int WM_MOUSELAST = 521;

		/* Scroll Bar stuff */
		// http://codebetter.com/patricksmacchia/2008/07/07/some-richtextbox-tricks/

		private const int SB_HORZ = 0;
		private const int SB_VERT = 0x01; 
		private const int SB_THUMBPOSITION = 4;

        #endregion

    }


    static public class FlashWindow
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static private extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        private struct FLASHWINFO
        {
            /// <summary>
            /// The size of the structure in bytes.
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// The Flash Status.
            /// </summary>
            public uint dwFlags;
            /// <summary>
            /// The number of times to Flash the window.
            /// </summary>
            public uint uCount;
            /// <summary>
            /// The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
            /// </summary>
            public uint dwTimeout;
        }

        /// <summary>
        /// Stop flashing. The system restores the window to its original stae.
        /// </summary>
        public const uint FLASHW_STOP = 0;

        /// <summary>
        /// Flash the window caption.
        /// </summary>
        public const uint FLASHW_CAPTION = 1;

        /// <summary>
        /// Flash the taskbar button.
        /// </summary>
        public const uint FLASHW_TRAY = 2;

        /// <summary>
        /// Flash both the window caption and taskbar button.
        /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
        /// </summary>
        public const uint FLASHW_ALL = 3;

        /// <summary>
        /// Flash continuously, until the FLASHW_STOP flag is set.
        /// </summary>
        public const uint FLASHW_TIMER = 4;

        /// <summary>
        /// Flash continuously until the window comes to the foreground.
        /// </summary>
        public const uint FLASHW_TIMERNOFG = 12;


        ///// <summary>
        ///// Flash the spacified Window (Form) until it recieves focus.
        ///// </summary>
        ///// <param name="form">The Form (Window) to Flash.</param>
        ///// <returns></returns>
        //static public bool Flash(System.Windows.Forms.Form form)
        //{
        //    // Make sure we're running under Windows 2000 or later
        //    if (Win2000OrLater)
        //    {
        //        FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL | FLASHW_TIMERNOFG, uint.MaxValue, 0);
        //        return FlashWindowEx(ref fi);
        //    }
        //    return false;
        //}

        static private FLASHWINFO Create_FLASHWINFO(IntPtr handle, uint flags, uint count, uint timeout)
        {
            FLASHWINFO fi = new FLASHWINFO();
            fi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fi));
            fi.hwnd = handle;
            fi.dwFlags = flags;
            fi.uCount = count;
            fi.dwTimeout = timeout;
            return fi;
        }

        ///// <summary>
        ///// Flash the specified Window (form) for the specified number of times
        ///// </summary>
        ///// <param name="form">The Form (Window) to Flash.</param>
        ///// <param name="count">The number of times to Flash.</param>
        ///// <returns></returns>
        //static public bool Flash(System.Windows.Forms.Form form, uint count)
        //{
        //    if (Win2000OrLater)
        //    {
        //        FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL, count, 0);
        //        return FlashWindowEx(ref fi);
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// Start Flashing the specified Window (form)
        ///// </summary>
        ///// <param name="form">The Form (Window) to Flash.</param>
        ///// <returns></returns>
        //static public bool Start(System.Windows.Forms.Form form)
        //{
        //    if (Win2000OrLater)
        //    {
        //        FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_ALL, uint.MaxValue, 0);
        //        return FlashWindowEx(ref fi);
        //    }
        //    return false;
        //}i had

        ///// <summary>
        ///// Stop Flashing the specified Window (form)
        ///// </summary>
        ///// <param name="form"></param>
        ///// <returns></returns>
        //static public bool Stop(System.Windows.Forms.Form form)
        //{
        //    if (Win2000OrLater)
        //    {
        //        FLASHWINFO fi = Create_FLASHWINFO(form.Handle, FLASHW_STOP, uint.MaxValue, 0);
        //        return FlashWindowEx(ref fi);
        //    }
        //    return false;
        //}

        /// <summary>
        /// Stop Flashing the specified Window (form)
        /// </summary>
        /// <param name="in"></param>
        /// <returns></returns>
        static public bool Stop(int hWnd)
        {
            if (Win2000OrLater)
            {
                FLASHWINFO fi = Create_FLASHWINFO(new IntPtr(hWnd), FLASHW_STOP, uint.MaxValue, 0);
                return FlashWindowEx(ref fi);
            }
            return false;
        }

        /// <summary>
        /// A boolean value indicating whether the application is running on Windows 2000 or later.
        /// </summary>
        static private bool Win2000OrLater
        {
            get { return System.Environment.OSVersion.Version.Major >= 5; }
        }


    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;

        public RECT(System.Drawing.Rectangle Rectangle)
            : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
        {
        }
        public RECT(int Left, int Top, int Right, int Bottom)
        {
            _Left = Left;
            _Top = Top;
            _Right = Right;
            _Bottom = Bottom;
        }

        public int X
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Y
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Left
        {
            get { return _Left; }
            set { _Left = value; }
        }
        public int Top
        {
            get { return _Top; }
            set { _Top = value; }
        }
        public int Right
        {
            get { return _Right; }
            set { _Right = value; }
        }
        public int Bottom
        {
            get { return _Bottom; }
            set { _Bottom = value; }
        }
        public int Height
        {
            get { return _Bottom - _Top; }
            set { _Bottom = value - _Top; }
        }
        public int Width
        {
            get { return _Right - _Left; }
            set { _Right = value + _Left; }
        }
        public Point Location
        {
            get { return new Point(Left, Top); }
            set
            {
                _Left = value.X;
                _Top = value.Y;
            }
        }
        public Size Size
        {
            get { return new Size(Width, Height); }
            set
            {
                _Right = value.Height + _Left;
                _Bottom = value.Height + _Top;
            }
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(this.Left, this.Top, this.Width, this.Height);
        }
        static public Rectangle ToRectangle(RECT Rectangle)
        {
            return Rectangle.ToRectangle();
        }
        static public RECT FromRectangle(Rectangle Rectangle)
        {
            return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
        }

        static public implicit operator Rectangle(RECT Rectangle)
        {
            return Rectangle.ToRectangle();
        }
        static public implicit operator RECT(Rectangle Rectangle)
        {
            return new RECT(Rectangle);
        }
        static public bool operator ==(RECT Rectangle1, RECT Rectangle2)
        {
            return Rectangle1.Equals(Rectangle2);
        }
        static public bool operator !=(RECT Rectangle1, RECT Rectangle2)
        {
            return !Rectangle1.Equals(Rectangle2);
        }

        public override string ToString()
        {
            return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
        }

        public bool Equals(RECT Rectangle)
        {
            return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
        }
        public override bool Equals(object Object)
        {
            if (Object is RECT)
            {
                return Equals((RECT)Object);
            }
            else if (Object is Rectangle)
            {
                return Equals(new RECT((Rectangle)Object));
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Left.GetHashCode() ^ Right.GetHashCode() ^ Top.GetHashCode() ^ Bottom.GetHashCode();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWINFO
    {
        public uint cbSize;
        public RECT rcWindow;
        public RECT rcClient;
        public uint dwStyle;
        public uint dwExStyle;
        public uint dwWindowStatus;
        public uint cxWindowBorders;
        public uint cyWindowBorders;
        public ushort atomWindowType;
        public ushort wCreatorVersion;

        public WINDOWINFO(Boolean? filler)
            : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
        {
            cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
        }
    }
    /// <summary>
    /// Write something here sometime...
    /// </summary>
    public class ClientIdleHandler : IDisposable
    {
        //idle counter
        static public bool bActive = false;
        // hook active or not
        static int hHookKbd = 0;
        static int hHookMouse = 0;

        // the Hook delegate
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        //Declare MouseHookProcedure as HookProc type.
        public event HookProc MouseHookProcedure;
        //Declare KbdHookProcedure as HookProc type.
        public event HookProc KbdHookProcedure;

        //Import for SetWindowsHookEx function.
        //Use this function to install thread-specific hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        static public extern int SetWindowsHookEx(int idHook, HookProc lpfn,
            IntPtr hInstance, int threadId);

        //Import for UnhookWindowsHookEx.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        static public extern bool UnhookWindowsHookEx(int idHook);

        //Import for CallNextHookEx
        //Use this function to pass the hook information to next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        static public extern int CallNextHookEx(int idHook, int nCode,
            IntPtr wParam, IntPtr lParam);

        //Added all the hook types here just for reference
        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        static public int MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //user is active, at least with the mouse
            bActive = true;
            
            //just return the next hook
            return CallNextHookEx(hHookMouse, nCode, wParam, lParam);
        }


        static public int KbdHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //user is active, at least with the mouse
            bActive = true;

            //just return the next hook
            return CallNextHookEx(hHookKbd, nCode, wParam, lParam);
        }

        public void Start()
        {
            if (hHookMouse == 0)
            {
                // Create an instance of HookProc.
                MouseHookProcedure = new HookProc(MouseHookProc);
                // Create an instance of HookProc.
                KbdHookProcedure = new HookProc(KbdHookProc);

                //register a global hook
                hHookMouse = SetWindowsHookEx((int)HookType.WH_MOUSE_LL,
                    MouseHookProcedure,
                    (System.IntPtr)Marshal.GetHINSTANCE(
                    System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);
                //If SetWindowsHookEx fails.
                if (hHookMouse == 0)
                {
                    Close();
                    throw new ApplicationException("SetWindowsHookEx() failed");
                }
            }

            if (hHookKbd == 0)
            {
                //register a global hook
                hHookKbd = SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL,
                    KbdHookProcedure,
                    (System.IntPtr)Marshal.GetHINSTANCE(
                    System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);
                //If SetWindowsHookEx fails.
                if (hHookKbd == 0)
                {
                    Close();
                    throw new ApplicationException("SetWindowsHookEx() failed");
                }
            }
        }

        public void Close()
        {
            if (hHookMouse != 0)
            {
                bool ret = UnhookWindowsHookEx(hHookMouse);
                //If UnhookWindowsHookEx fails.
                if (ret == false)
                {
                    throw new ApplicationException("UnhookWindowsHookEx() failed");
                }
                hHookMouse = 0;
            }

            if (hHookKbd != 0)
            {
                bool ret = UnhookWindowsHookEx(hHookKbd);
                //If UnhookWindowsHookEx fails.
                if (ret == false)
                {
                    throw new ApplicationException("UnhookWindowsHookEx() failed");
                }
                hHookKbd = 0;
            }
        }

        public ClientIdleHandler()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region IDisposable Members

        public void Dispose()
        {
            if (hHookMouse != 0 || hHookKbd != 0)
                Close();
        }

        #endregion
    }
}

/*

#undef FAR
#undef  NEAR
#define FAR                 far
#define NEAR                near
#ifndef CONST
#define CONST               const
#endif

typedef unsigned long       DWORD;
typedef int                 BOOL;
typedef unsigned char       BYTE;
typedef unsigned short      WORD;
typedef float               FLOAT;
typedef FLOAT               *PFLOAT;
typedef BOOL near           *PBOOL;
typedef BOOL far            *LPBOOL;
typedef BYTE near           *PBYTE;
typedef BYTE far            *LPBYTE;
typedef int near            *PINT;
typedef int far             *LPINT;
typedef WORD near           *PWORD;
typedef WORD far            *LPWORD;
typedef long far            *LPLONG;
typedef DWORD near          *PDWORD;
typedef DWORD far           *LPDWORD;
typedef void far            *LPVOID;
typedef CONST void far      *LPCVOID;

typedef int                 INT;
typedef unsigned int        UINT;
typedef unsigned int        *PUINT;

#ifndef NT_INCLUDED


#include <specstrings.h>

typedef UINT_PTR            WPARAM;
typedef LONG_PTR            LPARAM;
typedef LONG_PTR            LRESULT;
typedef _W64 long LONG_PTR, *PLONG_PTR;
typedef _W64 unsigned int UINT_PTR, *PUINT_PTR;


#ifndef NOMINMAX

#ifndef max
#define max(a,b)            (((a) > (b)) ? (a) : (b))
#endif

#ifndef min
#define min(a,b)            (((a) < (b)) ? (a) : (b))
#endif

#endif 

#define MAKEWORD(a, b)      ((WORD)(((BYTE)(((DWORD_PTR)(a)) & 0xff)) | ((WORD)((BYTE)(((DWORD_PTR)(b)) & 0xff))) << 8))
#define MAKELONG(a, b)      ((LONG)(((WORD)(((DWORD_PTR)(a)) & 0xffff)) | ((DWORD)((WORD)(((DWORD_PTR)(b)) & 0xffff))) << 16))
#define LOWORD(l)           ((WORD)(((DWORD_PTR)(l)) & 0xffff))
#define HIWORD(l)           ((WORD)((((DWORD_PTR)(l)) >> 16) & 0xffff))
#define LOBYTE(w)           ((BYTE)(((DWORD_PTR)(w)) & 0xff))
#define HIBYTE(w)           ((BYTE)((((DWORD_PTR)(w)) >> 8) & 0xff))


typedef unsigned short UHALF_PTR, *PUHALF_PTR;
typedef short HALF_PTR, *PHALF_PTR;
typedef _W64 long SHANDLE_PTR;
typedef _W64 unsigned long HANDLE_PTR;

    typedef _W64 int INT_PTR, *PINT_PTR;
    typedef _W64 unsigned int UINT_PTR, *PUINT_PTR;

    typedef _W64 long LONG_PTR, *PLONG_PTR;
    typedef _W64 unsigned long ULONG_PTR, *PULONG_PTR;

    #define __int64   __int
#define _W64 __w64

    typedef _W64 int INT_PTR, *PINT_PTR;
    typedef _W64 unsigned int UINT_PTR, *PUINT_PTR;

    typedef _W64 long LONG_PTR, *PLONG_PTR;
    typedef _W64 unsigned long ULONG_PTR, *PULONG_PTR;

    #define __int64   __int
*/
