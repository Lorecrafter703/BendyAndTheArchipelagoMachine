using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(AchievementManager))]
    internal class PerfectScores
    {
        [HarmonyPostfix]
        [HarmonyPatch("SetAchievement")]
        public static void BullsEyeAcheivement(AchievementName key, AchievementManager __instance)
        {
            if (!(bool)Client.serverData.GetSlotDataOption("minigame_sanity")) return;
            switch (key)
            {
                case AchievementName.BULLS_EYE:
                    Client.SendLocation("CH4 Bulls Eye");
                    break;
                case AchievementName.CALL_THE_MILK_MAN:
                    Client.SendLocation("CH4 Call the Milk Man");
                    break;
                case AchievementName.WASTING_TIME:
                    Client.SendLocation("CH4 Wasting Time");
                    break;
                default:
                    break;
            }
        }
    }
}
