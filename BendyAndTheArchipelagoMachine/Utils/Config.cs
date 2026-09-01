using BepInEx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Utils
{
    public class Config
    {
        public string LastUri;
        public string LastSlotName;
        public bool DeathlinkEnabled;
        public float ArchipelagoConsoleWidth;
        public float ArchipelagoConsoleHeight;


        public Config(string uri, string slot, bool deathlink)
        {
            LastUri = uri;
            LastSlotName = slot;
            DeathlinkEnabled = deathlink;
            ArchipelagoConsoleWidth = 0.4f;
            ArchipelagoConsoleHeight = 0.6f;
        }


        public void UpdateConnectionInfo(string uri, string slot)
        {
            LastUri = uri;
            LastSlotName = slot;

            SaveConfig();
        }


        public void UpdateDeathlink(bool deathlink)
        {
            DeathlinkEnabled = deathlink;

            SaveConfig();
        }


        private void SaveConfig()
        {
            string cfgPath = Path.Combine(Paths.PluginPath, "Lorecrafter703-Bendy_and_the_Archipelago_Machine", "BendyAndTheArchipelagoMachine", "config.json");
            string cfgData = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(cfgPath, cfgData);
        }
    }
}
