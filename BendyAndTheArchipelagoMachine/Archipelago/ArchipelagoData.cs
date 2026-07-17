using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BendyAndTheArchipelagoMachine.Archipelago
{
    public class ArchipelagoData
    {
        public string Uri;
        public string SlotName;
        public string Password;
        public int Index { get; private set; } // TODO Remember this value between sessions

        public List<long> CheckedLocations { get; private set; }
        public List<long> ReceivedItems { get; private set; }

        private int SaveSlot = -1;
        public string seed { get; private set; }

        private Dictionary<string, object> slotData;

        public bool NeedSlotData => slotData == null;

        public ArchipelagoData()
        {
            Uri = "localhost:38281";
            SlotName = "BendyTest";
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
            return JsonConvert.SerializeObject(this);
        }


        private void SaveData()
        {
            BendyAndTheArchipelagoMachine.Logger.LogWarning($"Seed: {seed}");
            string path = Path.Combine(Paths.PluginPath, "BendyAndTheArchipelagoMachine", $"{seed}.json");
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
            CheckedLocations.Add(itemID);
            SaveData();
        }
    }
}