using System.Runtime.Serialization;

namespace Shodan.Net.Models
{
    /// <summary>
    /// Basic location data returned by shodan
    /// </summary>
    [DataContract]
    public class Location
    {
        /// <summary>
        ///  An object containing all of the location information for the device.
        /// </summary>
        public Location()
        {
        }

        /// <summary>
        /// The 3-letter country code for the device location.
        /// </summary>

        [DataMember(Name = "country_code3")]
        public string CountryCode3 { get; set; }

        /// <summary>
        /// The name of the city where the device is located.
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// The postal code for the device's location.
        /// </summary>
        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The longitude for the geolocation of the device.
        /// </summary>
        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// The 2-letter country code for the device location.
        /// </summary>

        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// The latitude for the geolocation of the device
        /// </summary>

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// The name of the country where the device is located.
        /// </summary>
        [DataMember(Name = "country_name")]
        public string CountryName { get; set; }

        /// <summary>
        /// The area code for the device's location. Only available for the US.
        /// </summary>
        [DataMember(Name = "area_code")]
        public int? AreaCode { get; set; }

        /// <summary>
        ///  The designated market area code for the area where the device is located. Only available for the US.
        /// </summary>
        [DataMember(Name = "dma_code")]
        public int? DmaCode { get; set; }

        /// <summary>
        /// The name of the region where the device is located.
        /// </summary>
        [DataMember(Name = "region_code")]
        public string RegionCode { get; set; }
    }
}