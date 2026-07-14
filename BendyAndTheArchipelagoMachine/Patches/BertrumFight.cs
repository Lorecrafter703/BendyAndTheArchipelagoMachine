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
        public static Interactable BertrumFightAudioLog;

        [HarmonyPostfix]
        [HarmonyPatch("HandleDeathOnComplete")]
        public static void OnBertrumDeath()
        {
            Client.SendLocation("CH4 Bertrum Boss");
        }


        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void GetAudioLogReference(CH4BertrumController __instance, AudioLog ___m_AudioLog)
        {
            BertrumFightAudioLog = ___m_AudioLog;
        }


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearAudioLogRef()
        {
            BertrumFightAudioLog = null;
        }


        public static bool HandleAudioLogInteract()
        {
            return Client.HasItem("CH4 Bossfight Bertrum");
        }
    }
}
