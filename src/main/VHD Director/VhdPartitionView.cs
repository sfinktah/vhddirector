using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CSharp.cc;

namespace VHD_Director
{
    public partial class VhdPartitionView : TransparentControl
    {

        protected DiskRoundedPanel drp;
        protected List<PartitionRoundedPanel> partitionPanels = new List<PartitionRoundedPanel>();
        protected DiskUsageView view;
        protected DiskUsageModel model;
        protected List<PartitionUsageModel> partitionModels = new List<PartitionUsageModel>();
       
        public VhdPartitionView()
        {
            InitializeComponent();
            //          this.SetStyle(
            //ControlStyles.AllPaintingInWmPaint |
            //ControlStyles.UserPaint |
            //ControlStyles.DoubleBuffer, true);
        }

        //internal void AddDisk(DiskUsageView duv)
        //{
        //    view = duv;
        //    model = view.model;

        //    MakeView();
        //}

        internal void AddDisk(VirtualHardDisk vhd)
        {
            // TODO: Handle virtualDIskException

            //this.footer = vhd.footer;
            //this.dynamicHeader = vhd.dynamicHeader;
            //this.blockAllocationTable = vhd.blockAllocationTable;
            //this.masterBootRecord = vhd.masterBootRecord;
            this.vhd = vhd;
            if (vhd.vhdReadException == null)
            {
                if (CSharp.cc.Files.CheckIfFileIsBeingUsed(vhd.Filename))
                {
                    vhd.IOExceptionMessage = (string)CSharp.cc.Files.LastErrorMessage;
                }
                if (vhd.masterBootRecord != null)
                {
                    this.model = new DiskUsageModel(vhd.masterBootRecord.getPartitionRecords());
                }
                if (CSharp.cc.Files.CheckIfFileIsBeingUsed(vhd.Filename))
                {
                    vhd.IOExceptionMessage = (string)CSharp.cc.Files.LastErrorMessage;
                }
            }
             


            /*
            this._disk = new Medo.IO.VirtualDisk(vhd.footer.FileName);
            this._disk.Open(Medo.IO.VirtualDisk.NativeMethods.VIRTUAL_DISK_ACCESS_MASK.VIRTUAL_DISK_ACCESS_GET_INFO);
            // this._disk.Attach(Medo.IO.VirtualDiskAttachOptions.None);
            string _device = this._disk.GetPhysicalPath();
            if (!String.IsNullOrEmpty(_device))
            {
                this.vhd.MountedAs = _device;
                this.vhd.Partitions = DisksAndPartitions.GetPartitions(_device);
            }
            this._disk.Close();
            this._disk = null;
             */


            MakeView();
        }

        

        public void MakeView()
        {
            drp = new DiskRoundedPanel();
            // adl.Margin = new Padding(20);
            drp.BackColor = BackColor;
            drp.RectangleFillColor = SystemColors.ControlLight;
            drp.Padding = new System.Windows.Forms.Padding(drp.Width + 10, 5, 5, 5);

            // drp.diskModel = diskModel;
            drp.diskUsageModel = model;
            drp.vhd = vhd;

            drp.bitArrayBar1.FalseColor = drp.RectangleFillColor;
            drp.bitArrayBar1.TrueColor = Color.FromArgb(0xff, 128, 128, 128);
            drp.bitArrayBar1.bitArray = vhd.batUsage;

            drp.bitArrayBar1.MouseLeave += new System.EventHandler(this.VhdPartitionView_MouseLeave);
            drp.bitArrayBar1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VhdPartitionView_MouseMove);

            using (Bitmap b = new Bitmap(Properties.Resources.MagnifyingGlass_96_64_centered))
            {
                Cursor c = new Cursor(b.GetHicon());
                drp.bitArrayBar1.Cursor = c;
            }


            foreach (Control c in drp.Controls)
            {
                if (c.Location.X > drp.Width)
                {

                    c.Location = new Point(c.Location.X - drp.Width, c.Location.Y + 5);
                }
            }
            drp.Width = 640;

            // We may or may not get this every time, b/c you never know when it's changed.

