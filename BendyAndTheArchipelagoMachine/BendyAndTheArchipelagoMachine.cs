using BendyAndTheArchipelagoMachine.Archipelago;
using BendyAndTheArchipelagoMachine.Patches;
using BendyAndTheArchipelagoMachine.Utils;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Configuration;
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
using TMG.Controls;
using TMG.GamepadControl;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Windows;
using XInputDotNetPure;
using System.Runtime.InteropServices;

/*
    ArchipelagoData.cs, Client.cs, DeathLinkHandler.cs, and the OnGui
    function of this file are all taken from alwaysintereble's templates
    repo: https://github.com/alwaysintreble/ArchipelagoBepInExPluginTemplate
*/


namespace BendyAndTheArchipelagoMachine
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class Plugin : BasePlugin
    {
        public const string pluginGuid = "lorecrafter.bendyandtheinkmachine.archipelago";
        public const string pluginName = "Bendy and the Archipelago Machine";
        public const string pluginVersion = "1.3.0";

        public const string ModDisplayInfo = pluginName + " v" + pluginVersion;
        public static ManualLogSource Logger;


        public override void Load()
        {
            Logger = base.Log;
            ArchipelagoConsole.Awake();
            CheckpointMenu.Awake();

            AddComponent<BendyAndTheArchipelagoMachine>();

            Harmony harmony = new Harmony(pluginGuid);
            harmony.PatchAll();

            ArchipelagoConsole.LogMessage($"{ModDisplayInfo} loaded!");
        }

        
    }


    public class BendyAndTheArchipelagoMachine : MonoBehaviour
    {
        private const string APDisplayInfo = "Archipelago v" + Client.AP_VERSION;
        public static Client ArchipelagoClient;

        public static ManualLogSource Logger = Plugin.Logger;


        public void Awake()
        {
            ArchipelagoClient = new Client();

            Logger.LogMessage("THE PLUGIN IS WORKING");
        }

        void Update()
        {
            ArchipelagoClient.ProcessItems();
            ArchipelagoConsole.ProcessMessages();
            if (GetInputArchipelagoConsole())
            {
                ArchipelagoConsole.ToggleHidden();
            }
            ArchipelagoClient.deathLinkHandler?.ProcessDeaths();
        }


        static bool GetInputArchipelagoConsole()
        {
            bool gamepad = GamepadInput.GetButtonDown(InputControlType.Menu);
            bool keyboard = UnityEngine.Input.GetKeyDown(KeyCode.BackQuote);
            if (gamepad) return gamepad;
            return keyboard && keyboard;
        }


        private void OnGUI()
        {
            // show the mod is currently loaded in the corner
            GUI.Label(new Rect(16, 16, 300, 20), Plugin.ModDisplayInfo);
            GUI.Label(new Rect(16, 36, 300, 20), "Press ` or 'Menu' to Show/Hide Console");
            ArchipelagoConsole.OnGUI();
            CheckpointMenu.OnGUI();

            string statusMessage;
            // show the Archipelago Version and whether we're connected or not
            if (Client.authenticated)
            {
                statusMessage = " Status: Connected";
                GUI.Label(new Rect(16, 56, 300, 20), APDisplayInfo + statusMessage);
                if (Client.NeedBaconSoup) GUI.Label(new Rect(16, 76, 300, 20), Client.BaconSoupCount());
                bool deathLinkStatus = ArchipelagoClient.deathLinkHandler.GetDeathLinkStatus();

                GUIStyle btnStyle = GUI.skin.button;
                btnStyle.alignment = TextAnchor.MiddleCenter;
                if (GUI.Button(new Rect(16, 101, deathLinkStatus ? 117 : 120, 25), $"Deathlink {(deathLinkStatus ? "enabled" : "disabled")}", btnStyle))
                {
                    ArchipelagoClient.deathLinkHandler.ToggleDeathLink();
                }
            }
            else
            {
                statusMessage = " Status: Disconnected";
                GUI.Label(new Rect(16, 56, 300, 20), APDisplayInfo + statusMessage);
                GUI.Label(new Rect(16, 76, 150, 20), "Host: ");
                GUI.Label(new Rect(16, 96, 150, 20), "Player Name: ");
                GUI.Label(new Rect(16, 116, 150, 20), "Password: ");

                Client.serverData.Uri = GUI.TextField(new Rect(150, 76, 150, 20),
                    Client.serverData.Uri);
                Client.serverData.SlotName = GUI.TextField(new Rect(150, 96, 150, 20),
                    Client.serverData.SlotName);
                Client.serverData.Password = GUI.TextField(new Rect(150, 116, 150, 20),
                    Client.serverData.Password);

                if (GUI.Button(new Rect(16, 136, 100, 20), "Connect") &&
                !Client.serverData.SlotName.IsNullOrWhiteSpace())
                {
                    ArchipelagoClient.Connect();
                }
            }
        }
    }
}
