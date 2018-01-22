namespace VhdDirectorApp
{
    partial class ZoomOverlayForm
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
            this.zoomOverlay1 = new VhdDirectorApp.ZoomOverlay();
            this.SuspendLayout();
            // 
            // zoomOverlay1
            // 
            this.zoomOverlay1.BackColor = System.Drawing.Color.White;
            this.zoomOverlay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoomOverlay1.Location = new System.Drawing.Point(0, 1);
            this.zoomOverlay1.maximumX = 0;
            this.zoomOverlay1.maximumY = 0;
            this.zoomOverlay1.Name = "zoomOverlay1";
            this.zoomOverlay1.offsetX = 0;
            this.zoomOverlay1.RectangleFillColor = System.Drawing.Color.Empty;
            this.zoomOverlay1.Size = new System.Drawing.Size(700, 93);
            this.zoomOverlay1.TabIndex = 0;
            // 
            // ZoomOverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(700, 95);
            this.ControlBox = false;
            this.Controls.Add(this.zoomOverlay1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ZoomOverlayForm";
            this.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ZoomOverlayForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private ZoomOverlay zoomOverlay1;
    }
}