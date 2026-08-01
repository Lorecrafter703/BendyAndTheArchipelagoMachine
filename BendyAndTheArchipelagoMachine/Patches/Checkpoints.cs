using BendyAndTheArchipelagoMachine.Archipelago;
using BendyAndTheArchipelagoMachine.Utils;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch]
    internal class Checkpoints
    {
        public static void AddCheckpointItem(string checkpoint)
        {
            if (Client.HasItem(checkpoint)) return;
            Client.serverData.AddItem(IDTables.GetItemID(checkpoint));
            ArchipelagoConsole.LogMessage($"Received {checkpoint}");
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH1BendyFinaleController), "Complete")]
        public static void BasementCheckpoint()
        {
            string checkpoint = "CH1 Checkpoint Basement";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2LostKeysController), "HandleLostKeysObjectiveOnActive")]
        public static void LostKeysCheckpoint()
        {
            string checkpoint = "CH2 Checkpoint Lost Keys";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH2SammyOfficeController), "HandleLeverOnInteracted")]
        public static void SammysOfficeCheckpoint()
        {
            string checkpoint = "CH2 Checkpoint Sammy's Office";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AliceRevealController), "HandleRevealDialogueOnComplete")]
        public static void DecisionsCheckpoint()
        {
            string checkpoint = "CH3 Checkpoint Decisions";
            AddCheckpointItem(checkpoint);
        }
        //[HarmonyPatch(typeof(CH3HeavenlyToysController), "HandleBlockingTheWayOnComplete")]


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3AliceLairController), "HandleBodyRoomTriggerOnEnter")]
        public static void AngelsBiddingCheckpoint()
        {
            string checkpoint = "CH3 Checkpoint Angel's Bidding";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH3CutoutTaskController), "BreakAllCutouts")]
        public static void ButcherGangCheckpoint()
        {
            string checkpoint = "CH3 Checkpoint Butcher Gang";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4WarehouseController), "HandleEntranceTriggerOnEnter")]
        public static void WarehouseCheckpoint()
        {
            string checkpoint = "CH4 Checkpoint Warehouse";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH4HauntedHouseController), "HandlePowerLeverOnComplete")]
        public static void HauntedHouseCheckpoint()
        {
            string checkpoint = "CH4 Checkpoint Haunted House";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5LostHarbour), "Complete")]
        public static void AdministrationCheckpoint()
        {
            string checkpoint = "CH5 Checkpoint Administration";
            AddCheckpointItem(checkpoint);
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(CH5GiantInkMachine), "HandleThroneRoomDoorOnOpened")]
        public static void TheInkMachineCheckpoint()
        {
            string checkpoint = "CH5 Checkpoint The Ink Machine";
            AddCheckpointItem(checkpoint);
        }
    }
}
