using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MixyWhitelister
{
    public class Config : IRocketPluginConfiguration
    {
        [XmlArrayItem("SteamID")]
        public List<string> SteamIDList = new List<string>();
        public void LoadDefaults()
        {
            SteamIDList = new List<string>()
            {
                "00000000000000000",
            };
        }
    }
}
