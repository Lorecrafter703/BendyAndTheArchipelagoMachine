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
            if (!Client.HasItem($"Unlock CH{targetChapter}"))
            {
                BendyAndTheArchipelagoMachine.Logger.LogWarning($"Chapter not yet unlocked: {targetChapter}");
                //return;
            }
            MyChapterController.MyLoadChapter(MyChapterController.currentChapter, $"CH{targetChapter}");
            base.OnInteract();
        }
    }
}
