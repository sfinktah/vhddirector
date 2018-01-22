Imports System.Drawing
Imports System.Windows.Forms
Imports System.Reflection
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Configuration

'--
'-- Generic UNHANDLED error handling class
'--
'-- Intended as a last resort for errors which crash our application, so we can get feedback on what
'-- caused the error.
'-- 
'-- To use: UnhandledExceptionManager.AddHandler() in the STARTUP of your application
'--
'-- more background information on Exceptions at:
'--   http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
'--
'--
'-- Jeff Atwood
'-- http://www.codinghorror.com
'--
Public Class UnhandledExceptionManager

    Private Sub New()
        ' to keep this class from being creatable as an instance.
    End Sub

    Private Shared _blnLogToEventLog As Boolean
    Private Shared _blnLogToFile As Boolean
    Private Shared _blnLogToEmail As Boolean
    Private Shared _blnLogToScreenshot As Boolean
    Private Shared _blnLogToUI As Boolean

    Private Shared _blnLogToFileOK As Boolean
    Private Shared _blnLogToEmailOK As Boolean
    Private Shared _blnLogToScreenshotOK As Boolean
    Private Shared _blnLogToEventLogOK As Boolean

    Private Shared _blnEmailIncludeScreenshot As Boolean
    Private Shared _ScreenshotImageFormat As Imaging.ImageFormat = Imaging.ImageFormat.Png
    Private Shared _strScreenshotFullPath As String
    Private Shared _strLogFullPath As String

    Private Shared _blnConsoleApp As Boolean
    Private Shared _objParentAssembly As System.Reflection.Assembly = Nothing
    Private Shared _strException As String
    Private Shared _strExceptionType As String

    Private Shared _blnIgnoreDebugErrors As Boolean
    Private Shared _blnKillAppOnException As Boolean

    Private Const _strLogName As String = "UnhandledExceptionLog.txt"
    Private Const _strScreenshotName As String = "UnhandledException"
    Private Const _strClassName As String = "UnhandledExceptionManager"

#Region "Properties"

    Public Shared Property IgnoreDebugErrors() As Boolean
        Get
            Return _blnIgnoreDebugErrors
        End Get
        Set(ByVal Value As Boolean)
            _blnIgnoreDebugErrors = Value
        End Set
    End Property

    Public Shared Property DisplayDialog() As Boolean
        Get
            Return _blnLogToUI
        End Get
        Set(ByVal Value As Boolean)
            _blnLogToUI = Value
        End Set
    End Property

    Public Shared Property EmailScreenshot() As Boolean
        Get
            Return _blnEmailIncludeScreenshot
        End Get
        Set(ByVal Value As Boolean)
            _blnEmailIncludeScreenshot = Value
        End Set
    End Property

    Public Shared Property KillAppOnException() As Boolean
        Get
            Return _blnKillAppOnException
        End Get
        Set(ByVal Value As Boolean)
            _blnKillAppOnException = Value
        End Set
    End Property

    Public Shared Property ScreenshotImageFormat() As Imaging.ImageFormat
        Get
            Return _ScreenshotImageFormat
        End Get
        Set(ByVal Value As Imaging.ImageFormat)
            _ScreenshotImageFormat = Value
        End Set
    End Property

    Public Shared Property LogToFile() As Boolean
        Get
            Return _blnLogToFile
        End Get
        Set(ByVal Value As Boolean)
            _blnLogToFile = Value
        End Set
    End Property

    Public Shared Property LogToEventLog() As Boolean
        Get
            Return _blnLogToEventLog
        End Get
        Set(ByVal Value As Boolean)
            _blnLogToEventLog = Value
        End Set
    End Property

    Public Shared Property SendEmail() As Boolean
        Get
            Return _blnLogToEmail
        End Get
        Set(ByVal Value As Boolean)
            _blnLogToEmail = Value
        End Set
    End Property

    Public Shared Property TakeScreenshot() As Boolean
        Get
            Return _blnLogToScreenshot
        End Get
        Set(ByVal Value As Boolean)
            _blnLogToScreenshot = Value
        End Set
    End Property

