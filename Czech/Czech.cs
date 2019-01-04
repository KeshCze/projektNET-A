using Newtonsoft.Json;
using PluginsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Czech
{
    public class Czech : Plugins
    {
        string url = "http://data.fixer.io/api/latest?access_key=0f007733ee0a34353fa71745e4bc68a6&symbols=CZK&format=1";
        public Tuple<string, decimal> GetCurr()
        {
            WebClient wc = new WebClient();
            RootObject obj = null;
            try
            {
                obj = JsonConvert.DeserializeObject<RootObject>(wc.DownloadString(url));
            }
            catch (Exception)
            {

            }           
            return new Tuple<string, decimal>("Czech",obj.rates.CZK);           
        }
    }
}
