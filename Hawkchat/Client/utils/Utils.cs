using Hawkchat.Client.enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Client.utils
{
    public class Utils
    {

        public static Tuple<string, Color> GetUserStatusText(UserStatus status)
        {

            switch (status)
            {

                case UserStatus.OFFLINE:
                    return new Tuple<string, Color>("Offline", Constants.OFFLINE_COLOUR);

                case UserStatus.ONLINE:
                    return new Tuple<string, Color>("Online", Constants.ONLINE_COLOUR);

                case UserStatus.AWAY:
                    return new Tuple<string, Color>("Away", Constants.AWAY_COLOUR);

                case UserStatus.DND:
                    return new Tuple<string, Color>("DND", Constants.DND_COLOUR);

                default:
                    return new Tuple<string, Color>("Unknown", Constants.OFFLINE_COLOUR);

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
