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
        [HarmonyPrefix]
        [HarmonyPatch("HandleInitialBookOnInteracted")]
        public static bool HandleBookInteract(object sender)
        {
            if (Client.HasItem("CH4 Books")) return true;
            var il2cppObject = (Il2CppSystem.Object)sender;
            Interactable book = il2cppObject.Cast<Interactable>();
            book.isInteracted = false;
            book.m_HasInteractedOnce = false;
            return false;
        }
    }
}
