using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using BendyAndTheArchipelagoMachine.Patches;
using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Archipelago
{
    public class DeathLinkHandler
    {
        private static bool deathLinkEnabled;
        private string slotName;
        private readonly DeathLinkService service;
        private readonly Queue<DeathLink> deathLinks = new Queue<DeathLink>();

        public bool isDead = false;


        public DeathLinkHandler(DeathLinkService deathLinkService, string name, bool enableDeathLink = false)
        {
            service = deathLinkService;
            service.OnDeathLinkReceived += DeathLinkReceived;
            slotName = name;
            deathLinkEnabled = enableDeathLink;

            if (deathLinkEnabled)
            {
                service.EnableDeathLink();
            }
        }

        
        public void ToggleDeathLink()
        {
            deathLinkEnabled = !deathLinkEnabled;

            if (deathLinkEnabled)
            {
                service.EnableDeathLink();
            }
            else
            {
                service.DisableDeathLink();
            }

            Client.serverData.SetConfigDeathlink(deathLinkEnabled);
        }


        private void DeathLinkReceived(DeathLink deathLink)
        {
            deathLinks.Enqueue(deathLink);

            BendyAndTheArchipelagoMachine.Logger.LogDebug(deathLink.Cause.IsNullOrWhiteSpace()
                ? $"Received Death Link from {deathLink.Source}"
                : deathLink.Cause);
        }


        public void KillPlayer()
        {
            try
            {
                if (deathLinks.Count < 1) return;

                BendyAndTheArchipelagoMachine.Logger.LogDebug("Killing Player");
                var deathLink = deathLinks.Dequeue();
                if (!deathLinkEnabled) return;

                var cause = deathLink.Cause.IsNullOrWhiteSpace() ? GetDeathLinkCause(deathLink) : deathLink.Cause;

                DeathLinkController.KillPlayer(DeathLinkController.playerController);
                //DeathLinkController.playerController.Die();
                BendyAndTheArchipelagoMachine.Logger.LogMessage(cause);
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(e);
            }
        }


        public void ProcessDeaths()
        {
            DeathLinkController.Revive();
            if (DeathLinkController.isDead) return;
            KillPlayer();
        }


        private string GetDeathLinkCause(DeathLink deathLink)
        {
            return $"Received death from {deathLink.Source}";
        }


        public void SendDeathLink()
        {
            try
            {
                if (!deathLinkEnabled) return;

                BendyAndTheArchipelagoMachine.Logger.LogMessage("sharing your death...");

                var linkToSend = new DeathLink(slotName, $"{slotName} succumbed to the ink.");

                service.SendDeathLink(linkToSend);
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(e);
            }
        }


        public bool GetDeathLinkStatus()
        {
            return deathLinkEnabled;
        }
    }
}
