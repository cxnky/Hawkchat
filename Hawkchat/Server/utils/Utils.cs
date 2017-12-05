using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using System.Text;
using System.Threading.Tasks;

namespace Server.utils
{
    public class Utils
    {

        public static Color CYAN = Color.Cyan;

        public static void CyanWriteLine(string text)
        {

            Console.WriteLine(text, CYAN);

        }

        public static string RemoveEndSpecialCharacter(string data)
        {

            byte[] stringAsBytes = Encoding.ASCII.GetBytes(data);

            byte[] tmpArray = new byte[stringAsBytes.Length - 1];

            Array.Copy(stringAsBytes, tmpArray, stringAsBytes.Length - 1);

            return Encoding.Default.GetString(tmpArray);

        }

    }
}
