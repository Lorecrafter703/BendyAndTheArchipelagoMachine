using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(ChapterController))]
    internal class MyChapterController
    {
        public static ChapterController currentChapter;

        [HarmonyReversePatch]
        [HarmonyPatch("CompleteChapter")]
        public static void MyCompleteChapter(ChapterController instance) => throw (new NotImplementedException());

        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void ChapterControllerInit(ChapterController __instance)
        {
            currentChapter = __instance;
            BendyAndTheArchipelagoMachine.Logger.LogWarning("Tracking Chapter Controller Instance");
        }
    }
}
