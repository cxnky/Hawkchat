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
    public partial class LoginWindow : VisualForm
    {
        public LoginWindow()
        {

            InitializeComponent();
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private SimpleTcpClient client;

        private void btnLogin_Click(object sender, EventArgs e)
        {


            // TODO: Check if servers are online, Check if server maintenance is in place
#if DEBUG
            client = new SimpleTcpClient().Connect("127.0.0.1", 3289);
#else
            client = new SimpleTcpClient().Connect("server ip", 3289);
#endif

            dynamic authJson = new JObject();

            authJson.command = "AUTH";
            authJson.username = txtUsername.Text.Trim();
            authJson.password = Utils.SHA1(txtPassword.Text);
            authJson.IP = Utils.GetIP();


            SimpleTCP.Message message = client.WriteLineAndGetReply(authJson.ToString(Formatting.None), TimeSpan.FromSeconds(10));

            JObject returnedJson = JObject.Parse(message.MessageString);

            bool success = bool.Parse(returnedJson["authenticated"].ToString());

            if (success)
            {

                // Show main window

            } else
            {

                MessageBox.Show("Invalid credentials.", "Hawkchat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

        }
       

    }
}
