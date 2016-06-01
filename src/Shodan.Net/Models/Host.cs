using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    /// <summary>
    /// Represents return data for querying hosts
    /// </summary>
    [DataContract]
    public class Host
    {
        [DataMember(Name = "region_code")]
        public string RegionCode { get; set; }

        [DataMember(Name = "ip")]
        public string Ip { get; set; }

        [DataMember(Name = "area_code")]
        public string AreaCode { get; set; }

        [DataMember(Name = "country_names")]
        public string CountryName { get; set; }

        [DataMember(Name = "hostnames")]
        public List<string> Hostnames { get; set; }

        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        [DataMember(Name = "dma_code")]
        public string DmaCode { get; set; }

        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        public List<Banner> Data { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        [DataMember(Name = "country_code3")]
        public string CountryCode3 { get; set; }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        [DataMember(Name = "os")]
        public string Os { get; set; }

        [DataMember(Name = "ports")]
        public IList<int> Ports { get; set; }
    }
}