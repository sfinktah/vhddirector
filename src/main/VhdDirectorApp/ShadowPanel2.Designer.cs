﻿namespace VhdDirectorApp
{
    partial class ShadowPanel2
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InnerPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // InnerPanel
            // 
            this.InnerPanel.BackColor = System.Drawing.Color.White;
            this.InnerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerPanel.Location = new System.Drawing.Point(13, 13);
            this.InnerPanel.Name = "InnerPanel";
            this.InnerPanel.Padding = new System.Windows.Forms.Padding(5);
            this.InnerPanel.Size = new System.Drawing.Size(774, 374);
            this.InnerPanel.TabIndex = 0;
            // 
            // ShadowPanel2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.InnerPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ShadowPanel2";
            this.Padding = new System.Windows.Forms.Padding(13);
            this.Size = new System.Drawing.Size(800, 400);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.ShadowPanel_ControlAdded);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Panel InnerPanel;

    }
}
