using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH3BoneController))]
    internal class BorisBone
    {
        [HarmonyPostfix]
        [HarmonyPatch("HandleBoneOnInteracted")]
        public static void HandleBonePickup()
        {
            Client.SendLocation("CH3 Boris Bone");
        }


        [HarmonyPrefix]
        [HarmonyPatch("HandleBorisOnInteracted")]
        public static bool HandleBorisInteract(CH3BoneController __instance)
        {
            if (Client.HasItem("Poor Dog's Bone")) return true;
            __instance.m_Boris.Interact.isInteracted = false;
            __instance.m_Boris.Interact.m_HasInteractedOnce = false;
            return false;
        }
    }
}
