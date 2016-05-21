using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class Attributes
    {
        [DataMember(Name = "ports")]
        public IList<int> Ports { get; set; }
    }

    [DataContract]
    public class SearchTokens
    {
        [DataMember(Name = "attributes")]
        public Attributes Attributes { get; set; }

        [DataMember(Name = "errors")]
        public IList<dynamic> Errors { get; set; }

        [DataMember(Name = "string")]
        public string String { get; set; }

        [DataMember(Name = "filters")]
        public IList<dynamic> Filters { get; set; }
    }
}