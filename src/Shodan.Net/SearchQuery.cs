using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shodan.Net
{
    public class SearchQuery
    {
        internal SearchQuery(string builtQuery)
        {
            this.Query = builtQuery;
        }

        internal string Query { get; set; }
    }
}