#End Region

#Region "win32api screenshot calls"

    '--
    '-- Windows API calls necessary to support screen capture
    '--
    Private Declare Function BitBlt Lib "gdi32" Alias "BitBlt" _
        (ByVal hDestDC As Integer, ByVal x As Integer, _
        ByVal y As Integer, ByVal nWidth As Integer, _
        ByVal nHeight As Integer, ByVal hSrcDC As Integer, _
        ByVal xSrc As Integer, ByVal ySrc As Integer, _
        ByVal dwRop As Integer) As Integer

    Private Declare Function GetDC Lib "user32" Alias "GetDC" _
        (ByVal hwnd As Integer) As Integer

    Private Declare Function ReleaseDC Lib "user32" Alias "ReleaseDC" _
        (ByVal hwnd As Integer, ByVal hdc As Integer) As Integer
#End Region

    Private Shared Function ParentAssembly() As System.Reflection.Assembly
        If _objParentAssembly Is Nothing Then
            If System.Reflection.Assembly.GetEntryAssembly Is Nothing Then
                _objParentAssembly = System.Reflection.Assembly.GetCallingAssembly
            Else
                _objParentAssembly = System.Reflection.Assembly.GetEntryAssembly
            End If
        End If
        Return _objParentAssembly
    End Function

    '--
    '-- load some settings that may optionally be present in our .config file
    '-- if they aren't present, we get the defaults as defined here
    '--
    Private Shared Sub LoadConfigSettings()
        SendEmail = GetConfigBoolean("SendEmail", True)
        TakeScreenshot = GetConfigBoolean("TakeScreenshot", True)
        EmailScreenshot = GetConfigBoolean("EmailScreenshot", True)
        LogToEventLog = GetConfigBoolean("LogToEventLog", False)
        LogToFile = GetConfigBoolean("LogToFile", True)
        DisplayDialog = GetConfigBoolean("DisplayDialog", True)
        IgnoreDebugErrors = GetConfigBoolean("IgnoreDebug", True)
        KillAppOnException = GetConfigBoolean("KillAppOnException", True)
    End Sub

    '--
    '-- This *MUST* be called early in your application to set up global error handling
    '--
    Public Shared Sub [AddHandler](Optional ByVal blnConsoleApp As Boolean = False)
        '-- attempt to load optional settings from .config file
        LoadConfigSettings()

        '-- we don't need an unhandled exception handler if we are running inside
        '-- the vs.net IDE; it is our "unhandled exception handler" in that case
        If _blnIgnoreDebugErrors Then
            If Debugger.IsAttached Then Return
        End If

        '-- track the parent assembly that set up error handling
        '-- need to call this NOW so we set it appropriately; otherwise
        '-- we may get the wrong assembly at exception time!
        ParentAssembly()

        '-- for winforms applications
        RemoveHandler Application.ThreadException, AddressOf ThreadExceptionHandler
        AddHandler Application.ThreadException, AddressOf ThreadExceptionHandler

        '-- for console applications
        RemoveHandler System.AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionHandler
        AddHandler System.AppDomain.CurrentDomain.UnhandledException, AddressOf UnhandledExceptionHandler

        '-- I cannot find a good way to programatically detect a console app, so that must be specified.
        _blnConsoleApp = blnConsoleApp

    End Sub

    '--
    '-- handles Application.ThreadException event
    '--
    Private Shared Sub ThreadExceptionHandler(ByVal sender As System.Object, ByVal e As System.Threading.ThreadExceptionEventArgs)
        GenericExceptionHandler(e.Exception)
    End Sub


    '--
    '-- handles AppDomain.CurrentDoamin.UnhandledException event
    '--
    Private Shared Sub UnhandledExceptionHandler(ByVal sender As System.Object, ByVal args As UnhandledExceptionEventArgs)
        Dim objException As Exception = CType(args.ExceptionObject, Exception)
        GenericExceptionHandler(objException)
    End Sub

    '--
    '-- exception-safe file attrib retrieval; we don't care if this fails
    '--
    Private Shared Function AssemblyFileTime(ByVal objAssembly As System.Reflection.Assembly) As DateTime
        Try
            Return System.IO.File.GetLastWriteTime(objAssembly.Location)
        Catch ex As Exception
            Return DateTime.MaxValue
        End Try
    End Function

    '--
    '-- returns build datetime of assembly
    '-- assumes default assembly value in AssemblyInfo:
    '-- <Assembly: AssemblyVersion("1.0.*")> 
    '--
    '-- filesystem create time is used, if revision and build were overridden by user
    '--
    Private Shared Function AssemblyBuildDate(ByVal objAssembly As System.Reflection.Assembly, _
                                       Optional ByVal blnForceFileDate As Boolean = False) As DateTime
        Dim objVersion As System.Version = objAssembly.GetName.Version
        Dim dtBuild As DateTime

        If blnForceFileDate Then
            dtBuild = AssemblyFileTime(objAssembly)
        Else
            dtBuild = CType("01/01/2000", DateTime). _
                AddDays(objVersion.Build). _
                AddSeconds(objVersion.Revision * 2)
            If TimeZone.IsDaylightSavingTime(DateTime.Now, TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year)) Then
                dtBuild = dtBuild.AddHours(1)
            End If
            If dtBuild > DateTime.Now Or objVersion.Build < 730 Or objVersion.Revision = 0 Then
                dtBuild = AssemblyFileTime(objAssembly)
            End If
        End If

        Return dtBuild
    End Function

    '--
    '-- turns a single stack frame object into an informative string
    '--
    Private Shared Function StackFrameToString(ByVal sf As StackFrame) As String
        Dim sb As New System.Text.StringBuilder
        Dim intParam As Integer
        Dim mi As MemberInfo = sf.GetMethod

        With sb
            '-- build method name
            .Append("   ")
            .Append(mi.DeclaringType.Namespace)
            .Append(".")
            .Append(mi.DeclaringType.Name)
            .Append(".")
            .Append(mi.Name)

            '-- build method params
            Dim objParameters() As ParameterInfo = sf.GetMethod.GetParameters()
            Dim objParameter As ParameterInfo
            .Append("(")
            intParam = 0
            For Each objParameter In objParameters
                intParam += 1
                If intParam > 1 Then .Append(", ")
                .Append(objParameter.Name)
                .Append(" As ")
                .Append(objParameter.ParameterType.Name)
            Next
            .Append(")")
            .Append(Environment.NewLine)

            '-- if source code is available, append location info
            .Append("       ")
            If sf.GetFileName Is Nothing OrElse sf.GetFileName.Length = 0 Then
                .Append(System.IO.Path.GetFileName(ParentAssembly.CodeBase))
                '-- native code offset is always available
                .Append(": N ")
                .Append(String.Format("{0:#00000}", sf.GetNativeOffset))

            Else
                .Append(System.IO.Path.GetFileName(sf.GetFileName))
                .Append(": line ")
                .Append(String.Format("{0:#0000}", sf.GetFileLineNumber))
                .Append(", col ")
                .Append(String.Format("{0:#00}", sf.GetFileColumnNumber))
                '-- if IL is available, append IL location info
                If sf.GetILOffset <> StackFrame.OFFSET_UNKNOWN Then
                    .Append(", IL ")
                    .Append(String.Format("{0:#0000}", sf.GetILOffset))
                End If
            End If
            .Append(Environment.NewLine)
        End With
        Return sb.ToString
    End Function

    '--
    '-- enhanced stack trace generator
    '--
    Private Overloads Shared Function EnhancedStackTrace(ByVal objStackTrace As StackTrace, _
        Optional ByVal strSkipClassName As String = "") As String
        Dim intFrame As Integer

        Dim sb As New System.Text.StringBuilder

        sb.Append(Environment.NewLine)
        sb.Append("---- Stack Trace ----")
        sb.Append(Environment.NewLine)

        For intFrame = 0 To objStackTrace.FrameCount - 1
            Dim sf As StackFrame = objStackTrace.GetFrame(intFrame)
            Dim mi As MemberInfo = sf.GetMethod

            If strSkipClassName <> "" AndAlso mi.DeclaringType.Name.IndexOf(strSkipClassName) > -1 Then
                '-- don't include frames with this name
            Else
                sb.Append(StackFrameToString(sf))
            End If
        Next
        sb.Append(Environment.NewLine)

        Return sb.ToString
    End Function

    '--
    '-- enhanced stack trace generator (exception)
    '--
    Private Overloads Shared Function EnhancedStackTrace(ByVal objException As Exception) As String
        Dim objStackTrace As New StackTrace(objException, True)
        Return EnhancedStackTrace(objStackTrace)
    End Function

    '--
    '-- enhanced stack trace generator (no params)
    '--
    Private Overloads Shared Function EnhancedStackTrace() As String
        Dim objStackTrace As New StackTrace(True)
        Return EnhancedStackTrace(objStackTrace, "ExceptionManager")
    End Function

    '--
    '-- generic exception handler; the various specific handlers all call into this sub
    '--
    Private Shared Sub GenericExceptionHandler(ByVal objException As Exception)

        '-- turn the exception into an informative string
        Try
            _strException = ExceptionToString(objException)
            _strExceptionType = objException.GetType.FullName
        Catch ex As Exception
            _strException = "Error '" & ex.Message & "' while generating exception string"
            _strExceptionType = ""
        End Try

        If Not _blnConsoleApp Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        End If

        '-- log this error to various locations
        Try
            '-- screenshot takes around 1 second
            If _blnLogToScreenshot Then ExceptionToScreenshot()
            '-- event logging takes < 100ms
            If _blnLogToEventLog Then ExceptionToEventLog()
            '-- textfile logging takes < 50ms
            If _blnLogToFile Then ExceptionToFile()
            '-- email takes under 1 second
            If _blnLogToEmail Then ExceptionToEmail()
        Catch ex As Exception
            '-- generic catch because any exceptions inside the UEH
            '-- will cause the code to terminate immediately
        End Try

        If Not _blnConsoleApp Then
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
        '-- display message to the user
        If _blnLogToUI Then ExceptionToUI()

        If _blnKillAppOnException Then
            KillApp()
            Application.Exit()
        End If

    End Sub

    '--
    '-- This is in a private routine for .NET security reasons
    '-- if this line of code is in a sub, the entire sub is tagged as full trust
    '--
    Private Shared Sub KillApp()
        System.Diagnostics.Process.GetCurrentProcess.Kill()
    End Sub

    '--
    '-- turns exception into something an average user can hopefully
    '-- understand; still very technical
    '--
    Private Shared Function FormatExceptionForUser(ByVal blnConsoleApp As Boolean) As String
        Dim objStringBuilder As New System.Text.StringBuilder
        Dim strBullet As String
        If blnConsoleApp Then
            strBullet = "-"
        Else
            strBullet = "•"
        End If

        With objStringBuilder
            If Not blnConsoleApp Then
                .Append("The development team was automatically notified of this problem. ")
                .Append("If you need immediate assistance, contact (contact).")
            End If
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            .Append("The following information about the error was automatically captured: ")
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            If _blnLogToScreenshot Then
                .Append(" ")
                .Append(strBullet)
                .Append(" ")
                If _blnLogToScreenshotOK Then
                    .Append("a screenshot was taken of the desktop at:")
                    .Append(Environment.NewLine)
                    .Append("   ")
                    .Append(_strScreenshotFullPath)
                Else
                    .Append("a screenshot could NOT be taken of the desktop.")
                End If
                .Append(Environment.NewLine)
            End If
            If _blnLogToEventLog Then
                .Append(" ")
                .Append(strBullet)
                .Append(" ")
                If _blnLogToEventLogOK Then
                    .Append("an event was written to the application log")
                Else
                    .Append("an event could NOT be written to the application log")
                End If
                .Append(Environment.NewLine)
            End If
            If _blnLogToFile Then
                .Append(" ")
                .Append(strBullet)
                .Append(" ")
                If _blnLogToFileOK Then
                    .Append("details were written to a text log at:")
                Else
                    .Append("details could NOT be written to the text log at:")
                End If
                .Append(Environment.NewLine)
                .Append("   ")
                .Append(_strLogFullPath)
                .Append(Environment.NewLine)
            End If
            If _blnLogToEmail Then
                .Append(" ")
                .Append(strBullet)
                .Append(" ")
                .Append("attempted to send an email to: ")
                .Append(Environment.NewLine)
                .Append("   ")
                .Append(GetConfigString("EmailTo"))
                .Append(Environment.NewLine)
            End If
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            .Append("Detailed error information follows:")
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            .Append(_strException)
        End With
        Return objStringBuilder.ToString
    End Function

    '--
    '-- display a dialog to the user; otherwise we just terminate with no alert at all!
    '--
    Private Shared Sub ExceptionToUI()

        Const _strWhatHappened As String = "There was an unexpected error in (app). This may be due to a programming bug."
        Dim _strHowUserAffected As String
        Const _strWhatUserCanDo As String = "Restart (app), and try repeating your last action. Try alternative methods of performing the same action."

        If UnhandledExceptionManager.KillAppOnException Then
            _strHowUserAffected = "When you click OK, (app) will close."
        Else
            _strHowUserAffected = "The action you requested was not performed."
        End If

        If Not _blnConsoleApp Then
            '-- don't send ANOTHER email if we are already doing so!
            HandledExceptionManager.EmailError = Not SendEmail
            '-- pop the dialog
            HandledExceptionManager.ShowDialog(_strWhatHappened, _
                _strHowUserAffected, _
                _strWhatUserCanDo, _
                FormatExceptionForUser(False), _
                MessageBoxButtons.OK, MessageBoxIcon.Stop)
        Else
            '-- note that writing to console pauses for ENTER
            '-- otherwise console window just terminates immediately
            ExceptionToConsole()
        End If
    End Sub
    '--
    '-- for non-web hosted apps, returns:
    '--   "[path]\bin\YourAssemblyName."
    '-- for web hosted apps, returns URL with non-filesystem chars removed:
    '--   "c:\http___domain\path\YourAssemblyName."
    Private Shared Function GetApplicationPath() As String
        If ParentAssembly.CodeBase.StartsWith("http://") Then
            Return "c:\" + Regex.Replace(ParentAssembly.CodeBase(), "[\/\\\:\*\?\""\<\>\|]", "_") + "."
        Else
            Return System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName + "."
        End If
    End Function


    '--
    '-- take a desktop screenshot of our exception
    '-- note that this fires BEFORE the user clicks on the OK dismissing the crash dialog
    '-- so the crash dialog itself will not be displayed
    '--
    Private Shared Sub ExceptionToScreenshot()
        '-- note that screenshotname does NOT include the file type extension
        Try
            TakeScreenshotPrivate(GetApplicationPath() + _strScreenshotName)
            _blnLogToScreenshotOK = True
        Catch ex As Exception
            _blnLogToScreenshotOK = False
        End Try
    End Sub

    '-- 
    '-- write an exception to the Windows NT event log
    '--
    Private Shared Sub ExceptionToEventLog()
        Try
            System.Diagnostics.EventLog.WriteEntry(System.AppDomain.CurrentDomain.FriendlyName, _
                Environment.NewLine + _strException, _
                EventLogEntryType.Error)
            _blnLogToEventLogOK = True
        Catch ex As Exception
            _blnLogToEventLogOK = False
        End Try
    End Sub

    '-- 
    '-- write an exception to the console
    '--
    Private Shared Sub ExceptionToConsole()
        Console.WriteLine("This application encountered an unexpected problem.")
        Console.WriteLine(FormatExceptionForUser(True))
        Console.WriteLine("The application must now terminate. Press ENTER to continue...")
        Console.ReadLine()
    End Sub
    '--
    '-- write an exception to a text file
    '--
    Private Shared Sub ExceptionToFile()
        _strLogFullPath = GetApplicationPath() + _strLogName
        Try
            Dim objStreamWriter As New IO.StreamWriter(_strLogFullPath, True)
            objStreamWriter.Write(_strException)
            objStreamWriter.WriteLine()
            objStreamWriter.Close()
            _blnLogToFileOK = True
        Catch ex As Exception
            _blnLogToFileOK = False
        End Try
    End Sub


    '--
    '-- this is the code that executes in the spawned thread
    '--
    Private Shared Sub ThreadHandler()
        Dim objMail As New SimpleMail.SMTPClient
        Dim objMailMessage As New SimpleMail.SMTPMailMessage
        With objMailMessage
            .To = GetConfigString("EmailTo", "")
            .Subject = "Unhandled Exception notification - " & _strExceptionType
            .Body = _strException
            If _blnLogToScreenshot And _blnEmailIncludeScreenshot Then
                .AttachmentPath = _strScreenshotFullPath
            End If
        End With
        Try
            objMail.SendMail(objMailMessage)
            _blnLogToEmailOK = True
        Catch e As Exception
            _blnLogToEmailOK = False
            '-- don't do anything; sometimes SMTP isn't available, which generates an exception
            '-- and an exception in the unhandled exception manager.. is bad news.
            '--MsgBox("exception email failed to send:" + Environment.Newline + Environment.Newline + e.Message)
        End Try
    End Sub

    '--
    '-- send an exception via email
    '--
    Private Shared Sub ExceptionToEmail()
        '-- spawn off the email send attempt as a thread for improved throughput
        Dim objThread As New Thread(New ThreadStart(AddressOf ThreadHandler))
        objThread.Name = "SendExceptionEmail"
        objThread.Start()
    End Sub

    '--
    '-- exception-safe WindowsIdentity.GetCurrent retrieval returns "domain\username"
    '-- per MS, this sometimes randomly fails with "Access Denied" particularly on NT4
    '--
    Private Shared Function CurrentWindowsIdentity() As String
        Try
            Return System.Security.Principal.WindowsIdentity.GetCurrent.Name()
        Catch ex As Exception
            Return ""
        End Try
    End Function
    '--
    '-- exception-safe "domain\username" retrieval from Environment
    '--
    Private Shared Function CurrentEnvironmentIdentity() As String
        Try
            Return System.Environment.UserDomainName + "\" + System.Environment.UserName
        Catch ex As Exception
            Return ""
        End Try
    End Function
    '--
    '-- retrieve identity with fallback on error to safer method
    '--
    Private Shared Function UserIdentity() As String
        Dim strTemp As String
        strTemp = CurrentWindowsIdentity()
        If strTemp = "" Then
            strTemp = CurrentEnvironmentIdentity()
        End If
        Return strTemp
    End Function


    '--
    '-- gather some system information that is helpful to diagnosing
    '-- exception
    '--
    Friend Shared Function SysInfoToString(Optional ByVal blnIncludeStackTrace As Boolean = False) As String
        Dim objStringBuilder As New System.Text.StringBuilder

        With objStringBuilder

            .Append("Date and Time:         ")
            .Append(DateTime.Now)
            .Append(Environment.NewLine)

            .Append("Machine Name:          ")
            Try
                .Append(Environment.MachineName)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("IP Address:            ")
            .Append(GetCurrentIP)
            .Append(Environment.NewLine)

            .Append("Current User:          ")
            .Append(UserIdentity)
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)

            .Append("Application Domain:    ")
            Try
                .Append(System.AppDomain.CurrentDomain.FriendlyName())
            Catch e As Exception
                .Append(e.Message)
            End Try


            .Append(Environment.NewLine)
            .Append("Assembly Codebase:     ")
            Try
                .Append(ParentAssembly.CodeBase())
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Assembly Full Name:    ")
            Try
                .Append(ParentAssembly.FullName)
            Catch e As Exception
                .Append(e.message)
            End Try
            .Append(Environment.NewLine)

            .Append("Assembly Version:      ")
            Try
                .Append(ParentAssembly.GetName().Version().ToString)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Assembly Build Date:   ")
            Try
                .Append(AssemblyBuildDate(ParentAssembly).ToString)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)

            If blnIncludeStackTrace Then
                .Append(EnhancedStackTrace())
            End If

        End With

        Return objStringBuilder.ToString
    End Function


    '--
    '-- translate exception object to string, with additional system info
    '--
    Friend Shared Function ExceptionToString(ByVal objException As Exception) As String
        Dim objStringBuilder As New System.Text.StringBuilder

        If Not (objException.InnerException Is Nothing) Then
            '-- sometimes the original exception is wrapped in a more relevant outer exception
            '-- the detail exception is the "inner" exception
            '-- see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
            With objStringBuilder
                .Append("(Inner Exception)")
                .Append(Environment.NewLine)
                .Append(ExceptionToString(objException.InnerException))
                .Append(Environment.NewLine)
                .Append("(Outer Exception)")
                .Append(Environment.NewLine)
            End With
        End If
        With objStringBuilder
            '-- get general system and app information
            .Append(SysInfoToString)

            '-- get exception-specific information
            .Append("Exception Source:      ")
            Try
                .Append(objException.Source)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Exception Type:        ")
            Try
                .Append(objException.GetType.FullName)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Exception Message:     ")
            Try
                .Append(objException.Message)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            .Append("Exception Target Site: ")
            Try
                .Append(objException.TargetSite.Name)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)

            Try
                Dim x As String = EnhancedStackTrace(objException)
                .Append(x)
            Catch e As Exception
                .Append(e.Message)
            End Try
            .Append(Environment.NewLine)
        End With

        Return objStringBuilder.ToString
    End Function


    '--
    '-- returns ImageCodecInfo for the specified MIME type
    '--
    Private Shared Function GetEncoderInfo(ByVal strMimeType As String) As Imaging.ImageCodecInfo
        Dim j As Integer
        Dim objImageCodecInfo() As Imaging.ImageCodecInfo
        objImageCodecInfo = Imaging.ImageCodecInfo.GetImageEncoders()

        j = 0
        While j < objImageCodecInfo.Length
            If objImageCodecInfo(j).MimeType = strMimeType Then
                Return objImageCodecInfo(j)
            End If
            j += 1
        End While

        Return Nothing
    End Function


    '--
    '-- save bitmap object to JPEG of specified quality level
    '--
    Private Shared Sub BitmapToJPEG(ByVal objBitmap As Bitmap, ByVal strFilename As String, Optional ByVal lngCompression As Long = 75)
        Dim objEncoderParameters As New Imaging.EncoderParameters(1)
        Dim objImageCodecInfo As Imaging.ImageCodecInfo = GetEncoderInfo("image/jpeg")

        objEncoderParameters.Param(0) = New Imaging.EncoderParameter(Imaging.Encoder.Quality, lngCompression)
        objBitmap.Save(strFilename, objImageCodecInfo, objEncoderParameters)
    End Sub


    '--
    '-- takes a screenshot of the desktop and saves to filename and format specified
    '--
    Private Shared Sub TakeScreenshotPrivate(ByVal strFilename As String)
        Dim objRectangle As Rectangle = Screen.PrimaryScreen.Bounds
        Dim objBitmap As New Bitmap(objRectangle.Right, objRectangle.Bottom)
        Dim objGraphics As Graphics
        Dim hdcDest As IntPtr
        Dim hdcSrc As Integer
        Const SRCCOPY As Integer = &HCC0020
        Dim strFormatExtension As String

        objGraphics = objGraphics.FromImage(objBitmap)

        '-- get a device context to the windows desktop and our destination  bitmaps
        hdcSrc = GetDC(0)
        hdcDest = objGraphics.GetHdc
        '-- copy what is on the desktop to the bitmap
        BitBlt(hdcDest.ToInt32, 0, 0, objRectangle.Right, objRectangle.Bottom, hdcSrc, 0, 0, SRCCOPY)
        '-- release device contexts
        objGraphics.ReleaseHdc(hdcDest)
        ReleaseDC(0, hdcSrc)

        strFormatExtension = _ScreenshotImageFormat.ToString.ToLower
        If System.IO.Path.GetExtension(strFilename) <> "." + strFormatExtension Then
            strFilename += "." + strFormatExtension
        End If
        Select Case strFormatExtension
            Case "jpeg"
                BitmapToJPEG(objBitmap, strFilename, 80)
            Case Else
                objBitmap.Save(strFilename, _ScreenshotImageFormat)
        End Select

        '-- save the complete path/filename of the screenshot for possible later use
        _strScreenshotFullPath = strFilename
    End Sub

    '--
    '-- get IP address of this machine
    '-- not an ideal method for a number of reasons (guess why!)
    '-- but the alternatives are very ugly
    '--
    Private Shared Function GetCurrentIP() As String
        Try
            Dim strIP As String = _
                System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName).AddressList(0).ToString
            Return strIP
        Catch ex As Exception
            Return "127.0.0.1"
        End Try
    End Function

    Const _strKeyNotPresent As String = "The key <{0}> is not present in the <appSettings> section of .config file"
    Const _strKeyError As String = "Error {0} retrieving key <{1}> from <appSettings> section of .config file"

    '--
    '-- Returns the specified String value from the application .config file,
    '-- with many fail-safe checks (exceptions, key not present, etc)
    '--
    '-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
    '-- 
    Private Shared Function GetConfigString(ByVal strKey As String, Optional ByVal strDefault As String = Nothing) As String
        Try
            Dim strTemp As String = CType(ConfigurationSettings.AppSettings.Get(_strClassName & "/" & strKey), String)
            If strTemp = Nothing Then
                If strDefault Is Nothing Then
                    Return String.Format(_strKeyNotPresent, _strClassName & "/" & strKey)
                Else
                    Return strDefault
                End If
            Else
                Return strTemp
            End If
        Catch ex As Exception
            If strDefault Is Nothing Then
                Return String.Format(_strKeyError, ex.Message, _strClassName & "/" & strKey)
            Else
                Return strDefault
            End If
        End Try
    End Function

    '--
    '-- Returns the specified boolean value from the application .config file,
    '-- with many fail-safe checks (exceptions, key not present, etc)
    '--
    '-- this is important in an *unhandled exception handler*, because any unhandled exceptions will simply exit!
    '-- 
    Private Shared Function GetConfigBoolean(ByVal strKey As String, Optional ByVal blnDefault As Boolean = Nothing) As Boolean
        Dim strTemp As String
        Try
            strTemp = ConfigurationSettings.AppSettings.Get(_strClassName & "/" & strKey)
        Catch ex As Exception
            If blnDefault = Nothing Then
                Return False
            Else
                Return blnDefault
            End If
        End Try

        If strTemp = Nothing Then
            If blnDefault = Nothing Then
                Return False
            Else
                Return blnDefault
            End If
        Else
            Select Case strTemp.ToLower
                Case "1", "true"
                    Return True
                Case Else
                    Return False
            End Select
        End If
    End Function

End Class