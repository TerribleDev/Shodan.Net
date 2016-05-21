using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class Banner
    {
        /// <summary>
        /// The timestamp for when the banner was fetched from the device in the UTC timezone.
        /// </summary>
        [DataMember(Name = "timestamp")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Either "udp" or "tcp" to indicate which IP transport protocol was used to fetch the information
        /// </summary>
        [DataMember(Name = "transport")]
        public string Transport { get; set; }

        /// <summary>
        /// An array of strings containing all of the hostnames that have been assigned to the IP address for this device.
        /// </summary>
        [DataMember(Name = "hostnames")]
        public IList<string> Hostnames { get; set; }

        /// <summary>
        /// The name of the organization that is assigned the IP space for this device.
        /// </summary>
        [DataMember(Name = "org")]
        public string Org { get; set; }

        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Contains the banner information for the service.
        /// </summary>
        [DataMember(Name = "data")]
        public string Data { get; set; }

        /// <summary>
        /// The port number that the service is operating on
        /// </summary>
        [DataMember(Name = "port")]
        public int Port { get; set; }

        /// <summary>
        /// The ISP that is providing the organization with the IP space for this device. Consider this the "parent" of the organization in terms of IP ownership.
        /// </summary>
        [DataMember(Name = "isp")]
        public string Isp { get; set; }

        /// <summary>
        /// The autonomous system number (ex. "AS4837"
        /// </summary>
        [DataMember(Name = "asn")]
        public string Asn { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        /// <summary>
        /// The IP address of the host as an integer
        /// </summary>
        [DataMember(Name = "ip")]
        public int? Ip { get; set; }

        /// <summary>
        /// The IPv6 address of the host as a string. If this is present then the "ip" and "ip_str" fields wont be.
        /// </summary>
        [DataMember(Name = "ip")]
        public string Ipv6 { get; set; }

        /// <summary>
        /// ] An array of strings containing the top-level domains for the hostnames of the device. This is a utility property in case you want to filter by TLD instead of subdomain. It is smart enough to handle global TLDs with several dots in the domain (ex. "co.uk")
        /// </summary>
        [DataMember(Name = "domains")]
        public IList<string> Domains { get; set; }

        /// <summary>
        /// The IP address of the host as a string
        /// </summary>
        [DataMember(Name = "ip_str")]
        public string IpStr { get; set; }

        /// <summary>
        /// The operating system that powers the device.
        /// </summary>
        [DataMember(Name = "os")]
        public object Os { get; set; }

        /// <summary>
        ///  Contains experimental and supplemental data for the service. This can include the SSL certificate, robots.txt and other raw information that hasn't yet been formalized into the Banner Specification.
        /// </summary>
        [DataMember(Name = "opts", IsRequired = false)]
        public dynamic Opts { get; set; }

        #region Optional Properties

        /// <summary>
        /// The number of minutes that the device has been online.
        /// </summary>
        [DataMember(Name = "uptime", IsRequired = false)]
        public int? Uptime { get; set; }

        [DataMember(Name = "link", IsRequired = false)]
        public string Link { get; set; }

        [DataMember(Name = "title", IsRequired = false)]
        public string Title { get; set; }

        [DataMember(Name = "html", IsRequired = false)]
        public string Html { get; set; }

        [DataMember(Name = "product", IsRequired = false)]
        public string Product { get; set; }

        [DataMember(Name = "version", IsRequired = false)]
        public string Version { get; set; }

        [DataMember(Name = "devicetype", IsRequired = false)]
        public string DeviceType { get; set; }

        [DataMember(Name = "info", IsRequired = false)]
        public string Info { get; set; }

        [DataMember(Name = "cpe", IsRequired = false)]
        public string Cpe { get; set; }

        [DataMember(Name = "ssl", IsRequired = false)]
        public SslProperties Ssl { get; set; }

        #endregion Optional Properties
    }
}