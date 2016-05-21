using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
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

        [DataMember(Name = "data")]
        public List<Banner> Data { get; set; }
    }
}