namespace VHD_Director.BcdDirector
{
    partial class BcdOsSummaryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BcdOsSummaryView));
            this.masterPanel = new System.Windows.Forms.Panel();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.labelSubTitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.shadowPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.masterPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.shadowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.labelSubTitle);
            this.rightPanel.Controls.Add(this.labelTitle);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(40, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(222, 40);
            this.rightPanel.TabIndex = 1;
            // 
            // labelSubTitle
            // 
            this.labelSubTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSubTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubTitle.Location = new System.Drawing.Point(4, 22);
            this.labelSubTitle.Name = "labelSubTitle";
            this.labelSubTitle.Size = new System.Drawing.Size(215, 13);
            this.labelSubTitle.TabIndex = 1;
            this.labelSubTitle.Text = "Windows 7 Ultimate x64 (VHD)";
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(4, 5);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(215, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Windows 7 Ultimate x64 (VHD)";
            // 
            // shadowPanel
            // 
            this.shadowPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("shadowPanel.BackgroundImage")));
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
            // BcdOsSummaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.masterPanel);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximumSize = new System.Drawing.Size(1024, 40);
            this.MinimumSize = new System.Drawing.Size(120, 40);
            this.Name = "BcdOsSummaryView";
            this.Size = new System.Drawing.Size(262, 40);
            this.masterPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.shadowPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel masterPanel;
        private System.Windows.Forms.Panel shadowPanel;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelSubTitle;
    }
}
