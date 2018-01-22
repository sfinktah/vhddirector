namespace VhdApiExample {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnu = new System.Windows.Forms.ToolStrip();
            this.mnuCreate = new System.Windows.Forms.ToolStripButton();
            this.mnuOpen = new System.Windows.Forms.ToolStripButton();
            this.mnuClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAttach = new System.Windows.Forms.ToolStripButton();
            this.mnuDetach = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textFileName = new System.Windows.Forms.TextBox();
            this.textLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.mnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCreate,
            this.mnuOpen,
            this.mnuClose,
            this.toolStripSeparator1,
            this.mnuAttach,
            this.mnuDetach});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnu.Size = new System.Drawing.Size(524, 27);
            this.mnu.TabIndex = 0;
            // 
            // mnuCreate
            // 
            this.mnuCreate.Image = ((System.Drawing.Image)(resources.GetObject("mnuCreate.Image")));
            this.mnuCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuCreate.Name = "mnuCreate";
            this.mnuCreate.Size = new System.Drawing.Size(72, 24);
            this.mnuCreate.Text = "Create";
            this.mnuCreate.Click += new System.EventHandler(this.mnuCreate_Click);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpen.Image")));
            this.mnuOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.Size = new System.Drawing.Size(65, 24);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuClose
            // 
            this.mnuClose.Image = ((System.Drawing.Image)(resources.GetObject("mnuClose.Image")));
            this.mnuClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuClose.Name = "mnuClose";
            this.mnuClose.Size = new System.Drawing.Size(65, 24);
            this.mnuClose.Text = "Close";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuAttach
            // 
            this.mnuAttach.Image = ((System.Drawing.Image)(resources.GetObject("mnuAttach.Image")));
            this.mnuAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAttach.Name = "mnuAttach";
            this.mnuAttach.Size = new System.Drawing.Size(72, 24);
            this.mnuAttach.Text = "Attach";
            this.mnuAttach.Click += new System.EventHandler(this.mnuAttach_Click);
            // 
            // mnuDetach
            // 
            this.mnuDetach.Image = ((System.Drawing.Image)(resources.GetObject("mnuDetach.Image")));
            this.mnuDetach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDetach.Name = "mnuDetach";
            this.mnuDetach.Size = new System.Drawing.Size(76, 24);
            this.mnuDetach.Text = "Detach";
            this.mnuDetach.Click += new System.EventHandler(this.mnuDetach_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "File:";
            // 
            // textFileName
            // 
            this.textFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textFileName.Location = new System.Drawing.Point(132, 40);
            this.textFileName.Name = "textFileName";
            this.textFileName.ReadOnly = true;
            this.textFileName.Size = new System.Drawing.Size(380, 22);
            this.textFileName.TabIndex = 2;
            // 
            // textLocation
            // 
            this.textLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textLocation.Location = new System.Drawing.Point(132, 68);
            this.textLocation.Name = "textLocation";
            this.textLocation.ReadOnly = true;
            this.textLocation.Size = new System.Drawing.Size(380, 22);
            this.textLocation.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Location:";
            // 
            // openDialog
            // 
            this.openDialog.Filter = "Virtual disks|*.vhd|All files|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 102);
            this.Controls.Add(this.textLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mnu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "VHD API Example";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnu;
        private System.Windows.Forms.ToolStripButton mnuOpen;
        private System.Windows.Forms.ToolStripButton mnuCreate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mnuAttach;
        private System.Windows.Forms.ToolStripButton mnuDetach;
        private System.Windows.Forms.ToolStripButton mnuClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textFileName;
        private System.Windows.Forms.TextBox textLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openDialog;
    }
}

