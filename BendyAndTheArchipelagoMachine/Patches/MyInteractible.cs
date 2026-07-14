using BendyAndTheArchipelagoMachine.Archipelago;
using BendyAndTheArchipelagoMachine.Utils;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Interactable;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(Interactable))]
    internal class MyInteractible
    {
        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void InteractableInit(Interactable __instance)
        {
            //if (__instance is CannedSoupEdible)
            //{
            //    CannedSoupEdible soup = (CannedSoupEdible)__instance;
            //    BendyAndTheArchipelagoMachine.Logger.LogMessage("Initialized Bacon Soup # " + soup.GetID());
            //}
        }


        [HarmonyPrefix]
        [HarmonyPatch("Init")]
        public static void FixMyInteractable(Interactable __instance, ref List<MeshRenderer> ___m_HighlightMeshRenderers, List<MaterialData> ___m_MaterialDatas)
        {
            if (__instance is ChapterLoader)
            {
                BendyAndTheArchipelagoMachine.Logger.LogWarning("Chapter Loader Init");
                ___m_HighlightMeshRenderers = new List<MeshRenderer>();
                __instance.ResetInteraction();
            }
        }


        [HarmonyPrefix]
        [HarmonyPatch("Interact")]
        public static bool InteractPatch(Interactable __instance)
        {
            if (RitualItems.CH1RitualItemInteractablesToType.ContainsKey(__instance)) return RitualItems.HandleRitualItemPickup(__instance);
            if (LostKeys.keysReference != null && LostKeys.keysReference == __instance) return LostKeys.HandleKeysPickup();
            if (ValvePickup.valveReference != null && ValvePickup.valveReference == __instance) return ValvePickup.HandleValvePickup();
            if (ToyBlockages.ToysRef.Contains(__instance)) return ToyBlockages.HandleToyPickup();
            if (BertrumFight.BertrumFightAudioLog != null && BertrumFight.BertrumFightAudioLog == __instance) return BertrumFight.HandleAudioLogInteract();
            if (BookPuzzleDoor.Books.Contains(__instance)) return BookPuzzleDoor.HandleBookOnInteract();
            return true;
        }
    }
}