            if (mountedVolumes == null)
            {
                mountedVolumes = VDS.LaurentEtiemble.example();
            }
            if (mountedVolumes.Count > 0)
            {
                string physicalDisk;
                if (mountedVolumes.TryGetValue(vhd.Filename, out physicalDisk))
                {
                    SetAttached(physicalDisk);
                }
            }


#if false
            PartitionUsageModel pm;

            long LBADiskLength = (long)(footer.CurrentSize / 512);
            int partitionCount = model.PartitionCount;

            List<int> sizes = new List<int>();
            int sizeSum = (int)LBADiskLength;

            for (int i = 0; i < partitionCount; i++)
            {
                int size = (int)model.GetPartitionUsageModel(i).LBALength;
                double percent = (double)size / LBADiskLength;
                if (percent < 0.1)
                {
                    size = (int)LBADiskLength / 10;
                }
                sizes.Add(size);
                sizeSum = sizes.Sum();
            }
            double PercentPointer = 0.0;
            for (int i = 0; i < partitionCount; i++)
            {
                PartitionRoundedPanel prp = new PartitionRoundedPanel();

                pm = model.GetPartitionUsageModel(i);
                // double partitionPercent = (double)pm.LBALength / LBADiskLength;
                double partitionPercent = (double)sizes[i] / LBADiskLength;
                prp.PartitionStartPercent = (double)pm.LBAStart / LBADiskLength;
                if (prp.PartitionStartPercent < PercentPointer)
                {
                    prp.PartitionStartPercent = PercentPointer;
                }
                PercentPointer = prp.PartitionStartPercent;
                prp.PartitionLengthPercent = (double)pm.LBALength / LBADiskLength;
                if (prp.PartitionLengthPercent < 0.15)
                {
                    prp.PartitionLengthPercent = 0.15;
                }
                PercentPointer += prp.PartitionLengthPercent;
                prp.partitionModel = pm; // This is *so* wrong
                prp.UsedSpacePercentage = partitionPercent;
                // prp.Size = new Size((adl.clientWidth / partitionCount) - 20, prp.Size.Height);
                // int w = (ClientSize.Width - adl.Padding.Left) / partitionCount;
                // prp.Location = new Point(100 + (i * w), 5);
                prp.Number = i;
                prp.ChildCount = partitionCount;
                // prp.Size = new Size(w - 10, 80);
                // Size is calculated in prp.  Location should be too.  But hey.
                // partitionModels.Add(pm);
                adl.Controls.Add(prp);
            }
#endif
            // adl.CalculatePartitions();

