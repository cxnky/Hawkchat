using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Layout;

namespace Hawkchat.Client
{
    public partial class BannedWindow : VisualForm
    {
        public BannedWindow()
        {
            InitializeComponent();

            visualLabel1.BackColor = ColorTranslator.FromHtml("#f4765d");

        }

        private string BanID, AccountID, Reason, Expires;

        private void btnAppealBan_Click(object sender, EventArgs e)
        {

            Process.Start("https://support.blackhawksoftware.net/portal/newticket");

        }

        private void BannedWindow_FormClosing(object sender, FormClosingEventArgs e)
        {

            Environment.Exit(0);

        }

        private bool Appealable;

        private void BannedWindow_Load(object sender, EventArgs e)
        {

            string appealableText = Appealable ? "Yes" : "No";

            //visualPanel1.Text = $"Ban #: {BanID}\nAccount ID Affected: {AccountID}\nReason for ban: {Reason}\nExpires: {Expires}\nAppealable: {appealableText}";
            lblBanDetails.Text = $"Ban #: {BanID}\n";
            lblBanDetails.Text += $"Account ID affected: {AccountID}\n";
            lblBanDetails.Text += $"Reason for ban: {Reason}\n";
            lblBanDetails.Text += $"Expires: {Expires}\n";
            lblBanDetails.Text += $"Appealable: {appealableText}";

            btnAppealBan.Enabled = Appealable;
            if (!Appealable) { btnAppealBan.Text = "Ban cannot be appealed"; } else { btnAppealBan.Text = "Appeal"; }

            this.UpdateStyles();
            this.Update();

        }

        public void SetBanInformation(string BanID, string AccountID, string Reason, string Expires, bool Appealable)
        {

            this.BanID = BanID;
            this.AccountID = AccountID;
            this.Reason = Reason;
            this.Expires = Expires;

            this.Appealable = Appealable;

        }

    }
}
