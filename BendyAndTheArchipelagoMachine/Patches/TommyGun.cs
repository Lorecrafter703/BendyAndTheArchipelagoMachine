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
            long option = (long)Client.serverData.GetSlotDataOption("deathless_challenges");
            if (option == 0 || option == 2) return;
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"TommyGunCheck: {__result}");
            LocationEligable = __result;
            if (!__result && Client.HasItem("CH3 Tommy Gun"))
            {
                GameManager.Instance.GameData.CurrentSaveFile.CH3Data.HasTommyGun = true;
                __result = true;
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3ProjectionistTaskController), "HandleGunOnInteracted")]
        public static void HandleTommyGunInteract()
        {
            if (!LocationEligable) return;
            long option = (long)Client.serverData.GetSlotDataOption("deathless_challenges");
            if (option == 0 || option == 2) return;
            Client.SendLocation("CH3 Tommy Gun");
        }
    }
}
