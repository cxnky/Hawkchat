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
            this.Text = "Hawk Chat";

            this.Update();

        }

        private void visualButton1_Click(object sender, EventArgs e)
        {
            


        }
    }
}
