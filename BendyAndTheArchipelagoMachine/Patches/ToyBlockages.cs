using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH3ToyMachine))]
    internal class ToyBlockages
    {
        [HarmonyPrefix]
        [HarmonyPatch("HndleConveyerSwitchOnInteracted")]
        public static bool HandlyToyMachineInteract()
        {
            return Client.HasItem("Toy Machine");
        }


        [HarmonyPostfix]
        [HarmonyPatch("HandleToyOnInteracted")]
        public static void HandleToysPickup(CH3ToyMachine __instance)
        {
            BendyAndTheArchipelagoMachine.Logger.LogMessage("Toys Picked Up!");
            if (__instance.m_IsLeftSolved && __instance.m_IsRightSolved) Client.SendLocation("CH3 Toy Blockage");
        }
    }
}
