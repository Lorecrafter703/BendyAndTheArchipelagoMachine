using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH1Pedestal))]
    internal class RitualItems
    {
        [HarmonyPostfix]
        [HarmonyPatch("HandleCollectableOnCollected")]
        public static void HandleRitualItemPickup(CH1Pedestal __instance)
        {
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Ritual item picked up: {__instance.m_CollectableType}");
            Client.SendLocation(GetRitualItemName(__instance.m_CollectableType));
        }


        [HarmonyPrefix]
        [HarmonyPatch("HandlePedestalOnInteracted")]
        public static bool HandlePedestalInteract(CH1Pedestal __instance)
        {
            if (Client.HasItem(GetRitualItemName(__instance.m_CollectableType))) return true;
            __instance.m_Pedestal.isInteracted = false;
            __instance.m_Pedestal.m_HasInteractedOnce = false;
            return false;
        }


        private static string GetRitualItemName(CH1Pedestal.CollectableType type)
        {
            return type switch
            {
                CH1Pedestal.CollectableType.BOOK => "CH1 Book",
                CH1Pedestal.CollectableType.DOLL => "CH1 Doll",
                CH1Pedestal.CollectableType.GEAR => "CH1 Gear",
                CH1Pedestal.CollectableType.INKWELL => "CH1 Inkwell",
                CH1Pedestal.CollectableType.RECORD => "CH1 Record",
                CH1Pedestal.CollectableType.WRENCH => "CH1 Wrench",
                _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown Item Type {type}"),
            };
        }
    }
}
