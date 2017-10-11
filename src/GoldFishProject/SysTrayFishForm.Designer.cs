namespace FishTank
{
    partial class SysTrayFishForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FishForm));

            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reviewAppStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();


            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;

            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Fishtank";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFishToolStripMenuItem,
            this.removeFishToolStripMenuItem,
            this.toolStripMenuItem1,
            this.reviewAppStripMenuItem,
            this.hideToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 120);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new System.EventHandler(this.hideToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // addFishToolStripMenuItem
            // 
            this.addFishToolStripMenuItem.Image = global::FishTank.Properties.Resources.add;
            this.addFishToolStripMenuItem.Name = "addFishToolStripMenuItem";
            this.addFishToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.addFishToolStripMenuItem.Text = "Add fish";
            this.addFishToolStripMenuItem.Click += new System.EventHandler(this.addFishToolStripMenuItem_Click);
            // 
            // removeFishToolStripMenuItem
            // 
            this.removeFishToolStripMenuItem.Image = global::FishTank.Properties.Resources.cross;
            this.removeFishToolStripMenuItem.Name = "removeFishToolStripMenuItem";
            this.removeFishToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeFishToolStripMenuItem.Text = "Remove fish";
            this.removeFishToolStripMenuItem.Click += new System.EventHandler(this.removeFishToolStripMenuItem_Click);
            // 
            // reviewAppStripMenuItem
            // 
            this.reviewAppStripMenuItem.Image = global::FishTank.Properties.Resources.star_yellow;
            this.reviewAppStripMenuItem.Name = "reviewAppStripMenuItem";
            this.reviewAppStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.reviewAppStripMenuItem.Text = "Review app";
            this.reviewAppStripMenuItem.Click += new System.EventHandler(this.reviewAppStripMenuItem_Click);

            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
        }



        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reviewAppStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeFishToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
    }
}
