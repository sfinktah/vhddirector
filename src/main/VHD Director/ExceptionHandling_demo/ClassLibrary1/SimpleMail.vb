Imports System.Net
Imports System.Net.Sockets
Imports System.Threading

'--
'-- a simple class for trivial SMTP mail support
'-- 
'-- basic features:
'--
'--   ** trivial SMTP implementation
'--   ** HTML body
'--   ** plain text body
'--   ** one file attachment
'--   ** basic retry mechanism
'--
'-- Jeff Atwood
'-- http://www.codinghorror.com
'--
Public Class SimpleMail

    Public Class SMTPMailMessage
        Public From As String
        Public [To] As String
        Public Subject As String
        Public Body As String
        Public BodyHTML As String
        Public AttachmentPath As String
    End Class

    Public Class SMTPClient

        Private Const _intBufferSize As Integer = 1024
        Private Const _intResponseTimeExpected As Integer = 10
        Private Const _intResponseTimeMax As Integer = 750
        Private Const _strAddressSeperator As String = ";"
        Private Const _intMaxRetries As Integer = 5        
        Private Const _blnDebugMode As Boolean = False
        Private Const _blnPlainTextOnly As Boolean = False

        Private _strDefaultDomain As String = "domain.com"
        Private _strServer As String = "mailserver.domain.com"
        Private _intPort As Integer = 25

        Private _strUserName As String = ""
        Private _strUserPassword As String = ""

        Private _intRetries As Integer = 1
        Private _strLastResponse As String

        Public Property AuthUser() As String
            Get
                Return _strUserName
            End Get
            Set(ByVal Value As String)
                _strUserName = Value
            End Set
        End Property

        Public Property AuthPassword() As String
            Get
                Return _strUserPassword
            End Get
            Set(ByVal Value As String)
                _strUserPassword = Value
            End Set
        End Property

        Public Property Port() As Integer
            Get
                Return _intPort
            End Get
            Set(ByVal Value As Integer)
                _intPort = Value
            End Set
        End Property

        Public Property Server() As String
            Get
                Return _strServer
            End Get
            Set(ByVal Value As String)
                _strServer = Value
            End Set
        End Property

        Public Property DefaultDomain() As String
            Get
                Return _strDefaultDomain
            End Get
            Set(ByVal Value As String)
                _strDefaultDomain = Value
            End Set
        End Property

        '-- 
        '-- send data over the current network connection
        '--
        Private Sub SendData(ByVal tcp As TcpClient, ByVal strData As String)
            Dim objNetworkStream As NetworkStream = tcp.GetStream()
            Dim bytWriteBuffer(strData.Length) As Byte
            Dim en As New System.Text.UTF8Encoding

            bytWriteBuffer = en.GetBytes(strData)
            objNetworkStream.Write(bytWriteBuffer, 0, bytWriteBuffer.Length)
        End Sub

        '--
        '-- get data from the current network connection
        '--
        Private Function GetData(ByVal tcp As TcpClient) As String
            Dim objNetworkStream As System.Net.Sockets.NetworkStream = tcp.GetStream()

            If objNetworkStream.DataAvailable Then
                Dim bytReadBuffer() As Byte
                Dim intStreamSize As Integer
                bytReadBuffer = New Byte(_intBufferSize) {}
                intStreamSize = objNetworkStream.Read(bytReadBuffer, 0, bytReadBuffer.Length)
                Dim en As New System.Text.UTF8Encoding
                Return en.GetString(bytReadBuffer)
            Else
                Return ""
            End If
        End Function

        '--
        '-- issue a required SMTP command
        '--
        Private Sub Command(ByVal tcp As TcpClient, ByVal strCommand As String, _
            Optional ByVal strExpectedResponse As String = "250")

            If Not CommandInternal(tcp, strCommand, strExpectedResponse) Then
                tcp.Close()
                Throw New Exception("SMTP server at " & _strServer.ToString & ":" & _intPort.ToString + _
                    " was provided command '" & strCommand & _
                    "', but did not return the expected response '" & strExpectedResponse & "':" _
                    + Environment.NewLine + _strLastResponse)
            End If

        End Sub

        '--
        '-- issue a SMTP command
        '--
        Private Function CommandInternal(ByVal tcp As TcpClient, ByVal strCommand As String, _
            Optional ByVal strExpectedResponse As String = "250") As Boolean

            Dim intResponseTime As Integer

            '-- send the command over the socket with a trailing cr/lf
            If strCommand.Length > 0 Then
                SendData(tcp, strCommand & Environment.NewLine)
            End If

            '-- wait until we get a response, or time out
            _strLastResponse = ""
            intResponseTime = 0
            Do While (_strLastResponse = "") And (intResponseTime <= _intResponseTimeMax)
                intResponseTime += _intResponseTimeExpected
                _strLastResponse = GetData(tcp)
                Thread.CurrentThread.Sleep(_intResponseTimeExpected)
            Loop

            '-- this is helpful for debugging SMTP problems
            If _blnDebugMode Then
                Debug.WriteLine("SMTP >> " & strCommand & " (after " & intResponseTime.ToString & "ms)")
                Debug.WriteLine("SMTP << " & _strLastResponse)
            End If

            '-- if we have a response, check the first 10 characters for the expected response code
            If _strLastResponse = "" Then
                If _blnDebugMode Then
                    Debug.WriteLine("** EXPECTED RESPONSE " & strExpectedResponse & " NOT RETURNED **")
                End If
                Return False
            Else
                Return (_strLastResponse.IndexOf(strExpectedResponse, 0, 10) <> -1)
            End If
        End Function

        '--
        '-- send mail with integrated retry mechanism
        '--
        Public Function SendMail(ByVal mail As SMTPMailMessage) As Boolean
            Dim intRetryInterval As Integer = 333
            Try
                SendMailInternal(mail)
            Catch ex As Exception
                _intRetries += 1
                If _intRetries <= _intMaxRetries Then
                    Thread.CurrentThread.Sleep(intRetryInterval)
                    SendMail(mail)
                Else
                    Throw
                End If
            End Try
            'Console.WriteLine("sent after " & _intRetries.ToString)
            _intRetries = 1
            Return True
        End Function

        '--
        '-- send an email via trivial SMTP
        '--
        Private Sub SendMailInternal(ByVal mail As SMTPMailMessage)
            Dim iphost As IPHostEntry
            Dim tcp As New TcpClient

            '-- resolve server text name to an IP address
            Try
                iphost = Dns.GetHostByName(_strServer)
            Catch e As Exception
                Throw New Exception("Unable to resolve server name " & _strServer, e)
            End Try

            '-- attempt to connect to the server by IP address and port number
            Try
                tcp.Connect(iphost.AddressList(0), _intPort)
            Catch e As Exception
                Throw New Exception("Unable to connect to SMTP server at " & _strServer.ToString & ":" & _intPort.ToString, e)
            End Try

            '-- make sure we get the SMTP welcome message
            Command(tcp, "", "220")
            Command(tcp, "HELO " & Environment.MachineName)

            '--
            '-- authenticate if we have username and password
            '-- http://www.ietf.org/rfc/rfc2554.txt
            '--
            If Len(_strUserName & _strUserPassword) > 0 Then
                Command(tcp, "auth login", "334 VXNlcm5hbWU6") 'VXNlcm5hbWU6=base64'Username:'
                Command(tcp, ToBase64(_strUserName), "334 UGFzc3dvcmQ6") 'UGFzc3dvcmQ6=base64'Password:'
                Command(tcp, ToBase64(_strUserPassword), "235")
            End If

            If mail.From = "" Then
                mail.From = System.AppDomain.CurrentDomain.FriendlyName.ToLower & "@" & Environment.MachineName.ToLower & "." & _strDefaultDomain
            End If
            Command(tcp, "MAIL FROM: <" & mail.From & ">")

            '-- send email to more than one recipient
            Dim arRecipients() As String = mail.To.Split(_strAddressSeperator.ToCharArray)
            Dim strRecipient As String
            For Each strRecipient In arRecipients
                Command(tcp, "RCPT TO: <" & strRecipient & ">")
            Next

            Command(tcp, "DATA", "354")

            Dim objStringBuilder As New Text.StringBuilder
            With objStringBuilder
                '-- write common email headers
                .Append("To: " & mail.To + Environment.NewLine)
                .Append("From: " & mail.From + Environment.NewLine)
                .Append("Subject: " & mail.Subject + Environment.NewLine)

                If _blnPlainTextOnly Then
                    '-- write plain text body
                    .Append(Environment.NewLine + mail.Body + Environment.NewLine)
                Else
                    Dim strContentType As String
                    '-- typical case; mixed content will be displayed side-by-side
                    strContentType = "multipart/mixed"
                    '-- unusual case; text and HTML body are both included, let the reader determine which it can handle
                    If mail.Body <> "" And mail.BodyHTML <> "" Then
                        strContentType = "multipart/alternative"
                    End If

                    .Append("MIME-Version: 1.0" & Environment.NewLine)
                    .Append("Content-Type: " & strContentType & "; boundary=""NextMimePart""" & Environment.NewLine)
                    .Append("Content-Transfer-Encoding: 7bit" & Environment.NewLine)
                    ' -- default content (for non-MIME compliant email clients, should be extremely rare)
                    .Append("This message is in MIME format. Since your mail reader does not understand " & Environment.NewLine)
                    .Append("this format, some or all of this message may not be legible." & Environment.NewLine)
                    '-- handle text body (if any)
                    If mail.Body <> "" Then
                        .Append(Environment.NewLine & "--NextMimePart" & Environment.NewLine)
                        .Append("Content-Type: text/plain;" & Environment.NewLine)
                        .Append(Environment.NewLine + mail.Body + Environment.NewLine)
                    End If
                    ' -- handle HTML body (if any)
                    If mail.BodyHTML <> "" Then
                        .Append(Environment.NewLine & "--NextMimePart" & Environment.NewLine)
                        .Append("Content-Type: text/html; charset=iso-8859-1" & Environment.NewLine)
                        .Append(Environment.NewLine + mail.BodyHTML + Environment.NewLine)
                    End If
                    '-- handle attachment (if any)
                    If mail.AttachmentPath <> "" Then
                        .Append(FileToMimeString(mail.AttachmentPath))
                    End If
                End If
                '-- <crlf>.<crlf> marks end of message content
                .Append(Environment.NewLine & "." & Environment.NewLine)
            End With

            Command(tcp, objStringBuilder.ToString)
            Command(tcp, "QUIT", "")
            tcp.Close()
        End Sub

        '--
        '-- turn a file into a base 64 string
        '--
        Private Function FileToMimeString(ByVal strFilepath As String) As String

            Dim objFilestream As System.IO.FileStream
            Dim objStringBuilder As New Text.StringBuilder
            '-- note that chunk size is equal to maximum line width
            Const intChunkSize As Integer = 75
            Dim bytRead(intChunkSize) As Byte
            Dim intRead As Integer
            Dim strFilename As String

            '-- get just the filename out of the path
            strFilename = System.IO.Path.GetFileName(strFilepath)

            With objStringBuilder
                .Append(Environment.NewLine & "--NextMimePart" & Environment.NewLine)
                .Append("Content-Type: application/octet-stream; name=""" & strFilename & """" & Environment.NewLine)
                .Append("Content-Transfer-Encoding: base64" & Environment.NewLine)
                .Append("Content-Disposition: attachment; filename=""" & strFilename & """" & Environment.NewLine)
                .Append(Environment.NewLine)
            End With

            objFilestream = New System.IO.FileStream(strFilepath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            intRead = objFilestream.Read(bytRead, 0, intChunkSize)
            Do While intRead > 0
                objStringBuilder.Append(System.Convert.ToBase64String(bytRead, 0, intRead))
                objStringBuilder.Append(Environment.NewLine)
                intRead = objFilestream.Read(bytRead, 0, intChunkSize)
            Loop
            objFilestream.Close()

            Return objStringBuilder.ToString
        End Function

        Private Shared Function ToBase64(ByVal data As String) As String
            Dim Encoder As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
            Return Convert.ToBase64String(Encoder.GetBytes(data))
        End Function

    End Class

End Class