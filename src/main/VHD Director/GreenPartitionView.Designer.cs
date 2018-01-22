namespace VHD_Director
{
    partial class GreenPartitionView
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
            this.label1 = new VHD_Director.MyLabel2();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoWrap = false;
            this.label1.clientHeight = 40;
            this.label1.clientWidth = 60;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.lineHeight = 10;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label1.MaximumSize = new System.Drawing.Size(60, 250);
            this.label1.MinimumSize = new System.Drawing.Size(60, 40);
            this.label1.Name = "label1";
            this.label1.NoPaintParentBackground = false;
            this.label1.Size = new System.Drawing.Size(60, 40);
            this.label1.TabIndex = 0;
            this.label1.Transparent = true;
            this.label1.var1 = 0;
            this.label1.var2 = 0;
            // 
            // GreenPartitionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GreenPartitionView";
            this.Size = new System.Drawing.Size(95, 82);
            this.Load += new System.EventHandler(this.GreenPartitionView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLabel2 label1;
    }
}
