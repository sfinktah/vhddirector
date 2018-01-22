using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Drawing.Bitmap m_Bitmap;
		private System.Drawing.Bitmap m_Undo;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem FileLoad;
		private System.Windows.Forms.MenuItem FileSave;
		private System.Windows.Forms.MenuItem FileExit;
		private System.Windows.Forms.MenuItem FilterInvert;
		private System.Windows.Forms.MenuItem FilterGrayScale;
		private System.Windows.Forms.MenuItem FilterBrightness;
		private System.Windows.Forms.MenuItem FilterContrast;
		private System.Windows.Forms.MenuItem FilterGamma;
		private System.Windows.Forms.MenuItem FilterColor;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem Zoom25;
		private double Zoom = 1.0;
		private System.Windows.Forms.MenuItem Zoom50;
		private System.Windows.Forms.MenuItem Zoom100;
		private System.Windows.Forms.MenuItem Zoom150;
		private System.Windows.Forms.MenuItem Zoom200;
		private System.Windows.Forms.MenuItem Zoom300;
		private System.Windows.Forms.MenuItem Zoom500;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem FilterSmooth;
		private System.Windows.Forms.MenuItem GaussianBlur;
		private System.Windows.Forms.MenuItem MeanRemoval;
		private System.Windows.Forms.MenuItem Sharpen;
		private System.Windows.Forms.MenuItem EmbossLaplacian;
		private System.Windows.Forms.MenuItem EdgeDetectQuick;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem Undo;
		private System.Windows.Forms.MenuItem FilterCustom;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem EdgeKirsh;
		private System.Windows.Forms.MenuItem EdgePrewitt;
		private System.Windows.Forms.MenuItem EdgeSobell;
		private System.Windows.Forms.MenuItem EdgeDetectHorizontal;
		private System.Windows.Forms.MenuItem EdgeVertical;
		private System.Windows.Forms.MenuItem EdgeHomogenity;
		private System.Windows.Forms.MenuItem EdgeDifference;
		private System.Windows.Forms.MenuItem EdgeEnhance;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem Resize;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem FlipHorz;
		private System.Windows.Forms.MenuItem flipVert;
		private System.Windows.Forms.MenuItem flipBoth;
		private System.Windows.Forms.MenuItem randomJitter;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem swirlNormal;
		private System.Windows.Forms.MenuItem swirlAntiAlias;
		private System.Windows.Forms.MenuItem Sphere;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem sphereAntialias;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.MenuItem timeWarpNormal;
		private System.Windows.Forms.MenuItem timeWarpAntiAlias;
		private System.Windows.Forms.MenuItem Moire;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem waterNormal;
		private System.Windows.Forms.MenuItem waterAntialias;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem PixelateNoGrid;
		private System.Windows.Forms.MenuItem PixelateGrid;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem Luminance;
		private System.Windows.Forms.MenuItem Hue;
		private System.Windows.Forms.MenuItem Saturation;
		private System.Windows.Forms.MenuItem HLSChart;
		private System.Windows.Forms.MenuItem HSLPicker1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();

			m_Bitmap= new Bitmap(2, 2);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.FileLoad = new System.Windows.Forms.MenuItem();
			this.FileSave = new System.Windows.Forms.MenuItem();
			this.FileExit = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.Undo = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.FilterInvert = new System.Windows.Forms.MenuItem();
			this.FilterGrayScale = new System.Windows.Forms.MenuItem();
			this.FilterBrightness = new System.Windows.Forms.MenuItem();
			this.FilterContrast = new System.Windows.Forms.MenuItem();
			this.FilterGamma = new System.Windows.Forms.MenuItem();
			this.FilterColor = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.FilterSmooth = new System.Windows.Forms.MenuItem();
			this.GaussianBlur = new System.Windows.Forms.MenuItem();
			this.MeanRemoval = new System.Windows.Forms.MenuItem();
			this.Sharpen = new System.Windows.Forms.MenuItem();
			this.EmbossLaplacian = new System.Windows.Forms.MenuItem();
			this.EdgeDetectQuick = new System.Windows.Forms.MenuItem();
			this.FilterCustom = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.EdgeKirsh = new System.Windows.Forms.MenuItem();
			this.EdgePrewitt = new System.Windows.Forms.MenuItem();
			this.EdgeSobell = new System.Windows.Forms.MenuItem();
			this.EdgeDetectHorizontal = new System.Windows.Forms.MenuItem();
			this.EdgeVertical = new System.Windows.Forms.MenuItem();
			this.EdgeHomogenity = new System.Windows.Forms.MenuItem();
			this.EdgeDifference = new System.Windows.Forms.MenuItem();
			this.EdgeEnhance = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.Zoom25 = new System.Windows.Forms.MenuItem();
			this.Zoom50 = new System.Windows.Forms.MenuItem();
			this.Zoom100 = new System.Windows.Forms.MenuItem();
			this.Zoom150 = new System.Windows.Forms.MenuItem();
			this.Zoom200 = new System.Windows.Forms.MenuItem();
			this.Zoom300 = new System.Windows.Forms.MenuItem();
			this.Zoom500 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.Resize = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.FlipHorz = new System.Windows.Forms.MenuItem();
			this.flipVert = new System.Windows.Forms.MenuItem();
			this.flipBoth = new System.Windows.Forms.MenuItem();
			this.randomJitter = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.swirlNormal = new System.Windows.Forms.MenuItem();
			this.swirlAntiAlias = new System.Windows.Forms.MenuItem();
			this.Sphere = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.sphereAntialias = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.timeWarpNormal = new System.Windows.Forms.MenuItem();
			this.timeWarpAntiAlias = new System.Windows.Forms.MenuItem();
			this.Moire = new System.Windows.Forms.MenuItem();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.waterNormal = new System.Windows.Forms.MenuItem();
			this.waterAntialias = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.PixelateNoGrid = new System.Windows.Forms.MenuItem();
			this.PixelateGrid = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.Hue = new System.Windows.Forms.MenuItem();
			this.Saturation = new System.Windows.Forms.MenuItem();
			this.Luminance = new System.Windows.Forms.MenuItem();
			this.HLSChart = new System.Windows.Forms.MenuItem();
			this.HSLPicker1 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem5,
																					  this.menuItem4,
																					  this.menuItem3,
																					  this.menuItem6,
																					  this.menuItem2,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem17});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FileLoad,
																					  this.FileSave,
																					  this.FileExit});
			this.menuItem1.Text = "File";
			// 
			// FileLoad
			// 
			this.FileLoad.Index = 0;
			this.FileLoad.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
			this.FileLoad.Text = "Load";
			this.FileLoad.Click += new System.EventHandler(this.File_Load);
			// 
			// FileSave
			// 
			this.FileSave.Index = 1;
			this.FileSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.FileSave.Text = "Save";
			this.FileSave.Click += new System.EventHandler(this.File_Save);
			// 
			// FileExit
			// 
			this.FileExit.Index = 2;
			this.FileExit.Text = "Exit";
			this.FileExit.Click += new System.EventHandler(this.File_Exit);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 1;
			this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.Undo});
			this.menuItem5.Text = "Edit";
			// 
			// Undo
			// 
			this.Undo.Index = 0;
			this.Undo.Text = "Undo";
			this.Undo.Click += new System.EventHandler(this.OnUndo);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FilterInvert,
																					  this.FilterGrayScale,
																					  this.FilterBrightness,
																					  this.FilterContrast,
																					  this.FilterGamma,
																					  this.FilterColor});
			this.menuItem4.Text = "Filter";
			// 
			// FilterInvert
			// 
			this.FilterInvert.Index = 0;
			this.FilterInvert.Text = "Invert";
			this.FilterInvert.Click += new System.EventHandler(this.Filter_Invert);
			// 
			// FilterGrayScale
			// 
			this.FilterGrayScale.Index = 1;
			this.FilterGrayScale.Text = "GrayScale";
			this.FilterGrayScale.Click += new System.EventHandler(this.Filter_GrayScale);
			// 
			// FilterBrightness
			// 
			this.FilterBrightness.Index = 2;
			this.FilterBrightness.Text = "Brightness";
			this.FilterBrightness.Click += new System.EventHandler(this.Filter_Brightness);
			// 
			// FilterContrast
			// 
			this.FilterContrast.Index = 3;
			this.FilterContrast.Text = "Contrast";
			this.FilterContrast.Click += new System.EventHandler(this.Filter_Contrast);
			// 
			// FilterGamma
			// 
			this.FilterGamma.Index = 4;
			this.FilterGamma.Text = "Gamma";
			this.FilterGamma.Click += new System.EventHandler(this.Filter_Gamma);
			// 
			// FilterColor
			// 
			this.FilterColor.Index = 5;
			this.FilterColor.Text = "Color";
			this.FilterColor.Click += new System.EventHandler(this.Filter_Color);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 3;
			this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FilterSmooth,
																					  this.GaussianBlur,
																					  this.MeanRemoval,
																					  this.Sharpen,
																					  this.EmbossLaplacian,
																					  this.EdgeDetectQuick,
																					  this.FilterCustom});
			this.menuItem3.Text = "Convolution";
			// 
			// FilterSmooth
			// 
			this.FilterSmooth.Index = 0;
			this.FilterSmooth.Text = "Smooth";
			this.FilterSmooth.Click += new System.EventHandler(this.OnFilterSmooth);
			// 
			// GaussianBlur
			// 
			this.GaussianBlur.Index = 1;
			this.GaussianBlur.Text = "Gaussian Blur";
			this.GaussianBlur.Click += new System.EventHandler(this.OnGaussianBlur);
			// 
			// MeanRemoval
			// 
			this.MeanRemoval.Index = 2;
			this.MeanRemoval.Text = "Mean Removal";
			this.MeanRemoval.Click += new System.EventHandler(this.OnMeanRemoval);
			// 
			// Sharpen
			// 
			this.Sharpen.Index = 3;
			this.Sharpen.Text = "Sharpen";
			this.Sharpen.Click += new System.EventHandler(this.OnSharpen);
			// 
			// EmbossLaplacian
			// 
			this.EmbossLaplacian.Index = 4;
			this.EmbossLaplacian.Text = "EmbossLaplacian";
			this.EmbossLaplacian.Click += new System.EventHandler(this.OnEmbossLaplacian);
			// 
			// EdgeDetectQuick
			// 
			this.EdgeDetectQuick.Index = 5;
			this.EdgeDetectQuick.Text = "EdgeDetectQuick";
			this.EdgeDetectQuick.Click += new System.EventHandler(this.OnEdgeDetectQuick);
			// 
			// FilterCustom
			// 
			this.FilterCustom.Index = 6;
			this.FilterCustom.Text = "Custom";
			this.FilterCustom.Click += new System.EventHandler(this.OnFilterCustom);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.EdgeKirsh,
																					  this.EdgePrewitt,
																					  this.EdgeSobell,
																					  this.EdgeDetectHorizontal,
																					  this.EdgeVertical,
																					  this.EdgeHomogenity,
																					  this.EdgeDifference,
																					  this.EdgeEnhance});
			this.menuItem6.Text = "Edge Detection";
			// 
			// EdgeKirsh
			// 
			this.EdgeKirsh.Index = 0;
			this.EdgeKirsh.Text = "Kirsh";
			this.EdgeKirsh.Click += new System.EventHandler(this.OnEdgeKirsh);
			// 
			// EdgePrewitt
			// 
			this.EdgePrewitt.Index = 1;
			this.EdgePrewitt.Text = "Prewitt";
			this.EdgePrewitt.Click += new System.EventHandler(this.OnEdgePrewitt);
			// 
			// EdgeSobell
			// 
			this.EdgeSobell.Index = 2;
			this.EdgeSobell.Text = "Sobel";
			this.EdgeSobell.Click += new System.EventHandler(this.OnEdgeSobell);
			// 
			// EdgeDetectHorizontal
			// 
			this.EdgeDetectHorizontal.Index = 3;
			this.EdgeDetectHorizontal.Text = "Horizontal";
			this.EdgeDetectHorizontal.Click += new System.EventHandler(this.OnEdgeHorizontal);
			// 
			// EdgeVertical
			// 
			this.EdgeVertical.Index = 4;
			this.EdgeVertical.Text = "Vertical";
			this.EdgeVertical.Click += new System.EventHandler(this.OnEdgeVertical);
			// 
			// EdgeHomogenity
			// 
			this.EdgeHomogenity.Index = 5;
			this.EdgeHomogenity.Text = "Homogenity";
			this.EdgeHomogenity.Click += new System.EventHandler(this.OnEdgeHomogenity);
			// 
			// EdgeDifference
			// 
			this.EdgeDifference.Index = 6;
			this.EdgeDifference.Text = "Difference";
			this.EdgeDifference.Click += new System.EventHandler(this.OnEdgeDifference);
			// 
			// EdgeEnhance
			// 
			this.EdgeEnhance.Index = 7;
			this.EdgeEnhance.Text = "Edge Enhance";
			this.EdgeEnhance.Click += new System.EventHandler(this.EdgeEnhance_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 5;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.Zoom25,
																					  this.Zoom50,
																					  this.Zoom100,
																					  this.Zoom150,
																					  this.Zoom200,
																					  this.Zoom300,
																					  this.Zoom500});
			this.menuItem2.Text = "Zoom";
			// 
			// Zoom25
			// 
			this.Zoom25.Index = 0;
			this.Zoom25.Text = "25%";
			this.Zoom25.Click += new System.EventHandler(this.OnZoom25);
			// 
			// Zoom50
			// 
			this.Zoom50.Index = 1;
			this.Zoom50.Text = "50%";
			this.Zoom50.Click += new System.EventHandler(this.OnZoom50);
			// 
			// Zoom100
			// 
			this.Zoom100.Index = 2;
			this.Zoom100.Text = "100%";
			this.Zoom100.Click += new System.EventHandler(this.OnZoom100);
			// 
			// Zoom150
			// 
			this.Zoom150.Index = 3;
			this.Zoom150.Text = "150%";
			this.Zoom150.Click += new System.EventHandler(this.OnZoom150);
			// 
			// Zoom200
			// 
			this.Zoom200.Index = 4;
			this.Zoom200.Text = "200%";
			this.Zoom200.Click += new System.EventHandler(this.OnZoom200);
			// 
			// Zoom300
			// 
			this.Zoom300.Index = 5;
			this.Zoom300.Text = "300%";
			this.Zoom300.Click += new System.EventHandler(this.OnZoom300);
			// 
			// Zoom500
			// 
			this.Zoom500.Index = 6;
			this.Zoom500.Text = "500%";
			this.Zoom500.Click += new System.EventHandler(this.OnZoom500);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 6;
			this.menuItem7.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.Resize});
			this.menuItem7.Text = "Custom Filters";
			// 
			// Resize
			// 
			this.Resize.Index = 0;
			this.Resize.Text = "Resize";
			this.Resize.Click += new System.EventHandler(this.OnResize);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 7;
			this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem9,
																					  this.randomJitter,
																					  this.menuItem10,
																					  this.Sphere,
																					  this.menuItem12,
																					  this.Moire,
																					  this.menuItem13,
																					  this.menuItem14});
			this.menuItem8.Text = "Displacement Filters";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.FlipHorz,
																					  this.flipVert,
																					  this.flipBoth});
			this.menuItem9.Text = "Flip";
			// 
			// FlipHorz
			// 
			this.FlipHorz.Index = 0;
			this.FlipHorz.Text = "Horz";
			this.FlipHorz.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// flipVert
			// 
			this.flipVert.Index = 1;
			this.flipVert.Text = "Vert";
			this.flipVert.Click += new System.EventHandler(this.flipVert_Click);
			// 
			// flipBoth
			// 
			this.flipBoth.Index = 2;
			this.flipBoth.Text = "Both";
			this.flipBoth.Click += new System.EventHandler(this.flipBoth_Click);
			// 
			// randomJitter
			// 
			this.randomJitter.Index = 1;
			this.randomJitter.Text = "Random Jitter";
			this.randomJitter.Click += new System.EventHandler(this.randomJitter_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 2;
			this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.swirlNormal,
																					   this.swirlAntiAlias});
			this.menuItem10.Text = "Swirl";
			// 
			// swirlNormal
			// 
			this.swirlNormal.Index = 0;
			this.swirlNormal.Text = "Normal";
			this.swirlNormal.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// swirlAntiAlias
			// 
			this.swirlAntiAlias.Index = 1;
			this.swirlAntiAlias.Text = "AntiAlias";
			this.swirlAntiAlias.Click += new System.EventHandler(this.swirlAntiAlias_Click);
			// 
			// Sphere
			// 
			this.Sphere.Index = 3;
			this.Sphere.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.menuItem11,
																				   this.sphereAntialias});
			this.Sphere.Text = "Sphere";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 0;
			this.menuItem11.Text = "Normal";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click_1);
			// 
			// sphereAntialias
			// 
			this.sphereAntialias.Index = 1;
			this.sphereAntialias.Text = "Antialias";
			this.sphereAntialias.Click += new System.EventHandler(this.sphereAntialias_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 4;
			this.menuItem12.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.timeWarpNormal,
																					   this.timeWarpAntiAlias});
			this.menuItem12.Text = "Time Warp";
			// 
			// timeWarpNormal
			// 
			this.timeWarpNormal.Index = 0;
			this.timeWarpNormal.Text = "Normal";
			this.timeWarpNormal.Click += new System.EventHandler(this.timeWarpNormal_Click);
			// 
			// timeWarpAntiAlias
			// 
			this.timeWarpAntiAlias.Index = 1;
			this.timeWarpAntiAlias.Text = "AntiAlias";
			this.timeWarpAntiAlias.Click += new System.EventHandler(this.timeWarpAntiAlias_Click);
			// 
			// Moire
			// 
			this.Moire.Index = 5;
			this.Moire.Text = "Moire";
			this.Moire.Click += new System.EventHandler(this.Moire_Click);
			// 
			// menuItem13
			// 
			this.menuItem13.Index = 6;
			this.menuItem13.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.waterNormal,
																					   this.waterAntialias});
			this.menuItem13.Text = "Water";
			// 
			// waterNormal
			// 
			this.waterNormal.Index = 0;
			this.waterNormal.Text = "Normal";
			this.waterNormal.Click += new System.EventHandler(this.waterNormal_Click);
			// 
			// waterAntialias
			// 
			this.waterAntialias.Index = 1;
			this.waterAntialias.Text = "AntiAlias";
			this.waterAntialias.Click += new System.EventHandler(this.waterAntialias_Click);
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 7;
			this.menuItem14.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.PixelateNoGrid,
																					   this.PixelateGrid});
			this.menuItem14.Text = "Pixelate";
			// 
			// PixelateNoGrid
			// 
			this.PixelateNoGrid.Index = 0;
			this.PixelateNoGrid.Text = "No Grid";
			this.PixelateNoGrid.Click += new System.EventHandler(this.PixelateNoGrid_Click);
			// 
			// PixelateGrid
			// 
			this.PixelateGrid.Index = 1;
			this.PixelateGrid.Text = "Grid";
			this.PixelateGrid.Click += new System.EventHandler(this.PixelateGrid_Click);
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 8;
			this.menuItem17.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.Hue,
																					   this.Saturation,
																					   this.Luminance,
																					   this.HLSChart,
																					   this.HSLPicker1});
			this.menuItem17.Text = "ColorSpaces";
			// 
			// Hue
			// 
			this.Hue.Index = 0;
			this.Hue.Text = "Hue";
			this.Hue.Click += new System.EventHandler(this.Hue_Click);
			// 
			// Saturation
			// 
			this.Saturation.Index = 1;
			this.Saturation.Text = "Saturation";
			this.Saturation.Click += new System.EventHandler(this.Saturation_Click);
			// 
			// Luminance
			// 
			this.Luminance.Index = 2;
			this.Luminance.Text = "Luminance";
			this.Luminance.Click += new System.EventHandler(this.Luminance_Click);
			// 
			// HLSChart
			// 
			this.HLSChart.Index = 3;
			this.HLSChart.Text = "HLS Chart";
			this.HLSChart.Click += new System.EventHandler(this.HLSChart_Click);
			// 
			// HSLPicker1
			// 
			this.HSLPicker1.Index = 4;
			this.HSLPicker1.Text = "HSL Picker";
			this.HSLPicker1.Click += new System.EventHandler(this.HSLPicker1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(688, 344);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Graphic Filters For Dummies";
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			g.DrawImage(m_Bitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, (int)(m_Bitmap.Width*Zoom), (int)(m_Bitmap.Height * Zoom)));
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
		}

		private void File_Load(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			openFileDialog.InitialDirectory = "c:\\" ;
			openFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg| All Files|*.*";
			openFileDialog.FilterIndex = 2 ;
			openFileDialog.RestoreDirectory = true ;

			if(DialogResult.OK == openFileDialog.ShowDialog())
			{
				m_Bitmap = (Bitmap)Bitmap.FromFile(openFileDialog.FileName, false);
				this.AutoScroll = true;
				this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
				this.Invalidate();
			}
		}

		private void File_Save(object sender, System.EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			saveFileDialog.InitialDirectory = "c:\\" ;
			saveFileDialog.Filter = "Bitmap files (*.bmp)|*.bmp|Jpeg files (*.jpg)|*.jpg|All valid files (*.bmp/*.jpg)|*.bmp/*.jpg" ;
			saveFileDialog.FilterIndex = 1 ;
			saveFileDialog.RestoreDirectory = true ;

			if(DialogResult.OK == saveFileDialog.ShowDialog())
			{
				switch(saveFileDialog.FileName.Substring(saveFileDialog.FileName.Length-3, 3).ToLower())
				{
					case "bmp":
						m_Bitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
						break;
					case "png":
						m_Bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
						break;
					case "jpg":
					case "jpe":
					case "peg":
						m_Bitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
						break;
					case "tif":
					case "iff":
						m_Bitmap.Save(saveFileDialog.FileName, ImageFormat.Tiff);
						break;
				}
			}
		}

		private void File_Exit(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void Filter_Invert(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.Invert(m_Bitmap))
				this.Invalidate();
		}

		private void Filter_GrayScale(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.GrayScale(m_Bitmap))
				this.Invalidate();
		}

		private void Filter_Brightness(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Brightness(m_Bitmap, dlg.nValue))
					this.Invalidate();
			}
		}

		private void Filter_Contrast(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Contrast(m_Bitmap, (sbyte)dlg.nValue))
					this.Invalidate();
			}
		
		}

		private void Filter_Gamma(object sender, System.EventArgs e)
		{
			GammaInput dlg = new GammaInput();
			dlg.red = dlg.green = dlg.blue = 1;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Gamma(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
		}

		private void Filter_Color(object sender, System.EventArgs e)
		{
			ColorInput dlg = new ColorInput();
			dlg.red = dlg.green = dlg.blue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Color(m_Bitmap, dlg.red, dlg.green, dlg.blue))
					this.Invalidate();
			}
		
		}

		private void OnZoom25(object sender, System.EventArgs e)
		{
			Zoom = .25;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom50(object sender, System.EventArgs e)
		{
			Zoom = .5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom100(object sender, System.EventArgs e)
		{
			Zoom = 1.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom150(object sender, System.EventArgs e)
		{
			Zoom = 1.5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom200(object sender, System.EventArgs e)
		{
			Zoom = 2.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom300(object sender, System.EventArgs e)
		{
			Zoom = 3.0;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnZoom500(object sender, System.EventArgs e)
		{
			Zoom = 5;
			this.AutoScrollMinSize = new Size ((int)(m_Bitmap.Width * Zoom), (int)(m_Bitmap.Height * Zoom));
			this.Invalidate();
		}

		private void OnFilterSmooth(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.Smooth(m_Bitmap, 1))
				this.Invalidate();
		}

		private void OnGaussianBlur(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.GaussianBlur(m_Bitmap, 4))
				this.Invalidate();
		}

		private void OnMeanRemoval(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.MeanRemoval(m_Bitmap, 9))
				this.Invalidate();
		}

		private void OnSharpen(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.Sharpen(m_Bitmap, 11))
				this.Invalidate();
		}

		private void OnEmbossLaplacian(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.EmbossLaplacian(m_Bitmap))
				this.Invalidate();
		}

		private void OnEdgeDetectQuick(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.EdgeDetectQuick(m_Bitmap))
				this.Invalidate();
		}

		private void OnUndo(object sender, System.EventArgs e)
		{
			Bitmap temp = (Bitmap)m_Bitmap.Clone();
			m_Bitmap = (Bitmap)m_Undo.Clone();
			m_Undo = (Bitmap)temp.Clone();
			this.Invalidate();
		}

		private void OnFilterCustom(object sender, System.EventArgs e)
		{
			Convolution dlg = new Convolution();

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.Conv3x3(m_Bitmap, dlg.Matrix))
					this.Invalidate();
			}
		
		}

		private void OnEdgeKirsh(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.EdgeDetectConvolution(m_Bitmap, BitmapFilter.EDGE_DETECT_KIRSH,  (byte)dlg.nValue))
					this.Invalidate();
			}		
		}

		private void OnEdgePrewitt(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.EdgeDetectConvolution(m_Bitmap, BitmapFilter.EDGE_DETECT_PREWITT,  (byte)dlg.nValue))
					this.Invalidate();
			}		
		}

		private void OnEdgeSobell(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.EdgeDetectConvolution(m_Bitmap, BitmapFilter.EDGE_DETECT_SOBEL,  (byte)dlg.nValue))
					this.Invalidate();
			}		
		}

		private void OnEdgeHorizontal(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.EdgeDetectHorizontal(m_Bitmap))
				this.Invalidate();
		}

		private void OnEdgeVertical(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if(BitmapFilter.EdgeDetectVertical(m_Bitmap))
				this.Invalidate();
		}

		private void OnEdgeHomogenity(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.EdgeDetectHomogenity(m_Bitmap, (byte)dlg.nValue))
					this.Invalidate();
			}		
		}

		private void OnEdgeDifference(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.EdgeDetectDifference(m_Bitmap, (byte)dlg.nValue))
					this.Invalidate();
			}		
		}

		private void EdgeEnhance_Click(object sender, System.EventArgs e)
		{
			Parameter dlg = new Parameter();
			dlg.nValue = 0;

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				if(BitmapFilter.EdgeEnhance(m_Bitmap, (byte)dlg.nValue))
					this.Invalidate();
			}		
		}

		private void OnResize(object sender, System.EventArgs e)
		{
			Resize dlg = new Resize();
			dlg.nWidth = m_Bitmap.Width;
			dlg.nHeight = m_Bitmap.Height;
			dlg.bBilinear = true;

			m_Undo = (Bitmap)m_Bitmap.Clone();

			if (DialogResult.OK == dlg.ShowDialog())
			{
				m_Bitmap = BitmapFilter.Resize(m_Bitmap, dlg.nWidth, dlg.nHeight, dlg.bBilinear);
				this.Invalidate();
			}
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Flip(m_Bitmap, true, false))
				this.Invalidate();
		}

		private void flipVert_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Flip(m_Bitmap, false, true))
				this.Invalidate();
		}

		private void flipBoth_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Flip(m_Bitmap, true, true))
				this.Invalidate();		
		}

		private void randomJitter_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.RandomJitter(m_Bitmap, 5))
				this.Invalidate();		
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Swirl(m_Bitmap, .04, false))
				this.Invalidate();					
		}

		private void swirlAntiAlias_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Swirl(m_Bitmap, .04, true))
				this.Invalidate();							
		}

		private void menuItem11_Click_1(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Sphere(m_Bitmap, false))
				this.Invalidate();							
		}

		private void sphereAntialias_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Sphere(m_Bitmap, true))
				this.Invalidate();							
		}

		private void timeWarpNormal_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.TimeWarp(m_Bitmap, 15, false))
				this.Invalidate();							
		}

		private void timeWarpAntiAlias_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.TimeWarp(m_Bitmap, 15, true))
				this.Invalidate();							
		}

		private void Moire_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Moire(m_Bitmap, 3))
				this.Invalidate();							
		}

		private void waterNormal_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Water(m_Bitmap, 15, false))
				this.Invalidate();							
		}

		private void waterAntialias_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Water(m_Bitmap, 15, false))
				this.Invalidate();									
		}

		private void PixelateNoGrid_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Pixelate(m_Bitmap, 15, false))
				this.Invalidate();									
		}

		private void PixelateGrid_Click(object sender, System.EventArgs e)
		{
			m_Undo = (Bitmap)m_Bitmap.Clone();
			if (BitmapFilter.Pixelate(m_Bitmap, 15, true))
				this.Invalidate();									
		}

		private void Luminance_Click(object sender, System.EventArgs e)
		{
			LuminanceDlg dlg = new LuminanceDlg();
			dlg.source = m_Bitmap;

			if (dlg.ShowDialog()== DialogResult.OK)
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				m_Bitmap = (Bitmap)dlg.dest.Clone();
				Invalidate();
			}
		}

		private void Hue_Click(object sender, System.EventArgs e)
		{
			HueDlg dlg = new HueDlg();
			dlg.source = m_Bitmap;

			if (dlg.ShowDialog()== DialogResult.OK)
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				m_Bitmap = (Bitmap)dlg.dest.Clone();
				Invalidate();
			}
		}

		private void Saturation_Click(object sender, System.EventArgs e)
		{
			SaturationDlg dlg = new SaturationDlg();
			dlg.source = m_Bitmap;

			if (dlg.ShowDialog()== DialogResult.OK)
			{
				m_Undo = (Bitmap)m_Bitmap.Clone();
				m_Bitmap = (Bitmap)dlg.dest.Clone();
				Invalidate();
			}
		}

		private void HLSChart_Click(object sender, System.EventArgs e)
		{
			HLSDemo dlg = new HLSDemo();
			dlg.ShowDialog();
		}

		private Color hls1Selected = Color.Aqua;

		private void HSLPicker1_Click(object sender, System.EventArgs e)
		{
			HSLColorPicker dlg = new HSLColorPicker();
			dlg.SelectedColor = hls1Selected;
		
			if (dlg.ShowDialog()==DialogResult.OK) hls1Selected = dlg.SelectedColor;
		}
	}
}

