using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class ApiStatus
    {
        [DataMember(Name = "query_credits")]
        public int QueryCredits { get; set; }

        [DataMember(Name = "scan_credits")]
        public int ScanCredits { get; set; }

        [DataMember(Name = "telnet")]
        public bool Telnet { get; set; }

        [DataMember(Name = "plan")]
        public string Plan { get; set; }

        [DataMember(Name = "https")]
        public bool Https { get; set; }

        [DataMember(Name = "unlocked")]
        public bool Unlocked { get; set; }
    }
}