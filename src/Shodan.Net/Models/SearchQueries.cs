using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class Match
    {
        [DataMember(Name = "votes")]
        public int Votes { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "timestamp")]
        public DateTime Timestamp { get; set; }

        [DataMember(Name = "tags")]
        public IList<string> Tags { get; set; }

        [DataMember(Name = "query")]
        public string Query { get; set; }
    }

    /// <summary>
    /// Result of <see cref="ShodanClient.GetQueriesAsync(int?, SortOptions?, OrderOption?)"/> and <seealso cref="ShodanClient.SearchQueriesAsync(string, int?)"/>
    /// </summary>
    [DataContract]
    public class SearchQueries
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "matches")]
        public IList<Match> Matches { get; set; }
    }
}