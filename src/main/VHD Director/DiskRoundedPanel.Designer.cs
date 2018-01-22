namespace VHD_Director
{
    partial class DiskRoundedPanel
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
            this.bitArrayBar1 = new VHD_Director.BitArrayBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DiskLabel = new System.Windows.Forms.Label();
            this.DiskSummary = new VHD_Director.TransparentLabel();
            this.labelTopRight = new VHD_Director.TransparentLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bitArrayBar1
            // 
            this.bitArrayBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bitArrayBar1.bitArray = null;
            this.bitArrayBar1.FalseColor = this.BackColor;
            this.bitArrayBar1.Group256 = false;
            this.bitArrayBar1.Location = new System.Drawing.Point(7, 76);
            this.bitArrayBar1.Name = "bitArrayBar1";
            this.bitArrayBar1.Size = new System.Drawing.Size(241, 6);
            this.bitArrayBar1.TabIndex = 5;
            this.bitArrayBar1.TrueColor = System.Drawing.Color.Gray;
            this.bitArrayBar1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.ImageLocation = "";
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(7, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // DiskLabel
            // 
            this.DiskLabel.BackColor = System.Drawing.Color.White;
            this.DiskLabel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiskLabel.Location = new System.Drawing.Point(4, 0);
            this.DiskLabel.Name = "DiskLabel";
            this.DiskLabel.Size = new System.Drawing.Size(110, 13);
            this.DiskLabel.TabIndex = 1;
            this.DiskLabel.Text = "VHD 1";
            // 
            // DiskSummary
            // 
            this.DiskSummary.AutoSize = true;
            this.DiskSummary.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiskSummary.Location = new System.Drawing.Point(54, 28);
            this.DiskSummary.Name = "DiskSummary";
            this.DiskSummary.NoPaintParentBackground = false;
            this.DiskSummary.Size = new System.Drawing.Size(55, 39);
            this.DiskSummary.TabIndex = 0;
            this.DiskSummary.Text = "Basic MBR\r\n100 GB\r\nDynamic";
            this.DiskSummary.Transparent = false;
            // 
            // labelTopRight
            // 
            this.labelTopRight.AutoSize = true;
            this.labelTopRight.BackColor = System.Drawing.SystemColors.ControlLight;
            this.labelTopRight.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTopRight.Location = new System.Drawing.Point(610, 5);
            this.labelTopRight.Margin = new System.Windows.Forms.Padding(0);
            this.labelTopRight.Name = "labelTopRight";
            this.labelTopRight.NoPaintParentBackground = false;
            this.labelTopRight.Size = new System.Drawing.Size(0, 11);
            this.labelTopRight.TabIndex = 6;
            this.labelTopRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.labelTopRight.Transparent = false;
            this.labelTopRight.Resize += new System.EventHandler(this.labelTopRight_Resize);
            // 
            // DiskRoundedPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGreen;
            this.Controls.Add(this.labelTopRight);
            this.Controls.Add(this.bitArrayBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DiskLabel);
            this.Controls.Add(this.DiskSummary);
            this.CornerRadius = 10;
            this.Name = "DiskRoundedPanel";
            this.Size = new System.Drawing.Size(130, 95);
            this.StrokeWidth = 1.5F;
            this.Load += new System.EventHandler(this.DiskRoundedPanel_Load);
            this.ClientSizeChanged += new System.EventHandler(this.DiskRoundedPanel_ClientSizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TransparentLabel DiskSummary;
        private System.Windows.Forms.Label DiskLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        public BitArrayBar bitArrayBar1;
        private TransparentLabel labelTopRight;
    }
}
