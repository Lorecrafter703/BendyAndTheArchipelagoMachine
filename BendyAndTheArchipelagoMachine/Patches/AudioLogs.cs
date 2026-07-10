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


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogThePrayerOnInteracted")]
        public static void ThePrayerLog()
        {
            Client.SendLocation("CH2 Audio Log The Prayer");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogDistractionsOnInteracted")]
        public static void DistractionsLog()
        {
            Client.SendLocation("CH2 Audio Log Distractions");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogTheNewVoiceAcressOnInteracted")]
        public static void TheNewVoiceActressLog()
        {
            Client.SendLocation("CH2 Audio Log The New Voice Actress");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogTheProjectionistOnInteracted")]
        public static void ProjectionistLog()
        {
            Client.SendLocation("CH2 Audio Log The Projectionist");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogLostKeyOnInteracted")]
        public static void LostKeyLog()
        {
            Client.SendLocation("CH2 Audio Log Lost Key");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogFavoriteSongOnInteracted")]
        public static void FavoriteSongLog()
        {
            Client.SendLocation("CH2 Audio Log Favorite Song");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogJackFainOnInteracted")]
        public static void JackFainLog()
        {
            Client.SendLocation("CH2 Audio Log Jack Fain");
        }
    }
}
