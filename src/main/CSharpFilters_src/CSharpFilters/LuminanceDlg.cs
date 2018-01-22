using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for LuminanceDlg.
	/// </summary>
	public class LuminanceDlg : System.Windows.Forms.Form
	{
		private float luminance;
		public Bitmap dest;
		private Bitmap src;

		public Bitmap source
		{
			get
			{
				return src;
			}
			set
			{
				src = value;
				dest = src.Clone() as Bitmap;
			}
		}

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar setLuminance;
		private System.Windows.Forms.Label Luminance;
		private System.Windows.Forms.Button OK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LuminanceDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.setLuminance = new System.Windows.Forms.TrackBar();
			this.Luminance = new System.Windows.Forms.Label();
			this.OK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.setLuminance)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 472);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Luminance";
			// 
			// setLuminance
			// 
			this.setLuminance.Location = new System.Drawing.Point(96, 472);
			this.setLuminance.Maximum = 2056;
			this.setLuminance.Name = "setLuminance";
			this.setLuminance.Size = new System.Drawing.Size(448, 45);
			this.setLuminance.TabIndex = 2056;
			this.setLuminance.TickStyle = System.Windows.Forms.TickStyle.None;
			this.setLuminance.Value = 1;
			this.setLuminance.ValueChanged += new System.EventHandler(this.OnChange);
			// 
			// Luminance
			// 
			this.Luminance.Location = new System.Drawing.Point(16, 496);
			this.Luminance.Name = "Luminance";
			this.Luminance.Size = new System.Drawing.Size(64, 23);
			this.Luminance.TabIndex = 2057;
			this.Luminance.Text = "1";
			// 
			// OK
			// 
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(560, 480);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(32, 23);
			this.OK.TabIndex = 2058;
			this.OK.Text = "OK";
			// 
			// LuminanceDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 526);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.Luminance);
			this.Controls.Add(this.setLuminance);
			this.Controls.Add(this.label1);
			this.Name = "LuminanceDlg";
			this.Text = "LuminanceDlg";
			this.Load += new System.EventHandler(this.LuminanceDlg_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			((System.ComponentModel.ISupportInitialize)(this.setLuminance)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void LuminanceDlg_Load(object sender, System.EventArgs e)
		{
		
		}

		private void OnPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawImageUnscaled(dest, 0, 0);
		
		}

		private void OnChange(object sender, System.EventArgs e)
		{
			luminance = (float)(setLuminance.Value/256.0);
			dest = ColorSpace.Luminance(src, luminance);
			Luminance.Text = luminance.ToString();
			Invalidate(true);
		}
	}
}
