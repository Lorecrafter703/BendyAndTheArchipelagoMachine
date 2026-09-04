using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH4BertrumController))]
    internal class BertrumFight
    {
        [HarmonyPrefix]
        [HarmonyPatch("HandleAudioLogOnInteracted")]
        public static bool HandleAudioLogInteract(CH4BertrumController __instance)
        {
            if (Client.HasItem("CH4 Bossfight Bertrum")) return true;
            __instance.m_AudioLog.isInteracted = false;
            __instance.m_AudioLog.m_HasInteractedOnce = false;
            return false;
        }
    }
}
