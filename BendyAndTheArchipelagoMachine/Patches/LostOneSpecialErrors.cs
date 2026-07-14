using HarmonyLib;
using S13Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH4LostOneSpecial))]
    internal class LostOneSpecialErrors
    {
        [HarmonyPrefix]
        [HarmonyPatch("Splash")]
        public static bool SplashFix(CH4LostOneSpecial __instance, S13ObjectComplex ___m_S13Object)
        {
            return ___m_S13Object != null;
        }


        [HarmonyPostfix]
        [HarmonyPatch("Init")]
        public static void AmITheProblem(CH4LostOneSpecial __instance, S13ObjectComplex ___m_S13Object, ParticleSystem ___m_SplashParticles)
        {
            if (___m_S13Object != null) BendyAndTheArchipelagoMachine.Logger.LogWarning($"S13Object: {___m_S13Object.ToString()}");
            else BendyAndTheArchipelagoMachine.Logger.LogError("No S13Object");
            if (___m_SplashParticles != null) BendyAndTheArchipelagoMachine.Logger.LogWarning($"S13Object: {___m_SplashParticles.ToString()}");
            else BendyAndTheArchipelagoMachine.Logger.LogError("No SplashParticles");
            BendyAndTheArchipelagoMachine.Logger.LogWarning($"CH4LostOne: {__instance.gameObject.name}");
        }
    }
}
