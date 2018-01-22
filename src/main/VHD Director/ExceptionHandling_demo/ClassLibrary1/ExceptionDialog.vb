Imports System.Drawing

'--
'-- Generic user error dialog
'--
'-- UI adapted from
'--
'-- Alan Cooper's "About Face: The Essentials of User Interface Design"
'-- Chapter VII, "The End of Errors", pages 423-440
'--
'-- Jeff Atwood
'-- http://www.codinghorror.com
'--
Friend Class ExceptionDialog
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btn1 As System.Windows.Forms.Button
    Friend WithEvents btn2 As System.Windows.Forms.Button
    Friend WithEvents btn3 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblErrorHeading As System.Windows.Forms.Label
    Friend WithEvents lblScopeHeading As System.Windows.Forms.Label
    Friend WithEvents lblActionHeading As System.Windows.Forms.Label
    Friend WithEvents lblMoreHeading As System.Windows.Forms.Label
    Friend WithEvents txtMore As System.Windows.Forms.TextBox
    Friend WithEvents btnMore As System.Windows.Forms.Button
    Friend WithEvents ErrorBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ScopeBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ActionBox As System.Windows.Forms.RichTextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblErrorHeading = New System.Windows.Forms.Label
        Me.ErrorBox = New System.Windows.Forms.RichTextBox
        Me.lblScopeHeading = New System.Windows.Forms.Label
        Me.ScopeBox = New System.Windows.Forms.RichTextBox
        Me.lblActionHeading = New System.Windows.Forms.Label
        Me.ActionBox = New System.Windows.Forms.RichTextBox
        Me.lblMoreHeading = New System.Windows.Forms.Label
        Me.btn1 = New System.Windows.Forms.Button
        Me.btn2 = New System.Windows.Forms.Button
        Me.btn3 = New System.Windows.Forms.Button
        Me.txtMore = New System.Windows.Forms.TextBox
        Me.btnMore = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(8, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'lblErrorHeading
        '
        Me.lblErrorHeading.AutoSize = True
        Me.lblErrorHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblErrorHeading.Location = New System.Drawing.Point(48, 4)
        Me.lblErrorHeading.Name = "lblErrorHeading"
        Me.lblErrorHeading.Size = New System.Drawing.Size(91, 16)
        Me.lblErrorHeading.TabIndex = 0
        Me.lblErrorHeading.Text = "What happened"
        '
        'ErrorBox
        '
        Me.ErrorBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ErrorBox.BackColor = System.Drawing.SystemColors.Control
        Me.ErrorBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ErrorBox.CausesValidation = False
        Me.ErrorBox.Location = New System.Drawing.Point(48, 24)
        Me.ErrorBox.Name = "ErrorBox"
        Me.ErrorBox.ReadOnly = True
        Me.ErrorBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.ErrorBox.Size = New System.Drawing.Size(416, 64)
        Me.ErrorBox.TabIndex = 1
        Me.ErrorBox.Text = "(error message)"
        '
        'lblScopeHeading
        '
        Me.lblScopeHeading.AutoSize = True
        Me.lblScopeHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblScopeHeading.Location = New System.Drawing.Point(8, 92)
        Me.lblScopeHeading.Name = "lblScopeHeading"
        Me.lblScopeHeading.Size = New System.Drawing.Size(134, 16)
        Me.lblScopeHeading.TabIndex = 2
        Me.lblScopeHeading.Text = "How this will affect you"
        '
        'ScopeBox
        '
        Me.ScopeBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ScopeBox.BackColor = System.Drawing.SystemColors.Control
        Me.ScopeBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ScopeBox.CausesValidation = False
        Me.ScopeBox.Location = New System.Drawing.Point(24, 112)
        Me.ScopeBox.Name = "ScopeBox"
        Me.ScopeBox.ReadOnly = True
        Me.ScopeBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.ScopeBox.Size = New System.Drawing.Size(440, 64)
        Me.ScopeBox.TabIndex = 3
        Me.ScopeBox.Text = "(scope)"
        '
        'lblActionHeading
        '
        Me.lblActionHeading.AutoSize = True
        Me.lblActionHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblActionHeading.Location = New System.Drawing.Point(8, 180)
        Me.lblActionHeading.Name = "lblActionHeading"
        Me.lblActionHeading.Size = New System.Drawing.Size(143, 16)
        Me.lblActionHeading.TabIndex = 4
        Me.lblActionHeading.Text = "What you can do about it"
        '
        'ActionBox
        '
        Me.ActionBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ActionBox.BackColor = System.Drawing.SystemColors.Control
        Me.ActionBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ActionBox.CausesValidation = False
        Me.ActionBox.Location = New System.Drawing.Point(24, 200)
        Me.ActionBox.Name = "ActionBox"
        Me.ActionBox.ReadOnly = True
        Me.ActionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.ActionBox.Size = New System.Drawing.Size(440, 92)
        Me.ActionBox.TabIndex = 5
        Me.ActionBox.Text = "(action)"
        '
        'lblMoreHeading
        '
        Me.lblMoreHeading.AutoSize = True
        Me.lblMoreHeading.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblMoreHeading.Location = New System.Drawing.Point(8, 300)
        Me.lblMoreHeading.Name = "lblMoreHeading"
        Me.lblMoreHeading.TabIndex = 6
        Me.lblMoreHeading.Text = "More information"
        '
        'btn1
        '
        Me.btn1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn1.Location = New System.Drawing.Point(220, 544)
        Me.btn1.Name = "btn1"
        Me.btn1.TabIndex = 9
        Me.btn1.Text = "Button1"
        '
        'btn2
        '
        Me.btn2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn2.Location = New System.Drawing.Point(304, 544)
        Me.btn2.Name = "btn2"
        Me.btn2.TabIndex = 10
        Me.btn2.Text = "Button2"
        '
        'btn3
        '
        Me.btn3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn3.Location = New System.Drawing.Point(388, 544)
        Me.btn3.Name = "btn3"
        Me.btn3.TabIndex = 11
        Me.btn3.Text = "Button3"
        '
        'txtMore
        '
        Me.txtMore.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMore.CausesValidation = False
        Me.txtMore.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMore.Location = New System.Drawing.Point(8, 324)
        Me.txtMore.Multiline = True
        Me.txtMore.Name = "txtMore"
        Me.txtMore.ReadOnly = True
        Me.txtMore.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMore.Size = New System.Drawing.Size(456, 212)
        Me.txtMore.TabIndex = 8
        Me.txtMore.Text = "(detailed information, such as exception details)"
        '
        'btnMore
        '
        Me.btnMore.Location = New System.Drawing.Point(112, 296)
        Me.btnMore.Name = "btnMore"
        Me.btnMore.Size = New System.Drawing.Size(28, 24)
        Me.btnMore.TabIndex = 7
        Me.btnMore.Text = ">>"
        '
        'ExceptionDialog
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(472, 573)
        Me.Controls.Add(Me.btnMore)
        Me.Controls.Add(Me.txtMore)
        Me.Controls.Add(Me.btn3)
        Me.Controls.Add(Me.btn2)
        Me.Controls.Add(Me.btn1)
        Me.Controls.Add(Me.lblMoreHeading)
        Me.Controls.Add(Me.lblActionHeading)
        Me.Controls.Add(Me.lblScopeHeading)
        Me.Controls.Add(Me.lblErrorHeading)
        Me.Controls.Add(Me.ActionBox)
        Me.Controls.Add(Me.ScopeBox)
        Me.Controls.Add(Me.ErrorBox)
        Me.Controls.Add(Me.PictureBox1)
        Me.MinimizeBox = False
        Me.Name = "ExceptionDialog"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "(app) has encountered a problem"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Const _intSpacing As Integer = 10

    '--
    '-- security-safe process.start wrapper
    '--
    Private Sub LaunchLink(ByVal strUrl As String)
        Try
            System.Diagnostics.Process.Start(strUrl)
        Catch ex As System.Security.SecurityException
            '-- do nothing; we can't launch without full trust.
        End Try
    End Sub

    Private Sub SizeBox(ByVal ctl As System.Windows.Forms.RichTextBox)
        Dim g As Graphics
        Try
            '-- note that the height is taken as MAXIMUM, so size the label for maximum desired height!
            g = Graphics.FromHwnd(ctl.Handle)
            Dim objSizeF As SizeF = g.MeasureString(ctl.Text, ctl.Font, New SizeF(ctl.Width, ctl.Height))
            g.Dispose()
            ctl.Height = Convert.ToInt32(objSizeF.Height) + 5
        Catch ex As System.Security.SecurityException
            '-- do nothing; we can't set control sizes without full trust
        Finally
            If Not g Is Nothing Then g.Dispose()
        End Try
    End Sub

    Private Function DetermineDialogResult(ByVal strButtonText As String) As Windows.Forms.DialogResult
        '-- strip any accelerator keys we might have
        strButtonText = strButtonText.Replace("&", "")
        Select Case strButtonText.ToLower
            Case "abort"
                Return Windows.Forms.DialogResult.Abort
            Case "cancel"
                Return Windows.Forms.DialogResult.Cancel
            Case "ignore"
                Return Windows.Forms.DialogResult.Ignore
            Case "no"
                Return Windows.Forms.DialogResult.No
            Case "none"
                Return Windows.Forms.DialogResult.None
            Case "ok"
                Return Windows.Forms.DialogResult.OK
            Case "retry"
                Return Windows.Forms.DialogResult.Retry
            Case "yes"
                Return Windows.Forms.DialogResult.Yes
        End Select
    End Function

    Private Sub btn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
        Me.Close()
        Me.DialogResult = DetermineDialogResult(btn1.Text)
    End Sub

    Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
        Me.Close()
        Me.DialogResult = DetermineDialogResult(btn2.Text)
    End Sub

    Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
        Me.Close()
        Me.DialogResult = DetermineDialogResult(btn3.Text)
    End Sub

    Private Sub UserErrorDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '-- make sure our window is on top
        Me.TopMost = True
        Me.TopMost = False

        '-- More >> has to be expanded
        Me.txtMore.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.txtMore.Visible = False

        '-- size the labels' height to accommodate the amount of text in them
        SizeBox(ScopeBox)
        SizeBox(ActionBox)
        SizeBox(ErrorBox)

        '-- now shift everything up
        lblScopeHeading.Top = ErrorBox.Top + ErrorBox.Height + _intSpacing
        ScopeBox.Top = lblScopeHeading.Top + lblScopeHeading.Height + _intSpacing

        lblActionHeading.Top = ScopeBox.Top + ScopeBox.Height + _intSpacing
        ActionBox.Top = lblActionHeading.Top + lblActionHeading.Height + _intSpacing

        lblMoreHeading.Top = ActionBox.Top + ActionBox.Height + _intSpacing
        btnMore.Top = lblMoreHeading.Top - 3

        Me.Height = btnMore.Top + btnMore.Height + _intSpacing + 45

        Me.CenterToScreen()
    End Sub

    Private Sub btnMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMore.Click
        If btnMore.Text = ">>" Then
            Me.Height = Me.Height + 300
            With txtMore
                .Location = New System.Drawing.Point(lblMoreHeading.Left, lblMoreHeading.Top + lblMoreHeading.Height + _intSpacing)
                .Height = Me.ClientSize.Height - txtMore.Top - 45
                .Width = Me.ClientSize.Width - 2 * _intSpacing
                .Anchor = Windows.Forms.AnchorStyles.Top Or Windows.Forms.AnchorStyles.Bottom _
                            Or Windows.Forms.AnchorStyles.Left Or Windows.Forms.AnchorStyles.Right
                .Visible = True
            End With
            btn3.Focus()
            btnMore.Text = "<<"
        Else
            Me.SuspendLayout()
            btnMore.Text = ">>"
            Me.Height = btnMore.Top + btnMore.Height + _intSpacing + 45
            txtMore.Visible = False
            txtMore.Anchor = Windows.Forms.AnchorStyles.None
            Me.ResumeLayout()
        End If
    End Sub

    Private Sub ErrorBox_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles ErrorBox.LinkClicked
        LaunchLink(e.LinkText)
    End Sub

    Private Sub ScopeBox_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles ScopeBox.LinkClicked
        LaunchLink(e.LinkText)
    End Sub

    Private Sub ActionBox_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkClickedEventArgs) Handles ActionBox.LinkClicked
        LaunchLink(e.LinkText)
    End Sub
End Class