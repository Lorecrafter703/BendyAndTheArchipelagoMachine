using BendyAndTheArchipelagoMachine.Archipelago;
using BendyAndTheArchipelagoMachine.Utils;
using BepInEx;
using DG.Tweening;
using HarmonyLib;
using I2.Loc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TMG.Data;
using TMG.UI;
using TMG.UI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch]
    internal class Menu
    {
        public static TitleScreenController titleScreenController;


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreenController), "InitController")]
        public static void GetTitleScreenController(TitleScreenController __instance, List<MenuItemButton> ___m_BeginMenuItemButtons)
        {
            titleScreenController = __instance;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(BaseUIButton), "OnPointerClick")]
        public static bool HandleConnect(BaseUIButton __instance)
        {
            if (__instance.name != "BeginBtn") return true;
            if (!Client.authenticated && !Client.serverData.SlotName.IsNullOrWhiteSpace())
            {
                BendyAndTheArchipelagoMachine.ArchipelagoClient.Connect();
            }
            return Client.authenticated;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(TitleScreenController), "SelectSlot")]
        public static bool OnSlotSelect(int index)
        {
            if (!Client.serverData.VerifySlot(index))
            {
                ArchipelagoConsole.LogMessage($"Please Connect to Slot {Client.serverData.GetSlot()}");
                return false;
            }
            int slot = Client.serverData.GetSlot();
            SaveFileData data = new SaveFileData(slot);
            data.IsNewGamePlus = true;
            data.HasDied = false;
            data.PlayTime = 0f;
            data.CH1Data = new CH1DataVO();
            data.CH2Data = new CH2DataVO();
            data.CH3Data = new CH3DataVO();
            data.CH4Data = new CH4DataVO();
            data.CH5Data = new CH5DataVO();

            GameManager.Instance.GameData.SaveFiles[slot] = data;

            return true;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(TitleScreenController), "ShowBeginMenu")]
        public static void ModifyButtons(TitleScreenController __instance, ref List<MenuItemButton> ___m_BeginMenuItemButtons)
        {
            List<MenuItemButton> removeQueue = new List<MenuItemButton>();
            foreach (var button in ___m_BeginMenuItemButtons)
            {
                if (button.name != "ChaptersBtn")
                {
                    button.gameObject.SetActive(false);
                    removeQueue.Add(button);
                }
            }
            foreach (var button in removeQueue)
            {
                if (___m_BeginMenuItemButtons.Contains(button))
                {
                    ___m_BeginMenuItemButtons.Remove(button);
                }
            }

        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(TitleScreenController), "CheckSelectedBeginMenu")]
        public static void GoToChapterSelect(TitleScreenController __instance, ref int ___m_SelectedIndex)
        {
            ___m_SelectedIndex = 2;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreenController), "CheckSelectedBeginMenu")]
        public static void ShowCheckpointButtons()
        {
            CheckpointMenu.selectedChapter = 0;
            CheckpointMenu.hidden = false;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreenController), "HandleChapterArrowOnLeft")]
        public static void LeftArrowClick(TitleScreenController __instance, int ___m_SelectedChapter)
        {
            CheckpointMenu.selectedChapter = ___m_SelectedChapter;
            CheckpointMenu.selectedCheckpoint = 0;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreenController), "HandleChapterArrowOnRight")]
        public static void RightArrowClick(TitleScreenController __instance, int ___m_SelectedChapter)
        {
            CheckpointMenu.selectedChapter = ___m_SelectedChapter;
            CheckpointMenu.selectedCheckpoint = 0;
        }


        public static bool HasCheckpoint(int checkpoint)
        {
            switch (checkpoint)
            {
                case 0:
                    return Client.HasItem("Unlock CH1");
                case 1:
                    return Client.HasItem("CH1 Checkpoint Basement");
                case 5:
                    return Client.HasItem("Unlock CH2");
                case 6:
                    return Client.HasItem("CH2 Checkpoint Lost Keys");
                case 7:
                    return Client.HasItem("CH2 Checkpoint Sammy's Sanctuary");
                case 10:
                    return Client.HasItem("Unlock CH3");
                case 11:
                    return Client.HasItem("CH3 Checkpoint Decisions");
                case 12:
                    return Client.HasItem("CH3 Checkpoint Angel's Bidding");
                case 13:
                    return Client.HasItem("CH3 Checkpoint Ink Hearts");
                case 15:
                    return Client.HasItem("Unlock CH4");
                case 16:
                    return Client.HasItem("CH4 Checkpoint Warehouse");
                case 17:
                    return Client.HasItem("CH4 Checkpoint Haunted House");
                case 20:
                    var BaconSoupsRequiredOption = (long)Client.serverData.GetSlotDataOption("bacon_soups_required");
                    var TotalBaconSoupsOption = (long)Client.serverData.GetSlotDataOption("total_bacon_soups");
                    long BaconSoupsRequired = TotalBaconSoupsOption * BaconSoupsRequiredOption / 100;
                    return Client.BaconSoupCount() >= BaconSoupsRequired;
                case 21:
                    return Client.HasItem("CH5 Checkpoint Administration");
                case 22:
                    return Client.HasItem("CH5 Checkpoint The Ink Machine");
                case 25:
                    return true;
                default:
                    BendyAndTheArchipelagoMachine.Logger.LogError($"Unknown Checkpoint: {checkpoint}");
                    return false;
            }
        }


        public static void ResetChapterData(int checkpoint)
        {
            int slot = Client.serverData.GetSlot();
            SaveFileData data = new SaveFileData(slot);
            data.IsNewGamePlus = true;
            data.HasDied = false;
            data.PlayTime = 0f;
            data.CH1Data = new CH1DataVO();
            data.CH2Data = new CH2DataVO();
            data.CH3Data = new CH3DataVO();
            data.CH4Data = new CH4DataVO();
            data.CH5Data = new CH5DataVO();

            

            GameManager.Instance.GameData.SaveFiles[slot] = data;
            GameManager.Instance.GameData.CurrentSaveFile = data;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(TitleScreenController), "CheckSelectedChapter")]
        public static bool HandleChapterSelect(TitleScreenController __instance, int ___m_SelectedChapter)
        {
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Selected button: {CheckpointMenu.selectedCheckpoint}");
            int checkpoint = (___m_SelectedChapter * 5) + CheckpointMenu.selectedCheckpoint;
            switch (___m_SelectedChapter)
            {
                case 0:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    ResetChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH1");
                    break;
                case 1:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    ResetChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH2");
                    break;
                case 2:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    ResetChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH3");
                    break;
                case 3:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    ResetChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH4");
                    break;
                case 4:
                    var BaconSoupsRequiredOption = (long)Client.serverData.GetSlotDataOption("bacon_soups_required");
                    var TotalBaconSoupsOption = (long)Client.serverData.GetSlotDataOption("total_bacon_soups");
                    long BaconSoupsRequired = TotalBaconSoupsOption * BaconSoupsRequiredOption / 100;
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked: {Client.BaconSoupCount()} / {BaconSoupsRequired}");
                        break;
                    }
                    ResetChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH5");
                    break;
                case 5:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    ResetChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "Archives");
                    break;
                default:
                    BendyAndTheArchipelagoMachine.Logger.LogError($"Unrecognized chapter: {___m_SelectedChapter}");
                    break;
            }
            return false;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreenController), "OnDisposed")]
        public static void ClearTitleScreenRef()
        {
            titleScreenController = null;
        }


        [HarmonyReversePatch(HarmonyReversePatchType.Original)]
        [HarmonyPatch(typeof(TitleScreenController), "LaunchChapter")]
        public static void LoadChapterFromTitle(TitleScreenController instance, string chapterName) => throw (new NotImplementedException());
    }
}
