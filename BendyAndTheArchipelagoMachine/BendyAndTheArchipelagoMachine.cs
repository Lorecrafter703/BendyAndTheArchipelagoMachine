using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class BendyAndTheArchipelagoMachine : BaseUnityPlugin
    {
        public const string pluginGuid = "lorecrafter.bendyandtheinkmachine.archipelago";
        public const string pluginName = "Bendy and the Archipelago Machine";
        public const string pluginVersion = "1.0.0.0";

        internal static new ManualLogSource Logger;

        public void Awake()
        {
            Logger = base.Logger;
            Logger.LogMessage("Hello World");

            Harmony harmony = new Harmony(pluginGuid);
        }
    }
}
