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

        public override void OnInteract()
        {
            BendyAndTheArchipelagoMachine.Logger.LogWarning("INTERACTION WORKS!!!");
            MyChapterController.MyLoadChapter(MyChapterController.currentChapter, "CH2");
            base.OnInteract();
        }

        //public override void Init()
        //{
        //    BendyAndTheArchipelagoMachine.Logger.LogWarning("Overridden Init perhaps?");
        //}
    }
}
