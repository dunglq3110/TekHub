using LaserTag.Host.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserTag.Host.Dtos
{
    public class LogViewModel
    {
        public DateTime Time { get; set; }
        public Round Round { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Damage { get; set; }
    }
}
