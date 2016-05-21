using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    /// <summary>
    /// result of <see cref="ShodanClient.RequstScanAsync(string)"/>
    /// </summary>
    [DataContract]
    public class ScanResult
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "credits_left")]
        public int CreditsLeft { get; set; }
    }
}