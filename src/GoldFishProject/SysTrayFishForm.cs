using System;
using System.Windows.Forms;
using System.Reflection;

using Windows.UI.Notifications;

using Microsoft.HockeyApp;
using Microsoft.QueryStringDotNET;

using FishTank.Uwp;
using FishTank.Properties;

using DesktopBridgeEnvironment;

using FishTank.Animation;

namespace FishTank
{
    internal partial class SysTrayFishForm : FishForm
    {
        private readonly Program _program;

        public SysTrayFishForm()
        {
            InitializeComponent();
        }

        public SysTrayFishForm(Program program, FishAnimation animation)
            : base(animation)
        {
            InitializeComponent();
            _program = program;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (ExecutionEnvironment.Current.StartupArgs != null && ExecutionEnvironment.Current.StartupArgs.Length > 0)
            {
                ShowContextMenu(ExecutionEnvironment.Current.StartupArgs[0].ToString());
            }
            else if (ExecutionEnvironment.Current.IsUwp && Settings.Default.ShowIconHelperToast)
            {
                var notify = new SimpleNotification()
                {
                    Title = "In case you didn't notice...",
                    Content = "You can add more fish by right clicking the icon over here in the notifaction area. It might be hidden so use the little up arrow to show all the icons."
                };

                notify.Show(Notification_Activated);
            }

            base.OnLoad(e);
        }

        private void Notification_Activated(ToastNotification sender, object args)
        {
            string sArgs = args as string;
            if (args is ToastActivatedEventArgs eventArgs)
            {
                sArgs = eventArgs.Arguments;
            }

            this.Invoke(new Action<string>(ShowContextMenu), sArgs);
        }

        private void ShowContextMenu(string arguments)
        {
            if (!string.IsNullOrEmpty(arguments))
            {
                var args = QueryString.Parse(arguments);
                if (args.TryGetValue("action", out string action) && action == "showme")
                {
                    HockeyClient.Current.TrackEvent("ToastShowMe");

                    MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                    mi.Invoke(notifyIcon1, null);
                }

                Settings.Default.ShowIconHelperToast = false;
                Settings.Default.Save();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HockeyClient.Current.TrackEvent("ExitMenu");
            Application.Exit();
        }

        private void addFishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HockeyClient.Current.TrackEvent("AddFish");

            _program.AddFish();
        }

        private void removeFishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HockeyClient.Current.TrackEvent("RemoveFish");

            _program.RemoveFish();
        }

        private void reviewAppStripMenuItem_Click(object sender, EventArgs e)
        {
            //#if DEBUG
            //            throw new Exception("yowza");
            //#endif
            HockeyClient.Current.TrackEvent("ReviewApp");

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            UwpExtensions.LaunchAppReviewPageInStore();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HockeyClient.Current.TrackEvent("HideFish");

            _program.ShowHide();
            if (hideToolStripMenuItem.Text == "Hide")
                hideToolStripMenuItem.Text = "Show";
            else
                hideToolStripMenuItem.Text = "Hide";
        }
    }
}
