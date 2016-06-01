using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shodan.Net
{
    public class QueryGenerator
    {
        internal Dictionary<string, string> queryData = new Dictionary<string, string>();
        private HashSet<string> CalledMethods = new HashSet<string>();
        private string searchText = string.Empty;

        internal QueryGenerator()
        {
        }

        public QueryGenerator Before(DateTime time)
        {
            queryData.Add("before", time.ToString("dd/MM/yyyy"));
            return this;
        }

        /// <summary>
        /// Only show results that were collected after the given date
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public QueryGenerator After(DateTime time)
        {
            queryData.Add("after", time.ToString("dd/MM/yyyy"));
            return this;
        }

        /// <summary>
        /// The Autonomous System Number that identifies the network the device is on.
        /// </summary>
        /// <param name="asn"></param>
        /// <returns></returns>
        public QueryGenerator WithAsn(string asn)
        {
            queryData.Add("asn", asn);
            return this;
        }

        public QueryGenerator Withcity(string city)
        {
            queryData.Add("city", city);
            return this;
        }

        public QueryGenerator Withcountry(string country)
        {
            queryData.Add("country", country);
            return this;
        }

        /// <summary>
        /// If "true" only show results that have a screenshot available.
        /// </summary>
        /// <param name="hasScreenshot"></param>
        /// <returns></returns>
        public QueryGenerator HasScreenshot(bool hasScreenshot = true)
        {
            queryData.Add("has_screenshot", hasScreenshot.ToString());
            return this;
        }

        public QueryGenerator WithHostname(string hostname)
        {
            queryData.Add("hostname", hostname);
            return this;
        }

        public QueryGenerator WithHtml(string html)
        {
            queryData.Add("html", html);
            return this;
        }

        public QueryGenerator WithIsp(string isp)
        {
            queryData.Add("isp", isp);
            return this;
        }

        public QueryGenerator WithNet(string net)
        {
            queryData.Add("net", net);
            return this;
        }

        public QueryGenerator WithOrg(string org)
        {
            queryData.Add("org", org);
            return this;
        }

        public QueryGenerator WithOs(string os)
        {
            queryData.Add("os", os);
            return this;
        }

        public QueryGenerator Withport(string port)
        {
            queryData.Add("port", port);
            return this;
        }

        public QueryGenerator With_postal(string postal)
        {
            queryData.Add("postal", postal);
            return this;
        }

        public QueryGenerator With_product(string product)
        {
            queryData.Add("product", product);
            return this;
        }

        public QueryGenerator With_state(string state)
        {
            queryData.Add("state", state);
            return this;
        }

        public QueryGenerator With_title(string title)
        {
            queryData.Add("title", title);
            return this;
        }

        public QueryGenerator With_version(string version)
        {
            queryData.Add("version", version);
            return this;
        }

        public QueryGenerator With_Bitcoinip(string bitcoinip)
        {
            queryData.Add("bitcoin.ip", bitcoinip);
            return this;
        }

        public QueryGenerator With_Bitcoinip_count(string bitcoinip_count)
        {
            queryData.Add("bitcoin.ip_count", bitcoinip_count);
            return this;
        }

        public QueryGenerator WithBitcoinport(string bitcoinport)
        {
            queryData.Add("bitcoin.port", bitcoinport);
            return this;
        }

        public QueryGenerator WithBitcoinversion(string bitcoinversion)
        {
            queryData.Add("bitcoin.version", bitcoinversion);
            return this;
        }

        public QueryGenerator WithNtpip(string ntpip)
        {
            queryData.Add("ntp.ip", ntpip);
            return this;
        }

        public QueryGenerator WithNtpip_count(string ntpip_count)
        {
            queryData.Add("ntp.ip_count", ntpip_count);
            return this;
        }

        public QueryGenerator WithNtpmore(string ntpmore)
        {
            queryData.Add("ntp.more", ntpmore);
            return this;
        }

        public QueryGenerator WithNtpport(string ntpport)
        {
            queryData.Add("ntp.port", ntpport);
            return this;
        }

        public QueryGenerator WithText(string searchText)
        {
            if(!string.IsNullOrWhiteSpace(this.searchText))
            {
                throw new ShodanException("Method cannot be called twice");
            }
            this.searchText = searchText;
            return this;
        }

        internal string Generate()
        {
            var sb = new StringBuilder(searchText);
            if(!string.IsNullOrWhiteSpace(searchText))
            {
                sb.Append(" ");
            }
            foreach(var item in queryData)
            {
                sb.Append($"{item.Key}:{item.Value}");
            }
            return sb.ToString();
        }
    }
}