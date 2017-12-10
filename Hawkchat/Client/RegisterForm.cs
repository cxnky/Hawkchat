using Hawkchat.Client.utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class RegisterForm : VisualForm
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {

            if (!txtPasswordOnce.Text.Equals(txtConfirmPassword.Text))
            {

                txtPasswordNotMatchError.Text = "Your passwords do not match.";
                txtPasswordNotMatchError.ForeColor = Color.Red;

                txtPasswordNotMatchError.Visible = true;
                await Task.Delay(5000);
                txtPasswordNotMatchError.Visible = false;

                return;

            } else
            {

                txtPasswordNotMatchError.Text = "Passwords both match.";
                txtPasswordNotMatchError.ForeColor = Color.Green;
                txtPasswordNotMatchError.Visible = true;

            }

            // Passwords match, verify the username doesn't exist already.
            dynamic VerifyUsernameJson = new JObject();

            VerifyUsernameJson.command = "VERIFYUSERNAME";
            VerifyUsernameJson.username = txtUsername.Text.Trim();

            SimpleTCP.Message message = LoginWindow.client.WriteLineAndGetReply(VerifyUsernameJson.ToString(Formatting.None), TimeSpan.FromSeconds(30));

            JObject verifyUsernameJsonReturned = JObject.Parse(message.MessageString);

            bool exists = bool.Parse(verifyUsernameJsonReturned["exists"].ToString());

            if (exists)
            {

                // username exists
                txtUsernameTaken.Text = "That username is already taken.";
                txtUsernameTaken.ForeColor = Color.Red;

                txtUsernameTaken.Visible = true;
                await Task.Delay(5000);
                txtUsernameTaken.Visible = false;

                return;

            } else
            {

                txtUsernameTaken.Text = "That username is available";
                txtUsernameTaken.ForeColor = Color.Green;

                txtUsernameTaken.Visible = true;

            }

            // everything checked out, process the registration
            dynamic registrationJson = new JObject();

            registrationJson.command = "REGISTER";
            registrationJson.username = txtUsername.Text.Trim();
            registrationJson.password = Utils.SHA1(txtConfirmPassword.Text);
            registrationJson.IP = Utils.GetIP();

            SimpleTCP.Message registrationCallback = LoginWindow.client.WriteLineAndGetReply(registrationJson.ToString(Formatting.None), TimeSpan.FromSeconds(60));

            JObject jObject = JObject.Parse(registrationCallback.MessageString);

            LoginWindow.ACCOUNTID = long.Parse(jObject["AccountID"].ToString());
            LoginWindow.USERNAME = jObject["username"].ToString();
            LoginWindow.AVATAR_URL = jObject["avatarURL"].ToString();

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();
            this.Hide();

        }
    }
}
