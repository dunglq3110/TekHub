using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Dtos
{
    public class PlayerCredentialResponse
    {
        [JsonProperty("player_id")]
        public string PlayerId { get; set; } = string.Empty;
        [JsonProperty("gun_mac_address")]
        public string MacGun { get; set; } = string.Empty;
        [JsonProperty("vest_mac_address")]
        public string MacVest { get; set; } = string.Empty;
        [JsonProperty("team_id")]
        public string TeamId { get; set; } = string.Empty;
    }
}
