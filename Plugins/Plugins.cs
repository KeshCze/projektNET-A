using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginsNS
{
    public interface Plugins
    {
        Tuple<string, decimal,string> GetCurr();

    }
}
