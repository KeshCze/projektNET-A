using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginsNS
{
    public class Rates
    {
        public decimal CZK { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
    }
}
