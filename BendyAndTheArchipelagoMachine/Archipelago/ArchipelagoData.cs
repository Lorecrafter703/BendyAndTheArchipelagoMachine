using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BendyAndTheArchipelagoMachine.Archipelago
{
    public class ArchipelagoData
    {
        public string Uri;
        public string SlotName;
        public string Password;
        public int Index; // TODO Remember this value between sessions

        public List<long> CheckedLocations;
        public List<long> ReceivedItems;

        private int SaveSlot = -1;
        private string seed;

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


        public void SetupSession(Dictionary<string, object> roomSlotData, string roomSeed)
        {
            slotData = roomSlotData;
            seed = roomSeed;
        }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
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
    }
}