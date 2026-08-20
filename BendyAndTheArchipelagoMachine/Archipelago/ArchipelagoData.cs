using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using BendyAndTheArchipelagoMachine.Utils;

namespace BendyAndTheArchipelagoMachine.Archipelago
{
    public class ArchipelagoData
    {
        public string Uri;
        public string SlotName;
        public string Password;
        public int Index;

        public List<long> CheckedLocations { get; private set; }
        public List<long> ReceivedItems { get; private set; }

        private int SaveSlot = -1;
        public string seed;

        public Dictionary<string, object> slotData;
        private Config cfg;

        public bool NeedSlotData => slotData == null;

        public ArchipelagoData()
        {
            string cfgPath = Path.Combine(Paths.PluginPath, "Lorecrafter703-Bendy_and_the_Archipelago_Machine", "BendyAndTheArchipelagoMachine", "config.json");
            if (!File.Exists(cfgPath))
            {
                cfg = new Config("archipelago.gg:38281", "Bendy", false);
                string cfgData = JsonConvert.SerializeObject(cfg, Formatting.Indented);
                File.WriteAllText(cfgPath, cfgData);
            }
            else
            {
                string cfgData = File.ReadAllText(cfgPath);
                cfg = JsonConvert.DeserializeObject<Config>(cfgData);
            }
            Uri = cfg.LastUri;
            SlotName = cfg.LastSlotName;
            CheckedLocations = new List<long>();
            ReceivedItems = new List<long>();
        }


        public ArchipelagoData(string uri, string slotName, string password)
        {
            Uri = uri;
            SlotName = slotName;
            Password = password;
            CheckedLocations = new List<long>();
            ReceivedItems = new List<long>();
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }


        private void SaveData()
        {
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Seed: {seed}");
            string path = Path.Combine(Paths.PluginPath, "Lorecrafter703-Bendy_and_the_Archipelago_Machine", "BendyAndTheArchipelagoMachine", "savedata", $"{seed}.json");
            string data = this.ToString();
            File.WriteAllText(path, data);
        }


        public void SetupSession(Dictionary<string, object> roomSlotData, string roomSeed)
        {
            slotData = roomSlotData;
            seed = roomSeed;
            SaveData();
        }


        public object GetSlotDataOption(string key)
        {
            try
            {
                return slotData[key];
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(e);
                return null;
            }
        }


        public bool VerifySlot(int slot)
        {
            if (SaveSlot == -1) SaveSlot = slot;
            if (SaveSlot != slot) return false;
            return true;
        }

        
        public int GetSlot()
        {
            return SaveSlot;
        }


        public void AddItem(long itemID)
        {
            ReceivedItems.Add(itemID);
            Index++;
            SaveData();
        }

        public void CheckLocation(long itemID)
        {
            if (!CheckedLocations.Contains(itemID)) CheckedLocations.Add(itemID);
            SaveData();
        }


        public void UpdateConnectionInfo(string uri, string slot)
        {
            cfg.UpdateConnectionInfo(uri, slot);
        }
    }
}