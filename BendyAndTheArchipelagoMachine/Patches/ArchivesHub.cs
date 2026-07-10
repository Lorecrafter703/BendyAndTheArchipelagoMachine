using BendyAndTheArchipelagoMachine.Utils;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMG.Data;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(ArchivesController))]
    internal class ArchivesHub
    {
        [HarmonyPostfix]
        [HarmonyPatch("InitOnComplete")]
        public static void OnArchiveHubInit()
        {
            Vector3 spawnPoint = GameManager.Instance.Player.transform.position;
            BendyAndTheArchipelagoMachine.Logger.LogInfo($"Player Spawn: {spawnPoint} | Player Tag: {GameManager.Instance.Player.gameObject.tag}");

            //GameObject go = MakeTriggerBall(new Vector3(-24, 2, 29));
            GameObject go = MakeInteractableBall(new Vector3(-24, 2, 29));
        }

        
        public static GameObject MakeTriggerBall(Vector3 spawn)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = spawn;
            Collider collider = sphere.GetComponent<Collider>();
            collider.isTrigger = true;
            sphere.AddComponent(typeof(ChapterLoader));

            return sphere;
        }

        public static GameObject MakeInteractableBall(Vector3 spawn)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = spawn;
            sphere.AddComponent(typeof(ChapterLoader));

            return sphere;
        }
    }
}
