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
            return Client.HasItem("CH3 Toys");
        }


        [HarmonyPostfix]
        [HarmonyPatch("HandleToyOnInteracted")]
        public static void HandleToysPickup()
        {
            BendyAndTheArchipelagoMachine.Logger.LogMessage("Toys Picked Up!");
            //if (this.m_IsLeftSolved && this.m_IsRightSolved) Client.SendLocation("CH3 Toys");
        }
    }
}
