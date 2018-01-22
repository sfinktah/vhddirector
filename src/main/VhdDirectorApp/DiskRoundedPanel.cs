using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace VhdDirectorApp
{
    public partial class DiskRoundedPanel : RoundedPanel
    {
        public DiskRoundedPanel()
        {
            InitializeComponent();
            ResizeRedraw = true;
        }

        protected override void InitLayout()
        {
            csharp_debug.Debug.DebugWinControl(this);
            base.InitLayout();


            if (vhd.batUsage != null)
            {
                // bitArrayBar1 = new BitArrayBar(vhd.batUsage);
            }

            if (StartingWidth == 0)
            {
                StartingWidth = Width;
            }
            // Basic MBR
            // 100 GB


            // doneInitLayout = true;
        }

        public override void DrawRoundedRectangle(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            // Rectangle Bounds = new Rectangle(strokeOffset, strokeOffset, Width - strokeOffset * 2, Height - strokeOffset * 2);
            int notch = CornerRadius + (CornerRadius >> 1);
            int along = 150;


            int X = Bounds.X + 1;
            int radius = CornerRadius;
            int Y = Bounds.Y + notch + 1;
            int width = Bounds.Width - 2;
            int height = Bounds.Height - notch - 2;

            DrawPen.Width = StrokeWidth;
            DrawPen.EndCap = DrawPen.StartCap = LineCap.Round;


            using (GraphicsPath gfxPath = new GraphicsPath())
            {

                // TOP LINE LEFT
                //gfxPath.AddLine(X + radius, Y, X + along - (radius * 4), Y);

                gfx.SmoothingMode = SmoothingMode.AntiAlias;


                gfxPath.AddArc(X + along - (int)(radius * 3), Y - notch - radius * 0, radius * 2, radius * 2, 180 + 34, 34);


                // TOP LINE RIGHT
                //gfxPath.AddLine(X + along - (radius * 0), Y - notch, X + width - (radius*2), Y - notch);


                // TOP RIGHT
                gfxPath.AddArc(X + width - (radius * 2), Y - notch, radius * 2, radius * 2, 270, 90);

                // RIGHT LINE
                // gfxPath.AddLine(X + width, Y + (radius * 1), X + width, Y + height - (radius * 2));

                // BOTTOM RIGHT
                gfxPath.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);

                // BOTTOM LINE
                // gfxPath.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);

                // BOTTOM LEFT ?
                gfxPath.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);

                // LEFT LINE
                // gfxPath.AddLine(X, Y + height - (radius * 2), X, Y + radius + notch);

                // TOP LEFT
                gfxPath.AddArc(X, Y, radius * 2, radius * 2, 180, 90);



                gfxPath.AddArc(X + along - (radius * 5), Y - radius * 2, radius * 2, radius * 2, 90, -45);

                gfxPath.CloseAllFigures();

                gfx.FillPath(new SolidBrush(FillColor), gfxPath);
                gfx.DrawPath(DrawPen, gfxPath);
            }

            //  DrawRoundedRectangleCustomPost(gfx, Bounds, CornerRadius, DrawPen, FillColor);
        }

        protected override void DrawRoundedRectangleCustomPost(Graphics gfx, Rectangle Bounds, int CornerRadius, Pen DrawPen, Color FillColor)
        {
            using (GraphicsPath gfxPath = new GraphicsPath())
            {
                gfxPath.AddLine(
                    Bounds.X + CornerRadius / 3, Bounds.Y + Bounds.Height - CornerRadius,
                    Bounds.X + Bounds.Width - CornerRadius / 3, Bounds.Y + Bounds.Height - CornerRadius);
                gfx.DrawPath(DrawPen, gfxPath);
            }
            /*
            using (GraphicsPath gfxPath = new GraphicsPath())
            {
                gfxPath.AddRectangle(new Rectangle(
                    Bounds.X + CornerRadius / 3,                        Bounds.Y + notch + Bounds.Height + 4 - CornerRadius, 
                    Bounds.Width - CornerRadius * 2 / 3,                6));
                // gfx.FillPath(new SolidBrush(FillColor), gfxPath);
                gfx.FillPath(Brushes.Gray, gfxPath);
            }*/
        }

        protected void FillLabels()
        {
            if (diskModel.vhdReadException != null)
            {
                if (pictureBox1.Image != Properties.Resources.dead_identicon)
                {
                    pictureBox1.Image = Properties.Resources.dead_identicon;
                }

                this.DiskLabel.Text = Path.GetFileName(diskModel.footer.FileName);
                this.DiskSummary.Text = "VHD Error";
                this.labelTopRight.Text = (vhd.vhdReadException.InnerException != null) ? vhd.vhdReadException.InnerException.Message : vhd.vhdReadException.Message;

                return;
            }
            string identicon = "http://www.gravatar.com/avatar/" + diskModel.footer.UniqueId.ToLower() + "?d=identicon&f=y&s=40";
            if (!pictureBox1.ImageLocation.Equals(identicon))
            {
                try
                {
                    pictureBox1.LoadAsync(identicon);
                }
                catch (System.Net.WebException e) { }
            }

            string vhdType = diskModel.footer.DiskType;
            if (vhd.Partitions != null)
            {
                vhdType += " (M)";
                this.labelTopRight.Text = "mounted";


            }

            if (!String.IsNullOrEmpty(vhd.MountedAs))
            {
                this.labelTopRight.Text = "Mounted";
            }

            string driveSize = "Disc: " + Format.FormatBytes((long)diskModel.footer.CurrentSize);
            string vhdSize = "File: " + Format.FormatBytes(diskModel.footer.VhdFileSize);
            string creatorFiller = GetCreatorAppName(diskModel.footer.CreatorApplication); // +"::" + diskModel.footer.CreatorHostOS;
            // this.DiskLabel.Text = vhdType;
            this.DiskLabel.Text = Path.GetFileName(diskModel.footer.FileName);
            this.DiskSummary.Text = driveSize + "\r" + vhdSize + "\r" + vhdType;
            // labelTopRight.Text = "Ack!";


        }


        private string GetCreatorAppName(string p)
        {
            char[] trimChars = { ' ', '\r', '\n', '\t', '\x00' };
            switch (p.Trim(trimChars))
            {
                case "vhd1": return "VHD Director";
                case "win": return "Windows";
                case "dutl": return "DiscUtils";
                case "d2v": return "disk2vhd";
                default: return p.Trim(trimChars);
            }
        }

        internal VirtualHardDisk diskModel { get { return this.vhd; }  set { } }
        internal VirtualHardDisk vhd { get; set; }
        int resizeMaxDelta;

        private void DiskRoundedPanel_ClientSizeChanged(object sender, EventArgs e)
        {
            if (diskModel != null && DeltaX != 0)
            {
                // MOved this to the Delta calculation
            }
        }
        long sectorsPerPixel;
        private bool CalculateDeltaInSectors()
        {



            //if (Parent != null)
            //{
            //    OriginalWidth = Parent.Width - Parent.Margin.Right - Parent.Margin.Left;
            //}

            // OriginalWidth -= MinWidth;

            // TODO: Replace Parent.ClientSize.Width with a more accurate figure, perhaps
            // the width we started with.

            long LBADiskLength = bIsResizing ? oldCurrentSize : (long)(diskModel.footer.CurrentSize / 512);

            // long borrowedPixels = sizeBorrowed / sectorsPerPixel;
            // sectorsPerPixel = (long)(sectorsPerPixel * ((double)(LBADiskLength + sizeBorrowed) / LBADiskLength));

            long newLBADiskLength = LBADiskLength - (sectorsPerPixel * (int)(DeltaX * 1.0));
            long minFreeSpace = (diskUsageModel.GetPartitionUsageModel(diskUsageModel.PartitionCount - 1).LBALength +
                diskUsageModel.GetPartitionUsageModel(diskUsageModel.PartitionCount - 1).LBAStart);
            if (newLBADiskLength < minFreeSpace)
            {

                // return false;
                newLBADiskLength = minFreeSpace;
                if (bIsResizing)
                {
                    // Width = MinWidth + Padding.Left + Padding.Right + (int)(minFreeSpace / sectorsPerPixel);
                }
            }

            diskModel.footer.CurrentSize = (ulong)(newLBADiskLength * 512);
            DiscUtils.Geometry newGeo = DiscUtils.Geometry.FromCapacity((long)diskModel.footer.CurrentSize);
            // MessageBox.Show("Geometry is " + ((long)(diskModel.footer.CurrentSize / 512) - newGeo.TotalSectors).ToString() + " sectors under");
            diskModel.footer.DiskCylinders = (ushort)newGeo.Cylinders;
            diskModel.footer.DiskHeads = (byte)newGeo.HeadsPerCylinder;
            diskModel.footer.DiskSectors = (byte)newGeo.SectorsPerTrack;
            diskModel.footer.Changed = true;
            DeltaX = 0;
            if (!bIsResizing)
            {

                // ned to fix this TODO
                int OriginalWidth = bIsResizing ? (oldSize.Width - Padding.Left - Padding.Right) : InsideWidth;
                sectorsPerPixel = (LBADiskLength / InsideWidth);
            }
            return true;
        }

        int StartingWidth;
        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            // this.labelTopRight.Text = "Dumbasses";
        }

        private long sizeBorrowed = 0;
        private double FreeSpaceStartPercent;
        private void CalculatePartitions()
        {
            
            FillLabels();
            if (vhd.vhdReadException != null)
            {
                return;
            }
            
            // this.Width = Width - Margin.Right - Margin.Left;
            // this.Width = StartingWidth;
            if (Parent != null)
            {
                // this.Width = Parent.Width - Parent.Margin.Right - Parent.Margin.Left;
            }

            //this.Anchor = Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top)
            //            | System.Windows.Forms.AnchorStyles.Left)
            //            | System.Windows.Forms.AnchorStyles.Right)));
            // adl.Dock = System.Windows.Forms.DockStyle.Fill;

            double minPercent; // We want each prp to be a minimum of 95 pixels wide.  That's slightly over 0.15
            int availableWidth = (Width - Padding.Left - Padding.Right);
            minPercent = 80 / (double)availableWidth;
            if (minPercent > 1D / diskUsageModel.PartitionCount)
            {
                minPercent = 1D / diskUsageModel.PartitionCount;
            }
            // minPercent = 0;

            PartitionUsageModel pm;

            long LBADiskLength = (long)(diskModel.footer.CurrentSize / 512);

            int partitionCount = diskUsageModel.PartitionCount;

            List<int> sizes = new List<int>();
            List<double> sizePercents = new List<double>();
            int sizeLeft = (int)LBADiskLength;
            sizeBorrowed = 0;
            int newSize;

            for (int i = 0; i < partitionCount; i++)
            {
                int size = (int)diskUsageModel.GetPartitionUsageModel(i).LBALength;
                double percent = (double)size / LBADiskLength;
                if (percent < minPercent)
                {
                    percent = minPercent;
                    newSize = (int)(LBADiskLength * percent);
                    sizeBorrowed += (newSize - size);
                    size = newSize;
                }
                sizePercents.Add(percent);
                sizes.Add(size);
            }

            double totalPercent = sizePercents.Sum();
            while (totalPercent > 1D)
            {
                for (int i = 0; i < sizePercents.Count; i++)
                {
                    if ((sizePercents[i]) > minPercent)
                    {
                        sizePercents[i] -= 0.01;
                    }
                }
                totalPercent = sizePercents.Sum();
            }
            // e.g. we're using 115% of space (1.15), need to normalise down to 100%, but can't shorten below minPercent.



            double PercentPointer = 0.0;

            //List<Control> toDelete = new List<Control>();
            //foreach (Control c in this.Controls)
            //{
            //    if (c is PartitionRoundedPanel)
            //    {
            //        toDelete.Add(c);
            //    }
            //}
            //foreach (Control c in toDelete)
            //{
            //    //  Controls.Remove(c);
            //}
            

            for (int i = 0; i < partitionCount; i++)
            {
                PartitionRoundedPanel prp;
                if (Controls.Count < (5 + partitionCount))
                {
                    // TODO: SOrt out this mess of upside down MVC and duplicate values and improper or non-use of models and controllers
                    // NOTE: PartitionController will call GetCluster which may result in VhdReadException
                    prp = new PartitionRoundedPanel(i, partitionCount, new PartitionController(vhd, i));
                    // prp.controller = new PartitionController(vhd, i);
                }
                else
                {
                    prp = (Controls[i + 5] as PartitionRoundedPanel);
                }

                prp.SuspendLayout();

                pm = diskUsageModel.GetPartitionUsageModel(i);
                // double partitionPercent = (double)pm.LBALength / LBADiskLength;
                // double partitionPercent = (double)sizes[i] / LBADiskLength;
                double partitionPercent = sizePercents[i];
                prp.PartitionStartPercent = (double)pm.LBAStart / LBADiskLength;
                if (prp.PartitionStartPercent < PercentPointer)
                {
                    prp.PartitionStartPercent = PercentPointer;
                }
                PercentPointer = prp.PartitionStartPercent;
                prp.PartitionRealLengthPercent = (double)pm.LBALength / LBADiskLength;
                prp.PartitionLengthPercent = partitionPercent;
                PercentPointer += prp.PartitionLengthPercent;

                prp.partitionModel = pm; // This is *so* wrong
                prp.UsedSpacePercentage = prp.PartitionRealLengthPercent;
                // prp.Size = new Size((adl.clientWidth / partitionCount) - 20, prp.Size.Height);
                // int w = (ClientSize.Width - adl.Padding.Left) / partitionCount;
                // prp.Location = new Point(100 + (i * w), 5);
                prp.Number = i;
                prp.ChildCount = partitionCount;
                // prp.RectangleFillColor = Color.WhiteSmoke;
                // prp.Size = new Size(w - 10, 80);
                // Size is calculated in prp.  Location should be too.  But hey.
                // partitionModels.Add(pm);
                prp.Name = "PartitionRoundedPanel " + i;
                if (this.Controls.IndexOf(prp) < 0)
                {
                    this.Controls.Add(prp);
                }
                else
                {
                    
                }
                prp.ResumeLayout(true);
                // prp.Invalidate(true);
            }
            FillLabels();
            FreeSpaceStartPercent = PercentPointer;

        }


        // Resizable User Control
        // http://social.msdn.microsoft.com/Forums/en/csharplanguage/thread/8563650b-f88d-42a8-8b05-2dca4ae849a7
        // by http://social.msdn.microsoft.com/profile/kooboobird/?ws=usercard-mini

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (vhd.vhdReadException == null && e.Button == MouseButtons.Left && clientWidth - e.X < 10)
            {
                resizeMaxDelta = (int)(InsideWidth * (1D - FreeSpaceStartPercent));
                // resizeMaxDelta -= 20;
                oldCurrentSize = (long)diskModel.footer.CurrentSize / 512;
                CalculateDeltaInSectors();
                bIsResizing = true;
                Cursor = Cursors.SizeWE;
                oldPoint = e.Location;
                oldSize = Size;
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Cursor = Cursors.Default;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);


            if (bIsResizing)
            {
                bool recalc = false;
                // Height = oldSize.Height + (e.Location.Y - oldPoint.Y);
                DeltaX = (oldPoint.X - e.Location.X);
                int testWidth = oldSize.Width - DeltaX;

                long LBADiskLength = bIsResizing ? oldCurrentSize : (long)(diskModel.footer.CurrentSize / 512);
                long newLBADiskLength = LBADiskLength - (sectorsPerPixel * (int)(DeltaX * 1.0));
                long minFreeSpace = (diskUsageModel.GetPartitionUsageModel(diskUsageModel.PartitionCount - 1).LBALength +
                    diskUsageModel.GetPartitionUsageModel(diskUsageModel.PartitionCount - 1).LBAStart);
                if (newLBADiskLength < minFreeSpace)
                {
                    return;
                }


                //if (oldSize.Width - DeltaX < MinWidth)
                //{
                //    if (Width != MinWidth)
                //    {
                //        Width = MinWidth;
                //        recalc = true;
                //    }
                //}
                //else
                {
                    if (oldSize.Width - DeltaX != Width)
                    {
                        Width = oldSize.Width - DeltaX;
                        recalc = true;
                    }
                }

                if (recalc)
                {
                    CalculateDeltaInSectors();
                    CalculatePartitions();
                    Invalidate(true);
                }

                return;
            }

            if (clientWidth - e.X < 10)
            {
                this.Cursor = System.Windows.Forms.Cursors.SizeWE;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left && bIsResizing)
            {
                bIsResizing = false;
                DeltaX = oldPoint.X - e.X;
                Width = oldSize.Width;

                CalculateDeltaInSectors();
                CalculatePartitions();
            }
        }

        // End of Resizing User Control 

        private bool bIsResizing;
        private Point oldPoint;
        private Size oldSize;
        private int DeltaX;


        public DiskUsageModel diskUsageModel { get; set; }
        public int MinWidth = 140;
        private long oldCurrentSize;
        public int InsideWidth { get { return Width - Padding.Left - Padding.Right; } }

        private void DiskRoundedPanel_Load(object sender, EventArgs e)
        {
            CalculatePartitions();
//            CalculatePartitions();
//            Invalidate(true);
        }

        internal void Reload()
        {
            CalculatePartitions();
            Invalidate(true);
            foreach (Control c in Controls)
            {
                if (c is PartitionRoundedPanel)
                {
                    (c as PartitionRoundedPanel).Reload();
                }
            }
        }

        public bool ShowMounted
        {
            get
            {
                return false;
            }
            set
            {
                // this.labelTopRight.Text = " Mounted";
            }
        }

        private int labelTopRight_lastWidth = 0;
        private void labelTopRight_Resize(object sender, EventArgs e)
        {
            if (labelTopRight_lastWidth == 0)
            {
                labelTopRight_lastWidth = 11;
            }

            Label l = sender as Label;
            int d = labelTopRight_lastWidth - l.Width;
            

            labelTopRight_lastWidth = l.Width;

                l.Location = new Point(l.Location.X + d, l.Location.Y);
        }
    }
}