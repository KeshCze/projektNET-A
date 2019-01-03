using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace projektNETA
{
    public class Currencies
    {
        public delegate void PluginAddedDelegate();
        public event PluginAddedDelegate PluginAdded;

        FileSystemWatcher watcher;
        private string filePath = Environment.CurrentDirectory + @"\plugins";

        public List<Currency> CurrenciesList { get; set; }

        public Currencies()
        {
            setWatcher();
            
        }
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void setWatcher()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = filePath;
            watcher.NotifyFilter = NotifyFilters.FileName;
            watcher.Filter = "*.dll";
            watcher.Created += Watcher_Created;
            watcher.EnableRaisingEvents = true;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            update();
        }

        private void update()
        {
            List<string> pluginNames = getPluginNames();
            foreach (var item in pluginNames)
            {
                Tuple<string, int> tmpCurrency = getCurrencyFromPlugin(item);

            }


            PluginAdded?.Invoke();
        }

        private Tuple<string, int> getCurrencyFromPlugin(string item)
        {
            // načti assembly s jménem, zavolej metodu, vrať Tuple
            throw new NotImplementedException();
            
        }

        private List<string> getPluginNames()
        {
            // načti z XML jména všech pluginů ve složce
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(filePath + @"\\config.xml");
            var listNodes = xmlDocument.SelectNodes("//plugins/plugin");
            var list = new List<string>();
            foreach (XmlNode item in listNodes)
            {
                list.Add(item.InnerText.ToString());
            }
            return list;

        }
    }
}
