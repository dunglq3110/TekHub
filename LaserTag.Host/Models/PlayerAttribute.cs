using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Models
{
    public class PlayerAttribute
    {
        public Player Player { get; set; }
        public GameAttribute GameAttribute { get; set; }
        public int Value { get; set; }

    }
}
