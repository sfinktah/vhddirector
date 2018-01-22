using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*
 * Also, do backgroundimagelayout to be either 'center' or 'streach' to increase performance. This will enable the double buffer on the form
 * 
 * SetStyle(ControlStyles.OptimizedDoubleBuffer | 
         ControlStyles.AllPaintingInWmPaint, true);
 * ControlStyles.UserPaint
 * 
 * One of the things you should look at is whether you have set BackColor=Transparent on any of the child controls of your panels. The BackColor=Transparent will significantly degrade rendering performance especially if parent panels are using gradients.

Windows Forms does not use real transparency, rather it is uses "fake" one. Each child control paint call generates paint call on parent so parent can paint its background over which the child control paints its content so it appears transparent.

So if you have 50 child controls that will generate additional 50 paint calls on parent control for background painting. And since gradients are generally slower you will see performance degradation.
 * 
 * In your custom control drawing code are you redrawing the entire control every time, or just the portion that needs to be? 
 * 
 * If that's the order things are done, there's room for improvement. First create all your labels, change their properties, and when they are all ready, add them to the panel: Panel.Controls.AddRange(Control[])
 * 
 * Doublebuffering in a nutshell: It's very simple, a couple lines of code. On the paint event, render to a bitmap instead of rendering to the Graphics object, and then draw that bitmap to the Graphics object.
 * 
 * As always, simply enable DoubleBuffered=true on your CustomControl. Then, if you have any containers like FlowLayoutPanel or TableLayoutPanel, derive a class from each of these types and in the constructors, enable double buffering. Now, simply use your derived Containers instead of the Windows.Forms Containers.

class TableLayoutPanel : System.Windows.Forms.TableLayoutPanel
{
    public TableLayoutPanel()
    {
        DoubleBuffered = true;
    }
}

class FlowLayoutPanel : System.Windows.Forms.FlowLayoutPanel
{
    public FlowLayoutPanel()
    {
        DoubleBuffered = true;
    }
}
*/

namespace VhdDirectorApp
{
    public partial class UserControlLayout : UserControl
    {
        public UserControlLayout()
        {
            InitializeComponent();
        }

        protected bool doneOurInitLayout;
        protected bool doneResize;
        protected bool doneInitLayout;
        private byte layoutSuspendCount;
        private int ourLayoutSuspendCount;

        protected override void InitLayout()
        {
            // Called after the control has been added to another container.
            // The InitLayout method is called immediately after adding a control to a container. 
            // The InitLayout method enables a control to initialize its layout state based on its container. 

            // For example, you would typically apply anchoring and docking to the control in the InitLayout method.

            // When overriding InitLayout in a derived class, be sure to call the base class's InitLayout method so that the control is displayed correctly.

            csharp_debug.Debug.DebugWinControl(this);
            base.InitLayout();

            doneInitLayout = true;
        }

        public void SuspendOurLayout()
        {
            this.layoutSuspendCount = (byte)(this.layoutSuspendCount + 1);
            if (this.layoutSuspendCount == 1)
            {
                this.OnOurLayoutSuspended();
            }
        }

        public void ResumeOurLayout()
        {
            if (this.ourLayoutSuspendCount == 0)
            {
                return;
            }

            this.layoutSuspendCount = (byte)(this.layoutSuspendCount - 1);

            if (this.ourLayoutSuspendCount == 0)
            {
                this.OnOurLayoutResumed();
            }
        }

        public bool IsOurSuspended
        {
            get { return ourLayoutSuspendCount > 0; }
        }


        private void OnOurLayoutResumed()
        {
        }

        private void OnOurLayoutSuspended()
        {
        }


        protected void OurInvalidate(bool invalidateChildren)
        {
        }

        /*
                 label1::OnResize                 75 x 33              UserControlLayout:   0
                 label1::OnLayout                 75 x 33              UserControlLayout:   0
                 label1::InitLayout               75 x 33              UserControlLayout:   0
                 label1::OnResize                280 x 33              UserControlLayout:   0
                 label1::OnLayout                280 x 33              UserControlLayout:   0
        */
        protected override void OnResize(EventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);
            base.OnResize(e);

            if (this.doneInitLayout)
            {
                this.doneResize = true;
            }
        }
        protected override void OnLayout(LayoutEventArgs e)
        {
      
            // The Layout event occurs when child controls are added or removed, when the bounds of the control changes, and when 
            // other changes occur that can affect the layout of the control.

            // The layout event can be suppressed using the 
            // SuspendLayout and ResumeLayout methods. Suspending layout enables you to perform multiple actions on a control 
            // without having to perform a layout for each change.
            // For example, if you resize and move a control, each operation would raise a Layout event.

            csharp_debug.Debug.DebugWinControl(this);
            base.OnLayout(e);

            if (doneInitLayout)
            {
                if (!doneOurInitLayout)
                {
                    OurInitLayout();
                    doneOurInitLayout = true;
                }
            }
            else
            {
                OurLayout();
            }
        }

        protected virtual void OurInitLayout()
        {
            // csharp_debug.Debug.DebugWinControl(this);
            // The concept of OurInitLayout is to do all the one time layout stuff, ONCE, and at the right time.
            OurLayout(); // This may or may not include running OurLayout, which will also be run at future InitLayout() calls, if they happen (they shouldn't, but they seem to)
        }


        protected virtual void OurLayout()
        {
            // csharp_debug.Debug.DebugWinControl(this);
        }
#if fale
        //** http://stackoverflow.com/questions/835100/winforms-suspendlayout-resumelayout-is-not-enough **//


        BufferedGraphicsContext gfxManager;
        BufferedGraphics gfxBuffer;
        Graphics gfx;

        // a function to install graphics

        private void InstallGFX(bool forceInstall)
        {
            if (forceInstall || gfxManager == null)
            {
                gfxManager = BufferedGraphicsManager.Current;
                gfxBuffer = gfxManager.Allocate(this.CreateGraphics(), new Rectangle(0, 0, Width, Height));
                gfx = gfxBuffer.Graphics;
            }
        }

        // in its paint method

        protected override void OnPaint(PaintEventArgs e)
        {
            InstallGFX(false);
            // .. use gfx to draw
            gfxBuffer.Render(e.Graphics);
        }

        // in its resize method

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            InstallGFX(true); // to reallocate drawing space of new size
        }
#endif
#if false
        // Also here's the forms loading code because it's a bit unusual. It copies all Controls from another form onto the current form. This is done in order to be able to edit each screen's visual appearance seperately using the Designer while sharing a common form and common code basis. 
        private void LoadControls(Form form)
        {
            this.SuspendLayout();

            this.DoubleBuffered = true;
            EnableDoubleBuffering(this.Controls);

            this.BackgroundImage = form.BackgroundImage;
            this.BackColor = form.BackColor;

            this.Controls.Clear();
            foreach (Control c in form.Controls)
            {
                this.Controls.Add(c);
            }

            this.ResumeLayout();
        }
#endif
    }


    public static class SetRedraw32
    {
        #region Redraw Suspend/Resume
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
        public static void ResumeDrawing(this Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }
        #endregion
    }

    public static class SetRedrawNative
    {
        private const int WM_SETREDRAW = 0x0B;

        public static void SuspendDrawing(Control control)
        {
            Message msgSuspendUpdate = Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        public static void ResumeDrawing(Control control)
        {
            // Create a C "true" boolean as an IntPtr
            IntPtr wparam = new IntPtr(1);
            Message msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgResumeUpdate);

            control.Invalidate();
        }
    }

}
