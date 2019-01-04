using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            CurrenciesList = new List<Currency>();

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
                Tuple<string, decimal> tmpCurrency = getCurrencyFromPlugin(item);
                CurrenciesList.Add(new Currency() { Name = tmpCurrency.Item1, value = tmpCurrency.Item2 });
            }

            PluginAdded?.Invoke();
        }

        private Tuple<string, decimal> getCurrencyFromPlugin(string item)
        {
            // načti assembly s jménem, zavolej metodu, vrať Tuple
            string tmp = filePath + @"\plugins\" + item + ".dll";
            Assembly testAssembly = Assembly.LoadFile(filePath + @"\" + item + ".dll");
            Type objectType = testAssembly.GetType(item+ "." +item);
            if (!typeof(PluginsNS.Plugins).IsAssignableFrom(objectType))
            {
                throw new OutOfMemoryException();
            }
            MethodInfo methodInfo = objectType.GetMethod("GetCurr");
            object objectInstance = Activator.CreateInstance(objectType);
            var result = (Tuple<string,decimal>)methodInfo.Invoke(objectInstance, new object[0]);
            return result;           
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
