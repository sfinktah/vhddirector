using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace VHD_Director
{
    public partial class PartitionRoundedPanel : UserControlLayout
    {
        public int Number { get; set; }
        public int ChildCount { get; set; }

        public double UsedSpacePercentage
        {
            set { UsedSpaceBar.Value = UsedSpaceBar.Maximum; /* (int)(value * 100.0); */ }
            get { return (UsedSpaceBar.Value / 100.0); }
        }
        public double PartitionLengthPercent { get { return _PartitionLengthPercent; } set { if (value != _PartitionLengthPercent) { _PartitionLengthPercent = value; OurLayout(); } } }
        public PartitionUsageModel partitionModel { get; set; }
        public double PartitionStartPercent { get; set; }
        public double PartitionRealLengthPercent { get; set; }
        public System.Collections.BitArray ba { get; set; }

        private double _PartitionLengthPercent;
        internal PartitionController controller;
        private ZoomOverlayForm zoomform;
        private ProgressBar UsedSpaceBar = new ProgressBar();
        private VhdReadException vhdReadException;
        protected Color RectangleFillColorWas;


        public PartitionRoundedPanel(int Partition, int PartitionCount, PartitionController Controller)
        {

            Number = Partition;
            ChildCount = PartitionCount;
            this.controller = Controller;

            // this.Margin = new Padding(5);
            // this.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);

            InitializeComponent();

            if (Controller.vhd.vhdReadException != null)
            {
                this.vhdReadException = Controller.vhd.vhdReadException;
                greenPartitionView1.Hue = 0.2F;
            }
            // If we're checking this AFTER InitializeComponent and it appears to be working fine, we need to check when the actual bitmaps
            // and other complex drawing routines actually trigger.
            //
            // ... hmmm, apparently this is causing the MyLabel2's to draw (and request parent draw)
            // before we adjust our width.

            if (Number == 0)
            {
                greenPartitionView1.ShadowLeft = true;
            }

            if (Number == ChildCount - 1)
            {
                greenPartitionView1.ShadowRight = true;
            }

            if (controller.ClusterBitArray != null)
            {
                greenPartitionView1.ProgressBar = true;
                // greenPartitionView1.ClusterUsage = controller.ClusterBitArray;
            }

            // QuickTip.Add(UsedSpaceBar, "Used space");
            // QuickTip.Add(blackProgressBar1, "Used space");
            //using (Bitmap b = new Bitmap(Properties.Resources.MagnifyingGlass_96_64_centered))
            //{
            //    Cursor c = new Cursor(b.GetHicon());
            //    blackProgressBar1.Cursor = c;
            //}
            
            ResizeRedraw = true;

            //this.BackColor = Color.LightGreen;
            // this.RectangleFillColor = Color.WhiteSmoke;
            // this.Dock = System.Windows.Forms.DockStyle.Fill;
            
            //this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)
            //            | System.Windows.Forms.AnchorStyles.Left)
            //            )));
            
        }


        protected override void OurLayout()
        {
            csharp_debug.Debug.DebugWinControl(this);
            if (Parent == null)
            {
                return;
            }
            using (new csharp_debug.SectionStopWatch("PRP::OurLayout"))
            {

                // this.UsedSpaceBar.Size = this.UsedSpaceBar.Size;
                if (Parent != null && UsedSpaceBar != null)
                {
                    SuspendLayout();
                    if (controller.ClusterBitArray != null)
                    {
                        // greenPartitionView1.ProgressBar = true;
                        greenPartitionView1.ClusterUsage = controller.ClusterBitArray;
                    }

                    //    this.UsedSpaceBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)
                    //| System.Windows.Forms.AnchorStyles.Left)
                    //)));



                    string partitionSize = Format.FormatBytes((long)partitionModel.LBALength * 512);

                    if (partitionModel.isEmpty)
                    {
                        // this.RectangleFillColor = this.BackColor;
                        // this.UsedSpaceBar.Visible = false;
                        this.greenPartitionView1.ProgressBar = false;
                        this.greenPartitionView1.line1 = "Free Space";
                        this.greenPartitionView1.line2 = partitionSize;
                    }

                    else
                    {
                        // this.RectangleFillColor = Color.WhiteSmoke;

                        if (controller.IsPartitionModel)
                        {
                            this.greenPartitionView1.line1 = controller.PartitionModel.Type + "-" + Format.FormatBytes((long)controller.PartitionModel.Size);
                            if (controller.IsLogicalPartitionModel)
                            {
                                if (String.IsNullOrEmpty(controller.LogicalPartitionModel.FileSystem))
                                {
                                    this.greenPartitionView1.line1 = controller.LogicalPartitionModel.Name + " Unformatted";
                                    this.greenPartitionView1.line2 = Format.FormatBytes((long)controller.PartitionModel.Size);
                                }
                                else
                                {
                                    this.greenPartitionView1.line1 = controller.LogicalPartitionModel.Name + " " + (!String.IsNullOrEmpty(controller.LogicalPartitionModel.VolumeName) ? ("(" + controller.LogicalPartitionModel.VolumeName + ")") : "No Label");
                                    this.greenPartitionView1.line2 = Format.FormatBytes((long)controller.PartitionModel.Size) + " " + controller.LogicalPartitionModel.FileSystem; 
                                }
                            }
                            else
                            {
                                this.greenPartitionView1.line2 = "Not Mounted";
                            }
                        }
                        else
                        {
                            string partitionType = partitionModel.FriendlyPartitionType;
                            this.greenPartitionView1.line1 = partitionType;
                            this.greenPartitionView1.line2 = partitionSize;
                        }
                        // doneInitLayout = true;
                    }

                    //this.label2.Text += "\r";
                    if (this.partitionModel.pendingFormat)
                    {
                        this.greenPartitionView1.line3 += "Pending Format. ";
                    }

                    if (this.partitionModel.pendingRemove)
                    {
                        this.greenPartitionView1.line3 += "Pending Removal. ";
                    }
                    if (this.partitionModel.pendingResize)
                    {
                        this.greenPartitionView1.line3 += "Pending Resize. ";
                    }

                    // ---
                    // int w = (Parent.Width - Parent.Padding.Left - Parent.Padding.Right) / ChildCount;
                    int availableWidth = (Parent.Width - Parent.Padding.Left - Parent.Padding.Right);
                    int w = (int)(availableWidth * PartitionLengthPercent);
                    int x = (int)(availableWidth * PartitionStartPercent);
                    // this.Width = w; // -this.Margin.Left - this.Margin.Right;
                    // this.Location = new Point(Parent.Padding.Left + (Number * w), Parent.Padding.Top);

                    this.Width = w;
                    this.Location = new Point(Parent.Padding.Left + x - 8, Location.Y);
                    // this.UsedSpaceBar.Size = new System.Drawing.Size(w - Padding.Left - Padding.Right + 5, 14);
                    // this.blackProgressBar1.Size = new System.Drawing.Size(w - Padding.Left - Padding.Right + 5, 14);

                    ResumeLayout();
                }
            }
        }


#if false
        public override void DrawRoundedRectangle(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            int strokeOffset = Convert.ToInt32(Math.Ceiling(DrawPen.Width));
            Bounds = Rectangle.Inflate(Bounds, -strokeOffset, -strokeOffset);

            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            DrawPen.Width = StrokeWidth;
            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;

            // LinearGradientBrush blendBrush = new LinearGradientBrush(Bounds, Color.FromArgb(0xd2, 0xd2, 0xd2), Color.FromArgb(0xeb, 0xeb, 0xeb), 90f);
            LinearGradientBrush blendBrush = new LinearGradientBrush(Bounds, Color.FromArgb(0xe7, 0xe7, 0xe7), Color.White, 90f);
            Blend blend = new Blend();
            blend.Factors = new float[] { 1.0f, 1.0f, 0.0f, 0.0f, 1.0f };
            blend.Positions = new float[] { 0.0f, 0.05f, 0.15f, 0.71f, 1.0f };
            blendBrush.Blend = blend;


            using (GraphicsPath gfxPath = new GraphicsPath())
            {
                gfxPath.AddArc(Bounds.X, Bounds.Y, CornerRadius, CornerRadius, 180, 90);
                gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y, CornerRadius, CornerRadius, 270, 90);
                gfxPath.AddArc(Bounds.X + Bounds.Width - CornerRadius, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 0, 90);
                gfxPath.AddArc(Bounds.X, Bounds.Y + Bounds.Height - CornerRadius, CornerRadius, CornerRadius, 90, 90);
                gfxPath.CloseAllFigures();

                // gfx.FillPath(new SolidBrush(FillColor), gfxPath);
                gfx.FillPath(blendBrush, gfxPath);

                gfx.DrawPath(DrawPen, gfxPath);

                DrawRoundedRectangleCustomPost(gfx, Bounds, CornerRadius, DrawPen, FillColor);
            }

            blendBrush.Dispose();

            //background: #D0ECCC;
            ///* old browsers */
            //background: -moz-linear-gradient(top, #D0ECCC 5%, #87CD65 15%, #92D664 70%, #ADE763 99%);
            ///* firefox */
            //background: -webkit-gradient(linear, left top, left bottom, color-stop(5%,#D0ECCC), color-stop(15%,#87CD65), color-stop(70%,#92D664), color-stop(99%,#ADE763));
            ///* webkit */
            //filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#D0ECCC', endColorstr='#ADE763',GradientType=0 );
            ///* ie */
        }
        // protected new bool doneInitLayout = false;
        protected override void InitLayout()
        {
            csharp_debug.Debug.DebugWinControl(this);
            base.InitLayout();
            OurLayout();

  
        }

       
       


        protected override void OnResize(EventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);
            base.OnResize(e);
        }
