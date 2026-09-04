using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH2SewerController))]
    internal class ValvePickup
    {
        [HarmonyPostfix]
        [HarmonyPatch("HandleValvePickupOnInteracted")]
        public static void HandleValvePickup()
        {
            Client.SendLocation("CH2 Sewer Valve");
        }


        [HarmonyPrefix]
        [HarmonyPatch("HandleValveOnInteracted")]
        public static bool HandleValveInteract(CH2SewerController __instance)
        {
            if (Client.HasItem("Sewer Valve")) return true;
            __instance.m_Valve.m_Valve.isInteracted = false;
            __instance.m_Valve.m_Valve.m_HasInteractedOnce = false;
            return false;
        }
    }
}
