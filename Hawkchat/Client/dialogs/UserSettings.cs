using Hawkchat.Client.utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Layout;

namespace Hawkchat.Client
{
    public partial class UserSettings : VisualForm
    {
        public UserSettings()
        {
            InitializeComponent();

            LoadConfigFile();

        }

        private string ReadConfig()
        {

            FileStream fRead = new FileStream("hawkchat.settings", FileMode.Open);
            StreamReader reader = new StreamReader(fRead);

            string line = "";

            string configJson = "";

            while ((line = reader.ReadLine()) != null)
            {

                configJson += line;

            }

            reader.Close();
            fRead.Close();

            reader.Dispose();
            fRead.Dispose();

            return configJson;

        }

        private void LoadConfigFile()
        {

            if (!File.Exists("hawkchat.settings"))
            {
                
                return;

            }

            string configJson = ReadConfig();
            


            JObject jsonConfig = JObject.Parse(configJson);

            chkNotifyWhenFriendOnline.Checked = bool.Parse(jsonConfig["friend_online_notify"].ToString());

        }

        private void ReplaceConfigValue(string configValue, string newValue)
        {

            JObject json = JObject.Parse(ReadConfig());

            json[configValue] = newValue;

            FileStream fOverwrite = new FileStream("hawkchat.settings", FileMode.Create);
            StreamWriter writer = new StreamWriter(fOverwrite);

            writer.WriteLine(json.ToString());

            writer.Close();
            fOverwrite.Close();

            writer.Dispose();
            fOverwrite.Dispose();

        }

        private void visualCheckBox1_ToggleChanged(VisualPlus.EventArgs.ToggleEventArgs e)
        {

            string checkedState = e.State.ToString().ToLower();

            UserPreferences.NOTIFY_WHEN_FRIEND_ONLINE = e.State;

            ReplaceConfigValue("friend_online_notify", checkedState);

        }
    }
}
