namespace VHD_Director.BcdDirector
{
    partial class BcdList
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
            this.bcdListBox = new System.Windows.Forms.FlowLayoutPanel();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.bcdOsDetailView1 = new VHD_Director.BcdDirector.BcdOsDetailView();
            this.panelDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // bcdListBox
            // 
            this.bcdListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.bcdListBox.Location = new System.Drawing.Point(0, 0);
            this.bcdListBox.Name = "bcdListBox";
            this.bcdListBox.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.bcdListBox.Size = new System.Drawing.Size(233, 541);
            this.bcdListBox.TabIndex = 0;
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panelDetail.Controls.Add(this.bcdOsDetailView1);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(233, 0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(451, 541);
            this.panelDetail.TabIndex = 1;
            // 
            // bcdOsDetailView1
            // 
            this.bcdOsDetailView1.BackColor = System.Drawing.Color.White;
            this.bcdOsDetailView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bcdOsDetailView1.Location = new System.Drawing.Point(0, 0);
            this.bcdOsDetailView1.Margin = new System.Windows.Forms.Padding(0);
            this.bcdOsDetailView1.Name = "bcdOsDetailView1";
            this.bcdOsDetailView1.Size = new System.Drawing.Size(451, 541);
            this.bcdOsDetailView1.TabIndex = 0;
            // 
            // BcdList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 541);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.bcdListBox);
            this.Name = "BcdList";
            this.Text = "BcdList";
            this.Load += new System.EventHandler(this.BcdList_Load);
            this.panelDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel bcdListBox;
        private System.Windows.Forms.Panel panelDetail;
        private BcdOsDetailView bcdOsDetailView1;


    }
}