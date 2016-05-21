using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    /// <summary>
    /// Represents return facet data
    /// </summary>
    [DataContract]
    public class Facet
    {
        [DataMember(Name = "count")]
        public int? Count { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}