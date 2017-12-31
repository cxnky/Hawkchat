using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Client.utils
{
    public class Constants
    {

        public static Size SMALL_WINDOW = new Size(240, 440);
        public static Size LARGE_WINDOW = new Size(883, 538);

        public static Color OFFLINE_COLOUR = Color.FromArgb(1, 255, 0, 0);
        public static Color ONLINE_COLOUR = Color.FromArgb(1, 0, 255, 0);
        public static Color AWAY_COLOUR = Color.FromArgb(1, 249, 215, 0);
        public static Color DND_COLOUR = Color.FromArgb(1, 255, 165, 0);

    }
}
