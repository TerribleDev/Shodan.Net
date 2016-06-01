using Newtonsoft.Json;
using Shodan.Net.Models;
using Shodan.Net.Models.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shodan.Net
{
    /// <summary>
    /// Main mechanism to talk to the shodan api. This is what you should use to interact with the api
    /// </summary>
    public class ShodanClient : IShodanAsyncClient, IDisposable
    {
        private readonly string apikey;
        private const string BasePath = "https://api.shodan.io";
        private IRequstHandler RequestHandler = new RequestHandler();

        //todo error handle!!!

        public ShodanClient(string apikey)
        {
            if(string.IsNullOrWhiteSpace(apikey))
            {
                throw new ArgumentNullException(nameof(apikey));
            }
            this.apikey = apikey;
        }

        internal ShodanClient(string apikey, IRequstHandler requestHandler)
            : this(apikey)
        {
            RequestHandler = requestHandler;
        }

        /// <summary>
        /// Look up the IP address for the provided list of hostnames.
        /// </summary>
        /// <param name="hostnames">Comma-separated list of hostnames; example "google.com,bing.com" </param>
        /// <returns></returns>
        public Task<Dictionary<string, string>> DnsLookupAsync(string hostnames)
        {
            if(string.IsNullOrWhiteSpace(hostnames))
            {
                throw new ArgumentNullException(hostnames);
            }
            var url = new Uri($"{BasePath}/dns/resolve?hostnames={hostnames}&key={this.apikey}");
            return RequestHandler.MakeRequestAsync<Dictionary<string, string>>(url);
        }

        /// <summary>
        /// Search Shodan using the same query syntax as the website and use facets to get summary information for different properties.
        /// This method may use API query credits depending on usage. If any of the following criteria are met, your account will be deducated 1 query credit:
        /// 1. The search query contains a filter.
        /// 2. Accessing results past the 1st page using the "page". For every 100 results past the 1st page 1 query credit is deducted.
        /// </summary>
        /// <param name="query">lambda to generate a query. Shodan search query. The provided string is used to search the database of banners in Shodan, with the additional option to provide filters inside the search query using a "filter:value" format.</param>
        /// <param name="facet">static class to define facets.</param>
        /// <param name="page">The page number to page through results 100 at a time (default: 1) </param>
        /// <param name="minify">True or False; whether or not to truncate some of the larger fields (default: True) </param>
        /// <returns></returns>
        public Task<dynamic> SearchHosts(Action<QueryGenerator> query, Action<FacetGenerator> facet = null, int page = 1, bool minify = true)
        {
            if(query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            var queryGenerator = new QueryGenerator();
            query.Invoke(queryGenerator);
            var queryResult = queryGenerator.Generate();
            var url = new UriBuilder($"{BasePath}/shodan/host/search")
            {
                Query = $"key={apikey}&query={queryResult}&minify={minify.ToString()}"
            };
            if(facet != null)
            {
                var facetGenerator = new FacetGenerator();
                facet.Invoke(facetGenerator);
                url.Query = $"{url.Query}&facets={facetGenerator.GenerateFacets()}";
            }
            if(page > 1)
            {
                url.Query = $"{url.Query}&page={page}";
            }
            return RequestHandler.MakeRequestAsync<dynamic>(url.Uri);
        }

        /// <summary>
        /// This method behaves identical to <see cref="SearchHosts(SearchQuery, FacetQuery, int, bool)"/>" with the only difference that this method does not return any host results, it only returns the total number of results that matched the query and any facet information that was requested. As a result this method does not consume query credits.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="facet"></param>
        /// <returns></returns>
        public Task<SearchHostResults> SearchHostsCount(Action<QueryGenerator> query, Action<FacetGenerator> facet = null)
        {
            if(query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            var queryGenObj = new QueryGenerator();
            query.Invoke(queryGenObj);

            var url = new UriBuilder($"{BasePath}/shodan/host/count")
            {
                Query = $"key={apikey}&query={queryGenObj.Generate()}"
            };
            if(facet != null)
            {
                var facetGenObj = new FacetGenerator();
                facet.Invoke(facetGenObj);

                url.Query = $"{url.Query}&facets={facetGenObj.GenerateFacets()}";
            }

            return RequestHandler.MakeRequestAsync<SearchHostResults>(url.Uri);
        }

        public Task<SearchTokens> SearchTokens(Action<QueryGenerator> query)
        {
            if(query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            var queryObj = new QueryGenerator();
            query.Invoke(queryObj);
            var url = new Uri($"{BasePath}/shodan/host/search/tokens?key={apikey}&query={queryObj.Generate()}");
            return RequestHandler.MakeRequestAsync<SearchTokens>(url);
        }

        /// <summary>
        /// Calculates a honeypot probability score ranging from 0 (not a honeypot) to 1.0 (is a honeypot).
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<double> Experimental_GetHoneyPotScoreAsync(string ip)
        {
            if(string.IsNullOrWhiteSpace(ip))
            {
                throw new ArgumentNullException(nameof(ip));
            }
            var url = new Uri($"{BasePath}/labs/honeyscore/{ip}?key={apikey}");
            var result = await RequestHandler.MakeRequestAsync<string>(url);
            double resultParsed;
            if(!double.TryParse(result, out resultParsed))
            {
                throw new ShodanException($"honeypot score returned with {result} failed to parse to double");
            }
            return resultParsed;
        }

        /// <summary>
        /// Returns information about the API plan belonging to the given API key.
        /// </summary>
        /// <returns></returns>
        public Task<ApiStatus> GetApiStatusAsync()
        {
            var url = new Uri($"{BasePath}/api-info?key={apikey}");
            return RequestHandler.MakeRequestAsync<ApiStatus>(url);
        }

        /// <summary>
        /// Returns all services that have been found on the given host IP.
        /// </summary>
        /// <param name="Ip">Host IP address</param>
        /// <param name="history">True if all historical banners should be returned (default: False) </param>
        /// <param name="minify">True to only return the list of ports and the general host information, no banners. (default: False) </param>
        /// <returns></returns>
        public Task<Host> GetHostAsync(string Ip, bool history = false, bool minify = false)
        {
            if(string.IsNullOrWhiteSpace(Ip))
            {
                throw new ArgumentNullException(nameof(Ip));
            }
            var builder = new UriBuilder($"{BasePath}/shodan/host/{Ip}")
            {
                Query = $"key={this.apikey}&history={history.ToString()}&minify={minify.ToString()}"
            };

            return RequestHandler.MakeRequestAsync<Host>(builder.Uri);
        }

        /// <summary>
        /// Get your current IP address as seen from the Internet.
        /// </summary>
        /// <returns></returns>
        public Task<string> GetMyIpAsync()
        {
            var url = new Uri($"{BasePath}/tools/myip?key={this.apikey}");
            return RequestHandler.MakeRequestAsync<string>(url);
        }

        /// <summary>
        /// This method returns a list of port numbers that the crawlers are looking for.
        /// </summary>
        /// <returns></returns>
        public Task<List<int>> GetPortsAsync()
        {
            var builder = new Uri($"{BasePath}/shodan/ports?key={this.apikey}");
            return RequestHandler.MakeRequestAsync<List<int>>(builder);
        }

        /// <summary>
        /// Returns information about the Shodan account linked to this API key.
        /// </summary>
        /// <returns></returns>
        public Task<Profile> GetProfileAsync()
        {
            var url = new Uri($"{BasePath}/account/profile?key={apikey}");
            return RequestHandler.MakeRequestAsync<Profile>(url);
        }

        /// <summary>
        /// This method returns an object containing all the protocols that can be used when launching an Internet scan.
        /// </summary>
        /// <returns></returns>
        public Task<Dictionary<string, string>> GetProtocolsAsync()
        {
            var url = new Uri($"{BasePath}/shodan/protocols?key={this.apikey}");
            return RequestHandler.MakeRequestAsync<Dictionary<string, string>>(url);
        }

        /// <summary>
        /// Use this method to obtain a list of search queries that users have saved in Shodan.
        /// </summary>
        /// <param name="page"> Page number to iterate over results; each page contains 10 items </param>
        /// <param name="sort"> Sort the list based on a property. Possible values are: votes, timestamp </param>
        /// <param name="order">Whether to sort the list in ascending or descending order. Possible values are: asc, desc </param>
        /// <returns></returns>
        public Task<SearchQueries> GetQueriesAsync(int? page = null, SortOptions? sort = null, OrderOption? order = null)
        {
            var url = new UriBuilder($"{BasePath}/shodan/query")
            {
                Query = $"key={apikey}"
            };
            if(sort.HasValue)
            {
                var sortName = Enum.GetName(typeof(SortOptions), sort.Value);
                url.Query = $"{url.Query}&sort={sortName}";
            }
            if(order.HasValue)
            {
                var orderName = Enum.GetName(typeof(OrderOption), order.Value);
                url.Query = $"{url.Query}&order={orderName}";
            }
            return RequestHandler.MakeRequestAsync<SearchQueries>(url.Uri);
        }

        /// <summary>
        ///  Use this method to search the directory of search queries that users have saved in Shodan.
        /// </summary>
        /// <param name="query"> What to search for in the directory of saved search queries. </param>
        /// <param name="page">Page number to iterate over results; each page contains 10 items </param>
        /// <returns></returns>
        public Task<SearchQueries> SearchQueriesAsync(string query, int? page = null)
        {
            if(string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(query);
            }
            var url = new UriBuilder($"{BasePath}/shodan/query/search")
            {
                Query = $"key={apikey}&query={query}"
            };
            if(page != null)
            {
                url.Query = $"{url.Query}&page={page}";
            }
            return RequestHandler.MakeRequestAsync<SearchQueries>(url.Uri);
        }

        /// <summary>
        /// Check the progress of a previously submitted scan request
        /// </summary>
        /// <param name="id">the unique scan ID that was returned by <see cref="RequstScanAsync(string)"/></param>
        /// <returns></returns>
        public Task<ScanStatus> GetScanStatusAsync(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            var url = new Uri($"{BasePath}/shodan/scan/{id}");
            return RequestHandler.MakeRequestAsync<ScanStatus>(url);
        }

        /// <summary>
        /// This method returns an object containing all the services that the Shodan crawlers look at. It can also be used as a quick and practical way to resolve a port number to the name of a service
        /// </summary>
        /// <returns></returns>
        public Task<Dictionary<string, string>> GetServicesAsync()
        {
            var url = new Uri($"{BasePath}/shodan/services?key={this.apikey}");
            return RequestHandler.MakeRequestAsync<Dictionary<string, string>>(url);
        }

        /// <summary>
        /// Use this method to obtain a list of popular tags for the saved search queries in Shodan.
        /// </summary>
        /// <param name="size">The number of tags to return </param>
        /// <returns></returns>
        public Task<TagResult> GetTagsAsync(int size = 10)
        {
            var url = new UriBuilder($"{BasePath}/shodan/query/tags")
            {
                Query = $"key={apikey}&size={size}"
            };
            return RequestHandler.MakeRequestAsync<TagResult>(url.Uri);
        }

        /// <summary>
        /// Use this method to request Shodan to crawl the Internet for a specific port.
        /// This method is restricted to security researchers and companies with a Shodan Data license. To apply for access to this method as a researcher, please email jmath@shodan.io with information about your project. Access is restricted to prevent abuse.
        /// </summary>
        /// <param name="port">The port that Shodan should crawl the Internet for. </param>
        /// <param name="protocol">The name of the protocol that should be used to interrogate the port. See <see cref="GetProtocolsAsync"/> for a list of supported protocols. </param>
        /// <returns></returns>
        public Task<ScanPortResult> RequestInternetPortScanAsync(int port, string protocol)
        {
            var url = new Uri($"{BasePath}/shodan/scan/internet?key={this.apikey}");
            using(var data = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("port", port.ToString()),
                new KeyValuePair<string, string>("protocol", protocol)
            }))
            {
                return RequestHandler.MakeRequestAsync<ScanPortResult>(url, data, RequestType.POST);
            }
        }

        /// <summary>
        /// Use this method to request Shodan to crawl a network
        /// <strong>Requirements:</strong> This method uses API scan credits: 1 IP consumes 1 scan credit. You must have a paid API plan (either one-time payment or subscription) in order to use this method
        /// </summary>
        /// <param name="ips"></param>
        /// <returns></returns>
        public Task<ScanResult> RequstScanAsync(string ips)
        {
            if(string.IsNullOrWhiteSpace(ips))
            {
                throw new ArgumentNullException(nameof(ips));
            }
            if(!ips.Split(',').Any())
            {
                throw new ArgumentOutOfRangeException($"{ips} must have one valid record");
            }
            var url = new Uri($"{BasePath}/shodan/scan?key={this.apikey}");
            using(var data = new FormUrlEncodedContent(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("ips", ips) }))
            {
                return RequestHandler.MakeRequestAsync<ScanResult>(url, data, RequestType.POST);
            }
        }

        /// <summary>
        /// Look up the hostnames that have been defined for the given list of IP addresses
        /// </summary>
        /// <param name="ips">Comma-separated list of IP addresses; example "74.125.227.230,204.79.197.200"</param>
        /// <returns></returns>
        public Task<Dictionary<string, List<string>>> ReverseLookupAsync(string ips)
        {
            if(string.IsNullOrWhiteSpace(ips))
            {
                throw new ArgumentNullException(ips);
            }
            var url = new Uri($"{BasePath}/dns/reverse?ips={ips}&key={this.apikey}");
            return RequestHandler.MakeRequestAsync<Dictionary<string, List<string>>>(url);
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    RequestHandler.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}