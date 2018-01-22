namespace VHD_Director
{
    partial class PartitionRoundedPanel
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackProgressBar1 = new VHD_Director.BlackProgressBar();
            this.greenPartitionView1 = new VHD_Director.GreenPartitionView();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.resizeToolStripMenuItem,
            this.formatToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(128, 114);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // resizeToolStripMenuItem
            // 
            this.resizeToolStripMenuItem.Name = "resizeToolStripMenuItem";
            this.resizeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.resizeToolStripMenuItem.Text = "Resize";
            this.resizeToolStripMenuItem.Click += new System.EventHandler(this.resizeToolStripMenuItem_Click);
            // 
            // formatToolStripMenuItem
            // 
            this.formatToolStripMenuItem.Name = "formatToolStripMenuItem";
            this.formatToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.formatToolStripMenuItem.Text = "Format";
            this.formatToolStripMenuItem.Click += new System.EventHandler(this.formatToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // blackProgressBar1
            // 
            this.blackProgressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.blackProgressBar1.BitArray = null;
            this.blackProgressBar1.Location = new System.Drawing.Point(261, 53);
            this.blackProgressBar1.Maximum = 0;
            this.blackProgressBar1.Name = "blackProgressBar1";
            this.blackProgressBar1.RectangleFillColor = System.Drawing.Color.Empty;
            this.blackProgressBar1.Size = new System.Drawing.Size(29, 14);
            this.blackProgressBar1.TabIndex = 3;
            this.blackProgressBar1.Transparent = false;
            this.blackProgressBar1.Value = 0;
            this.blackProgressBar1.Visible = false;
            // 
            // greenPartitionView1
            // 
            this.greenPartitionView1.AdjustHue = false;
            this.greenPartitionView1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.greenPartitionView1.BitArray = null;
            this.greenPartitionView1.ClusterUsage = null;
            this.greenPartitionView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.greenPartitionView1.line1 = "";
            this.greenPartitionView1.line2 = "";
            this.greenPartitionView1.line3 = "";
            this.greenPartitionView1.Location = new System.Drawing.Point(0, 0);
            this.greenPartitionView1.Luminance = 1.1F;
            this.greenPartitionView1.Margin = new System.Windows.Forms.Padding(0);
            this.greenPartitionView1.Maximum = 0;
            this.greenPartitionView1.Name = "greenPartitionView1";
            this.greenPartitionView1.ProgressBar = true;
            this.greenPartitionView1.RectangleFillColor = System.Drawing.Color.Empty;
            this.greenPartitionView1.ShadowLeft = false;
            this.greenPartitionView1.ShadowRight = false;
            this.greenPartitionView1.Size = new System.Drawing.Size(300, 82);
            this.greenPartitionView1.TabIndex = 4;
            this.greenPartitionView1.Transparent = false;
            this.greenPartitionView1.Value = 0;
            // 
            // PartitionRoundedPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.Controls.Add(this.greenPartitionView1);
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(0, 11);
            this.Name = "PartitionRoundedPanel";
            this.Size = new System.Drawing.Size(300, 82);
            this.Load += new System.EventHandler(this.PartitionRoundedPanel_Load);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.PartitionRoundedPanel_ControlAdded);
            this.Resize += new System.EventHandler(this.PartitionRoundedPanel_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private BlackProgressBar blackProgressBar1;
        private GreenPartitionView greenPartitionView1;

    }
}
