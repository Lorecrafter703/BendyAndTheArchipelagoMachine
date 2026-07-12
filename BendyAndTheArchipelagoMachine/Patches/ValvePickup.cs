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
        public static Interactable valveReference;

        [HarmonyPostfix]
        [HarmonyPatch("InitOnComplete")]
        public static void RegisterValve(CH2SewerController __instance, Interactable ___m_ValvePickup)
        {
            valveReference = ___m_ValvePickup;
        }


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearValveRef()
        {
            valveReference = null;
        }


        public static bool HandleValvePickup()
        {
            Client.SendLocation("CH2 Valve");
            return Client.HasItem("CH2 Valve");
        }
    }
}
