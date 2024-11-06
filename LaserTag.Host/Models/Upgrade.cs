using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{
    public class Upgrade
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Cost { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<UpgradeAttribute> Attributes { get; set; } = new List<UpgradeAttribute>();

        public Upgrade Clone()
        {
            return new Upgrade
            {
                Id = this.Id,
                Name = this.Name,
                Cost = this.Cost,
                Description = this.Description,
                Attributes = new List<UpgradeAttribute>(this.Attributes)
            };
        }
    }
}
