using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch]
    internal class AudioLogs
    {
        [HarmonyPostfix]
        [HarmonyPatch((typeof(CH1AudioLogsController)),"HandleAudioLogThomas01OnInteracted")]
        public static void ThomasLog1()
        {
            Client.SendLocation("CH1 Audio Log Thomas 1");
        }


        [HarmonyPostfix]
        [HarmonyPatch((typeof(CH1AudioLogsController)), "HandleAudioLogWally01OnInteracted")]
        public static void WallyLog1()
        {
            Client.SendLocation("CH1 Audio Log Wally 1");
        }
    }
}
