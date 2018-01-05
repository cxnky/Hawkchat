using Hawkchat.Client.enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VisualPlus.Toolkit.Controls.Interactivity;

namespace Hawkchat.Client.utils
{
    public class Utils
    {

        public static Tuple<string, VisualLabel> GetUserStatusText(UserStatus status)
        {

            switch (status)
            {

                case UserStatus.OFFLINE:
                    return new Tuple<string, VisualLabel>("Offline", Constants.OFFLINE_LABEL);

                case UserStatus.ONLINE:
                    return new Tuple<string, VisualLabel>("Online", Constants.ONLINE_LABEL);

                case UserStatus.AWAY:
                    return new Tuple<string, VisualLabel>("Away", Constants.AWAY_LABEL);

                case UserStatus.DND:
                    return new Tuple<string, VisualLabel>("DND", Constants.DND_LABEL);

                default:
                    return new Tuple<string, VisualLabel>("Unknown", Constants.OFFLINE_LABEL);

            }

        }

        public static string SHA1(string rawText)
        {

            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(rawText));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }

        }
        
        public static string GetIP()
        {

            using (WebClient client = new WebClient())
            {

                return client.DownloadString("https://api.ipify.org/?format=raw");

            }

        }

    }
}
