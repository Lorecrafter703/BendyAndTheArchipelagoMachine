using BendyAndTheArchipelagoMachine.Archipelago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Utils
{
    public static class CheckpointMenu
    {
        private static float checkpointsWidth;
        private static float checkpointsHeight;
        private static float xPos;
        private static float yPos;

        private static Rect position;
        private static Dictionary<int, List<string>> CheckpointNames = new Dictionary<int, List<string>>()
        {
            { 0, new List<string> { "Start", "Basement" } },
            { 1, new List<string> { "Start", "Lost Keys", "Sammy's Sanctuary" } },
            { 2, new List<string> { "Start", "Decisions", "Angel's Bidding", "Ink Hearts" } },
            { 3, new List<string> { "Start", "Warehouse", "Haunted House" } },
            { 4, new List<string> { "Start", "Administration", "The Ink Machine" } },
            { 5, new List<string> { "Start" } }
        };

        public static bool hidden;
        public static int selectedChapter;
        public static int selectedCheckpoint;


        public static void Awake()
        {
            hidden = true;
            selectedChapter = 0;
            selectedCheckpoint = 0;
        }


        public static void OnGUI()
        {
            if (hidden) return;

            string[] checkpointOptions = GetAvailableCheckpoints();

            xPos = Screen.width * 0.70f;
            yPos = Screen.height * 0.2f;
            checkpointsWidth = Screen.width * 0.15f;
            checkpointsHeight = Screen.height * 0.03f * checkpointOptions.Length;

            position = new Rect(xPos, yPos, checkpointsWidth, checkpointsHeight);

            selectedCheckpoint = GUI.SelectionGrid(position, selectedCheckpoint, checkpointOptions, 1);
        }


        private static string[] GetAvailableCheckpoints()
        {
            string[] output = new string[CheckpointNames[selectedChapter].Count];

            switch (selectedChapter)
            {
                case 0:
                    output[0] = Client.HasItem("Unlock CH1") ? "Start" : "Locked";
                    output[1] = Client.HasItem("CH1 Checkpoint Basement") ? "Basement" : "Locked";
                    break;
                case 1:
                    output[0] = Client.HasItem("Unlock CH2") ? "Start" : "Locked";
                    output[1] = Client.HasItem("CH2 Checkpoint Lost Keys") ? "Lost Keys" : "Locked";
                    output[2] = Client.HasItem("CH2 Checkpoint Sammy's Office") ? "Sammy's Office" : "Locked";
                    break;
                case 2:
                    output[0] = Client.HasItem("Unlock CH3") ? "Start" : "Locked";
                    output[1] = Client.HasItem("CH3 Checkpoint Decisions") ? "Decisions" : "Locked";
                    output[2] = Client.HasItem("CH3 Checkpoint Angel's Bidding") ? "Angel's Bidding" : "Locked";
                    output[3] = Client.HasItem("CH3 Checkpoint Butcher Gang") ? "Butcher Gang" : "Locked";
                    break;
                case 3:
                    output[0] = Client.HasItem("Unlock CH4") ? "Start" : "Locked";
                    output[1] = Client.HasItem("CH4 Checkpoint Warehouse") ? "Warehouse" : "Locked";
                    output[2] = Client.HasItem("CH4 Checkpoint Haunted House") ? "Haunted House" : "Locked";
                    break;
                case 4:
                    output[0] = Client.HasItem("Unlock CH5") ? "Start" : "Locked";
                    output[1] = Client.HasItem("CH5 Checkpoint Administration") ? "Administration" : "Locked";
                    output[2] = Client.HasItem("CH5 Checkpoint The Ink Machine") ? "The Ink Machine" : "Locked";
                    break;
                default:
                    output[0] = "Start";
                    break;
            }

            return output;
        }
    }
}
