namespace VHD_Director.BcdDirector
{
    partial class BcdOsView
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
            this.masterPanel = new System.Windows.Forms.Panel();
            this.shadowPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.labelHeader = new System.Windows.Forms.Label();
            this.masterPanel.SuspendLayout();
            this.shadowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // masterPanel
            // 
            this.masterPanel.BackColor = System.Drawing.Color.White;
            this.masterPanel.Controls.Add(this.rightPanel);
            this.masterPanel.Controls.Add(this.shadowPanel);
            this.masterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterPanel.Location = new System.Drawing.Point(0, 0);
            this.masterPanel.Name = "masterPanel";
            this.masterPanel.Size = new System.Drawing.Size(262, 40);
            this.masterPanel.TabIndex = 0;
            // 
            // shadowPanel
            // 
            this.shadowPanel.BackgroundImage = global::VHD_Director.Properties.Resources.shadow_box_40x40_for_32x32;
            this.shadowPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.shadowPanel.Controls.Add(this.pictureBox1);
            this.shadowPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.shadowPanel.Location = new System.Drawing.Point(0, 0);
            this.shadowPanel.Margin = new System.Windows.Forms.Padding(0);
            this.shadowPanel.Name = "shadowPanel";
            this.shadowPanel.Size = new System.Drawing.Size(40, 40);
            this.shadowPanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.labelHeader);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(40, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(222, 40);
            this.rightPanel.TabIndex = 1;
            // 
            // labelHeader
            // 
            this.labelHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(4, 4);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(215, 13);
            this.labelHeader.TabIndex = 0;
            this.labelHeader.Text = "Windows 7 Ultimate x64 (VHD)";
            // 
            // BcdOsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.masterPanel);
            this.MaximumSize = new System.Drawing.Size(1024, 40);
            this.MinimumSize = new System.Drawing.Size(120, 40);
            this.Name = "BcdOsView";
            this.Size = new System.Drawing.Size(262, 40);
            this.masterPanel.ResumeLayout(false);
            this.shadowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.rightPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel masterPanel;
        private System.Windows.Forms.Panel shadowPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
