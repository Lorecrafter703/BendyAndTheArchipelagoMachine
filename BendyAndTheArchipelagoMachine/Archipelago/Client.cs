using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.MessageLog.Messages;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using BendyAndTheArchipelagoMachine.Data;
using BendyAndTheArchipelagoMachine.Utils;
using BepInEx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

        public const string AP_VERSION = "0.6.8";
        public const string GAME_NAME = "Bendy and the Ink Machine";

        public static bool authenticated;
        private bool attemptingConnection;

        public static ArchipelagoData serverData = new ArchipelagoData();
        private DeathLinkHandler deathLinkHandler;
        static ArchipelagoSession session = ArchipelagoSessionFactory.CreateSession(SERVER, PORT);


        // Call Connect after setting up SERVER, PORT and SLOT_NAME in serverData
        public void Connect()
        {
            if (authenticated || attemptingConnection) return;

            try
            {
                session = ArchipelagoSessionFactory.CreateSession(serverData.Uri);
                SetupSession();
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

                serverData.SetupSession(success.SlotData, session.RoomState.Seed);
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

            // Add Item to List
            if (receivedItem.ItemName != null) ReceivedItems.AddItem(receivedItem.ItemName);
            else ReceivedItems.AddItem(receivedItem.ItemId);

            // If Item has been received before return
            if (helper.Index <= serverData.Index) return;

            // Otherwise increment the "received index" and notify the player
            string message = $"Received {receivedItem.ItemName} from {receivedItem.Player} ({receivedItem.LocationName}).";
            ArchipelagoConsole.LogMessage(message);
            serverData.Index++;
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
                session.Locations.CompleteLocationChecks(itemID);
        }


        public static bool HasItem(string itemName)
        {
            return ReceivedItems.HasItem(itemName);
        }


        public static void CompleteGoal()
        {
            session.SetGoalAchieved();
        }
    }
}
