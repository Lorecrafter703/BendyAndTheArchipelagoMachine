using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH4AccountingController))]
    internal class BookPuzzleDoor
    {
        public static List<Interactable> Books = new List<Interactable>();

        [HarmonyPostfix]
        [HarmonyPatch("InitOnComplete")]
        public static void AddBookRefs(CH4AccountingController __instance, List<CH4AccountingController.BookPuzzle> ___m_BookPuzzle)
        {
            foreach (var bookPuzzle in ___m_BookPuzzle)
            {
                Books.Add(bookPuzzle.Book);
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearBookRefs()
        {
            Books.Clear();
        }


        public static bool HandleBookOnInteract()
        {
            return Client.HasItem("CH4 Books");
        }
    }
}
