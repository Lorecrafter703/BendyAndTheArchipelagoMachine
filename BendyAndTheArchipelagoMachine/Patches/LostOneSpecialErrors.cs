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
    }
}
