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

namespace Hawkchat.Client.admin
{
    public partial class AdminTools : VisualForm
    {
        public AdminTools()
        {
            
            InitializeComponent();

        }

        private void btnEnableMessaging_Click(object sender, EventArgs e)
        {

            dynamic json = new JObject();

            json.command = "ENABLEMESSAGES";
            json.authorisedby = LoginWindow.USERNAME;

            LoginWindow.client.WriteLine(json.ToString(Formatting.None));

        }

        private void btnDisableMessaging_Click(object sender, EventArgs e)
        {

            dynamic json = new JObject();

            json.command = "DISABLEMESSAGES";
            json.authorisedby = LoginWindow.USERNAME;
            json.reason = "An administrator has temporarily disabled this feature.";

            LoginWindow.client.WriteLine(json.ToString(Formatting.None));


        }

        private void visualGroupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
