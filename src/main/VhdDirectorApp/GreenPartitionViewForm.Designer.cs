namespace VhdDirectorApp
{
    partial class GreenPartitionViewForm
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
            this.trackBarH = new System.Windows.Forms.TrackBar();
            this.textBoxH = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarL = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxL = new System.Windows.Forms.TextBox();
            this.trackBarS = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.button1 = new System.Windows.Forms.Button();
            this.greenPartitionView1 = new VhdDirectorApp.GreenPartitionView();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarH)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarS)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarH
            // 
            this.trackBarH.LargeChange = 1;
            this.trackBarH.Location = new System.Drawing.Point(16, 57);
            this.trackBarH.Maximum = 20;
            this.trackBarH.Name = "trackBarH";
            this.trackBarH.Size = new System.Drawing.Size(188, 45);
            this.trackBarH.TabIndex = 1;
            this.trackBarH.Value = 10;
            this.trackBarH.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // textBoxH
            // 
            this.textBoxH.Location = new System.Drawing.Point(139, 31);
            this.textBoxH.Name = "textBoxH";
            this.textBoxH.Size = new System.Drawing.Size(50, 20);
            this.textBoxH.TabIndex = 2;
            this.textBoxH.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Hue:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarL);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxL);
            this.groupBox1.Controls.Add(this.trackBarS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxS);
            this.groupBox1.Controls.Add(this.trackBarH);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxH);
            this.groupBox1.Location = new System.Drawing.Point(16, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 293);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hue/Saturation";
            // 
            // trackBarL
            // 
            this.trackBarL.LargeChange = 1;
            this.trackBarL.Location = new System.Drawing.Point(16, 229);
            this.trackBarL.Maximum = 20;
            this.trackBarL.Name = "trackBarL";
            this.trackBarL.Size = new System.Drawing.Size(188, 45);
            this.trackBarL.TabIndex = 7;
            this.trackBarL.Value = 10;
            this.trackBarL.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Lightness:";
            // 
            // textBoxL
            // 
            this.textBoxL.Location = new System.Drawing.Point(139, 203);
            this.textBoxL.Name = "textBoxL";
            this.textBoxL.Size = new System.Drawing.Size(50, 20);
            this.textBoxL.TabIndex = 8;
            this.textBoxL.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // trackBarS
            // 
            this.trackBarS.LargeChange = 1;
            this.trackBarS.Location = new System.Drawing.Point(16, 140);
            this.trackBarS.Maximum = 20;
            this.trackBarS.Name = "trackBarS";
            this.trackBarS.Size = new System.Drawing.Size(188, 45);
            this.trackBarS.TabIndex = 4;
            this.trackBarS.Value = 10;
            this.trackBarS.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Saturation:";
            // 
            // textBoxS
            // 
            this.textBoxS.Location = new System.Drawing.Point(139, 114);
            this.textBoxS.Name = "textBoxS";
            this.textBoxS.Size = new System.Drawing.Size(50, 20);
            this.textBoxS.TabIndex = 5;
            this.textBoxS.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Image: GreenPartition";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Format: PNG24";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Source: Resources";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(257, 460);
            this.shapeContainer1.TabIndex = 8;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 13;
            this.lineShape1.X2 = 235;
            this.lineShape1.Y1 = 108;
            this.lineShape1.Y2 = 108;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(164, 428);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // greenPartitionView1
            // 
            this.greenPartitionView1.AdjustHue = false;
            this.greenPartitionView1.BackColor = System.Drawing.Color.White;
            this.greenPartitionView1.BitArray = null;
            this.greenPartitionView1.ClusterUsage = null;
            this.greenPartitionView1.line1 = "";
            this.greenPartitionView1.line2 = "";
            this.greenPartitionView1.line3 = "";
            this.greenPartitionView1.Location = new System.Drawing.Point(142, 13);
            this.greenPartitionView1.Margin = new System.Windows.Forms.Padding(0);
            this.greenPartitionView1.Maximum = 10;
            this.greenPartitionView1.Name = "greenPartitionView1";
            this.greenPartitionView1.ProgressBar = false;
            this.greenPartitionView1.RectangleFillColor = System.Drawing.Color.Empty;
            this.greenPartitionView1.ShadowLeft = false;
            this.greenPartitionView1.ShadowRight = false;
            this.greenPartitionView1.Size = new System.Drawing.Size(95, 82);
            this.greenPartitionView1.TabIndex = 0;
            this.greenPartitionView1.Transparent = false;
            this.greenPartitionView1.Value = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(83, 428);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GreenPartitionViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(257, 460);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.greenPartitionView1);
            this.Controls.Add(this.shapeContainer1);
            this.Name = "GreenPartitionViewForm";
            this.Text = "Theme Editor";
            this.Load += new System.EventHandler(this.GreenPartitionViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarH)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GreenPartitionView greenPartitionView1;
        private System.Windows.Forms.TrackBar trackBarH;
        private System.Windows.Forms.TextBox textBoxH;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBarL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxL;
        private System.Windows.Forms.TrackBar trackBarS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}