using AutoUpdaterDotNET;
using Hawkchat.Client.admin;
using Hawkchat.Client.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Layout;

namespace Hawkchat.Client
{
    public partial class MainWindow : VisualForm
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ReceivedEnableMessaging()
        {

            BeginInvoke((Action)(() =>
            {

                txtMessage.Enabled = true;
                txtMessage.Text = "";
                btnSendMessage.Enabled = true;

            }));
        }

        public void ReceivedDisableMessaging()
        {

            BeginInvoke((Action)(() =>
            {

                txtMessage.Enabled = false;
                txtMessage.Text = LoginWindow.REASON;
                btnSendMessage.Enabled = false;

            }));
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

            Environment.Exit(0);

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

            // Set the user's username and avatar URL
            userProfilePicture.ImageLocation = LoginWindow.AVATAR_URL;
            groupBoxUserInfo.Text = LoginWindow.USERNAME;
            lblLoggedInUserStatus.Text = "Online";
            lblLoggedInUserStatus.ForeColor = Color.Green;

            if (LoginWindow.USERNAME.Equals("Connor") || LoginWindow.USERNAME.Equals("Ethan"))
            {

                btnPerformModeration.Visible = true;

            }
            else
            {

                btnPerformModeration.Visible = false;

            }

            this.Text = "Hawk Chat";
            this.Size = Constants.SMALL_WINDOW;
            
            btnEndConversation.Visible = false;
            visualPanel1.Visible = false;
            btnReportUser.Visible = false;
            btnRequestMore.Visible = false;
            btnSendMessage.Visible = false;

            txtMessage.Visible = false;
            
            this.Update();

            AutoUpdater.Start("http://blackhawksoftware.net/software/hawkchat/updater/appcast.xml");

            System.Timers.Timer updateChecker = new System.Timers.Timer();

            updateChecker.Interval = 60000;
            updateChecker.Elapsed += UpdateChecker_Elapsed;
            updateChecker.Enabled = true;
            updateChecker.Start();

        }

        private void UpdateChecker_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            AutoUpdater.Start("http://blackhawksoftware.net/software/hawkchat/updater/appcast.xml");

        }

        private void visualButton1_Click(object sender, EventArgs e)
        {

            UserSettings userSettings = new UserSettings();

            userSettings.ShowDialog();

        }
        
        private void btnPerformModeration_Click(object sender, EventArgs e)
        {

            AdminTools adminTools = new AdminTools();

            adminTools.ShowDialog();
            
        }
    }
}
