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
                    return Client.HasItem("CH2 Checkpoint Sammy's Office");
                case 10:
                    return Client.HasItem("Unlock CH3");
                case 11:
                    return Client.HasItem("CH3 Checkpoint Decisions");
                case 12:
                    return Client.HasItem("CH3 Checkpoint Angel's Bidding");
                case 13:
                    return Client.HasItem("CH3 Checkpoint Butcher Gang");
                case 15:
                    return Client.HasItem("Unlock CH4");
                case 16:
                    return Client.HasItem("CH4 Checkpoint Warehouse");
                case 17:
                    return Client.HasItem("CH4 Checkpoint Haunted House");
                case 20:
                    //var BaconSoupsRequiredOption = (long)Client.serverData.GetSlotDataOption("bacon_soups_required");
                    //var TotalBaconSoupsOption = (long)Client.serverData.GetSlotDataOption("total_bacon_soups");
                    //long BaconSoupsRequired = TotalBaconSoupsOption * BaconSoupsRequiredOption / 100;
                    //return Client.BaconSoupCount() >= BaconSoupsRequired;
                    return Client.HasItem("Unlock CH5");
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


        public static void PrepareChapterData(int checkpoint)
        {
            int slot = Client.serverData.GetSlot();

            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Loading checkpoint {checkpoint}");
            SaveFileData data = SetCheckpointFlags(slot, checkpoint);
            
            GameManager.Instance.GameData.SaveFiles[slot] = data;
            GameManager.Instance.GameData.CurrentSaveFile = data;
        }


        public static SaveFileData SetCheckpointFlags(int slot, int checkpoint)
        {
            SaveFileData data = new SaveFileData(slot);
            data.IsNewGamePlus = true;
            data.HasDied = false;
            data.PlayTime = 0f;
            data.CH1Data = new CH1DataVO();
            data.CH2Data = new CH2DataVO();
            data.CH3Data = new CH3DataVO();
            data.CH4Data = new CH4DataVO();
            data.CH5Data = new CH5DataVO();

            // CH1 Intro
            if (checkpoint < 1) return data;
            // CH1 Basement
            data.CH1Data.Book = SetObjectiveSaveData(true, true);
            data.CH1Data.Doll = SetObjectiveSaveData(true, true);
            data.CH1Data.Inkwell = SetObjectiveSaveData(true, true);
            data.CH1Data.Gear = SetObjectiveSaveData(true, true);
            data.CH1Data.Record = SetObjectiveSaveData(true, true);
            data.CH1Data.Wrench = SetObjectiveSaveData(true, true);
            data.CH1Data.InkMachineRevealObjective = SetObjectiveSaveData(true, true);
            data.CH1Data.CollectablesObjective = SetObjectiveSaveData(true, true);
            data.CH1Data.TheatreObjective = SetObjectiveSaveData(true, true);
            data.CH1Data.InkMachineObjective = SetObjectiveSaveData(true, true);
            data.CH1Data.BendyChaseObjective = SetObjectiveSaveData(true, true);
            data.CH1Data.BasementObjective = SetObjectiveSaveData(true, false);
            data.CH1Data.IsChapterComplete = false;
            data.CH1Data.PlayerPosition = new Vector3DataVO(new Vector3(20, -57.045f, 10));
            data.CH1Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 180, 0));
            data.CH1Data.HasSaveData = true;
            if (checkpoint < 5) return data;
            // CH2 Intro
            data.CH1Data.BasementObjective.IsComplete = true;
            data.CH1Data.IsChapterComplete = true;
            if (checkpoint < 6) return data;
            // CH2 Lost Keys
            data.CH2Data.RitualObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.GateObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.MusicDepartmentObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.LostKeysObjective = SetObjectiveSaveData(true, false);
            data.CH2Data.IsChapterComplete = false;
            data.CH2Data.PlayerPosition = new Vector3DataVO(new Vector3(17, -6.378374f, -237));
            data.CH2Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 360, 0));
            data.CH2Data.HasSaveData = true;
            if (checkpoint < 7) return data;
            // CH2 Sammy's Office
            data.CH2Data.LostKeysObjective.IsComplete = true;
            data.CH2Data.MusicPuzzleObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.SanctuaryObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.InfirmaryObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.SewersObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.SammysOfficeObjective = SetObjectiveSaveData(true, true);
            data.CH2Data.PlayerPosition = new Vector3DataVO(new Vector3(36, -7.044999f, -230));
            data.CH2Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 90, 0));
            if (checkpoint < 10) return data;
            // CH3 Intro
            data.CH2Data.IsChapterComplete = true;
            if (checkpoint < 11) return data;
            // CH3 Decisions
            data.CH3Data.SafehouseObjective = SetObjectiveSaveData(true, true);
            data.CH3Data.DarkHallwayObjective = SetObjectiveSaveData(true, true);
            data.CH3Data.HeavenlyToysObjective = SetObjectiveSaveData(true, true); // Cutoff Here
            data.CH3Data.AliceRevealObjective = SetObjectiveSaveData(true, true);
            data.CH3Data.DecisionObjective = SetObjectiveSaveData(true, false);
            data.CH3Data.IsChapterComplete = false;
            data.CH3Data.PlayerPosition = new Vector3DataVO(new Vector3(244, 12.955f, -126));
            data.CH3Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 360, 0));
            data.CH3Data.HasSaveData = true;
            if (checkpoint < 12) return data;
            // CH3 Angel's Bidding
            data.CH3Data.DecisionObjective.IsComplete = true;
            data.CH3Data.ChoseDevilsPath = false;
            data.CH3Data.BorisJumpscareObjective = SetObjectiveSaveData(true, true);
            data.CH3Data.PosterPiperObjective = SetObjectiveSaveData(true, true);
            data.CH3Data.EnterLiftObjective = SetObjectiveSaveData(true, true);
            data.CH3Data.AliceLairObjective = SetObjectiveSaveData(true, false);
            data.CH3Data.PlayerPosition = new Vector3DataVO(new Vector3(250, -178.045f, -82));
            data.CH3Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 250, 0));
            if (checkpoint < 13) return data;
            // CH3 Butcher Gang
            data.CH3Data.AliceLairObjective.IsComplete = true;
            data.CH3Data.AliceTasksObjective = SetObjectiveSaveData(true, false);
            foreach (var gear in data.CH3Data.GearTask.Object) gear.IsComplete = true;
            data.CH3Data.GearTask.Status = SetObjectiveSaveData(true, true);
            foreach (var ink in data.CH3Data.ThickInkTask.Object) ink.IsComplete = true;
            data.CH3Data.ThickInkTask.Status = SetObjectiveSaveData(true, true);
            foreach (var core in data.CH3Data.PowerCoreTask.Object) core.IsComplete = true;
            data.CH3Data.PowerCoreTask.Status = SetObjectiveSaveData(true, true);
            foreach (var cutout in data.CH3Data.CutoutTask.Object) cutout.IsComplete = true;
            data.CH3Data.CutoutTask.Status = SetObjectiveSaveData(true, true);
            data.CH3Data.PlayerPosition = new Vector3DataVO(new Vector3(324, -177.952f, -95));
            data.CH3Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 215, 0));
            if (checkpoint < 15) return data;
            // CH4 Intro
            data.CH3Data.AliceTasksObjective.IsComplete = true;
            data.CH3Data.IsChapterComplete = true;
            if (checkpoint < 16) return data;
            // CH4 Warehouse
            data.CH4Data.AccountingObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.BridgeMachineObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.LostOnesObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.VentObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.MapRoomObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.WarehouseObjective = SetObjectiveSaveData(true, false);
            data.CH4Data.IsChapterComplete = false;
            data.CH4Data.PlayerPosition = new Vector3DataVO(new Vector3(376, 65.99462f, -222));
            data.CH4Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 82, 0));
            data.CH4Data.HasSaveData = true;
            if (checkpoint < 17) return data;
            // CH4 Haunted House
            data.CH4Data.WarehouseObjective.IsComplete = true;
            data.CH4Data.FairGamesObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.ResearchObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.RideStorageObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.MaintenanceObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.HauntedHouseObjective = SetObjectiveSaveData(true, true);
            data.CH4Data.PlayerPosition = new Vector3DataVO(new Vector3(460, 55.955f, -239));
            data.CH4Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 130, 0));
            if (checkpoint < 20) return data;
            // CH5 Intro
            data.CH4Data.IsChapterComplete = true;
            if (checkpoint < 21) return data;
            // CH5 Administration
            data.CH5Data.SafehouseObjective = SetObjectiveSaveData(true, true);
            data.CH5Data.CavesObjective = SetObjectiveSaveData(true, true);
            data.CH5Data.DockObjective = SetObjectiveSaveData(true, true);
            data.CH5Data.TunnelsObjective = SetObjectiveSaveData(true, true);
            data.CH5Data.LostHarbourObjective = SetObjectiveSaveData(true, true);
            data.CH5Data.IsChapterComplete = false;
            data.CH5Data.PlayerPosition = new Vector3DataVO(new Vector3(-193, -98.045f, -196.31f));
            data.CH5Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 117.5244f, 0));
            data.CH5Data.HasSaveData = true;
            if (checkpoint < 22) return data;
            // CH5 Ink Machine
            data.CH5Data.AdministrationObjective.IsComplete = true;
            data.CH5Data.VaultObjective = SetObjectiveSaveData(true, true);
            data.CH5Data.GiantInkMachineObjective = SetObjectiveSaveData(true, true);
            data.CH5Data.PlayerPosition = new Vector3DataVO(new Vector3(340, -96.045f, -299));
            data.CH5Data.PlayerRotation = new Vector3DataVO(new Vector3(0, 90, 0));
            return data;
        }


        public static ObjectiveSaveDataVO SetObjectiveSaveData(bool isStarted, bool isComplete)
        {
            ObjectiveSaveDataVO data = new ObjectiveSaveDataVO();
            data.IsStarted = isStarted;
            data.IsComplete = isComplete;
            return data;
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(GameDataManager), "Save", new Type[] { typeof(SaveFileData), typeof(bool), typeof(bool) })]
        public static void CheckSavePosition(GameDataManager __instance, ref SaveFileData saveFileData)
        {
            if (!GameManager.Instance.Player) return;
            Vector3DataVO pos;
            Vector3DataVO rot;
            switch (GameManager.Instance.CurrentChapter.Chapter)
            {
                case Chapters.ONE:
                    pos = saveFileData.CH1Data.PlayerPosition;
                    rot = saveFileData.CH1Data.PlayerRotation;
                    break;
                case Chapters.TWO:
                    pos = saveFileData.CH2Data.PlayerPosition;
                    rot = saveFileData.CH2Data.PlayerRotation;
                    break;
                case Chapters.THREE:
                    pos = saveFileData.CH3Data.PlayerPosition;
                    rot = saveFileData.CH3Data.PlayerRotation;
                    break;
                case Chapters.FOUR:
                    pos = saveFileData.CH4Data.PlayerPosition;
                    rot = saveFileData.CH4Data.PlayerRotation;
                    break;
                case Chapters.FIVE:
                    pos = saveFileData.CH5Data.PlayerPosition;
                    rot = saveFileData.CH5Data.PlayerRotation;
                    break;
                default:
                    pos = new Vector3DataVO(Vector3.zero);
                    rot = new Vector3DataVO(Vector3.zero);
                    break;
            }
            BendyAndTheArchipelagoMachine.Logger.LogDebug($"Position: ({pos.X}, {pos.Y}, {pos.Z}) | Rotation: ({rot.X}, {rot.Y}, {rot.Z})");
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
                    PrepareChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH1");
                    break;
                case 1:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    PrepareChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH2");
                    break;
                case 2:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    PrepareChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH3");
                    break;
                case 3:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    PrepareChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH4");
                    break;
                case 4:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    PrepareChapterData(checkpoint);
                    CheckpointMenu.hidden = true;
                    LoadChapterFromTitle(titleScreenController, "CH5");
                    break;
                case 5:
                    if (!HasCheckpoint(checkpoint))
                    {
                        ArchipelagoConsole.LogMessage($"Locked");
                        break;
                    }
                    PrepareChapterData(checkpoint);
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
