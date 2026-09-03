using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch]
    internal class LostKeys
    {
        public static BaseDoorController closetDoor;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2LostKeysController), "InitOnComplete")]
        public static void RegisterDoor(CH2LostKeysController __instance)
        {
            closetDoor = __instance.m_ClosetDoor;
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Registered Door: {closetDoor}");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(BaseDoorController), "OnDisposed")]
        public static void ClearDoorRef(BaseDoorController __instance)
        {
            if (__instance != closetDoor) return;
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Clearing Door: {closetDoor}");
            closetDoor = null;
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Cleared Door: {closetDoor}");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2LostKeysController), "HandleKeysOnCollected")]
        public static void HandleKeysPickup(CH2LostKeysController __instance)
        {
            Client.SendLocation("CH2 Keys");
            if (!Client.HasItem("CH2 Keys")) __instance.m_ClosetDoor.Lock();
        }


        public static void UnlockDoor()
        {
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Calling Unlock Door: {closetDoor}");
            closetDoor.Unlock();
        }
    }
}
