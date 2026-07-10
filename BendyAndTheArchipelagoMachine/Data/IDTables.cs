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
            { 8, "Unlock CH1" },
            { 9, "CH2 Keys" },
            { 10, "CH2 Valve" },
            { 11, "Unlock CH2" }
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
            { "CH2 Bacon Soup 0", 31 },
            { "CH2 Bacon Soup 1", 32 },
            { "CH2 Bacon Soup 2", 33 },
            { "CH2 Bacon Soup 3", 34 },
            { "CH2 Bacon Soup 4", 35 },
            { "CH2 Bacon Soup 5", 36 },
            { "CH2 Bacon Soup 6", 37 },
            { "CH2 Bacon Soup 7", 38 },
            { "CH2 Bacon Soup 8", 39 },
            { "CH2 Bacon Soup 9", 40 },
            { "CH2 Bacon Soup 10", 41 },
            { "CH2 Bacon Soup 11", 42 },
            { "CH2 Bacon Soup 12", 43 },
            { "CH2 Bacon Soup 13", 44 },
            { "CH2 Bacon Soup 14", 45 },
            { "CH2 Bacon Soup 15", 46 },
            { "CH2 Bacon Soup 16", 47 },
            { "CH2 Bacon Soup 17", 48 },
            { "CH2 Bacon Soup 18", 49 },
            { "CH2 Bacon Soup 19", 50 },
            { "CH2 Bacon Soup 20", 51 },
            { "CH2 Bacon Soup 21", 52 },
            { "CH2 Bacon Soup 22", 53 },
            { "CH2 Bacon Soup 23", 54 },
            { "CH2 Bacon Soup 24", 55 },
            { "CH2 Bacon Soup 25", 56 },
            { "CH2 Bacon Soup 26", 57 },
            { "CH2 Bacon Soup 27", 58 },
            { "CH2 Bacon Soup 28", 59 },
            { "CH2 Bacon Soup 29", 60 },
            { "CH2 Bacon Soup 30", 61 },
            { "CH2 Keys", 62 },
            { "CH2 Valve", 63 },
            { "CH2 Audio Log The Prayer", 64 },
            { "CH2 Audio Log Distractions", 65 },
            { "CH2 Audio Log The New Voice Actress", 66 },
            { "CH2 Audio Log The Projectionist", 67 },
            { "CH2 Audio Log Lost Key", 68 },
            { "CH2 Audio Log Favorite Song", 69 },
            { "CH2 Audio Log Jack Fain", 70 },
            { "CH2 Complete", 71 }
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
