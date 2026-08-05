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
    internal class TommyGun
    {
        private static bool LocationEligable = false;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3ProjectionistTaskController), "CheckTommyGun")]
        public static void GuaranteeTommyGun(ref bool __result)
        {
            if ((long)Client.serverData.GetSlotDataOption("include_tommy_gun") == 0) return;
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"TommyGunCheck: {__result}");
            LocationEligable = __result;
            if (!__result && Client.HasItem("CH3 Tommy Gun"))
            {
                GameManager.Instance.GameData.CurrentSaveFile.CH3Data.HasTommyGun = true;
                __result = true;
                return;
            }
            __result = false;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3ProjectionistTaskController), "HandleGunOnInteracted")]
        public static void HandleTommyGunInteract()
        {
            if (!LocationEligable) return;
            if ((long)Client.serverData.GetSlotDataOption("include_tommy_gun") == 0) return;
            Client.SendLocation("CH3 Tommy Gun");
        }
    }
}
