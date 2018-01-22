namespace VHD_Director
{
    partial class Error
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Error));
            this.label1 = new System.Windows.Forms.Label();
            this.ErrorSubHeading = new System.Windows.Forms.Label();
            this.ErrorDetail = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.ErrorHeading = new System.Windows.Forms.Label();
            this.checkBoxReport = new System.Windows.Forms.CheckBox();
            this.userComments = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxScreenshot = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.authorTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(546, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "A not completely unexpected error has occured.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ErrorSubHeading
            // 
            this.ErrorSubHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorSubHeading.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorSubHeading.Location = new System.Drawing.Point(14, 49);
            this.ErrorSubHeading.Name = "ErrorSubHeading";
            this.ErrorSubHeading.Size = new System.Drawing.Size(542, 20);
            this.ErrorSubHeading.TabIndex = 4;
            this.ErrorSubHeading.Text = "ErrorSubHeading";
            this.ErrorSubHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ErrorDetail
            // 
            this.ErrorDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorDetail.Location = new System.Drawing.Point(17, 73);
            this.ErrorDetail.Multiline = true;
            this.ErrorDetail.Name = "ErrorDetail";
            this.ErrorDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ErrorDetail.Size = new System.Drawing.Size(539, 167);
            this.ErrorDetail.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.okButton.Location = new System.Drawing.Point(479, 460);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ErrorHeading
            // 
            this.ErrorHeading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorHeading.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorHeading.Location = new System.Drawing.Point(12, 31);
            this.ErrorHeading.Name = "ErrorHeading";
            this.ErrorHeading.Size = new System.Drawing.Size(542, 20);
            this.ErrorHeading.TabIndex = 7;
            this.ErrorHeading.Text = "Error";
            this.ErrorHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxReport
            // 
            this.checkBoxReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxReport.AutoSize = true;
            this.checkBoxReport.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxReport.Checked = true;
            this.checkBoxReport.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxReport.Location = new System.Drawing.Point(346, 445);
            this.checkBoxReport.Name = "checkBoxReport";
            this.checkBoxReport.Size = new System.Drawing.Size(122, 17);
            this.checkBoxReport.TabIndex = 8;
            this.checkBoxReport.Text = "Report to Developer";
            this.checkBoxReport.UseVisualStyleBackColor = true;
            this.checkBoxReport.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // userComments
            // 
            this.userComments.AcceptsReturn = true;
            this.userComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userComments.Location = new System.Drawing.Point(17, 283);
            this.userComments.Multiline = true;
            this.userComments.Name = "userComments";
            this.userComments.Size = new System.Drawing.Size(314, 149);
            this.userComments.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 264);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(219, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Please add any comments you wish to make:";
            // 
            // checkBoxScreenshot
            // 
            this.checkBoxScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxScreenshot.AutoSize = true;
            this.checkBoxScreenshot.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxScreenshot.Checked = true;
            this.checkBoxScreenshot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxScreenshot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxScreenshot.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.checkBoxScreenshot.Location = new System.Drawing.Point(350, 468);
            this.checkBoxScreenshot.Name = "checkBoxScreenshot";
            this.checkBoxScreenshot.Size = new System.Drawing.Size(118, 17);
            this.checkBoxScreenshot.TabIndex = 11;
            this.checkBoxScreenshot.Text = "Include Screenshot";
            this.checkBoxScreenshot.UseVisualStyleBackColor = true;
            this.checkBoxScreenshot.UseWaitCursor = true;
            this.checkBoxScreenshot.CheckedChanged += new System.EventHandler(this.checkBoxScreenshot_CheckedChanged);
            this.checkBoxScreenshot.MouseHover += new System.EventHandler(this.checkBoxScreenshot_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(338, 283);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(216, 149);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 445);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Your name/email:";
            // 
            // authorTextBox
            // 
            this.authorTextBox.Location = new System.Drawing.Point(116, 442);
            this.authorTextBox.Name = "authorTextBox";
            this.authorTextBox.Size = new System.Drawing.Size(215, 20);
            this.authorTextBox.TabIndex = 14;
            this.authorTextBox.Text = "anonymous";
            // 
            // Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 489);
            this.Controls.Add(this.authorTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBoxScreenshot);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userComments);
            this.Controls.Add(this.checkBoxReport);
            this.Controls.Add(this.ErrorHeading);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.ErrorDetail);
            this.Controls.Add(this.ErrorSubHeading);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Error";
            this.Text = "Error";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Error_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ErrorSubHeading;
        private System.Windows.Forms.TextBox ErrorDetail;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label ErrorHeading;
        private System.Windows.Forms.CheckBox checkBoxReport;
        private System.Windows.Forms.TextBox userComments;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxScreenshot;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox authorTextBox;
    }
}
