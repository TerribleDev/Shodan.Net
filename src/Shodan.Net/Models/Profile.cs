using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class Profile
    {
        [DataMember(Name = "member")]
        public bool Member { get; set; }

        [DataMember(Name = "credits")]
        public int Credits { get; set; }

        [DataMember(Name = "display_name")]
        public string DisplayName { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }
    }
}