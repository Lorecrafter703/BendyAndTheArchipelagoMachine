using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(ChapterController))]
    internal class MyChapterController
    {
        public static ChapterController currentChapter;
        public static Chapters currentChapterNumber;

        [HarmonyReversePatch(HarmonyReversePatchType.Original)]
        [HarmonyPatch("LoadChapter")]
        public static void MyLoadChapter(ChapterController instance, string chapter) => throw (new NotImplementedException());


        [HarmonyPrefix]
        [HarmonyPatch("CompleteChapter")]
        public static bool OnChapterComplete(ChapterController __instance, Chapters ___m_Chapter)
        {
            // send check
            switch (___m_Chapter)
            {
                case Chapters.ONE:
                    Client.SendLocation("CH1 Complete");
                    break;
                case Chapters.TWO:
                    Client.SendLocation("CH2 Complete");
                    break;
                case Chapters.THREE:
                    Client.SendLocation("CH3 Complete");
                    break;
                case Chapters.FOUR:
                    Client.SendLocation("CH4 Complete");
                    break;
                case Chapters.FIVE:
                    Client.SendLocation("CH5 Complete");
                    break;
                default:
                    BendyAndTheArchipelagoMachine.Logger.LogWarning($"Unknown Chapter: {___m_Chapter}");
                    break;
            }
            // Return to hub
            MyLoadChapter(currentChapter, "Archives");

            return false;
        }


        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void ChapterControllerInit(ChapterController __instance, Chapters ___m_Chapter)
        {
            currentChapter = __instance;
            currentChapterNumber = ___m_Chapter;
            BendyAndTheArchipelagoMachine.Logger.LogWarning("Tracking Chapter Controller Instance");
        }


        public static int GetChapterNumber()
        {
            switch (currentChapterNumber)
            {
                case Chapters.ONE:
                    return 1;
                case Chapters.TWO:
                    return 2;
                case Chapters.THREE:
                    return 3;
                case Chapters.FOUR:
                    return 4;
                case Chapters.FIVE:
                    return 5;
                default:
                    return -1;
            }
        }
    }
}
