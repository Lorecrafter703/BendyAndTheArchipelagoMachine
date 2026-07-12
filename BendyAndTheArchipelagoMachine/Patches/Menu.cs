using BendyAndTheArchipelagoMachine.Archipelago;
using BendyAndTheArchipelagoMachine.Utils;
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


        [HarmonyPrefix]
        [HarmonyPatch(typeof(BaseUIButton), "OnPointerClick")]
        public static bool HandleConnect(BaseUIButton __instance)
        {
            if (__instance.name != "NewGameBtn") return true;
            if (!Client.authenticated)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError("Please connect to a server before entering the game");
                ArchipelagoConsole.LogMessage("Please connect to a server before entering the game");
            }
            else
            {
                LoadChapterFromTitle(titleScreenController, "Archives");
            }
            return false;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(TitleScreenController), "SelectSlot")]
        public static void OnSlotSelect(int index)
        {
            BendyAndTheArchipelagoMachine.Logger.LogMessage($"Selected slot {index}");
            SaveFileData data = new SaveFileData(index);
            data.IsNewGamePlus = true;
            data.CH1Data = new CH1DataVO();
            data.CH2Data = new CH2DataVO();
            data.CH3Data = new CH3DataVO();
            data.CH4Data = new CH4DataVO();
            data.CH5Data = new CH5DataVO();
            GameManager.Instance.GameData.SaveFiles[index] = data;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreenController), "InitController")]
        public static void GetTitleScreenController(TitleScreenController __instance, List<MenuItemButton> ___m_BeginMenuItemButtons)
        {
            titleScreenController = __instance;
            BendyAndTheArchipelagoMachine.Logger.LogMessage("Begin Menu Item Buttons:");
            foreach (var button in ___m_BeginMenuItemButtons)
            {
                BendyAndTheArchipelagoMachine.Logger.LogMessage(button.name);
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(TitleScreenController), "OnDisposed")]
        public static void ClearTitleScreenRef()
        {
            titleScreenController = null;
        }


        [HarmonyPrefix]
        [HarmonyPatch(typeof(TitleScreenController), "ShowBeginMenu")]
        public static void ModifyButtons(TitleScreenController __instance, ref List<MenuItemButton> ___m_BeginMenuItemButtons)
        {
            List<MenuItemButton> removeQueue = new List<MenuItemButton>();
            foreach (var button in ___m_BeginMenuItemButtons)
            {
                if (button.name != "NewGameBtn")
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


        [HarmonyReversePatch(HarmonyReversePatchType.Original)]
        [HarmonyPatch(typeof(TitleScreenController), "LaunchChapter")]
        public static void LoadChapterFromTitle(TitleScreenController instance, string chapterName) => throw (new NotImplementedException());


        /*
            Attemtping to load the archives when hitting the quit button 
        */
        //[HarmonyReversePatch(HarmonyReversePatchType.Original)]
        //[HarmonyPatch(typeof(BaseUIController), "Kill")]
        //public static void QuitBaseKill(BaseUIController instance) => throw (new NotImplementedException());


        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(GameMenuController), "Quit")]
        //public static bool QuitToArchivesHub(GameMenuController __instance, CanvasGroup ___m_BlackoutCanvas)
        //{
        //    GameManager.Instance.GameCamera.Camera.enabled = false;
        //    ___m_BlackoutCanvas.alpha = 1f;
        //    GameManager.Instance.ShowScreenBlocker(0f, 0f, null);
        //    GameManager.Instance.UIManager.Camera.enabled = false;
        //    MyChapterController.MyLoadChapter(MyChapterController.currentChapter, "Archives");
        //    QuitBaseKill(__instance);
        //    return false;
        //}
    }
}
