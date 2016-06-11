using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    /// <summary>
    /// result of <see cref="ShodanClient.SearchHosts(SearchQuery, FacetQuery, int, bool)"/> and <see cref="ShodanClient.SearchHostsCount(SearchQuery, FacetQuery)"/>
    /// </summary>
    [DataContract]
    public class SearchHostResults
    {
        [DataMember(Name = "matches")]
        public List<Banner> Matches { get; set; }

        [DataMember(Name = "facets")]
        public Dictionary<string, Facet> Facets { get; set; }

        [DataMember(Name = "total")]
        public int? Total { get; set; }
    }
}