using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(DeathController))]
    internal class DeathLinkController
    {
        [HarmonyPostfix]
        [HarmonyPatch("HandlePlayerOnDeath")]
        public static void onDeath()
        {
            BendyAndTheArchipelagoMachine.Logger.LogDebug("Died");
            BendyAndTheArchipelagoMachine.ArchipelagoClient.deathLinkHandler.SendDeathLink();
        }
    }
}