            drp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VhdPartitionView_MouseClick);
            foreach (Control c in drp.Controls)
            {
                if (!(c is TransparentControl) || (c is DiskRoundedPanel))
                {
                    c.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VhdPartitionView_MouseClick);
                }
            }
            Controls.Add(drp);
        }

        protected override void InitLayout()
        {
            csharp_debug.Debug.DebugWinControl(this);
            base.InitLayout();
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VhdPartitionView_MouseClick);

            // It only has one control, which is a DiskRoundedPanel anyway.

        }

        // http://bytes.com/topic/c-sharp/answers/270020-resizing-form

        // First, override OnResize() and the first time you enter that method,
        // you set a flag. You also call this.SuspendLayout();, if the flag was
        // wasn't set already.

        // then, you override OnSizeChanged. In there, you reset the flag and call
        // this.ResumeLayout(false);. Then draw your graph.

        //protected bool resizing = false;
        //protected override void OnResize(EventArgs e)
        //{
        //    // base.OnResize(e);

        //    if (!resizing)
        //    {
        //        csharp_debug.Debug.DebugWinControl(this);
        //        resizing = true;
        //        SuspendLayout();
        //    }
        //}

        //protected override void OnSizeChanged(EventArgs e)
        //{
        //    if (resizing && !mouseDown )
        //    {
        //        csharp_debug.Debug.DebugWinControl(this);
        //        ResumeLayout();
        //        resizing = false;
        //    }
        //    base.OnSizeChanged(e);
        //}

        //public Footer footer { get; set; }
        //public DynamicHeader dynamicHeader { get; set; }
        //public BlockAllocationTable blockAllocationTable { get; set; }
        //public MasterBootRecord masterBootRecord { get; set; }
        
        Medo.IO.VirtualDisk _disk;


        // internal DiskModel diskModel { get; set; }
        public VirtualHardDisk vhd { get; set; }
        private ZoomOverlayForm zoomform;
        private void VhdPartitionView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items[2].Enabled = vhd.footer.Changed;
                this.contextMenuStrip1.Show((sender as Control).PointToScreen(e.Location));
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (sender is BitArrayBar)
                {
                    if (zoomform == null || zoomform.IsDisposed)
                    {
                        // (sender as Control).MouseLeave +=new EventHandler(PartitionRoundedPanel_MouseLeaveCancelZoom);
                        zoomform = new ZoomOverlayForm();
                        zoomform.Location = new Point(0, PointToScreen(Location).Y - 100);
                        zoomform.Width = Screen.PrimaryScreen.Bounds.Width;
                        zoomform.maximumX = (sender as Control).Width - 10;
                        zoomform.Image = (sender as BitArrayBar).BitmapFull;
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

        private void VhdPartitionView_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is BitArrayBar && zoomform != null && !zoomform.IsDisposed)
            {
                zoomform.offsetX = e.X;
                zoomform.Invalidate(true);
            }
        }

        private void VhdPartitionView_MouseLeave(object sender, EventArgs e)
        {
            if (zoomform != null)
            {
                zoomform.Close();
                zoomform = null;
            }
            Invalidate(true);
        }

        private void attachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this._disk == null)
            {
                if (CSharp.cc.Files.CheckIfFileIsBeingUsed(vhd.footer.FileName))
                {
                    List<String> whoLockedIt = FileLockInfo.Handle(vhd.footer.FileName);
                    string whoString = String.Empty;
                    foreach (var who in whoLockedIt)
                    {
                        whoString += who.ToString() + ", ";
                    }
                    MessageBox.Show(vhd.footer.FileName + " Locked by " + whoString);
                    return;
                }
                this._disk = new Medo.IO.VirtualDisk(vhd.footer.FileName);
                try
                {
                    this._disk.Open();

                }
                catch (InvalidOperationException ex)
                {
                    // "Native error {0}."
                    string message = ex.Message;
                    int nativeError = 0;
                    if (message.StartsWith("Native error"))
                    {
                        nativeError = Convert.ToInt32(message.Replace("Native error ", "").Replace(".", ""));
                    }

                    if (nativeError == 0x20) // ERROR_SHARING_VIOLATION
                    {
                        this._disk = null;
                        List<String> whoLockedIt = FileLockInfo.Handle(vhd.footer.FileName);
                        string whoString = String.Empty;
                        foreach (var who in whoLockedIt)
                        {
                            whoString += who.ToString() + ", ";
                        }
                        MessageBox.Show("Locked by " + whoString);
                        // FileLockInfo.DetectOpenFiles.GetOpenFilesEnumerator(0);

                        return;
                        //this._disk = new Medo.IO.VirtualDisk(vhd.footer.FileName);
                        //this._disk.Open(Medo.IO.VirtualDisk.NativeMethods.VIRTUAL_DISK_ACCESS_MASK.VIRTUAL_DISK_ACCESS_ATTACH_RO);
                        //// this._disk.Attach(Medo.IO.VirtualDiskAttachOptions.None);
                        //this._disk.Attach(Medo.IO.VirtualDiskAttachOptions.ReadOnly);
                        //// this._disk.Detach();
                        //MessageBox.Show(CSharp.cc.WinApi.SystemErrorCodes.GetMessage(nativeError), "Already attached, read only now.");
                        //// return;
                    }
                    else
                    {
                       
                        MessageBox.Show(CSharp.cc.WinApi.SystemErrorCodes.GetMessage(nativeError), "Failed to Open VHD");
                        return;
                    }
                }
                catch (System.IO.InvalidDataException ex) {
                    MessageBox.Show(ex.Message + " at " + ex.TargetSite.ToString(), "Error attaching VHD");

                }
                catch (System.IO.FileNotFoundException ex) {
                    new Error("File not found", ex);
                }
                /*
                        *  if ((res == NativeMethods.ERROR_FILE_NOT_FOUND) || (res == NativeMethods.ERROR_PATH_NOT_FOUND)) {
                   throw new System.IO.FileNotFoundException("File not found.");
               } else if (res == NativeMethods.ERROR_FILE_CORRUPT) {
                   throw new System.IO.InvalidDataException("File format not recognized.");
               } else {
                   throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Native error {0}.", res));
               }
           }*/

                if (this._disk == null) { return; }
                if (!this._disk.IsOpen) { return; }

                try
                {
                    if (!this._disk.IsAttached)
                    {
                        this._disk.Attach(Medo.IO.VirtualDiskAttachOptions.None);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    // "Native error {0}."
                    string message = ex.Message;
                    int nativeError = 0;
                    if (message.StartsWith("Native error"))
                    {
                        nativeError = Convert.ToInt32(message.Replace("Native error ", "").Replace(".", ""));
                    }

                    MessageBox.Show(CSharp.cc.WinApi.SystemErrorCodes.GetMessage(nativeError), "Failed to Attach VHD");
                    return;
                }
                MessageBox.Show("VHD Mounted as " + this._disk.GetPhysicalPath());

                SetAttached(this._disk.GetPhysicalPath());


            }


            // (sender as System.Windows.Forms.ToolStripDropDownItem).GetCurrentParent;
        }

        private void SetAttached(string p)
        {
            contextMenuStrip1.Items[0].Enabled = false;
            contextMenuStrip1.Items[1].Enabled = true;

            // this._disk = new Medo.IO.VirtualDisk(vhd.footer.FileName);
            // this._disk.Open(Medo.IO.VirtualDisk.NativeMethods.VIRTUAL_DISK_ACCESS_MASK.VIRTUAL_DISK_ACCESS_GET_INFO);
            // this._disk.Attach(Medo.IO.VirtualDiskAttachOptions.None);
            string _device = p;
            if (!String.IsNullOrEmpty(_device))
            {
                this.vhd.MountedAs = _device;
                this.vhd.Partitions = DisksAndPartitions.GetPartitions(_device);
            }

            drp.ShowMounted = true;
            // this._disk.Close();
            // this._disk = null;
            drp.Reload();
            // drp.Invalidate(true);
        }

        private void detachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            detachDisk();
        }

        private void detachDisk()
        {
            if (this._disk != null && this._disk.IsOpen)
            {
                if (this._disk.IsAttached)
                {
                    try
                    {
                       // this._disk.Detach();
                    }
                    catch (Exception ex)
                    {
                        // CSharp.cc.DebugConsole.WriteLine("VhdPartitionView::detachDisk() - " + ex.Message);
                        Console.WriteLine("VhdPartitionView::detachDisk() - " + ex.Message);
                    }
                }
                this._disk.Close();
                this._disk = null;

                contextMenuStrip1.Items[0].Enabled = true;
                contextMenuStrip1.Items[1].Enabled = false;
            }
        }

        private void VhdPartitionView_ControlAdded(object sender, ControlEventArgs e)
        {
            // e.Control.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VhdPartitionView_MouseClick);
            // e.Control.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.VhdPartitionView_ControlAdded);

          
        }

        private void saveChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This software is in early development, and will most likely fry your VHD. Don't do this unless you have a backup.", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                vhd.footer.UpdateFile();
            }
        }


        private void compactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compactionForm = new RtfForm();
            compactionForm.Icon = this.Icon;
            compactionForm.Text = "Compact " + vhd.Filename;

            compactionForm.buttonClicked += new RtfForm.ButtonClicked(compactionForm_buttonClicked);
            compactionForm._disk = this._disk;
            compactionForm.Show();

        }

        private RtfForm compactionForm;

        void compactionForm_buttonClicked(object source, object eventArgument)
        {
            string button = (string)eventArgument;

            switch (button.ToLower())
            {
                case "compact":
                    this._disk = new Medo.IO.VirtualDisk(vhd.footer.FileName);
                    CSharp.cc.QueuedBackgroundWorker.QueueWorkItem(CSharp.cc.QueuedBackgroundWorker.m_Queue, null,
                        (args) => { return Compact(); },
                        (args) => { compactionForm.Stop(args.Result); }
                    );
                    break;

                case "cancel":

                    MessageBox.Show("Yeah, you can't really cancel this without exiting the program.", "My Bad");
                    break;
            }

        }

        private string Compact(Medo.IO.VirtualDisk.CompactionTypes compactionType = Medo.IO.VirtualDisk.CompactionTypes.ALL) {
            if (compactionType == Medo.IO.VirtualDisk.CompactionTypes.ALL)
            {
                return this.Compact(Medo.IO.VirtualDisk.CompactionTypes.FILE_SYSTEM_AWARE) + "\r\n" + this.Compact(Medo.IO.VirtualDisk.CompactionTypes.FILE_SYSTEM_AGNOSTIC);
            }
            if (compactionType == Medo.IO.VirtualDisk.CompactionTypes.NONE)
            {
                return "No compaction method selected";
            }

            string resultString = "Compaction "+(int)compactionType+" ";
            int rv = 0;
            try
            {
                if (this._disk != null)
                {
                    this.detachDisk();
                }
                this._disk = new Medo.IO.VirtualDisk(vhd.footer.FileName);
                rv = this._disk.Compact(compactionType);
                if (rv == 0)
                {
                    resultString += "Succeeded";
                }
                else
                {
                    resultString += ("Failed: " + CSharp.cc.WinApi.SystemErrorCodes.GetMessage(rv));
                }
            }
            catch (InvalidOperationException ex)
            {
                // "Native error {0}."
                string message = ex.Message;
                int nativeError = 0;
                if (message.StartsWith("Native error"))
                {
                    nativeError = Convert.ToInt32(message.Replace("Native error ", "").Replace(".", ""));
                }
             
                resultString += ("Failed: " + CSharp.cc.WinApi.SystemErrorCodes.GetMessage(nativeError));
            }
            catch (System.Exception ex)
            {
               resultString += ("Failed: " + ex.Message);
            }

            if (compactionType == Medo.IO.VirtualDisk.CompactionTypes.FILE_SYSTEM_AWARE)
            {
                //  detachDisk();
            }
            return resultString;
        }


        public Icon Icon { get; set; }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VhdProperties vprop = new VhdProperties(vhd);
            vprop.Icon = Icon;
            vprop.Text = "Properties of " + vhd.footer.FileName;
            vprop.Show();
        }

        private void removeFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remove(this, sender);
        }

        public delegate void RemoveHandler(object source, object eventArgument);
        public event RemoveHandler remove;
        private string _device;
        static private Dictionary<string, string> mountedVolumes;

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpandForm expandform = new ExpandForm();
            expandform.Icon = Icon;
            expandform.buttonClicked += new ExpandForm.ButtonClicked(expandform_buttonClicked);
            expandform.ShowDialog();
        }

        void expandform_buttonClicked(object source, object eventArgument)
        {
            string resultString = "Expand ";
            int rv = 0;
            try
            {
                if (this._disk.IsOpen)
                {
                    this._disk.Close();
                    this._disk = null;
                }
                this._disk = new Medo.IO.VirtualDisk(vhd.footer.FileName);
                rv = this._disk.Expand((long)eventArgument);
                if (rv == 0)
                {
                    resultString += "Succeeded";
                }
                else
                {
                    resultString += ("Failed: " + CSharp.cc.WinApi.SystemErrorCodes.GetMessage(rv));
                }
            }
            catch (System.Exception ex)
            {
                resultString += ("Failed: " + ex.Message);
            }

            MessageBox.Show(resultString, "Expand VHD");
            try
            {
                this._disk.Close();
            }
            catch { }
            this._disk = null;
            remove(this, this);
        }
    }

}
