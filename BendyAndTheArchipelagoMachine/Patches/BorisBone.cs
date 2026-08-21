using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH3BoneController))]
    internal class BorisBone
    {
        public static Interactable boneReference;

        [HarmonyPostfix]
        [HarmonyPatch("InitOnComplete")]
        public static void RegisterBone(CH3BoneController __instance, Interactable ___m_Bone)
        {
            boneReference = ___m_Bone;
        }


        public static bool HandleBonePickup()
        {
            if ((long)Client.serverData.GetSlotDataOption("boris_bone") == 0) return true;
            Client.SendLocation("CH3 Boris Bone");
            return Client.HasItem("CH3 Boris Bone");
        }


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearBoneRef()
        {
            boneReference = null;
        }
    }
}
