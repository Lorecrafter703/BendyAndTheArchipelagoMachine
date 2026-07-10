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

        [HarmonyReversePatch(HarmonyReversePatchType.Original)]
        [HarmonyPatch("LoadChapter")]
        public static void MyLoadChapter(ChapterController instance, string chapter) => throw (new NotImplementedException());


        [HarmonyPrefix]
        [HarmonyPatch("CompleteChapter")]
        public static bool OnChapterComplete(ChapterController __instance, Chapters ___m_Chapter, ref bool ___m_IsActive)
        {
            // send check
            switch (___m_Chapter)
            {
                case Chapters.ONE:
                    Client.SendLocation("CH1 Complete");
                    break;
            }
            // Return to hub
            MyLoadChapter(currentChapter, "Archives");

            return false;
        }


        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void ChapterControllerInit(ChapterController __instance)
        {
            currentChapter = __instance;
            BendyAndTheArchipelagoMachine.Logger.LogWarning("Tracking Chapter Controller Instance");
        }
    }
}
