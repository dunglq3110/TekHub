using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{public class PlayerUpgrades
    {
        public int Id { get; set; }
        public Player Player { get; set; } = new Player();
        public Upgrade Upgrade { get; set; } = new Upgrade();  

    }
    
}
