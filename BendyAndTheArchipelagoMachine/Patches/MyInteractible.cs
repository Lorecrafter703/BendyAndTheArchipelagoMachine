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
            if (__instance is CannedSoupEdible)
            {
                CannedSoupEdible soup = (CannedSoupEdible)__instance;
                BendyAndTheArchipelagoMachine.Logger.LogMessage("Initialized Bacon Soup # " + soup.GetID());
            }
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
            if (!RitualItems.CH1RitualItemInteractablesToType.ContainsKey(__instance)) return true;
            var itemType = RitualItems.CH1RitualItemInteractablesToType[__instance];
            switch (itemType)
            {
                case CH1Pedestal.CollectableType.BOOK:
                    Client.SendLocation("CH1 Book");
                    return Client.HasItem("CH1 Book");
                case CH1Pedestal.CollectableType.DOLL:
                    Client.SendLocation("CH1 Doll");
                    return Client.HasItem("CH1 Doll");
                case CH1Pedestal.CollectableType.GEAR:
                    Client.SendLocation("CH1 Gear");
                    return Client.HasItem("CH1 Gear");
                case CH1Pedestal.CollectableType.INKWELL:
                    Client.SendLocation("CH1 Inkwell");
                    return Client.HasItem("CH1 Inkwell");
                case CH1Pedestal.CollectableType.RECORD:
                    Client.SendLocation("CH1 Record");
                    return Client.HasItem("CH1 Record");
                case CH1Pedestal.CollectableType.WRENCH:
                    Client.SendLocation("CH1 Wrench");
                    return Client.HasItem("CH1 Wrench");
                default:
                    BendyAndTheArchipelagoMachine.Logger.LogError($"Unknown Item Type {itemType}");
                    return false;
            }
        }
    }
}
