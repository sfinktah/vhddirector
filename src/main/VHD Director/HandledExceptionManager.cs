using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using System.Configuration;
using System;
// --
// -- Generic HANDLED error handling class
// --
// -- It's like MessageBox, but specific to handled exceptions, and supports email notifications
// --
// -- Jeff Atwood
// -- http://www.codinghorror.com
// --
namespace VHD_Director
{
    public class HandledExceptionManager
    {

        private static bool _blnHaveException = false;

        private static bool _blnEmailError = true;

        private static string _strEmailBody;

        private static string _strExceptionType;

        private const string _strDefaultMore = "No further information is available. If the problem persists, contact (contact).";

        public static bool EmailError
        {
            get
            {
                return _blnEmailError;
            }
            set
            {
                _blnEmailError = value;
            }
        }

        public enum UserErrorDefaultButton
        {

            Default = 0,

            Button1 = 1,

            Button2 = 2,

            Button3 = 3,
        }

        // -- 
        // -- replace generic constants in strings with specific values
        // --
        private static string ReplaceStringVals(string strOutput)
        {
            string strTemp;
            if ((strOutput == null))
            {
                strTemp = "";
            }
            else
            {
                strTemp = strOutput;
            }
            // strTemp = strTemp.Replace("(app)", AppSettings.AppProduct);
            // strTemp = strTemp.Replace("(contact)", AppSettings.GetString("UnhandledExceptionManager/ContactInfo"));
            return strTemp;
        }

        // --
        // -- make sure "More" text is populated with something useful
        // --
        private static string GetDefaultMore(string strMoreDetails)
        {
            if ((strMoreDetails == ""))
            {
                System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
                // With...
                return objStringBuilder.ToString();
            }
            else
            {
                return strMoreDetails;
            }
        }

        // --
        // -- converts exception to a formatted "more" string
        // --
        private static string ExceptionToMore(System.Exception objException)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            // With...
            //if (_blnEmailError) {
            //    sb.Append(
            //    "Information about this problem was automatically mailed to ")
            //    .Append(AppSettings().GetString("UnhandledExceptionManager/EmailTo"))
            //    .Append((Environment.NewLine + Environment.NewLine));
            //}
            sb.Append(("Detailed technical information follows: " + Environment.NewLine)).Append(("---" + Environment.NewLine));
            string x = UnhandledExceptionManager.ExceptionToString(objException);
            sb.Append(x);
            return sb.ToString();
        }

        // --
        // -- perform our string replacements for (app) and (contact), etc etc
        // -- also make sure More has default values if it is blank.
        // --       
        private static void ProcessStrings(ref string strWhatHappened, ref string strHowUserAffected, ref string strWhatUserCanDo, ref string strMoreDetails)
        {
            strWhatHappened = ReplaceStringVals(strWhatHappened);
            strHowUserAffected = ReplaceStringVals(strHowUserAffected);
            strWhatUserCanDo = ReplaceStringVals(strWhatUserCanDo);
            strMoreDetails = ReplaceStringVals(GetDefaultMore(strMoreDetails));
        }

        // -- 
        // -- simplest possible error dialog
        // --
        public static DialogResult ShowDialog(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo)
        {
            _blnHaveException = false;


            return ShowDialogInternal(strWhatHappened,
                strHowUserAffected,
                strWhatUserCanDo,
                "",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Warning,
                HandledExceptionManager.UserErrorDefaultButton.Default);
        }

