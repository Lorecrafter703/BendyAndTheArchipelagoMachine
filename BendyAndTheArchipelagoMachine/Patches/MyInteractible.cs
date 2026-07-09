using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(Interactable))]
    internal class MyInteractible
    {
        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void InteractableInit(Interactable __instance)
        {
            if (__instance is CannedSoupEdible)
            {
                CannedSoupEdible soup = (CannedSoupEdible)__instance;
                BendyAndTheArchipelagoMachine.Logger.LogMessage("Initialized Bacon Soup # " + soup.GetID());
            }
        }
    }
}
