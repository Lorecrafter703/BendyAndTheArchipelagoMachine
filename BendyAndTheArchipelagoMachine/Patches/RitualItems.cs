using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH1Pedestal))]
    internal class RitualItems
    {
        public static Dictionary<CH1Pedestal.CollectableType, CH1Pedestal> CH1RitualItemsTypeToPedestal = new Dictionary<CH1Pedestal.CollectableType, CH1Pedestal>();
        public static Dictionary<Interactable, CH1Pedestal.CollectableType> CH1RitualItemInteractablesToType = new Dictionary<Interactable, CH1Pedestal.CollectableType>();

        [HarmonyPostfix]
        [HarmonyPatch("Initialize")]
        public static void OnInit(Transform collectableLocation, CH1Pedestal __instance, CH1Pedestal.CollectableType ___m_CollectableType, Interactable ___m_Collectable)
        {
            CH1RitualItemsTypeToPedestal.Add(___m_CollectableType, __instance);
            CH1RitualItemInteractablesToType.Add(___m_Collectable, ___m_CollectableType);
        }
    }
}
