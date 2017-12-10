using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Server.models
{
    public class UserBan
    {

        public long BanID { get; set; }

        public long UserAccount { get; set; }

        public string Reason { get; set; }

        public DateTime Expires { get; set; }

        public bool Appealable { get; set; }
        
    }
}
