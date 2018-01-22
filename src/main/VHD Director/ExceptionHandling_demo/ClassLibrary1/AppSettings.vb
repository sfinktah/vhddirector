Imports System.Configuration
Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions

'--
'-- Class for returning general settings related to our Application, such as..
'--
'--  ** .config file settings
'--  ** runtime version
'--  ** application version
'--  ** version and build date
'--  ** whether we're being debugged or not
'--  ** our security zone
'--  ** our path and filename
'--  ** any command line arguments we have
'--
'--
'-- Jeff Atwood
'-- http://www.codinghorror.com
'--
Public Class AppSettings

    Private Shared _strAppBase As String
    Private Shared _strConfigPath As String
    Private Shared _strSecurityZone As String
    Private Shared _strRuntimeVersion As String
    Private Shared _objCommandLineArgs As Specialized.NameValueCollection = Nothing
    Private Shared _objAssemblyAttribs As Specialized.NameValueCollection = Nothing

    Private Sub New()
        ' to keep this class from being creatable as an instance.
    End Sub

#Region "Properties"

    Public Shared ReadOnly Property DebugMode() As Boolean
        Get
            If CommandLineArgs.Item("debug") Is Nothing Then
                Return System.Diagnostics.Debugger.IsAttached
            Else
                Return True
            End If
        End Get
    End Property

    Public Shared ReadOnly Property AppBuildDate() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("BuildDate")
        End Get
    End Property

    Public Shared ReadOnly Property AppProduct() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("Product")
        End Get
    End Property

    Public Shared ReadOnly Property AppCompany() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("Company")
        End Get
    End Property

    Public Shared ReadOnly Property AppCopyright() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("Copyright")
        End Get
    End Property

    Public Shared ReadOnly Property AppDescription() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("Description")
        End Get
    End Property

    Public Shared ReadOnly Property AppTitle() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("Title")
        End Get
    End Property

    Public Shared ReadOnly Property AppFileName() As String
        Get
            Return Regex.Match(AppPath, "[^/]*.(exe|dll)", RegexOptions.IgnoreCase).ToString
        End Get
    End Property

    Public Shared ReadOnly Property AppPath() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("CodeBase")
        End Get
    End Property

    Public Shared ReadOnly Property AppFullName() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("FullName")
        End Get
    End Property

    Public Shared ReadOnly Property CommandLineArgs() As Specialized.NameValueCollection
        Get
            If _objCommandLineArgs Is Nothing Then
                _objCommandLineArgs = GetCommandLineArgs()
            End If
            Return _objCommandLineArgs
        End Get
    End Property

    Public Shared ReadOnly Property CommandLineHelpRequested() As Boolean
        Get
            If _objCommandLineArgs Is Nothing Then
                _objCommandLineArgs = GetCommandLineArgs()
            End If
            If Not _objCommandLineArgs.HasKeys Then
                Return False
            End If

            Const strHelpRegEx As String = "^(help|\?)"

            Dim strKey As String
            For Each strKey In _objCommandLineArgs.AllKeys()
                If Regex.IsMatch(strKey, strHelpRegEx, RegexOptions.IgnoreCase) Then
                    Return True
                End If
                If Regex.IsMatch(_objCommandLineArgs(strKey), strHelpRegEx, RegexOptions.IgnoreCase) Then
                    Return True
                End If
            Next
            Return False
        End Get
    End Property

    Public Shared ReadOnly Property RuntimeVersion() As String
        Get
            If _strRuntimeVersion Is Nothing Then
                '-- returns 1.0.3705.288; we don't want that much detail
                _strRuntimeVersion = Regex.Match(System.Environment.Version.ToString, "\d+.\d+.\d+").ToString
            End If
            Return _strRuntimeVersion
        End Get
    End Property

    Public Shared ReadOnly Property AppVersion() As String
        Get
            If _objAssemblyAttribs Is Nothing Then
                _objAssemblyAttribs = GetAssemblyAttribs()
            End If
            Return _objAssemblyAttribs("Version")
        End Get
    End Property

    Public Shared ReadOnly Property ConfigPath() As String
        Get
            If _strConfigPath Is Nothing Then
                _strConfigPath = Convert.ToString(System.AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE"))
            End If
            Return _strConfigPath
        End Get
    End Property

    Public Shared ReadOnly Property AppBase() As String
        Get
            If _strAppBase Is Nothing Then
                _strAppBase = Convert.ToString(System.AppDomain.CurrentDomain.GetData("APPBASE"))
            End If
            Return _strAppBase
        End Get
    End Property

    Public Shared ReadOnly Property SecurityZone() As String
        Get
            If _strSecurityZone Is Nothing Then
                _strSecurityZone = System.Security.Policy.Zone.CreateFromUrl(AppBase).SecurityZone.ToString
            End If
            Return _strSecurityZone
        End Get
    End Property

#End Region

    Private Shared Function GetEntryAssembly() As Reflection.Assembly
        If System.Reflection.Assembly.GetEntryAssembly Is Nothing Then
            Return System.Reflection.Assembly.GetCallingAssembly
        Else
            Return System.Reflection.Assembly.GetEntryAssembly
        End If
    End Function

    '--
    '-- returns string name / string value pair of all attribs
    '-- for specified assembly
    '--
    '-- note that Assembly* values are pulled from AssemblyInfo file in project folder
    '--
    '-- Product         = AssemblyProduct string
    '-- Copyright       = AssemblyCopyright string
    '-- Company         = AssemblyCompany string
    '-- Description     = AssemblyDescription string
    '-- Title           = AssemblyTitle string
    '--
    Private Shared Function GetAssemblyAttribs() As Specialized.NameValueCollection
        Dim objAttributes() As Object
        Dim objAttribute As Object
        Dim strAttribName As String
        Dim strAttribValue As String
        Dim objNameValueCollection As New Specialized.NameValueCollection
        Dim objAssembly As System.Reflection.Assembly = GetEntryAssembly()

        objAttributes = objAssembly.GetCustomAttributes(False)
        For Each objAttribute In objAttributes
            strAttribName = objAttribute.GetType().ToString()
            strAttribValue = ""
            Select Case strAttribName
                Case "System.Reflection.AssemblyTrademarkAttribute"
                    strAttribName = "Trademark"
                    strAttribValue = CType(objAttribute, AssemblyTrademarkAttribute).Trademark.ToString
                Case "System.Reflection.AssemblyProductAttribute"
                    strAttribName = "Product"
                    strAttribValue = CType(objAttribute, AssemblyProductAttribute).Product.ToString
                Case "System.Reflection.AssemblyCopyrightAttribute"
                    strAttribName = "Copyright"
                    strAttribValue = CType(objAttribute, AssemblyCopyrightAttribute).Copyright.ToString
                Case "System.Reflection.AssemblyCompanyAttribute"
                    strAttribName = "Company"
                    strAttribValue = CType(objAttribute, AssemblyCompanyAttribute).Company.ToString
                Case "System.Reflection.AssemblyTitleAttribute"
                    strAttribName = "Title"
                    strAttribValue = CType(objAttribute, AssemblyTitleAttribute).Title.ToString
                Case "System.Reflection.AssemblyDescriptionAttribute"
                    strAttribName = "Description"
                    strAttribValue = CType(objAttribute, AssemblyDescriptionAttribute).Description.ToString
                Case Else
                    'Console.WriteLine(strAttribName)
            End Select
            If strAttribValue <> "" Then
                If objNameValueCollection.Item(strAttribName) = "" Then
                    objNameValueCollection.Add(strAttribName, strAttribValue)
                End If
            End If
        Next

        '-- add some extra values that are not in the AssemblyInfo, but nice to have
        With objNameValueCollection
            .Add("CodeBase", objAssembly.CodeBase.Replace("file:///", ""))
            .Add("BuildDate", AssemblyBuildDate(objAssembly).ToString)
            .Add("Version", objAssembly.GetName.Version.ToString)
            .Add("FullName", objAssembly.FullName)
        End With

        '-- we must have certain assembly keys to proceed.
        If objNameValueCollection.Item("Product") Is Nothing Then
            Throw New MissingFieldException("The AssemblyInfo file for the assembly " & objAssembly.GetName.Name & " must have the <Assembly:AssemblyProduct()> key populated.")
        End If
        If objNameValueCollection.Item("Company") Is Nothing Then
            Throw New MissingFieldException("The AssemblyInfo file for the assembly " & objAssembly.GetName.Name & " must have the <Assembly: AssemblyCompany()>  key populated.")
        End If

        Return objNameValueCollection
    End Function

    '--
    '--
    '-- when this app is loaded via URL, it is possible to pass in "command line arguments" like so:
    '--
    '-- http://localhost/App.Website/App.Loader.exe?a=1&b=2&c=3
    '--
    '-- string[] args = {
    '--     "C:\WINDOWS\Microsoft.NET\Framework\v1.0.3705\IEExec", 
    '--     "http://localhost/WebCommandLine/App.Loader.exe?a=1&b=2&c=3", 
    '--     "3", "1",  "86474707A3C6F63616C686F6374710000000"};
    '--
    '--
    Private Shared Sub GetURLCommandLineArgs(ByVal strURL As String, _
        ByRef objNameValueCollection As Specialized.NameValueCollection)

        Dim objMatchCollection As MatchCollection
        Dim objMatch As Match

        '-- http://localhost/App.Website/App.Loader.exe?a=1&b=2&c=apple
        '-- a = 1
        '-- b = 2
        '-- c = apple
        objMatchCollection = Regex.Matches(strURL, "(?<Key>[^=#&?]+)=(?<Value>[^=#&]*)")
        For Each objMatch In objMatchCollection
            objNameValueCollection.Add(objMatch.Groups("Key").ToString, objMatch.Groups("Value").ToString)
        Next
    End Sub

    Private Shared Function IsURL(ByVal strAny As String) As Boolean
        Return strAny.IndexOf("&") > -1 _
            OrElse strAny.StartsWith("?") _
            OrElse strAny.ToLower.StartsWith("http://")
    End Function

    Private Shared Function RemoveArgPrefix(ByVal strArg As String) As String
        If strArg.StartsWith("-") Or strArg.StartsWith("/") Then
            Return strArg.Substring(1)
        Else
            Return strArg
        End If
    End Function

    '--
    '-- breaks space delimited command line arguments into key value pairs, if they exist
    '--
    '-- App.Loader.exe -remoting=0 /sample=yes c=true
    '-- remoting = 0
    '-- sample   = yes
    '-- c        = true
    '--
    Private Shared Function GetKeyValueCommandLineArg(ByVal strArg As String, _
        ByRef objNameValueCollection As Specialized.NameValueCollection) As Boolean

        Dim objMatchCollection As MatchCollection
        Dim objMatch As Match

        objMatchCollection = Regex.Matches(strArg, "(?<Key>^[^=]+)=(?<Value>[^= ]*$)")
        If objMatchCollection.Count = 0 Then
            Return False
        Else
            For Each objMatch In objMatchCollection
                objNameValueCollection.Add(RemoveArgPrefix(objMatch.Groups("Key").ToString), _
                    objMatch.Groups("Value").ToString)
            Next
            Return True
        End If
    End Function

    '--
    '-- parses command line arguments, handling special case when app was launched via URL
    '-- note that the default .GetCommandLineArgs is SPACE DELIMITED !
    '--
    Private Shared Function GetCommandLineArgs() As Specialized.NameValueCollection
        Dim strArgs() As String = Environment.GetCommandLineArgs()
        Dim objNameValueCollection As New Specialized.NameValueCollection

        If strArgs.Length > 0 Then
            '--
            '-- handles typical case where app was launched via local .EXE
            '--
            Dim strArg As String
            Dim intArg As Integer = 0
            For Each strArg In strArgs
                If IsURL(strArg) Then
                    GetURLCommandLineArgs(strArg, objNameValueCollection)
                Else
                    If Not GetKeyValueCommandLineArg(strArg, objNameValueCollection) Then
                        objNameValueCollection.Add("arg" & intArg, RemoveArgPrefix(strArg))
                        intArg += 1
                    End If
                End If
            Next
        End If

        Return objNameValueCollection
    End Function

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
            If TimeZone.IsDaylightSavingTime(dtBuild, TimeZone.CurrentTimeZone.GetDaylightChanges(dtBuild.Year)) Then
                dtBuild = dtBuild.AddHours(1)
            End If
            If dtBuild > DateTime.Now Or objVersion.Build < 730 Or objVersion.Revision = 0 Then
                dtBuild = AssemblyFileTime(objAssembly)
            End If
        End If

        Return dtBuild
    End Function

    '-- Returns the specified application value as a boolean
    '-- True values: 1, True, true
    '-- False values: anything else
    Public Shared Function GetBoolean(ByVal key As String) As Boolean
        Dim strTemp As String = ConfigurationSettings.AppSettings.Get(key)
        If strTemp = Nothing Then
            Return False
        Else
            Select Case strTemp.ToLower
                Case "1", "true"
                    Return True
                Case Else
                    Return False
            End Select
        End If
    End Function

    '-- Returns the specified String value from the application .config file
    Public Shared Function GetString(ByVal key As String) As String
        Dim strTemp As String = CType(ConfigurationSettings.AppSettings.Get(key), String)
        If strTemp = Nothing Then
            Return ""
        Else
            Return strTemp
        End If
    End Function


    '--
    '-- Returns the specified Integer value from the application .config file
    '--
    Public Shared Function GetInteger(ByVal key As String) As Integer
        Dim intTemp As Integer = CType(ConfigurationSettings.AppSettings.Get(key), Integer)
        If intTemp = Nothing Then
            Return 0
        Else
            Return intTemp
        End If
    End Function

End Class