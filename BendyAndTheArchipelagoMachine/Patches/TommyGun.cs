using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH3ProjectionistTaskController))]
    internal class TommyGun
    {
        [HarmonyPostfix]
        [HarmonyPatch("Activate")]
        public static void GuaranteeTommyGun(CH3ProjectionistTaskController __instance)
        {
            if ((long)Client.serverData.GetSlotDataOption("include_tommy_gun") == 0) return;
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"TommyGunCheck: {__instance.m_CanHaveTommyGun}");
            if (__instance.m_CanHaveTommyGun) Client.SendLocation("CH3 Tommy Gun Challenge");

            if (Client.HasItem("Tommy Gun"))
            {
                GameManager.Instance.GameData.CurrentSaveFile.CH3Data.HasTommyGun = true;
                __instance.m_CanHaveTommyGun = true;
                __instance.m_Weapon.gameObject.SetActive(true);
                __instance.m_WeaponFake.gameObject.SetActive(false);
                return;
            }
            __instance.m_CanHaveTommyGun = false;
            __instance.m_Weapon.gameObject.SetActive(false);
            __instance.m_WeaponFake.gameObject.SetActive(true);
        }
    }
}
