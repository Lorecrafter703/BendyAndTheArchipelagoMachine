using BendyAndTheArchipelagoMachine.Archipelago;
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
    [HarmonyPatch]
    internal class AudioLogs
    {
        [HarmonyPostfix]
        [HarmonyPatch((typeof(CH1AudioLogsController)),"HandleAudioLogThomas01OnInteracted")]
        public static void DarkAndColdLog()
        {
            Client.SendLocation("CH1 Audio Log Dark and Cold");
        }


        [HarmonyPostfix]
        [HarmonyPatch((typeof(CH1AudioLogsController)), "HandleAudioLogWally01OnInteracted")]
        public static void ThisMachineLog()
        {
            Client.SendLocation("CH1 Audio Log This Machine");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogThePrayerOnInteracted")]
        public static void CanIGetAnAmenLog()
        {
            Client.SendLocation("CH2 Audio Log Can I Get an Amen?");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogDistractionsOnInteracted")]
        public static void ThePumpSwitchLog()
        {
            Client.SendLocation("CH2 Audio Log The Pump Switch");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogTheNewVoiceAcressOnInteracted")]
        public static void NewActressLog()
        {
            Client.SendLocation("CH2 Audio Log New Actress");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogTheProjectionistOnInteracted")]
        public static void CrazySammyLog()
        {
            Client.SendLocation("CH2 Audio Log Crazy Sammy");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogLostKeyOnInteracted")]
        public static void StupidKeysLog()
        {
            Client.SendLocation("CH2 Audio Log Stupid Keys");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogFavoriteSongOnInteracted")]
        public static void SanctuaryPuzzleLog()
        {
            Client.SendLocation("CH2 Audio Log Sanctuary Puzzle");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogJackFainOnInteracted")]
        public static void QuietAndSmellySewersLog()
        {
            Client.SendLocation("CH2 Audio Log Quiet and Smelly Sewers");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogGrantGeniusOnInteracted")]
        public static void TheGeniusUpstairsLog()
        {
            Client.SendLocation("CH3 Audio Log The Genius Upstairs");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogHenryOnInteracted")]
        public static void ManOfIdeasLog()
        {
            Client.SendLocation("CH3 Audio Log Man of Ideas");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogJoeyDrewBeliefOnInteracted")]
        public static void TimeToBelieveLog()
        {
            Client.SendLocation("CH3 Audio Log Time to Believe");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogNormanTroubleOnInteracted")]
        public static void LookingForTroubleLog()
        {
            Client.SendLocation("CH3 Audio Log Looking for Trouble");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogShawnCrookedOnInteracted")]
        public static void CrookedSmileLog()
        {
            Client.SendLocation("CH3 Audio Log Crooked Smile");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogSusieApartOnInteracted")]
        public static void EverythingIsComingApartLog()
        {
            Client.SendLocation("CH3 Audio Log Everything is Coming Apart");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogSusieLunchOnInteracted")]
        public static void LunchWithJoeyLog()
        {
            Client.SendLocation("CH3 Audio Log Lunch with Joey");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogThomasOnInteracted")]
        public static void CuttingCornersLog()
        {
            Client.SendLocation("CH3 Audio Log Cutting Corners");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogWallySmileOnInteracted")]
        public static void CrackASmileLog()
        {
            Client.SendLocation("CH3 Audio Log Crack a Smile");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogWallyThomasOnInteracted")]
        public static void InkPressureLog()
        {
            Client.SendLocation("CH3 Audio Log Ink Pressure");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogBertTransformOnInteracted")]
        public static void ColossalWondersLog()
        {
            Client.SendLocation("CH4 Audio Log Colossal Wonders");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogGrantTransformOnInteracted")]
        public static void IndiscernableLog()
        {
            Client.SendLocation("CH4 Audio Log Indiscernable");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogJoeyTransformOnInteracted")]
        public static void TurnItOffLog()
        {
            Client.SendLocation("CH4 Audio Log Turn It Off");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogLacieTransformOnInteracted")]
        public static void MechanicalDemonLog()
        {
            Client.SendLocation("CH4 Audio Log Mechanical Demon");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogSusieTransformOnInteracted")]
        public static void BehindClosedDoorsLog()
        {
            Client.SendLocation("CH4 Audio Log Behind Closed Doors");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogWallyTransformOnInteracted")]
        public static void PlayingGamesLog()
        {
            Client.SendLocation("CH4 Audio Log Playing Games");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4BertrumController), "HandleAudioLogOnInteracted")]
        public static void BertrumsRevealLog()
        {
            Client.SendLocation("CH4 Audio Log Bertrum's Reveal");
        }
    }
}
