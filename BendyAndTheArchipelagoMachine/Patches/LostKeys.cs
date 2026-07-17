using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH2LostKeysController))]
    internal class LostKeys
    {
        public static Interactable keysReference;

        [HarmonyPostfix]
        [HarmonyPatch("InitOnComplete")]
        public static void RegisterKeys(CH2LostKeysController __instance, Interactable ___m_Keys)
        {
            keysReference = ___m_Keys;
        }


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearKeysRef()
        {
            keysReference = null;
        }


        public static bool HandleKeysPickup()
        {
            Client.SendLocation("CH2 Keys");
            return Client.HasItem("CH2 Keys");
        }
    }
}
