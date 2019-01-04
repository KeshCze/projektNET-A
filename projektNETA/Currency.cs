using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace projektNETA
{
    [XmlRoot("Currencies")]
    public class Currency
    {
        public string Name { get; set; }
        public decimal value { get; set; }
        public string Globalization { get; set; }

    }
}
