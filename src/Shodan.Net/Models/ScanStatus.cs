using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class ScanStatus
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "status")]
        public StatusEnum? Status { get; set; }
    }
}