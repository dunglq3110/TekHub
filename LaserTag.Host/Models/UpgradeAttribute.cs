using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{
    public class UpgradeAttribute
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Upgrade Upgrade { get; set; }

        public GameAttribute GameAttribute { get; set; }

        public int Value { get; set; }

        public UpgradeAttribute(int id, Upgrade upgrade, GameAttribute gameAttribute, int value)
        {
            Id = id;
            Upgrade = upgrade;
            GameAttribute = gameAttribute.Clone();
            Value = value;
        }
    }
}
