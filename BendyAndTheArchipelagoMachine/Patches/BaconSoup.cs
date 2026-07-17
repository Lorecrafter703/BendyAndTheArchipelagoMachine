using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using HarmonyLib.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(BaconSoupController))]
    internal class BaconSoup
    {
        [HarmonyPostfix]
        [HarmonyPatch("HandleCannedSoupOnInteracted")]
        private static void PickupBaconSoup(object sender, EventArgs e)
        {
            CannedSoupEdible cannedSoupEdible = (CannedSoupEdible)sender;
            int id = cannedSoupEdible.GetID();
            int chapter = MyChapterController.GetChapterNumber();
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"Interacted with CH{chapter} Bacon Soup {id}");
            Client.SendLocation($"CH{chapter} Bacon Soup {id}");
        }

        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        private static void OnInit(BaconSoupController __instance, List<CannedSoupEdible> ___m_BaconSoups)
        {
            BendyAndTheArchipelagoMachine.Logger.LogMessage("Total BaconSoups: " + ___m_BaconSoups.Count);
        }
    }
}