        // --
        // -- advanced error dialog with Exception object
        // --
        public static DialogResult ShowDialog(string strWhatHappened,
            string strHowUserAffected,
            string strWhatUserCanDo,
            System.Exception objException,
            MessageBoxButtons Buttons = System.Windows.Forms.MessageBoxButtons.OK,
            MessageBoxIcon Icon = System.Windows.Forms.MessageBoxIcon.Warning,
            UserErrorDefaultButton DefaultButton = UserErrorDefaultButton.Default)
        {
            _blnHaveException = true;
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            _strExceptionType = objException.GetType().FullName;
            return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, ExceptionToMore(objException), Buttons, Icon, DefaultButton);
        }

        // --
        // -- advanced error dialog with More string
        // -- leave "more" string blank to get the default
        // --
        public static DialogResult ShowDialog(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, string strMoreDetails, MessageBoxButtons Buttons = MessageBoxButtons.OK, MessageBoxIcon Icon = MessageBoxIcon.Warning, UserErrorDefaultButton DefaultButton = UserErrorDefaultButton.Default)
        {
            _blnHaveException = false;
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            return ShowDialogInternal(strWhatHappened, strHowUserAffected, strWhatUserCanDo, strMoreDetails, Buttons, Icon, DefaultButton);
        }

        // -- 
        // -- internal method to show error dialog
        // --
        public static DialogResult ShowDialogInternal(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, string strMoreDetails, MessageBoxButtons Buttons, MessageBoxIcon Icon, UserErrorDefaultButton DefaultButton)
        {
            // -- set default values, etc
            // ProcessStrings(ref strWhatHappened, ref strHowUserAffected, ref strWhatUserCanDo, ref strMoreDetails);
            return DialogResult.OK;
            // Error objForm = new Error();



            /*
           ExceptionDialog objForm = new ExceptionDialog();
           // With...
           strHowUserAffected.ActionBox.Text = strMoreDetails;
           strWhatHappened.ScopeBox.Text = strMoreDetails;
           ReplaceStringVals(objForm.Text).ErrorBox.Text = strMoreDetails;
           objForm.Text = strMoreDetails;
           // -- determine what button text, visibility, and defaults are
           // With...
           switch (Buttons) {
           }
           "&Ignore".AcceptButton = objForm.btn3;
           "&Retry".btn3.Text = objForm.btn3;
           "&Abort".btn2.Text = objForm.btn3;
           MessageBoxButtons.AbortRetryIgnore.btn1.Text = objForm.btn3;
           false.AcceptButton = objForm.btn3;
           false.btn1.Visible = objForm.btn3;
           "OK".btn2.Visible = objForm.btn3;
           MessageBoxButtons.OK.btn3.Text = objForm.btn3;
           objForm.btn2.CancelButton = objForm.btn3;
           false.AcceptButton = objForm.btn3;
           "OK".btn1.Visible = objForm.btn3;
           "Cancel".btn2.Text = objForm.btn3;
           MessageBoxButtons.OKCancel.btn3.Text = objForm.btn3;
           objForm.btn2.CancelButton = objForm.btn3;
           false.AcceptButton = objForm.btn3;
           "&Retry".btn1.Visible = objForm.btn3;
           "Cancel".btn2.Text = objForm.btn3;
           MessageBoxButtons.RetryCancel.btn3.Text = objForm.btn3;
           "&Yes".btn1.Visible = false;
           "&No".btn2.Text = false;
           MessageBoxButtons.YesNo.btn3.Text = false;
           "&Yes".CancelButton = objForm.btn3;
           "&No".btn1.Text = objForm.btn3;
           "Cancel".btn2.Text = objForm.btn3;
           MessageBoxButtons.YesNoCancel.btn3.Text = objForm.btn3;
             */
            //    }
            //}
            //// -- override the default button
            //switch (DefaultButton) {
            //    case UserErrorDefaultButton.Button1:
            //        objForm.AcceptButton = objForm.btn1;
            //        objForm.btn1.TabIndex = 0;
            //        break;
            //    case UserErrorDefaultButton.Button2:
            //        objForm.AcceptButton = objForm.btn2;
            //        objForm.btn2.TabIndex = 0;
            //        break;
            //    case UserErrorDefaultButton.Button3:
            //        objForm.AcceptButton = objForm.btn3;
            //        objForm.btn3.TabIndex = 0;
            //        break;
            //}
            //if (_blnEmailError) {
            //    SendNotificationEmail(strWhatHappened, strHowUserAffected, strWhatUserCanDo, strMoreDetails);
            //}
            //// -- show the user our error dialog
            //return objForm.ShowDialog();
            //EndFunctionEndclass Unknown {
            //}
        }

        // --
        // -- this is the code that executes in the spawned thread
        // --
        private static void ThreadHandler()
        {
            //SimpleMail.SMTPClient smtp = new SimpleMail.SMTPClient();
            //SimpleMail.SMTPMailMessage mail = new SimpleMail.SMTPMailMessage();
            //// With...
            //if (_blnHaveException) {
            //    Subject = ("Handled Exception notification - " + _strExceptionType);
            //}
            //else {
            //    Subject = "HandledExceptionManager notification";
            //}
            //Body = _strEmailBody;
            //// -- try to send email, but we don't care if it succeeds (for now)
            //try {
            //    smtp.SendMail(mail);
            //}
            //catch (Exception e) {
            //    Debug.WriteLine("** SMTP email failed to send!");
            //    Debug.WriteLine(("** " + e.Message));
            //}
        }

        // --
        // -- send notification about this error via e-mail
        // --
        private static void SendNotificationEmail(string strWhatHappened, string strHowUserAffected, string strWhatUserCanDo, string strMoreDetails)
        {
            //// -- ignore debug exceptions (eg, development testing)?
            //if (UnhandledExceptionManager.IgnoreDebugErrors) {
            //    if (AppSettings.DebugMode) {
            //        return;
            //    }
            //    System.Text.StringBuilder objStringBuilder = new System.Text.StringBuilder();
            //    // With...
            //    SendEmail(objStringBuilder.ToString());
            //}
        }

        void SendEmail(string strEmailBody)
        {
            //_strEmailBody = strEmailBody;
            //// -- spawn off the email send attempt as a thread for improved throughput
            //Thread objThread = new Thread(new ThreadStart(new System.EventHandler(this.ThreadHandler)));
            //objThread.Name = "HandledExceptionEmail";
            //objThread.Start();
        }
    }
}