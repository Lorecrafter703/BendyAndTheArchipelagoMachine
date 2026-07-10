using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Data
{
    public static class ReceivedItems
    {
        private static List<string> items = new List<string>();

        public static void AddItem(string item) { items.Add(item); }

        public static void AddItem(long id) { items.Add(IDTables.GetItemName(id)); }

        public static bool HasItem(string item) { return items.Contains(item); }

        public static bool HasItem(long id) { return items.Contains(IDTables.GetItemName(id)); }

        public static void ShowAllItems()
        {
            BendyAndTheArchipelagoMachine.Logger.LogMessage("Showing all Items:");
            foreach (var item in items) BendyAndTheArchipelagoMachine.Logger.LogMessage(item);
            BendyAndTheArchipelagoMachine.Logger.LogMessage("End of Item List");
        }
    }
}
