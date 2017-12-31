using AutoUpdaterDotNET;
using Hawkchat.Client.admin;
using Hawkchat.Client.enums;
using Hawkchat.Client.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;
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

            dynamic disconnectJson = new JObject();

            disconnectJson.command = "DISCONNECT";

            LoginWindow.client.WriteLine(disconnectJson.ToString(Formatting.None));

            LoginWindow.client.Disconnect();
           
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
            this.Size = Constants.LARGE_WINDOW;
            
            btnEndConversation.Visible = true;
            visualPanel1.Visible = true;
            btnReportUser.Visible = true;
            btnRequestMore.Visible = true;
            btnSendMessage.Visible = true;

            txtMessage.Visible = true;
            
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

        private void btnSendMessage_Click(object sender, EventArgs e)
        {

            // send to a test account
            // flow:
            // client -> want to establish connection to <id> -> server gets that IP and port and forwards it back -> p2p
            string toAccountID = "93605610";
            string message = txtMessage.Text.Trim();

            dynamic json = new JObject();

            json.command = "REQUESTCHAT";
            json.myaccountid = LoginWindow.ACCOUNTID;
            json.otheraccountid = toAccountID;

            SimpleTCP.Message m = LoginWindow.client.WriteLineAndGetReply(json.ToString(), TimeSpan.FromSeconds(30));

            // message should contain IP and port to contact the user
            JObject returnedJson = JObject.Parse(m.MessageString);

            if (bool.Parse(returnedJson["success"].ToString()))
            {

                // get the recipient user information to show on the HawkChat window.
                JObject info = JObject.Parse(returnedJson["recipientinfo"].ToString());

                string avatarURL = info["RecipientAvatarURL"].ToString();
                UserStatus currentStatus = (UserStatus)int.Parse(info["CurrentStatus"].ToString());
                string username = info["PersonTwoUsername"].ToString();

                Tuple<string, Color> userStatus = Utils.GetUserStatusText(currentStatus);

                lblConversationUserStatus.Text = userStatus.Item1;
                lblConversationUserStatus.ForeColor = userStatus.Item2;

                lblConversationUserStatus.Update();
                this.Update();

                conversationOneBox.Text = username;
                conversationBoxOne.ImageLocation = avatarURL;

            }

        }
    }
}
