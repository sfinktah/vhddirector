namespace VhdDirectorApp
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findVHDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acronisPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createVHDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bCDDirectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vHDMountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadVHDMOUNTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vHDTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defragTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clusterViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileClusterVIewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nTFSTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userControlDiskUsageBar1 = new VhdDirectorApp.UserControlDiskUsageBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(12, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(385, 446);
            this.treeView1.TabIndex = 2;
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1MouseUp);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.windowToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(414, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.findVHDsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.browseFileButton_Click);
            // 
            // findVHDsToolStripMenuItem
            // 
            this.findVHDsToolStripMenuItem.Name = "findVHDsToolStripMenuItem";
            this.findVHDsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.findVHDsToolStripMenuItem.Text = "Find";
            this.findVHDsToolStripMenuItem.Click += new System.EventHandler(this.findVHDsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acronisPanelToolStripMenuItem,
            this.myFontToolStripMenuItem,
            this.disksToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // acronisPanelToolStripMenuItem
            // 
            this.acronisPanelToolStripMenuItem.Name = "acronisPanelToolStripMenuItem";
            this.acronisPanelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.acronisPanelToolStripMenuItem.Text = "Acronis Panel";
            this.acronisPanelToolStripMenuItem.Click += new System.EventHandler(this.acronisPanelToolStripMenuItem_Click);
            // 
            // myFontToolStripMenuItem
            // 
            this.myFontToolStripMenuItem.Name = "myFontToolStripMenuItem";
            this.myFontToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.myFontToolStripMenuItem.Text = "MyFont";
            this.myFontToolStripMenuItem.Click += new System.EventHandler(this.myFontToolStripMenuItem_Click);
            // 
            // disksToolStripMenuItem
            // 
            this.disksToolStripMenuItem.Name = "disksToolStripMenuItem";
            this.disksToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.disksToolStripMenuItem.Text = "Disks";
            this.disksToolStripMenuItem.Click += new System.EventHandler(this.disksToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewMBRToolStripMenuItem,
            this.createVHDToolStripMenuItem,
            this.bCDDirectorToolStripMenuItem,
            this.registryToolStripMenuItem,
            this.vHDMountToolStripMenuItem,
            this.downloadVHDMOUNTToolStripMenuItem,
            this.vHDTestToolStripMenuItem,
            this.defragTestToolStripMenuItem,
            this.clusterViewerToolStripMenuItem,
            this.fileClusterVIewToolStripMenuItem,
            this.nTFSTestToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // viewMBRToolStripMenuItem
            // 
            this.viewMBRToolStripMenuItem.Name = "viewMBRToolStripMenuItem";
            this.viewMBRToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.viewMBRToolStripMenuItem.Text = "View MBR";
            this.viewMBRToolStripMenuItem.Click += new System.EventHandler(this.viewMBRToolStripMenuItem_Click);
            // 
            // createVHDToolStripMenuItem
            // 
            this.createVHDToolStripMenuItem.Name = "createVHDToolStripMenuItem";
            this.createVHDToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.createVHDToolStripMenuItem.Text = "Create VHD";
            this.createVHDToolStripMenuItem.Click += new System.EventHandler(this.createVHDToolStripMenuItem_Click);
            // 
            // bCDDirectorToolStripMenuItem
            // 
            this.bCDDirectorToolStripMenuItem.Name = "bCDDirectorToolStripMenuItem";
            this.bCDDirectorToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.bCDDirectorToolStripMenuItem.Text = "BCD Director";
            this.bCDDirectorToolStripMenuItem.Click += new System.EventHandler(this.bCDDirectorToolStripMenuItem_Click);
            // 
            // registryToolStripMenuItem
            // 
            this.registryToolStripMenuItem.Name = "registryToolStripMenuItem";
            this.registryToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.registryToolStripMenuItem.Text = "Registry";
            this.registryToolStripMenuItem.Click += new System.EventHandler(this.registryToolStripMenuItem_Click);
            // 
            // vHDMountToolStripMenuItem
            // 
            this.vHDMountToolStripMenuItem.Name = "vHDMountToolStripMenuItem";
            this.vHDMountToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.vHDMountToolStripMenuItem.Text = "VHD Mount";
            this.vHDMountToolStripMenuItem.Click += new System.EventHandler(this.vHDMountToolStripMenuItem_Click);
            // 
            // downloadVHDMOUNTToolStripMenuItem
            // 
            this.downloadVHDMOUNTToolStripMenuItem.Name = "downloadVHDMOUNTToolStripMenuItem";
            this.downloadVHDMOUNTToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.downloadVHDMOUNTToolStripMenuItem.Text = "Download VHDMOUNT";
            this.downloadVHDMOUNTToolStripMenuItem.Click += new System.EventHandler(this.downloadVHDMOUNTToolStripMenuItem_Click);
            // 
            // vHDTestToolStripMenuItem
            // 
            this.vHDTestToolStripMenuItem.Name = "vHDTestToolStripMenuItem";
            this.vHDTestToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.vHDTestToolStripMenuItem.Text = "VHD Test";
            this.vHDTestToolStripMenuItem.Click += new System.EventHandler(this.vHDTestToolStripMenuItem_Click);
            // 
            // defragTestToolStripMenuItem
            // 
            this.defragTestToolStripMenuItem.Name = "defragTestToolStripMenuItem";
            this.defragTestToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.defragTestToolStripMenuItem.Text = "Defrag Test";
            this.defragTestToolStripMenuItem.Click += new System.EventHandler(this.defragTestToolStripMenuItem_Click);
            // 
            // clusterViewerToolStripMenuItem
            // 
            this.clusterViewerToolStripMenuItem.Name = "clusterViewerToolStripMenuItem";
            this.clusterViewerToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.clusterViewerToolStripMenuItem.Text = "Cluster Viewer";
            this.clusterViewerToolStripMenuItem.Click += new System.EventHandler(this.clusterViewerToolStripMenuItem_Click);
            // 
            // fileClusterVIewToolStripMenuItem
            // 
            this.fileClusterVIewToolStripMenuItem.Name = "fileClusterVIewToolStripMenuItem";
            this.fileClusterVIewToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.fileClusterVIewToolStripMenuItem.Text = "File Cluster VIew";
            this.fileClusterVIewToolStripMenuItem.Click += new System.EventHandler(this.fileClusterVIewToolStripMenuItem_Click);
            // 
            // nTFSTestToolStripMenuItem
            // 
            this.nTFSTestToolStripMenuItem.Name = "nTFSTestToolStripMenuItem";
            this.nTFSTestToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.nTFSTestToolStripMenuItem.Text = "NTFS Test";
            this.nTFSTestToolStripMenuItem.Click += new System.EventHandler(this.nTFSTestToolStripMenuItem_Click);
            // 
            // windowToolStripMenuItem
            // 
            this.windowToolStripMenuItem.Name = "windowToolStripMenuItem";
            this.windowToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.windowToolStripMenuItem.Text = "Window";
            // 
            // userControlDiskUsageBar1
            // 
            this.userControlDiskUsageBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userControlDiskUsageBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(234)))), ((int)(((byte)(238)))));
            this.userControlDiskUsageBar1.clientHeight = 23;
            this.userControlDiskUsageBar1.clientWidth = 385;
            this.userControlDiskUsageBar1.lineHeight = 0;
            this.userControlDiskUsageBar1.Location = new System.Drawing.Point(12, 479);
            this.userControlDiskUsageBar1.Maximum = 100;
            this.userControlDiskUsageBar1.Name = "userControlDiskUsageBar1";
            this.userControlDiskUsageBar1.Size = new System.Drawing.Size(385, 23);
            this.userControlDiskUsageBar1.TabIndex = 3;
            this.userControlDiskUsageBar1.var1 = 0;
            this.userControlDiskUsageBar1.var2 = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.editToolStripMenuItem1.Text = "Edit";
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.editToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 514);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.userControlDiskUsageBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "VHD Director";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private UserControlDiskUsageBar userControlDiskUsageBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMBRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createVHDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acronisPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bCDDirectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vHDMountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadVHDMOUNTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findVHDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vHDTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defragTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clusterViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileClusterVIewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nTFSTestToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
    }
}

