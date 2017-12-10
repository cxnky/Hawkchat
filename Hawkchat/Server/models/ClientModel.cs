using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Server.Models
{
    public class ClientModel
    {

        public TcpClient TCPClient { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        //public string AvatarURL { get; set; }

        public Int64 UserID { get; set; }

    }
}
