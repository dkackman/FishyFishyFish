using System;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.HockeyApp;

namespace FishTank
{
    partial class FishForm : Form
    {
        private bool _mouseDown;
        private Point _oldPoint = new Point(0, 0);
        private readonly FishAnimation _animation;

        private static NativeMethods.BLENDFUNCTION s_blendFunc;

        static FishForm()
        {
            s_blendFunc.AlphaFormat = NativeMethods.AC_SRC_ALPHA;
            s_blendFunc.BlendOp = NativeMethods.AC_SRC_OVER;
            s_blendFunc.BlendFlags = 0;
            s_blendFunc.SourceConstantAlpha = 255;
        }

        public FishForm()
        {
            InitializeComponent();
        }

        public FishForm(FishAnimation animation)
        {
            SetStyle(ControlStyles.Opaque | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight | ControlStyles.UserMouse | ControlStyles.CacheText, true);

            InitializeComponent();

            _animation = animation;
        }

        protected override void OnLoad(EventArgs e)
        {
            HockeyClient.Current.TrackPageView("FishForm");

            base.OnLoad(e);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            _animation.SwitchDirections();

            base.OnMouseClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            Close();

            base.OnDoubleClick(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _oldPoint = e.Location;
            _mouseDown = true;

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Close();
            }
            else
            {
                _animation.ResetAnimationCounter();

                _mouseDown = false;

                timerSpeed.Interval = Program.TheRandom.Next(20) + 2;
                timerSpeed.Enabled = true;
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_mouseDown)
            {
                Left += (e.X - _oldPoint.X);
                Top += (e.Y - _oldPoint.Y);

                _animation.MoveTo(Location);
            }

            base.OnMouseMove(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cParms = base.CreateParams;
                cParms.ExStyle |= NativeMethods.WS_EX_LAYERED;

                return cParms;
            }
        }

        private void AnimateFish()
        {
            if (!_mouseDown)
            {
                // don't re-arrange this to set timerSpeed.Enabled unconditionally
                // even if setting it to its existing value it seems to do a lot based on cpu usage
                if (_animation.CheckAnimationCounter() && timerSpeed.Enabled)
                {
                    timerSpeed.Enabled = false;
                }

                Location = _animation.Move(Screen.FromControl(this).WorkingArea);
            }

            SetBits(_animation.NextFrame());
        }

        public void UpdateFish()
        {
            if (!Visible)
            {
                Show();
            }

            if (!timerSpeed.Enabled)
            {
                AnimateFish();
            }
        }

        private void timerSpeed_Tick(object sender, EventArgs e)
        {
            AnimateFish();
        }

        private void SetBits(Bitmap bitmap)
        {
            using (var hWindowDC = new SafeGdiHandle(NativeMethods.GetDC(Handle), h => NativeMethods.ReleaseDC(Handle, h)))
            using (var hDC = new SafeGdiHandle(NativeMethods.CreateCompatibleDC(hWindowDC), h => NativeMethods.DeleteDC(h)))
            using (var hBitmap = new SafeGdiHandle(bitmap.GetHbitmap(Color.FromArgb(0)), h => NativeMethods.DeleteObject(h)))
            using (new SafeGdiHandle(NativeMethods.SelectObject(hDC, hBitmap), h => (int)NativeMethods.SelectObject(hDC, h)))
            {
                Point topLoc = Location;
                Point srcLoc = Point.Empty;
                Size bitMapSize = bitmap.Size;
                
                NativeMethods.UpdateLayeredWindow(Handle, hWindowDC, ref topLoc, ref bitMapSize, hDC, ref srcLoc, 0, ref s_blendFunc, NativeMethods.ULW_ALPHA);
            }
        }
    }
}