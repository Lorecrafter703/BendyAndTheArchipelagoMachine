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
    [HarmonyPatch]
    internal class BaconSoup
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BaconSoupController), nameof(BaconSoupController.HandleCannedSoupOnInteracted))]
        private static void PickupBaconSoup(object sender, EventArgs e)
        {
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"sender type: {sender.GetType()}");
            try
            {
                var il2Obj = (Il2CppSystem.Object)sender;
                CannedSoupEdible cannedSoupEdible = il2Obj.Cast<CannedSoupEdible>();
                int id = cannedSoupEdible.GetID();
                int chapter = MyChapterController.GetChapterNumber();
                BendyAndTheArchipelagoMachine.Logger.LogDebug($"Interacted with CH{chapter} Bacon Soup {id}");
                Client.SendLocation($"CH{chapter} Bacon Soup {id}");
            } catch (Exception ex)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(ex);
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BaconSoupController), nameof(BaconSoupController.InitOnComplete))]
        private static void OnInit(BaconSoupController __instance)
        {
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"instance: {__instance} | name: {__instance.name}");
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"    type: {__instance.GetType()}");
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"    Total BaconSoups: {__instance.m_BaconSoups.Count}");
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"    Chapter: {__instance.m_Chapter}");
        }
    }
}
