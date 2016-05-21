using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    [DataContract]
    public class SslProperties
    {
        /// <summary>
        /// The parsed certificate properties that includes information such as when it was issued, the SSL extensions, the issuer, subject etc.
        /// </summary>
        [DataMember(Name = "cert")]
        public dynamic Cert { get; set; }

        /// <summary>
        /// Preferred cipher for the SSL connection
        /// </summary>
        [DataMember(Name = "ciper")]
        public dynamic Ciper { get; set; }

        /// <summary>
        /// An array of certificates, where each string is a PEM-encoded SSL certificate. This includes the user SSL certificate up to its root certificate.
        /// </summary>
        [DataMember(Name = "chain")]
        public IList<dynamic> Chain { get; set; }

        /// <summary>
        /// The Diffie-Hellman parameters if available: "prime", "public_key", "bits", "generator" and an optional "fingerprint" if we know which program generated these parameters.
        /// </summary>
        [DataMember(Name = "dhparams")]
        public dynamic DhParams { get; set; }

        /// <summary>
        /// A list of SSL versions that are supported by the server. If a version isnt supported the value is prefixed with a "-". Example: ["TLSv1", "-SSLv2"] means that the server supports TLSv1 but doesnt support SSLv2.
        /// </summary>
        [DataMember(Name = "versions")]
        public IList<string> Versions { get; set; }
    }
}