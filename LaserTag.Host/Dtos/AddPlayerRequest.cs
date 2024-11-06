using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Dtos
{
    public class AddPlayerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string MacGun { get; set; } = string.Empty;
        public string MacVest { get; set; } = string.Empty;
    }
}
