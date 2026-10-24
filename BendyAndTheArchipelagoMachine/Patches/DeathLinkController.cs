using Archipelago.MultiClient.Net.Enums;
using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using S13Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch]
    internal class DeathLinkController
    {
        public static PlayerController playerController;
        public static bool isDead = false;
        public static bool deathLinkDeath = false;
        public static DeathController deathController;


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
        [HarmonyPatch(typeof(DeathController), "HandlePlayerOnDeath")]
        public static void OnPlayerDeath(object sender, EventArgs e)
        {
            if (!deathLinkDeath)
            {
                BendyAndTheArchipelagoMachine.Logger.LogDebug($"Sender: {sender} ({sender}) | EventArgs: {e} ({e.GetType()})");
                BendyAndTheArchipelagoMachine.ArchipelagoClient.deathLinkHandler.SendDeathLink();
            }
            deathLinkDeath = false;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(DeathLinkController), "KillPlayer")]
        public static void DefineSource()
        {
            deathLinkDeath = true;
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


        [HarmonyPostfix]
        [HarmonyPatch(typeof(DeathController), "InitOnComplete")]
        public static void getDeathControllerRef(ref DeathController __instance)
        {
            deathController = __instance;
            if (MyChapterController.currentChapterNumber == Chapters.TWO)
            {
                __instance.m_SpawnPoints = AddSpawnPoint(new Vector3(0f, 2.96f, -15.00f), __instance.m_SpawnPoints);
            }
        }


        public static Il2CppReferenceArray<PlayerSpawnNode> AddSpawnPoint(Vector3 position, Il2CppReferenceArray<PlayerSpawnNode> current)
        {
            GameObject newNode = new GameObject();
            newNode.AddComponent<PlayerSpawnNode>();
            newNode.transform.position = position;
            Il2CppReferenceArray<PlayerSpawnNode> output = new Il2CppReferenceArray<PlayerSpawnNode>(current.Length + 1);
            for (int i = 0; i < current.Length; i++)
            {
                output[i] = current[i];
            }
            output[current.Length] = newNode.GetComponent<PlayerSpawnNode>();
            return output;
        }
    }
}
