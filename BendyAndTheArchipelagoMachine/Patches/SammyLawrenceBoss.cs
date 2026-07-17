using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(SammyLawrence_Ai))]
    internal class SammyLawrenceBoss
    {
        [HarmonyPostfix]
        [HarmonyPatch("TriggerDeath")]
        public static void OnSammyDeath()
        {
            Client.SendLocation("CH5 Sammy Lawrence Boss");
        }
    }
}
