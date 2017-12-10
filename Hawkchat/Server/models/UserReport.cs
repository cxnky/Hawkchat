using Hawkchat.Server.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hawkchat.Server.models
{
    public class UserReport
    {

        public long ReportID { get; set; }

        public long ReporterAccountID { get; set; }

        public long ReportedAccountID { get; set; }

        public string ChatLogURL { get; set; }

        public ReportCategory ReportCategory { get; set; }
        
        public long Timestamp { get; set; }

    }
}
