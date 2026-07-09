using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using BepInEx;
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

                var deathLink = deathLinks.Dequeue();
                var cause = deathLink.Cause.IsNullOrWhiteSpace() ? GetDeathLinkCause(deathLink) : deathLink.Cause;

                // TODO Call the Kill Player code
                BendyAndTheArchipelagoMachine.Logger.LogMessage(cause);
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(e);
            }
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

                // add the cause as second parameter
                var linkToSend = new DeathLink(slotName);

                service.SendDeathLink(linkToSend);
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(e);
            }
        }
    }
}
