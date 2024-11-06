using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{
    public class Config
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string CodeName { get; set; } = string.Empty;
        public SharedBase ConfigType { get; set; }
        public string Value1 { get; set; } = string.Empty;
        public string Value2 { get; set; } = string.Empty;
        public string Value3 { get; set; } = string.Empty;
        public string Value4 { get; set; } = string.Empty;
        public string Value5 { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Name} ({CodeName}): " + Value1;
        }

    }
}