#endif
         private void Partition_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                // contextMenuStrip1.Items[2].Enabled = vhd.footer.Changed;
                this.contextMenuStrip1.Show((sender as Control).PointToScreen(e.Location));
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (sender is BlackProgressBar)
                {
                    if (zoomform == null || zoomform.IsDisposed)
                    {
                        // (sender as Control).MouseLeave +=new EventHandler(PartitionRoundedPanel_MouseLeaveCancelZoom);
                        zoomform = new ZoomOverlayForm();
                        zoomform.Location = new Point(0, PointToScreen(Location).Y - 100);
                        zoomform.Width = Screen.PrimaryScreen.Bounds.Width;
                        zoomform.maximumX = (sender as Control).Width - 10;
                        zoomform.Image = (sender as BlackProgressBar).BitArrayBarBitmapFull;
                    }

                    zoomform.offsetX = e.X;
                    if (zoomform.Visible)
                    {
                        zoomform.Invalidate(true);
                    }
                    else
                    {
                        zoomform.Show();
                    }
                }
            }
        }



        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // RectangleFillColor = RectangleFillColorWas = Color.LightGray;
            partitionModel.pendingFormat = true; 
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // RectangleFillColor = RectangleFillColorWas = Color.Gray;
            partitionModel.pendingRemove = true;
        }

        private void resizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            partitionModel.pendingResize = true;
        }

        private void PartitionRoundedPanel_MouseEnter(object sender, EventArgs e)
        {
            //if (this.RectangleFillColorWas != this.RectangleFillColor)
            //{
            //    RectangleFillColorWas = this.RectangleFillColor;
            //}
            //// this.RectangleFillColor = Color.White;
            //Invalidate(true);
        }

        private void PartitionRoundedPanel_MouseLeave(object sender, EventArgs e)
        {
            // this.RectangleFillColor = RectangleFillColorWas;
            if (zoomform != null)
            {
                zoomform.Close();
                zoomform = null;
            }
            Invalidate(true);

        }

        private void PartitionRoundedPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.MouseEnter += new System.EventHandler(this.PartitionRoundedPanel_MouseEnter);
            e.Control.MouseLeave += new System.EventHandler(this.PartitionRoundedPanel_MouseLeave);
            e.Control.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Partition_MouseClick);
        }

        private void PartitionRoundedPanel_Load(object sender, EventArgs e)
        {
            OurLayout();

            this.MouseEnter += new System.EventHandler(this.PartitionRoundedPanel_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.PartitionRoundedPanel_MouseLeave);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Partition_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PartitionRoundedPanel_MouseMove);


            foreach (Control c in Controls)
            {
                c.MouseEnter += new System.EventHandler(this.PartitionRoundedPanel_MouseEnter);
                c.MouseLeave += new System.EventHandler(this.PartitionRoundedPanel_MouseLeave);
                c.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Partition_MouseClick);
                c.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PartitionRoundedPanel_MouseMove);

              
            }
        }


        private void PartitionRoundedPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is BlackProgressBar && zoomform != null && !zoomform.IsDisposed)
            {
                zoomform.offsetX = e.X;
                zoomform.Invalidate(true);
            }
        }

        private void PartitionRoundedPanel_Resize(object sender, EventArgs e)
        {
            greenPartitionView1.InvalidateBuffer(true);
        }

        internal void Reload()
        {
            OurLayout();
        }
    }
}