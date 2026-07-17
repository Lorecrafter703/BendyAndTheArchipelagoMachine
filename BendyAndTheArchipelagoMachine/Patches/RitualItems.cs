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


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearReferences(CH1Pedestal __instance, CH1Pedestal.CollectableType ___m_CollectableType, Interactable ___m_Collectable)
        {
            CH1RitualItemsTypeToPedestal.Remove(___m_CollectableType);
            CH1RitualItemInteractablesToType.Remove(___m_Collectable);
        }


        public static bool HandleRitualItemPickup(Interactable item)
        {
            switch (CH1RitualItemInteractablesToType[item])
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
                    BendyAndTheArchipelagoMachine.Logger.LogError($"Unknown Item Type {CH1RitualItemInteractablesToType[item]}");
                    return false;
            }
        }
    }
}
