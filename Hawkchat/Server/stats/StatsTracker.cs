using SUF.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Server.stats
{
    public class StatsTracker
    {

        private static long bytesReceived = 0, bytesSent = 0;

        public static void IncreaseBytesReceived(long amount) => bytesReceived += amount;
        public static void IncreaseBytesSent(long amount) => bytesSent += amount;

        public static string GetHumanReadableBytesReceived()
        {

            return StorageUtils.BeautifyBytes(bytesReceived);

        }

        public static string GetHumanReadableBytesSent()
        {

            return StorageUtils.BeautifyBytes(bytesSent);

        }

    }
}
