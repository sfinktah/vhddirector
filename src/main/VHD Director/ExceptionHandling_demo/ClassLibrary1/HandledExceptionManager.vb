Imports System.Windows.Forms
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Configuration

'--
'-- Generic HANDLED error handling class
'--
'-- It's like MessageBox, but specific to handled exceptions, and supports email notifications
'--
'-- Jeff Atwood
'-- http://www.codinghorror.com
'--
Public Class HandledExceptionManager

    Private Shared _blnHaveException As Boolean = False
    Private Shared _blnEmailError As Boolean = True
    Private Shared _strEmailBody As String
    Private Shared _strExceptionType As String
    Private Const _strDefaultMore As String = "No further information is available. If the problem persists, contact (contact)."

    Public Shared Property EmailError() As Boolean
        Get
            Return _blnEmailError
        End Get
        Set(ByVal Value As Boolean)
            _blnEmailError = Value
        End Set
    End Property

    Public Enum UserErrorDefaultButton
        [Default] = 0
        Button1 = 1
        Button2 = 2
        Button3 = 3
    End Enum

    '-- 
    '-- replace generic constants in strings with specific values
    '--
    Private Shared Function ReplaceStringVals(ByVal strOutput As String) As String
        Dim strTemp As String
        If strOutput Is Nothing Then
            strTemp = ""
        Else
            strTemp = strOutput
        End If
        strTemp = strTemp.Replace("(app)", AppSettings.AppProduct)
        strTemp = strTemp.Replace("(contact)", AppSettings.GetString("UnhandledExceptionManager/ContactInfo"))
        Return strTemp
    End Function

    '--
    '-- make sure "More" text is populated with something useful
    '--
    Private Shared Function GetDefaultMore(ByVal strMoreDetails As String) As String
        If strMoreDetails = "" Then
            Dim objStringBuilder As New System.Text.StringBuilder
            With objStringBuilder
                .Append(_strDefaultMore)
                .Append(Environment.NewLine)
                .Append(Environment.NewLine)
                .Append("Basic technical information follows: " + Environment.NewLine)
                .Append("---" + Environment.NewLine)
                .Append(UnhandledExceptionManager.SysInfoToString(True))
            End With
            Return objStringBuilder.ToString
        Else
            Return strMoreDetails
        End If
    End Function

    '--
    '-- converts exception to a formatted "more" string
    '--
    Private Shared Function ExceptionToMore(ByVal objException As System.Exception) As String
        Dim sb As New System.Text.StringBuilder
        With sb
            If _blnEmailError Then
                .Append("Information about this problem was automatically mailed to ")
                .Append(AppSettings.GetString("UnhandledExceptionManager/EmailTo"))
                .Append(Environment.NewLine + Environment.NewLine)
            End If
            .Append("Detailed technical information follows: " + Environment.NewLine)
            .Append("---" + Environment.NewLine)
            Dim x As String = UnhandledExceptionManager.ExceptionToString(objException)
            .Append(x)
        End With
        Return sb.ToString
    End Function

    '--
    '-- perform our string replacements for (app) and (contact), etc etc
    '-- also make sure More has default values if it is blank.
    '--       
    Private Shared Sub ProcessStrings(ByRef strWhatHappened As String, _
        ByRef strHowUserAffected As String, _
        ByRef strWhatUserCanDo As String, _
        ByRef strMoreDetails As String)

        strWhatHappened = ReplaceStringVals(strWhatHappened)
        strHowUserAffected = ReplaceStringVals(strHowUserAffected)
        strWhatUserCanDo = ReplaceStringVals(strWhatUserCanDo)
        strMoreDetails = ReplaceStringVals(GetDefaultMore(strMoreDetails))
    End Sub

    '-- 
    '-- simplest possible error dialog
    '--
    Public Overloads Shared Function ShowDialog(ByVal strWhatHappened As String, _
                                                ByVal strHowUserAffected As String, _
                                                ByVal strWhatUserCanDo As String) As DialogResult
        _blnHaveException = False
        Return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, UserErrorDefaultButton.Default)
    End Function

    '--
    '-- advanced error dialog with Exception object
    '--
    Public Overloads Shared Function ShowDialog(ByVal strWhatHappened As String, _
        ByVal strHowUserAffected As String, _
        ByVal strWhatUserCanDo As String, _
        ByVal objException As System.Exception, _
        Optional ByVal Buttons As MessageBoxButtons = MessageBoxButtons.OK, _
        Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.Warning, _
        Optional ByVal DefaultButton As UserErrorDefaultButton = UserErrorDefaultButton.Default) As DialogResult

        _blnHaveException = True
        _strExceptionType = objException.GetType.FullName
        Return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, _
            ExceptionToMore(objException), Buttons, Icon, DefaultButton)
    End Function


    '--
    '-- advanced error dialog with More string
    '-- leave "more" string blank to get the default
    '--
    Public Overloads Shared Function ShowDialog(ByVal strWhatHappened As String, _
        ByVal strHowUserAffected As String, _
        ByVal strWhatUserCanDo As String, _
        ByVal strMoreDetails As String, _
        Optional ByVal Buttons As MessageBoxButtons = MessageBoxButtons.OK, _
        Optional ByVal Icon As MessageBoxIcon = MessageBoxIcon.Warning, _
        Optional ByVal DefaultButton As UserErrorDefaultButton = UserErrorDefaultButton.Default _
        ) As DialogResult

        _blnHaveException = False
        Return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, strMoreDetails, _
            Buttons, Icon, DefaultButton)
    End Function

    '-- 
    '-- internal method to show error dialog
    '--
    Private Shared Function ShowDialogInternal(ByVal strWhatHappened As String, _
                ByVal strHowUserAffected As String, _
                ByVal strWhatUserCanDo As String, _
                ByVal strMoreDetails As String, _
                ByVal Buttons As MessageBoxButtons, _
                ByVal Icon As MessageBoxIcon, _
                ByVal DefaultButton As UserErrorDefaultButton) As DialogResult

        '-- set default values, etc
        ProcessStrings(strWhatHappened, strHowUserAffected, strWhatUserCanDo, strMoreDetails)

        Dim objForm As New ExceptionDialog
        With objForm
            .Text = ReplaceStringVals(objForm.Text)
            .ErrorBox.Text = strWhatHappened
            .ScopeBox.Text = strHowUserAffected
            .ActionBox.Text = strWhatUserCanDo
            .txtMore.Text = strMoreDetails
        End With

        '-- determine what button text, visibility, and defaults are
        With objForm
            Select Case Buttons
                Case MessageBoxButtons.AbortRetryIgnore
                    .btn1.Text = "&Abort"
                    .btn2.Text = "&Retry"
                    .btn3.Text = "&Ignore"
                    .AcceptButton = objForm.btn2
                    .CancelButton = objForm.btn3
                Case MessageBoxButtons.OK
                    .btn3.Text = "OK"
                    .btn2.Visible = False
                    .btn1.Visible = False
                    .AcceptButton = objForm.btn3
                Case MessageBoxButtons.OKCancel
                    .btn3.Text = "Cancel"
                    .btn2.Text = "OK"
                    .btn1.Visible = False
                    .AcceptButton = objForm.btn2
                    .CancelButton = objForm.btn3
                Case MessageBoxButtons.RetryCancel
                    .btn3.Text = "Cancel"
                    .btn2.Text = "&Retry"
                    .btn1.Visible = False
                    .AcceptButton = objForm.btn2
                    .CancelButton = objForm.btn3
                Case MessageBoxButtons.YesNo
                    .btn3.Text = "&No"
                    .btn2.Text = "&Yes"
                    .btn1.Visible = False
                Case MessageBoxButtons.YesNoCancel
                    .btn3.Text = "Cancel"
                    .btn2.Text = "&No"
                    .btn1.Text = "&Yes"
                    .CancelButton = objForm.btn3
            End Select
        End With

        '-- set the proper dialog icon
        Select Case Icon
            Case MessageBoxIcon.Error
                objForm.PictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap
            Case MessageBoxIcon.Stop
                objForm.PictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap
            Case MessageBoxIcon.Exclamation
                objForm.PictureBox1.Image = System.Drawing.SystemIcons.Exclamation.ToBitmap
            Case MessageBoxIcon.Information
                objForm.PictureBox1.Image = System.Drawing.SystemIcons.Information.ToBitmap
            Case MessageBoxIcon.Question
                objForm.PictureBox1.Image = System.Drawing.SystemIcons.Question.ToBitmap
            Case Else
                objForm.PictureBox1.Image = System.Drawing.SystemIcons.Error.ToBitmap
        End Select

        '-- override the default button
        Select Case DefaultButton
            Case UserErrorDefaultButton.Button1
                objForm.AcceptButton = objForm.btn1
                objForm.btn1.TabIndex = 0
            Case UserErrorDefaultButton.Button2
                objForm.AcceptButton = objForm.btn2
                objForm.btn2.TabIndex = 0
            Case UserErrorDefaultButton.Button3
                objForm.AcceptButton = objForm.btn3
                objForm.btn3.TabIndex = 0
        End Select

        If _blnEmailError Then
            SendNotificationEmail(strWhatHappened, strHowUserAffected, strWhatUserCanDo, strMoreDetails)
        End If

        '-- show the user our error dialog
        Return objForm.ShowDialog()
    End Function


    '--
    '-- this is the code that executes in the spawned thread
    '--
    Private Shared Sub ThreadHandler()
        Dim smtp As New SimpleMail.SMTPClient
        Dim mail As New SimpleMail.SMTPMailMessage
        With mail
            .To = AppSettings.GetString("UnhandledExceptionManager/EmailTo")
            If _blnHaveException Then
                .Subject = "Handled Exception notification - " & _strExceptionType
            Else
                .Subject = "HandledExceptionManager notification"
            End If
            .Body = _strEmailBody
        End With
        '-- try to send email, but we don't care if it succeeds (for now)
        Try
            smtp.SendMail(mail)
        Catch e As Exception
            Debug.WriteLine("** SMTP email failed to send!")
            Debug.WriteLine("** " & e.Message)
        End Try
    End Sub

    '--
    '-- send notification about this error via e-mail
    '--
    Private Shared Sub SendNotificationEmail(ByVal strWhatHappened As String, _
        ByVal strHowUserAffected As String, _
        ByVal strWhatUserCanDo As String, _
        ByVal strMoreDetails As String)

        '-- ignore debug exceptions (eg, development testing)?
        If UnhandledExceptionManager.IgnoreDebugErrors Then
            If AppSettings.DebugMode Then Return
        End If

        Dim objStringBuilder As New System.Text.StringBuilder
        With objStringBuilder
            .Append("What happened:")
            .Append(Environment.NewLine)
            .Append(strWhatHappened)
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            .Append("How this will affect the user:")
            .Append(Environment.NewLine)
            .Append(strHowUserAffected)
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            .Append("What the user can do about it:")
            .Append(Environment.NewLine)
            .Append(strWhatUserCanDo)
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
            .Append("More information:")
            .Append(Environment.NewLine)
            .Append(strMoreDetails)
            .Append(Environment.NewLine)
            .Append(Environment.NewLine)
        End With
        SendEmail(objStringBuilder.ToString)
    End Sub

    Private Shared Sub SendEmail(ByVal strEmailBody As String)
        _strEmailBody = strEmailBody
        '-- spawn off the email send attempt as a thread for improved throughput
        Dim objThread As New Thread(New ThreadStart(AddressOf ThreadHandler))
        objThread.Name = "HandledExceptionEmail"
        objThread.Start()
    End Sub

End Class