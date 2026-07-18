using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using BendyAndTheArchipelagoMachine.Utils;
using BepInEx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BendyAndTheArchipelagoMachine.Archipelago
{
    public class Client
    {
        // FIXME : Hardcoded server values
        public const string SERVER = "localhost";
        public const int PORT = 38281;
        public const string SLOT_NAME = "BendyTest";

        public const string AP_VERSION = "0.6.7";
        public const string GAME_NAME = "Bendy and the Ink Machine";

        public static bool authenticated;
        private bool attemptingConnection;

        public static ArchipelagoData serverData = new ArchipelagoData();
        private DeathLinkHandler deathLinkHandler;
        static ArchipelagoSession session = ArchipelagoSessionFactory.CreateSession(SERVER, PORT);

        private Queue<ItemInfo> ItemQueue = new Queue<ItemInfo>();
        ReceivedItemsHelper Helper;


        public void Connect()
        {
            if (authenticated || attemptingConnection) return;

            try
            {
                session = ArchipelagoSessionFactory.CreateSession(serverData.Uri);
                SetupSession();
                attemptingConnection = true;
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(e);
            }

            TryConnect();
        }


        private void SetupSession()
        {
            session.MessageLog.OnMessageReceived += (message) => ArchipelagoConsole.LogMessage(message.ToString());
            session.Items.ItemReceived += (receivedItemsHelper) => OnItemReceived(receivedItemsHelper);
            session.Socket.ErrorReceived += OnSessionErrorReceived;
            session.Socket.SocketClosed += OnSessionSocketClosed;
        }


        private void TryConnect()
        {
            try
            {
                ThreadPool.QueueUserWorkItem(
                    _ => HandleConnectResult(
                        session.TryConnectAndLogin(
                            GAME_NAME,
                            serverData.SlotName,
                            ItemsHandlingFlags.AllItems,
                            new Version(AP_VERSION),
                            password: serverData.Password,
                            requestSlotData: serverData.NeedSlotData
                    )));
            }
            catch (Exception e)
            {
                BendyAndTheArchipelagoMachine.Logger.LogError(e);
                HandleConnectResult(new LoginFailure(e.ToString()));
                attemptingConnection = false;
            }
        }


        private void HandleConnectResult(LoginResult result)
        {
            string outText;
            if (result.Successful)
            {
                var success = (LoginSuccessful)result;

                string path = Path.Combine(Paths.PluginPath, "BendyAndTheArchipelagoMachine", $"{session.RoomState.Seed}.json");
                if (File.Exists(path))
                {
                    serverData = JsonConvert.DeserializeObject<ArchipelagoData>(File.ReadAllText(path));
                }
                else
                {
                    serverData.SetupSession(success.SlotData, session.RoomState.Seed);
                }

                authenticated = true;

                deathLinkHandler = new DeathLinkHandler(session.CreateDeathLinkService(), serverData.SlotName);
                session.Locations.CompleteLocationChecksAsync(serverData.CheckedLocations.ToArray());
                outText = $"Successfully connected to {serverData.Uri} as {serverData.SlotName}!";

                ArchipelagoConsole.LogMessage(outText);
            }
            else
            {
                var failure = (LoginFailure)result;
                outText = $"Failed to connect to {serverData.Uri} as {serverData.SlotName}.";
                outText = failure.Errors.Aggregate(outText, (current, error) => current + $"\n    {error}");

                BendyAndTheArchipelagoMachine.Logger.LogError(outText);

                authenticated = false;
                Disconnect();
            }

            attemptingConnection = false;
        }


        private void Disconnect()
        {
            BendyAndTheArchipelagoMachine.Logger.LogDebug("disconnecting from server..");
            session?.Socket.DisconnectAsync();
            session = null;
            authenticated = false;
        }


        public void SendMessage(string message)
        {
            session.Socket.SendPacketAsync(new SayPacket { Text = message });
        }


        private void OnItemReceived(ReceivedItemsHelper helper)
        {
            var receivedItem = helper.DequeueItem();
            Helper = helper;
            ItemQueue.Enqueue(receivedItem);
        }


        public void ProcessItems()
        {
            if (!authenticated || attemptingConnection) return;
            if (Helper == null) return;
            if (serverData.seed.IsNullOrWhiteSpace()) return;
            if (ItemQueue.Count > 0)
            {
                var receivedItem = ItemQueue.Dequeue();

                // If Item has been received before return
                if (Helper.Index <= serverData.Index) return;

                // Add Item to List
                serverData.AddItem(receivedItem.ItemId);
                string message = $"Received {receivedItem.ItemName} from {receivedItem.Player} ({receivedItem.LocationName}).";
                ArchipelagoConsole.LogMessage(message);
            }
        }


        private void OnSessionErrorReceived(Exception e, string message)
        {
            BendyAndTheArchipelagoMachine.Logger.LogError(e);
            ArchipelagoConsole.LogMessage(message);
        }


        private void OnSessionSocketClosed(string reason)
        {
            BendyAndTheArchipelagoMachine.Logger.LogError($"Connection to Archipelago lost: {reason}");
            Disconnect();
        }


        public static void SendLocation(string itemName)
        {
            long itemID = IDTables.GetLocationID(itemName);

            if (itemID != -1)
            {
                session.Locations.CompleteLocationChecks(itemID);
                serverData.CheckLocation(itemID);
            }
        }


        public static bool HasItem(string itemName)
        {
            long itemID = IDTables.GetItemID(itemName);
            return serverData.ReceivedItems.Contains(itemID);
        }


        public static int BaconSoupCount()
        {
            int count = 0;
            foreach (long _ in serverData.ReceivedItems)
            {
                if (_ == IDTables.GetItemID("Bacon Soup")) count++;
            }
            return count;
        }


        public static void CompleteGoal()
        {
            session.SetGoalAchieved();
        }
    }
}
