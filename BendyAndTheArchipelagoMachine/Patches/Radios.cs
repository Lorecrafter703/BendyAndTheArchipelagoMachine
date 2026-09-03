using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(RadioEasterEggController))]
    internal class Radios
    {
        [HarmonyPostfix]
        [HarmonyPatch("HandleRadioOnInteracted")]
        public static void OnRadioInteract(RadioEasterEggController __instance)
        {
            switch (__instance.m_AchievementAssetKey)
            {
                case AchievementName.CROONER_TUNER:
                    Client.SendLocation("CH1 Radio");
                    return;
                case AchievementName.COAST_TO_COAST:
                    Client.SendLocation("CH2 Radio");
                    return;
                case AchievementName.TURN_IT_UP:
                    Client.SendLocation("CH3 Radio");
                    return;
                case AchievementName.FINGER_WAGGIN:
                    Client.SendLocation("CH4 Radio");
                    return;
                case AchievementName.TOE_TAPPIN:
                    Client.SendLocation("CH5 Radio");
                    return;
                default:
                    BendyAndTheArchipelagoMachine.Logger.LogError($"Unrecognized Radio. AchievementName: {__instance.m_AchievementAssetKey}");
                    return;
            }
        }
    }
}
