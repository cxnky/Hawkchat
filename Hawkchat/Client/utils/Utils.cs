using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Client.utils
{
    public class Utils
    {

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
