using System;
using System.Windows.Forms;

using Microsoft.HockeyApp;

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
