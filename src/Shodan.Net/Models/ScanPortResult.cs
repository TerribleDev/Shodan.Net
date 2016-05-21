using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class ScanPortResult
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}