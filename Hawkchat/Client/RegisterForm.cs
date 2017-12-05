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

            if (bool.Parse(verifyUsernameJsonReturned["exists"].ToString()))
            {

                // username exists
                txtUsernameTaken.Visible = true;
                await Task.Delay(5000);
                txtUsernameTaken.Visible = false;

                return;

            }

        }
    }
}
