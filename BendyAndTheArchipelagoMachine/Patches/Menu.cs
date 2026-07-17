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


        public static void ResetChapterData()
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
            switch (___m_SelectedChapter)
            {
                case 0:
                    if (!Client.HasItem("Unlock CH1"))
                    {
                        ArchipelagoConsole.LogMessage($"Chapter 1 not yet unlocked");
                        break;
                    }
                    ResetChapterData();
                    LoadChapterFromTitle(titleScreenController, "CH1");
                    break;
                case 1:
                    if (!Client.HasItem("Unlock CH2"))
                    {
                        ArchipelagoConsole.LogMessage($"Chapter 2 not yet unlocked");
                        break;
                    }
                    ResetChapterData();
                    LoadChapterFromTitle(titleScreenController, "CH2");
                    break;
                case 2:
                    if (!Client.HasItem("Unlock CH3"))
                    {
                        ArchipelagoConsole.LogMessage($"Chapter 3 not yet unlocked");
                        break;
                    }
                    ResetChapterData();
                    LoadChapterFromTitle(titleScreenController, "CH3");
                    break;
                case 3:
                    if (!Client.HasItem("Unlock CH4"))
                    {
                        ArchipelagoConsole.LogMessage($"Chapter 4 not yet unlocked");
                        break;
                    }
                    ResetChapterData();
                    LoadChapterFromTitle(titleScreenController, "CH4");
                    break;
                case 4:
                    var BaconSoupsRequiredOption = (long)Client.serverData.GetSlotDataOption("bacon_soups_required");
                    var TotalBaconSoupsOption = (long)Client.serverData.GetSlotDataOption("total_bacon_soups");
                    long BaconSoupsRequired = TotalBaconSoupsOption * BaconSoupsRequiredOption / 100;
                    if (Client.BaconSoupCount() < BaconSoupsRequired)
                    {
                        ArchipelagoConsole.LogMessage($"Chapter 5 not yet unlocked: {Client.BaconSoupCount()} / {BaconSoupsRequired} Bacon Soups");
                        break;
                    }
                    ResetChapterData();
                    LoadChapterFromTitle(titleScreenController, "CH5");
                    break;
                case 5:
                    ResetChapterData();
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
