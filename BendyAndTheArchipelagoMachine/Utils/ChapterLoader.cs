using BendyAndTheArchipelagoMachine.Archipelago;
using BendyAndTheArchipelagoMachine.Patches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Utils
{
    internal class ChapterLoader : Interactable
    {
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        BendyAndTheArchipelagoMachine.Logger.LogMessage("Player entered the zone!");
        //    }
        //}
        private int targetChapter = -1;


        public void setTargetChapter(int chapter) { this.targetChapter = chapter; }


        public override void OnInteract()
        {
            if (targetChapter < 1 || targetChapter > 5)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError($"Invalid chapter: {targetChapter}");
                return;
            }
            if (targetChapter == 5)
            {
                var BaconSoupsRequiredOption = (long)Client.serverData.GetSlotDataOption("bacon_soups_required");
                var TotalBaconSoupsOption = (long)Client.serverData.GetSlotDataOption("total_bacon_soups");
                long BaconSoupsRequired = TotalBaconSoupsOption * BaconSoupsRequiredOption / 100;
                BendyAndTheArchipelagoMachine.Logger.LogWarning($"Chapter 5 not yet unlocked: {Client.BaconSoupCount()} / {BaconSoupsRequired} Bacon Soups");
                return;
            }
            else if (!Client.HasItem($"Unlock CH{targetChapter}"))
            {
                BendyAndTheArchipelagoMachine.Logger.LogWarning($"Chapter {targetChapter} not yet unlocked");
                return;
            }
            MyChapterController.MyLoadChapter(MyChapterController.currentChapter, $"CH{targetChapter}");
            base.OnInteract();
        }
    }
}
