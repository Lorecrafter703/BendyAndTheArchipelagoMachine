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
        public static Dictionary<CH1Pedestal.CollectableType, CH1Pedestal> CH1RitualItems = new Dictionary<CH1Pedestal.CollectableType, CH1Pedestal>();

        [HarmonyPostfix]
        [HarmonyPatch("Initialize")]
        public static void OnInit(Transform collectableLocation, CH1Pedestal __instance, CH1Pedestal.CollectableType ___m_CollectableType)
        {
            CH1RitualItems.Add(___m_CollectableType, __instance);
        }

        [HarmonyPrefix]
        [HarmonyPatch("HandleCollectableOnCollected")]
        public static bool PickupRitualItem(CH1Pedestal __instance, CH1Pedestal.CollectableType ___m_CollectableType)
        {
            BendyAndTheArchipelagoMachine.Logger.LogWarning($"Picked up Ritual Item ({___m_CollectableType})");

            bool hasItem = false;

            // Try to send check
            switch (___m_CollectableType)
            {
                case CH1Pedestal.CollectableType.DOLL:
                    Client.SendCheck("CH1 Doll");
                    hasItem = Client.HasItem("CH1 Doll");
                    break;
                case CH1Pedestal.CollectableType.BOOK:
                    Client.SendCheck("CH1 Book");
                    Client.HasItem("CH1 Book");
                    break;
                case CH1Pedestal.CollectableType.WRENCH:
                    Client.SendCheck("CH1 Wrench");
                    Client.HasItem("CH1 Wrench");
                    break;
                case CH1Pedestal.CollectableType.INKWELL:
                    Client.SendCheck("CH1 Inkwell");
                    Client.HasItem("CH1 Inkwell");
                    break;
                case CH1Pedestal.CollectableType.GEAR:
                    Client.SendCheck("CH1 Gear");
                    Client.HasItem("CH1 Gear");
                    break;
                case CH1Pedestal.CollectableType.RECORD:
                    Client.SendCheck("CH1 Record");
                    Client.HasItem("CH1 Record");
                    break;
                default:
                    string message = "Unknown Collectable Type: " + ___m_CollectableType.ToString();
                    throw new Exception(message);
            }

            return hasItem;
        }

        //[HarmonyTranspiler]
        //[HarmonyPatch("HandleCollectableOnCollected")]
        //private static IEnumerable<CodeInstruction> PickupRitualItem(IEnumerable<CodeInstruction> instructions)
        //{
        //    bool foundLdcCall1 = false;
        //    bool foundLdcCall2 = false;
        //    int startIndex = -1; // Start of individual stretch
        //    int endIndex = -1; // End of individual stretch
        //    int tempIndex = -1; // Midpoint start

        //    List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
        //    for (int i = 0; i < codes.Count; i++)
        //    {
        //        if (codes[i].opcode == OpCodes.Callvirt) // Check for the Callvirts
        //        {
        //            if (foundLdcCall2)
        //            {
        //                BendyAndTheArchipelagoMachine.Logger.LogError("End Second Call " + i);

        //                endIndex = i; // End of block is on the Callvirt instruction
        //                break;
        //            }
        //            else
        //            {
        //                BendyAndTheArchipelagoMachine.Logger.LogError("Start " + (i + 1));
        //                if (!foundLdcCall1)
        //                    startIndex = i + 1; // Start of block is the instruction after a Callvirt
        //                else
        //                    tempIndex = i + 1;

        //                for (int j = foundLdcCall1 ? tempIndex : startIndex; j < codes.Count; j++) // Loop until next Callvirt, or find the ldc call
        //                {
        //                    if (codes[j].opcode == OpCodes.Callvirt)
        //                    {
        //                        break; // Not in this bllock
        //                    }
        //                    if (codes[j].opcode == OpCodes.Ldc_I4_1)
        //                    {
        //                        if (!foundLdcCall1)
        //                            foundLdcCall1 = true;
        //                        else
        //                            foundLdcCall2 = true;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (startIndex > -1 && endIndex > -1)
        //    {
        //        BendyAndTheArchipelagoMachine.Logger.LogWarning("start: " + startIndex + " | end: " + endIndex + " | temp: " + tempIndex);
        //        for (int i = startIndex; i <= endIndex; i++)
        //        {
        //            codes[i].opcode = OpCodes.Nop;
        //        }
        //    }
        //    else
        //    {
        //        BendyAndTheArchipelagoMachine.Logger.LogError("Couldn't Find Block");
        //    }
        //    return codes.AsEnumerable();
        //}

        //[HarmonyReversePatch(HarmonyReversePatchType.Original)]
        //[HarmonyPatch("HandleCollectableOnCollected")]
        //public static void GiveRitualItem(CH1Pedestal item)
        //{
        //    IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        //    {
        //        List<CodeInstruction> codes = new List<CodeInstruction>(instructions);

        //        bool foundEnd = false;
        //        int endIndex = -1;

        //        for (int i = 0; i < codes.Count; i++)
        //        {
        //            if (codes[i].opcode == OpCodes.Callvirt)
        //            {
        //                endIndex = i;
        //                BendyAndTheArchipelagoMachine.Logger.LogWarning("End " + i);
        //            }
        //            if (codes[i].opcode == OpCodes.Ldc_I4_1)
        //            {
        //                foundEnd = true;
        //                BendyAndTheArchipelagoMachine.Logger.LogWarning("Found End");
        //                break;
        //            }
        //        }

        //        if (foundEnd)
        //        {
        //            codes[0].opcode = OpCodes.Nop;
        //            codes.RemoveRange(1, endIndex);
        //        }

        //        return codes.AsEnumerable();
        //    }
        //}
    }
}
