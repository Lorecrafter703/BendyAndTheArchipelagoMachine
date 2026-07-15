using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(MeatlyController))]
    internal class theMeatly
    {
        [HarmonyPostfix]
        [HarmonyPatch("HandleMeatlyAchievementTrigger")]
        public static void HandleMeatlySighting(MeatlyController __instance, Chapters ___m_Chapter)
        {
            if (!(bool)Client.serverData.GetSlotDataOption("the_meatly_sanity")) return;
            switch (___m_Chapter)
            {
                case Chapters.ONE:
                    Client.SendLocation("CH1 theMeatly");
                    break;
                case Chapters.TWO:
                    Client.SendLocation("CH2 theMeatly");
                    break;
                case Chapters.THREE:
                    Client.SendLocation("CH3 theMeatly");
                    break;
                case Chapters.FOUR:
                    Client.SendLocation("CH4 theMeatly");
                    break;
                case Chapters.FIVE:
                    Client.SendLocation("CH5 theMeatly");
                    break;
            }
        }
    }
}
