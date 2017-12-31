using Hawkchat.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Server.Models
{
    public class EstablishedChat
    {

        public long PersonOneAccountID { get; set; }

        public long PersonTwoAccountID { get; set; }

        public string PersonTwoUsername { get; set; }

        public DateTime EstablishedOn { get; set; }
        
        public string RecipientAvatarURL { get; set; }

        public UserStatus CurrentStatus { get; set; }


    }
}
