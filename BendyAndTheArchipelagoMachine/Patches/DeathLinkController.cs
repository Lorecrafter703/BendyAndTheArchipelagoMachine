using Archipelago.MultiClient.Net.Enums;
using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using S13Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch]
    internal class DeathLinkController
    {
        public static PlayerController playerController;
        public static bool isDead = false;


        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerController), "Init")]
        public static void getPlayerRef(PlayerController __instance)
        {
            playerController = __instance;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerController), "OnDisposed")]
        public static void clearPlayerRef()
        {
            playerController = null;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerController), "Die")]
        public static void onDeath()
        {
            BendyAndTheArchipelagoMachine.Logger.LogDebug("Died");
            BendyAndTheArchipelagoMachine.ArchipelagoClient.deathLinkHandler.SendDeathLink();
        }


        [HarmonyReversePatch(HarmonyReversePatchType.Original)]
        [HarmonyPatch(typeof(PlayerController), "Die")]
        public static void KillPlayer(PlayerController instance) => throw (new NotImplementedException());


        public static void Revive()
        {
            if (playerController == null)
            {
                isDead = true;
                return;
            }
            isDead = playerController.CurrentStatus == CombatStatus.Hiding;
        }
    }
}
