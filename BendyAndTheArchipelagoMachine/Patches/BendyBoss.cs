using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH5ThroneRoom))]
    internal class BendyBoss
    {
        public static Interactable BendyAudioLog;


        [HarmonyPostfix]
        [HarmonyPatch("InitOnComplete")]
        public static void GetAudioLogReference(CH5ThroneRoom __instance, AudioLog ___m_AudioLog)
        {
            BendyAudioLog = ___m_AudioLog;
        }


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearAudioLogRef()
        {
            BendyAudioLog = null;
        }


        public static bool CheckBaconSoupRequirement()
        {
            int count = 0;
            foreach (long _ in Client.serverData.ReceivedItems)
            {
                if (_ == IDTables.GetItemID("Bacon Soup")) count++;
            }

            var BaconSoupsRequiredOption = (long)Client.serverData.GetSlotDataOption("bacon_soups_required");
            var TotalBaconSoupsOption = (long)Client.serverData.GetSlotDataOption("total_bacon_soups");
            long BaconSoupsRequired = TotalBaconSoupsOption * BaconSoupsRequiredOption / 100;

            return count >= BaconSoupsRequired;
        }
    }
}
