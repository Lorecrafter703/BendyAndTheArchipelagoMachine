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
            Client.SendLocation("CH1 Audio Log - Thomas Connor (Flooded Basement)");
        }


        [HarmonyPostfix]
        [HarmonyPatch((typeof(CH1AudioLogsController)), "HandleAudioLogWally01OnInteracted")]
        public static void ThisMachineLog()
        {
            Client.SendLocation("CH1 Audio Log - Wally Franks (Hallway Table)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogThePrayerOnInteracted")]
        public static void CanIGetAnAmenLog()
        {
            Client.SendLocation("CH2 Audio Log - Sammy Lawrence (Can I Get An Amen?)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogDistractionsOnInteracted")]
        public static void ThePumpSwitchLog()
        {
            Client.SendLocation("CH2 Audio Log - Sammy Lawrence (Music Department Lobby)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogTheNewVoiceAcressOnInteracted")]
        public static void NewActressLog()
        {
            Client.SendLocation("CH2 Audio Log - Susie Campbell (Recording Studio)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogTheProjectionistOnInteracted")]
        public static void CrazySammyLog()
        {
            Client.SendLocation("CH2 Audio Log - Norman Polk (Projector)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogLostKeyOnInteracted")]
        public static void StupidKeysLog()
        {
            Client.SendLocation("CH2 Audio Log - Wally Franks (Sammy’s Office)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogFavoriteSongOnInteracted")]
        public static void SanctuaryPuzzleLog()
        {
            Client.SendLocation("CH2 Audio Log - Sammy Lawrence (Wally’s Closet)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2AudioLogsController), "HandleAudioLogJackFainOnInteracted")]
        public static void QuietAndSmellySewersLog()
        {
            Client.SendLocation("CH2 Audio Log - Jack Fain (Desk in Sewers)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogGrantGeniusOnInteracted")]
        public static void TheGeniusUpstairsLog()
        {
            Client.SendLocation("CH3 Audio Log - Grant Cohen (Accounting Office on Level 9)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogHenryOnInteracted")]
        public static void ManOfIdeasLog()
        {
            Client.SendLocation("CH3 Audio Log - Henry (Sunken Room)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogJoeyDrewBeliefOnInteracted")]
        public static void TimeToBelieveLog()
        {
            Client.SendLocation("CH3 Audio Log - Joey Drew (Path of the Demon)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogNormanTroubleOnInteracted")]
        public static void LookingForTroubleLog()
        {
            Client.SendLocation("CH3 Audio Log - Norman Polk (Projectionist’s Maze)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogShawnCrookedOnInteracted")]
        public static void CrookedSmileLog()
        {
            Client.SendLocation("CH3 Audio Log - Shawn Flynn (Toy Machine Room)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogSusieApartOnInteracted")]
        public static void EverythingIsComingApartLog()
        {
            Client.SendLocation("CH3 Audio Log - Susie Campbell (Path of the Angel)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogSusieLunchOnInteracted")]
        public static void LunchWithJoeyLog()
        {
            Client.SendLocation("CH3 Audio Log - Susie Campbell (Flooded Morgue)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogThomasOnInteracted")]
        public static void CuttingCornersLog()
        {
            Client.SendLocation("CH3 Audio Log - Thomas Connor (Level 9)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogWallySmileOnInteracted")]
        public static void CrackASmileLog()
        {
            Client.SendLocation("CH3 Audio Log - Wally Franks (Level 11)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AudioLogController), "HandleAudioLogWallyThomasOnInteracted")]
        public static void InkPressureLog()
        {
            Client.SendLocation("CH3 Audio Log - Wally Franks and Thomas Connor (Level K Power Hallway)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogBertTransformOnInteracted")]
        public static void ColossalWondersLog()
        {
            Client.SendLocation("CH4 Audio Log - Bertrum Piedmont (Planning Room)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogGrantTransformOnInteracted")]
        public static void IndiscernableLog()
        {
            Client.SendLocation("CH4 Audio Log - ??? (Management Office)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogJoeyTransformOnInteracted")]
        public static void TurnItOffLog()
        {
            Client.SendLocation("CH4 Audio Log - Joey Drew (Maintenance Room)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogLacieTransformOnInteracted")]
        public static void MechanicalDemonLog()
        {
            Client.SendLocation("CH4 Audio Log - Lacie Benton (Research & Design)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogSusieTransformOnInteracted")]
        public static void BehindClosedDoorsLog()
        {
            Client.SendLocation("CH4 Audio Log - Susie Campbell (Library)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4AudioLogController), "HandleAudioLogWallyTransformOnInteracted")]
        public static void PlayingGamesLog()
        {
            Client.SendLocation("CH4 Audio Log - Wally Franks (Minigame Station)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4BertrumController), "HandleAudioLogOnInteracted")]
        public static void BertrumsRevealLog()
        {
            Client.SendLocation("CH4 Audio Log - Bertrum Piedmont (Attraction Storage)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5AudioLogController), "HandleAudioLogThomasTransformOnInteracted")]
        public static void OfficeReportLog()
        {
            Client.SendLocation("CH5 Audio Log - Thomas Connor (Film Vault)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5AudioLogController), "HandleAudioLogWallyTransformOnInteracted")]
        public static void ChocolateCakeLog()
        {
            Client.SendLocation("CH5 Audio Log - Wally Franks (Administration Maze)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5AudioLogController), "HandleAudioLogJoeyMemoTransformOnInteracted")]
        public static void TheBigPictureLog()
        {
            Client.SendLocation("CH5 Audio Log - Joey Drew (Administration Maze Entrance)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5AudioLogController), "HandleAudioLogJoeyTommyTransformOnInteracted")]
        public static void ThousandsOfSoulsLog()
        {
            Client.SendLocation("CH5 Audio Log - Joey Drew (Administration Maze Side Room)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5AudioLogController), "HandleAudioLogJoeySusieTransformOnInteracted")]
        public static void BringingAliceToLifeLog()
        {
            Client.SendLocation("CH5 Audio Log - Joey Drew (Joey’s Office)");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5ThroneRoom), "HandleAudioLogOnInteracted")]
        public static void BendysEndLog()
        {
            Client.SendLocation("CH5 Audio Log - Joey Drew (Bendy’s Throne)");
        }
    }
}
