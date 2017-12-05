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

    }
}
