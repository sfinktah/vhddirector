using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for SaturationDlg.
	/// </summary>
	public class SaturationDlg : System.Windows.Forms.Form
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

		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Label Luminance;
		private System.Windows.Forms.TrackBar setLuminance;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SaturationDlg()
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
			this.OK = new System.Windows.Forms.Button();
			this.Luminance = new System.Windows.Forms.Label();
			this.setLuminance = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.setLuminance)).BeginInit();
			this.SuspendLayout();
			// 
			// OK
			// 
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(560, 520);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(32, 23);
			this.OK.TabIndex = 2062;
			this.OK.Text = "OK";
			// 
			// Luminance
			// 
			this.Luminance.Location = new System.Drawing.Point(16, 536);
			this.Luminance.Name = "Luminance";
			this.Luminance.Size = new System.Drawing.Size(64, 23);
			this.Luminance.TabIndex = 2061;
			this.Luminance.Text = "1";
			// 
			// setLuminance
			// 
			this.setLuminance.Location = new System.Drawing.Point(96, 512);
			this.setLuminance.Maximum = 2056;
			this.setLuminance.Name = "setLuminance";
			this.setLuminance.Size = new System.Drawing.Size(448, 45);
			this.setLuminance.TabIndex = 2060;
			this.setLuminance.TickStyle = System.Windows.Forms.TickStyle.None;
			this.setLuminance.Value = 1;
			this.setLuminance.ValueChanged += new System.EventHandler(this.OnChange);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 512);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 2059;
			this.label1.Text = "Saturation";
			// 
			// SaturationDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(608, 566);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.Luminance);
			this.Controls.Add(this.setLuminance);
			this.Controls.Add(this.label1);
			this.Name = "SaturationDlg";
			this.Text = "SaturationDlg";
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			((System.ComponentModel.ISupportInitialize)(this.setLuminance)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OnPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawImageUnscaled(dest, 0, 0);
		
		}

		private void OnChange(object sender, System.EventArgs e)
		{
			luminance = (float)(setLuminance.Value/256.0);
			dest = ColorSpace.Saturation(src, luminance);
			Luminance.Text = luminance.ToString();
			Invalidate(true);
		}
	}
}
