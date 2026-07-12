using BendyAndTheArchipelagoMachine.Archipelago;
using BendyAndTheArchipelagoMachine.Data;
using BendyAndTheArchipelagoMachine.Utils;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using InControl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMG.GamepadControl;
using UnityEngine;
using UnityEngine.Windows;
using XInputDotNetPure;

namespace BendyAndTheArchipelagoMachine
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class BendyAndTheArchipelagoMachine : BaseUnityPlugin
    {
        public const string pluginGuid = "lorecrafter.bendyandtheinkmachine.archipelago";
        public const string pluginName = "Bendy and the Archipelago Machine";
        public const string pluginVersion = "0.1.0";

        public const string ModDisplayInfo = pluginName + " v" + pluginVersion;
        private const string APDisplayInfo = "Archipelago v" + Client.AP_VERSION;
        public static new ManualLogSource Logger;
        public static Client ArchipelagoClient;

        public void Awake()
        {
            Logger = base.Logger;
            ArchipelagoClient = new Client();
            ArchipelagoConsole.Awake();

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();

            ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
        }


        private void OnGUI()
        {
            // show the mod is currently loaded in the corner
            GUI.Label(new Rect(16, 16, 300, 20), ModDisplayInfo);
            ArchipelagoConsole.OnGUI();

            string statusMessage;
            // show the Archipelago Version and whether we're connected or not
            if (Client.authenticated)
            {
                // if your game doesn't usually show the cursor this line may be necessary
                // Cursor.visible = false;

                statusMessage = " Status: Connected";
                GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
            }
            else
            {
                // if your game doesn't usually show the cursor this line may be necessary
                // Cursor.visible = true;

                statusMessage = " Status: Disconnected";
                GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
                GUI.Label(new Rect(16, 70, 150, 20), "Host: ");
                GUI.Label(new Rect(16, 90, 150, 20), "Player Name: ");
                GUI.Label(new Rect(16, 110, 150, 20), "Password: ");

                Client.serverData.Uri = GUI.TextField(new Rect(150, 70, 150, 20),
                    Client.serverData.Uri);
                Client.serverData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
                    Client.serverData.SlotName);
                Client.serverData.Password = GUI.TextField(new Rect(150, 110, 150, 20),
                    Client.serverData.Password);

                // requires that the player at least puts *something* in the slot name
                if (GUI.Button(new Rect(16, 130, 100, 20), "Connect") &&
                    !Client.serverData.SlotName.IsNullOrWhiteSpace())
                {
                    ArchipelagoClient.Connect();
                }
            }
            // this is a good place to create and add a bunch of debug buttons
        }
    }
}
