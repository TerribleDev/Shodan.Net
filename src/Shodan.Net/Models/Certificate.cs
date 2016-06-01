using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Shodan.Net.Models
{
    public class Certificate
    {
        [DataMember(Name = "sig_alg")]
        public string SignatureAlgorithmm { get; set; }

        [DataMember(Name = "issued")]
        public string Issued { get; set; }

        [DataMember(Name = "expires")]
        public DateTime Expires { get; set; }

        [DataMember(Name = "expired")]
        public bool Expired { get; set; }

        [DataMember(Name = "version")]
        public int Version { get; set; }

        [DataMember(Name = "fingerprint")]
        public Fingerprint Fingerprint { get; set; }

        [DataMember(Name = "subject")]
        public dynamic Subject { get; set; }

        [DataMember(Name = "pubkey")]
        public dynamic PublicKey { get; set; }

        [DataMember(Name = "issuer")]
        public dynamic Issuer { get; set; }

        [DataMember(Name = "ciper")]
        public dynamic Ciper { get; set; }

        [JsonIgnore()]
        [DataMember(Name = "serial")]
        public string Serial { get; set; }
    }

    public class Fingerprint
    {
        public string sha256 { get; set; }
        public string sha1 { get; set; }
    }
}