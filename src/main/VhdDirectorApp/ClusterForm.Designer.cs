namespace VhdDirectorApp
{
    partial class ClusterForm
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
            this.clusterBitmaps1 = new VhdDirectorApp.ShadedClusterBitmaps();
            this.SuspendLayout();
            // 
            // clusterBitmaps1
            // 
            this.clusterBitmaps1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clusterBitmaps1.ClusterColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.clusterBitmaps1.Clusters = null;
            this.clusterBitmaps1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clusterBitmaps1.GradientEnd = System.Drawing.Color.RoyalBlue;
            this.clusterBitmaps1.GradientStart = System.Drawing.Color.LightSkyBlue;
            this.clusterBitmaps1.GradientSteps = 8;
            this.clusterBitmaps1.Location = new System.Drawing.Point(0, 0);
            this.clusterBitmaps1.Name = "clusterBitmaps1";
            this.clusterBitmaps1.Size = new System.Drawing.Size(684, 371);
            this.clusterBitmaps1.TabIndex = 0;
            // 
            // ClusterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 371);
            this.Controls.Add(this.clusterBitmaps1);
            this.Name = "ClusterForm";
            this.Text = "ClusterForm";
            this.ResumeLayout(false);

        }

        #endregion

        public ShadedClusterBitmaps clusterBitmaps1;
    }
}