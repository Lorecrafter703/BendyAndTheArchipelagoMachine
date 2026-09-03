using BendyAndTheArchipelagoMachine.Archipelago;
using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Utils
{
    public static class ArchipelagoConsole
    {
        public static bool Hidden;
        private static bool Awoken = false;

        private static float windowWidth;
        private static float windowHeight;
        private static float windowMarginW = Screen.width * 0.0025f;
        private static float windowMarginH = Screen.height * 0.0175f;
        public static Rect windowRect;
        private static Rect textPos;
        private static GUIStyle textStyle = new GUIStyle();
        private static int fontSize = (int)(Screen.height * 0.0165f);

        private static Queue<string> msgQueue = new Queue<string>();
        private static List<string> logLines = new List<string>();
        private static int wrapCount = 0;
        private static string logText = "";
        private static int MaxLogLines;
        private static int minWrapLength = 0;

        private static string CommandText = "!help";
        private static float btnWidth = Screen.width * 0.03f;
        private static float btnHeight = Screen.height * 0.02f;
        private static float btnMarginH = Screen.height * 0.005f;
        private static float btnMarginW = Screen.height * 0.005f;
        private static Rect CommandTextRect;
        private static Rect SendCommandButton;


        public static void Awake()
        {
            Hidden = true;

            windowWidth = Screen.width * Client.serverData.GetConsoleWindowWidth();
            windowHeight = Screen.height * Client.serverData.GetConsoleWindowHeight();
            windowRect = new Rect(Screen.width * 0.3f, 0, windowWidth, windowHeight);
            textPos = new Rect(windowMarginW, windowMarginH, windowWidth, windowHeight);

            CommandTextRect = new Rect(windowMarginW, windowHeight - btnHeight - btnMarginH, windowWidth - btnWidth - btnMarginW - windowMarginW, btnHeight);
            SendCommandButton = new Rect(windowWidth - btnWidth - windowMarginW, windowHeight - btnHeight - btnMarginH, btnWidth, btnHeight);

            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = Color.white;
            textStyle.wordWrap = true;

            MaxLogLines = (int)Math.Floor((windowHeight - windowMarginH - btnMarginH) / (textStyle.lineHeight));

            string lineWidth = "w";
            float height0 = textStyle.CalcHeight(new GUIContent(lineWidth), windowWidth);
            float height1 = 0;
            while (height1 <= height0)
            {
                lineWidth += "w";
                minWrapLength++;
                height1 = textStyle.CalcHeight(new GUIContent(lineWidth), windowWidth);
            }

            BendyAndTheArchipelagoMachine.Logger.LogDebug($"MaxLogLines: {MaxLogLines} | minWrapLength: {minWrapLength}");
            Awoken = true;
        }
        

        public static void OnGUI()
        {
            if (!Awoken) return;
            if (Hidden) return;
            windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)ConsoleGUI, $"ArchipelagoConsole - Press ` or 'Menu' to Show/Hide");
        }


        public static void ToggleHidden()
        {
            Hidden = !Hidden;
        }


        public static void LogMessage(string message)
        {
            if (message.IsNullOrWhiteSpace()) return;

            msgQueue.Enqueue(message);
        }


        public static void ProcessMessages()
        {
            if (!Awoken) return;

            if (msgQueue.Count <= 0) return;
            string message = msgQueue.Dequeue();

            logLines.Add(message);
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"logLines size: {logLines.Count} | wrapCount: {wrapCount}");
            BendyAndTheArchipelagoMachine.Logger.LogMessage(message);

            int msgLen = message.Length;
            int index = 1;
            while (msgLen > minWrapLength)
            {
                wrapCount++;
                BendyAndTheArchipelagoMachine.Logger.LogDebug($"wrapCount: {wrapCount}");
                index++;
                msgLen -= minWrapLength;
            }

            while (logLines.Count + wrapCount >= MaxLogLines)
            {
                if (logLines[0].Length > minWrapLength)
                {
                    int wrapAmount = (logLines[0].Length - 1) / minWrapLength;
                    BendyAndTheArchipelagoMachine.Logger.LogDebug($"wrapAmount: {wrapAmount}");
                    wrapCount -= wrapAmount;
                    BendyAndTheArchipelagoMachine.Logger.LogDebug($"wrapCount: {wrapCount}");
                }
                logLines.RemoveAt(0);
                BendyAndTheArchipelagoMachine.Logger.LogDebug($"logLines size: {logLines.Count} | wrapCount: {wrapCount}");
            }
        }


        public static void ConsoleGUI(int windowID)
        {
            logText = "";
            foreach (string line in logLines) logText += line + "\n";
            GUI.Label(textPos, logText, textStyle);

            CommandText = GUI.TextField(CommandTextRect, CommandText);
            if (GUI.Button(SendCommandButton, "Send") && !CommandText.IsNullOrWhiteSpace())
            {
                BendyAndTheArchipelagoMachine.ArchipelagoClient.SendMessage(CommandText);
                CommandText = "";
            }

            GUI.DragWindow();
        }
    }
}
