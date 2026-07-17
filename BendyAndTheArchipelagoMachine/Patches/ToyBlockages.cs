using BendyAndTheArchipelagoMachine.Archipelago;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BendyAndTheArchipelagoMachine.Patches
{
    [HarmonyPatch(typeof(CH3ToyMachine))]
    internal class ToyBlockages
    {
        public static List<Interactable> ToysRef = new List<Interactable>();


        [HarmonyPostfix]
        [HarmonyPatch("InitOnComplete")]
        public static void RegisterToys(CH3ToyMachine __instance, List<CH3ToyMachine.Spinners> ___m_SpinnersLeft, List<CH3ToyMachine.Spinners> ___m_SpinnersRight)
        {
            foreach (CH3ToyMachine.Spinners spinner in ___m_SpinnersLeft)
            {
                foreach (Interactable toy in spinner.Toys)
                {
                    ToysRef.Add(toy);
                }
            }
            foreach (CH3ToyMachine.Spinners spinner in ___m_SpinnersRight)
            {
                foreach (Interactable toy in spinner.Toys)
                {
                    ToysRef.Add(toy);
                }
            }
        }


        [HarmonyPostfix]
        [HarmonyPatch("OnDisposed")]
        public static void ClearToysRef()
        {
            ToysRef.Clear();
        }


        public static bool HandleToyPickup()
        {
            return Client.HasItem("CH3 Toys");
        }
    }
}
