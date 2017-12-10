using Hawkchat.Client.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Layout;

namespace Hawkchat.Client
{
    public partial class LoginWindow : VisualForm
    {

        public static long ACCOUNTID;
        public static string USERNAME;

        public LoginWindow()
        {

            InitializeComponent();
            try
            {
#if DEBUG
                client = new SimpleTcpClient().Connect("127.0.0.1", 3289);
#else
               client = new SimpleTcpClient().Connect("server ip", 3289);
#endif


            }
            catch (Exception)
            {

                MessageBox.Show("Our login server is currently undergoing maintenance. Sorry for any inconvenience.", "Server Maintenance", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(-1);

            }
        }
        
        public static SimpleTcpClient client;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            dynamic authJson = new JObject();

            authJson.command = "AUTH";
            authJson.username = txtUsername.Text.Trim();
            authJson.password = Utils.SHA1(txtPassword.Text);
            authJson.IP = Utils.GetIP();


            SimpleTCP.Message message = client.WriteLineAndGetReply(authJson.ToString(Formatting.None), TimeSpan.FromSeconds(30));

            JObject returnedJson = JObject.Parse(message.MessageString);

            bool success = bool.Parse(returnedJson["authenticated"].ToString());

            if (success)
            {

                // Show main window
                ACCOUNTID = long.Parse(returnedJson["accountID"].ToString());

                btnLogin.Enabled = false;

            } else
            {

                string reason = returnedJson["reason"].ToString();

                switch (reason)
                {

                    case "BANNED":
                        string BanID = returnedJson["BanID"].ToString();
                        string AccountID = returnedJson["AccountID"].ToString();
                        string Reason = returnedJson["Reason"].ToString();
                        string Expires = returnedJson["expires"].ToString();
                        bool Appealable = bool.Parse(returnedJson["Appealable"].ToString());

                        BannedWindow bannedWindow = new BannedWindow();

                        bannedWindow.SetBanInformation(BanID, AccountID, Reason, Expires, Appealable);

                        bannedWindow.Show();
                        this.Hide();
                        
                        break;

                    case "CREDENTIALS":
                        MessageBox.Show("Invalid credentials.", "Hawkchat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                }

                return;

            }

        }

        private void LoginWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.TcpClient.Connected)
            {
                dynamic disconnectJson = new JObject();

                disconnectJson.command = "DISCONNECT";

                client.WriteLine(disconnectJson.ToString(Formatting.None));

            }

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            RegisterForm registerForm = new RegisterForm();
            
            registerForm.ShowDialog();

        }
    }
}
