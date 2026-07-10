using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Data
{
    internal struct IDTables
    {
        private static readonly Dictionary<long, string> itemIDtoName = new Dictionary<long, string>
        {
            { 1, "Bacon Soup" },
            { 2, "CH1 Book" },
            { 3, "CH1 Doll" },
            { 4, "CH1 Gear" },
            { 5, "CH1 Inkwell" },
            { 6, "CH1 Record" },
            { 7, "CH1 Wrench" },
            { 8, "Unlock CH2" },
        };

        private static readonly Dictionary<string, long> locationNametoID = new Dictionary<string, long>
        {
            { "CH1 Bacon Soup 1", 1 },
            { "CH1 Bacon Soup 2", 2  },
            { "CH1 Bacon Soup 3", 3 },
            { "CH1 Bacon Soup 4", 4 },
            { "CH1 Bacon Soup 5", 5 },
            { "CH1 Bacon Soup 6", 6 },
            { "CH1 Bacon Soup 7", 7 },
            { "CH1 Bacon Soup 8", 8 },
            { "CH1 Bacon Soup 9", 9 },
            { "CH1 Bacon Soup 10", 10 },
            { "CH1 Bacon Soup 11", 11 },
            { "CH1 Bacon Soup 12", 12 },
            { "CH1 Bacon Soup 13", 13 },
            { "CH1 Bacon Soup 14", 14 },
            { "CH1 Bacon Soup 15", 15 },
            { "CH1 Bacon Soup 16", 16 },
            { "CH1 Bacon Soup 17", 17  },
            { "CH1 Bacon Soup 18", 18 },
            { "CH1 Bacon Soup 19", 19 },
            { "CH1 Bacon Soup 20", 20 },
            { "CH1 Bacon Soup 0", 21 },
            { "CH1 Book", 22 },
            { "CH1 Doll", 23 },
            { "CH1 Gear", 24 },
            { "CH1 Inkwell", 25 },
            { "CH1 Record", 26 },
            { "CH1 Wrench", 27 },
            { "CH1 Audio Log Thomas 1", 28 },
            { "CH1 Audio Log Wally 1", 29 },
            { "CH1 Complete", 30 },
        };

        public static string GetItemName(long id)
        {
            try
            {
                return itemIDtoName[id];
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError($"No item found at id {id}.\n    {e.Message}");
                return null;
            }
        }

        public static long GetLocationID(string name)
        {
            try
            {
                return locationNametoID[name];
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError($"No location found with name {name}.\n    {e.Message}");
                return -1;
            }
        }
    }
}
