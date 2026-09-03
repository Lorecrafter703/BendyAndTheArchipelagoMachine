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
        [HarmonyPrefix]
        [HarmonyPatch("HandleAudioLogOnInteracted")]
        public static bool HandleBendyBossStart(CH5ThroneRoom __instance)
        {
            int count = 0;
            foreach (long _ in Client.serverData.ReceivedItems)
            {
                if (_ == IDTables.GetItemID("Bacon Soup")) count++;
            }

            var BaconSoupsRequiredOption = (long)Client.serverData.GetSlotDataOption("bacon_soups_required");
            var TotalBaconSoupsOption = (long)Client.serverData.GetSlotDataOption("total_bacon_soups");
            long BaconSoupsRequired = TotalBaconSoupsOption * BaconSoupsRequiredOption / 100;

            if (count >= BaconSoupsRequired) return true;
            __instance.m_AudioLog.isInteracted = false;
            __instance.m_AudioLog.m_HasInteractedOnce = false;
            return false;
        }
    }
}
