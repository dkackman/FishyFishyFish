using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

using FishTank.Properties;
using DesktopBridgeEnvironment;

using Microsoft.HockeyApp;

using FishTank.Animation;

namespace FishTank
{
    sealed class Program : IDisposable
    {
        private const int WIDTH = 143;

        private readonly Timer _timer = new Timer();
        private readonly List<FishForm> _fish = new List<FishForm>();

        private readonly IList<IEnumerable<Tuple<Bitmap, Bitmap>>> _colorFrameList;

        public readonly static Random TheRandom = new Random(unchecked((int)(DateTime.Now.Ticks)));

        /// <summary>
        /// Main Entry
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            using (var instance = new SingleInstance(ExecutionEnvironment.Current.AppId))
            {
                if (instance.IsFirstInstance)
                {
                    ExecutionEnvironment.Current.StartupArgs = args;

                    HockeyClient.Current.Configure("241375ed658746ee8ae1d014a3f52797");

                    try
                    {
                        HockeyClient.Current.SendCrashesAsync();
                    }
                    catch { }

                    using (var program = new Program())
                    {
                        program.Show();
                    }
                }
            }
        }

        private Program()
        {
            _colorFrameList = (from color in new string[] { "Blue", "Green", "Orange", "Pink", "Yellow", "Red" }
                                select Factory.GetFrames(color, WIDTH)).ToList();
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        private void Show()
        {
            int count = Settings.Default.FishCount > 0 ? Settings.Default.FishCount : 1;
            HockeyClient.Current.TrackMetric("StartupFishCount", count);

            var tank = AllScreens();
            for (int i = 0; i < count; i++)
            {
                CreateAndAddFish(tank);
            }

            _timer.Tick += (o, e) => _fish.ForEach(f => f.UpdateFish());
            _timer.Interval = 50;
            _timer.Enabled = true;

            Application.Run(_fish[0]);
        }

        private void CreateAndAddFish(Rectangle tank)
        {
            var animation = new FishAnimation(tank, _colorFrameList[TheRandom.Next(_colorFrameList.Count)], WIDTH);
            var f = _fish.Count > 0 ? new FishForm(animation) : new SysTrayFishForm(this, animation);
            f.Disposed += new EventHandler(FishForm_Disposed);

            _fish.Add(f);
        }

        private static Rectangle AllScreens()
        {
            var tank = new Rectangle();
            foreach (var screen in Screen.AllScreens)
            {
                tank = Rectangle.Union(tank, screen.WorkingArea);
            }

            return tank;
        }

        public void AddFish()
        {
            CreateAndAddFish(AllScreens());

            if (_fish.Count > 0)
            {
                Settings.Default.FishCount = _fish.Count;
                Settings.Default.Save();
            }
        }

        public void ShowHide()
        {
            _timer.Enabled = !_timer.Enabled;

            _fish.ForEach(f => f.Visible = _timer.Enabled);
        }

        public void RemoveFish()
        {
            if (_fish.Count >= 1)
            {
                _fish[_fish.Count - 1].Dispose();

                Settings.Default.FishCount = _fish.Count;
                Settings.Default.Save();
            }
        }

        private void FishForm_Disposed(object sender, EventArgs e)
        {
            var f = (FishForm)sender;
            _fish.Remove(f);
            f.Disposed -= new EventHandler(FishForm_Disposed);
        }
    }
}