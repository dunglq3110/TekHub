using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Dtos
{
    public class PlayerAttributesResponse
    {
        [JsonProperty("key")]
        public string Key { get; set; } = "start_battle";
        [JsonProperty("for_gun")]
        public Dictionary<string, int> ForGun { get; set; }
        [JsonProperty("for_vest")]
        public Dictionary<string, int> ForVest { get; set; }
    }
}
