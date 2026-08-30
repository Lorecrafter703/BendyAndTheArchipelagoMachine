using Ai;
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
    internal class LeverChallenge
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3ServiceController), "HandleSearcherOnDeath")]
        public static void LeverChallenge1(CH3ServiceController __instance)
        {
            if ((long)Client.serverData.GetSlotDataOption("include_lever_challenges") == 0) return;
            if (__instance.m_Searchers.Count <= 0)
            {
                Client.SendLocation("CH3 Lever Challenge 1");
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3ServiceController), "HandleButcherGangOnDeath")]
        public static void LeverChallenge2(CH3ServiceController __instance)
        {
            if ((long)Client.serverData.GetSlotDataOption("include_lever_challenges") == 0) return;
            if (__instance.m_ButcherGang.Count <= 0)
            {
                Client.SendLocation("CH3 Lever Challenge 2");
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3ServiceController), "HandleSearcherBossOnDeath")]
        public static void LeverChallenge3(CH3ServiceController __instance)
        {
            if ((long)Client.serverData.GetSlotDataOption("include_lever_challenges") == 0) return;
            Client.SendLocation("CH3 Lever Challenge 3");
        }
    }
}
