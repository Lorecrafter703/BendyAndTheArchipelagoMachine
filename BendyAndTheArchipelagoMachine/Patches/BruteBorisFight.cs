using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(BruteBorisAi))]
    internal class BruteBorisFight
    {
        [HarmonyPostfix]
        [HarmonyPatch("Death")]
        public static void OnBruteBorisDeath()
        {
            Client.SendLocation("CH4 Brute Boris Boss");
        }
    }
}
