using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TekHub.Host.Models
{
    public class SharedBase
    {
        public string BaseId { get; set; } = string.Empty;
        public string BaseName1 { get; set; } = string.Empty;
        public string BaseName2 { get; set; } = string.Empty;
        public string BaseName3 { get; set; } = string.Empty;
        public string BaseName4 { get; set; } = string.Empty;
        public string BaseName5 { get; set; } = string.Empty;
        public int Sort { get; set; }
        public int Description { get; set; }

        public SharedGroup Group { get; set; } = new SharedGroup();
    }
}
