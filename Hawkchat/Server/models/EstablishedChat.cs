using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Server.models
{
    public class EstablishedChat
    {

        public int PersonOneAccountID { get; set; }

        public int PersonTwoAccountID { get; set; }

        public DateTime Established { get; set; }
        

    }
}
