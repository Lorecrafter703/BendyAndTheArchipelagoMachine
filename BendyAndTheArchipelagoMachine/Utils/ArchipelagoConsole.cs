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

        public static Rect windowRect = new Rect(Screen.width * 0.3f, 0, Screen.width * 0.4f, Screen.height * 0.3f);
        private static Rect textPos = new Rect(4, 20, Screen.width * 0.4f, Screen.height * 0.3f);
        private static GUIStyle textStyle = new GUIStyle();
        private static int fontSize = (int)(Screen.height * 0.0165f);

        private static List<string> logLines = new List<string>();
        private static int wrapCount = 0;
        private static string logText = "";
        private static int MaxLogLines = 14;
        private static int minWrapLength = 84;


        public static void Awake()
        {
            Hidden = false;

            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = Color.white;
            textStyle.wordWrap = true;
        }
        

        public static void OnGUI()
        {
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

            logLines.Add(message);
            BendyAndTheArchipelagoMachine.Logger.LogWarning($"logLines size: {logLines.Count} | wrapCount: {wrapCount}");
            BendyAndTheArchipelagoMachine.Logger.LogMessage(message);

            int msgLen = message.Length;
            int index = 1;
            while (msgLen > minWrapLength)
            {
                wrapCount++;
                BendyAndTheArchipelagoMachine.Logger.LogWarning($"wrapCount: {wrapCount}");
                index++;
                msgLen -= minWrapLength;
            }

            while (logLines.Count + wrapCount >= MaxLogLines)
            {
                if (logLines[0].Length > minWrapLength)
                {
                    int wrapAmount = (logLines[0].Length - 1) / minWrapLength;
                    BendyAndTheArchipelagoMachine.Logger.LogWarning($"wrapAmount: {wrapAmount}");
                    wrapCount -= wrapAmount;
                    BendyAndTheArchipelagoMachine.Logger.LogWarning($"wrapCount: {wrapCount}");
                }
                logLines.RemoveAt(0);
                BendyAndTheArchipelagoMachine.Logger.LogWarning($"logLines size: {logLines.Count} | wrapCount: {wrapCount}");
            }
        }


        public static void ConsoleGUI(int windowID)
        {
            logText = "";
            foreach (string line in logLines) logText += line + "\n";

            GUI.Label(textPos, logText, textStyle);
            GUI.DragWindow();
        }
    }
}
