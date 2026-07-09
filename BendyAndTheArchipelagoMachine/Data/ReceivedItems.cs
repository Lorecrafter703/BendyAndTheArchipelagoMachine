using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Data
{
    public struct ReceivedItems
    {
        private static List<string> items = new List<string>();

        public static void AddItem(string item) { items.Add(item); }

        public static void AddItem(long id) { items.Add(IDTables.GetItemName(id)); }

        public static bool HasItem(string item) { return items.Contains(item); }

        public static bool HasItem(long id) { return items.Contains(IDTables.GetItemName(id)); }
    }
}
