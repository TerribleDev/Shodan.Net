using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class TagResult
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "matches")]
        public IList<Match> Matches { get; set; }

        [DataContract]
        public class Match
        {
            [DataMember(Name = "value")]
            public string Value { get; set; }

            [DataMember(Name = "count")]
            public int Count { get; set; }
        }
    }
}