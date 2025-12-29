using System;
using System.Linq;
using DiscordRPC;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace StardewDiscordRPC
{
    public class ModEntry : Mod
    {
        private DiscordRpcClient? client;
        private const string ClientId = "1351572438760292382"; 
        private DateTime? startTime;
        private int modCount;

        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.GameLaunched += OnGameLaunched;
            helper.Events.GameLoop.OneSecondUpdateTicked += OnOneSecondUpdateTicked;
            helper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
            helper.Events.GameLoop.ReturnedToTitle += OnReturnedToTitle;
        }

        private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
        {
            try
            {
                client = new DiscordRpcClient(ClientId);
                client.Initialize();
                startTime = DateTime.UtcNow;
                modCount = this.Helper.ModRegistry.GetAll().Count();
                this.Monitor.Log("Discord RPC Initialized", LogLevel.Info);
                UpdatePresence();
            }
            catch (Exception ex)
            {
                this.Monitor.Log($"Failed to initialize Discord RPC: {ex.Message}", LogLevel.Error);
            }
        }

        private void OnSaveLoaded(object? sender, SaveLoadedEventArgs e)
        {
            UpdatePresence();
        }

        private void OnReturnedToTitle(object? sender, ReturnedToTitleEventArgs e)
        {
            UpdatePresence();
        }

        private void OnOneSecondUpdateTicked(object? sender, OneSecondUpdateTickedEventArgs e)
        {
            UpdatePresence();
        }

        private void UpdatePresence()
        {
            if (client == null) return;

            if (!Context.IsWorldReady)
            {
                client.SetPresence(new RichPresence()
                {
                    Details = "In Main Menu",
                    State = "Ready to farm",
                    Timestamps = new Timestamps()
                    {
                        Start = startTime
                    },
                    Assets = new Assets()
                    {
                        LargeImageKey = "stardew_icon",
                        LargeImageText = "Stardew Valley",
                        SmallImageKey = "mods_count",
                        SmallImageText = $"Playing with {modCount} Mods!"
                    }
                });
                return;
            }

            if (Game1.player == null || Game1.currentLocation == null)
                return;

            string playerName = Game1.player.Name;
            string farmName = Game1.player.farmName.Value + " Farm";
            string locationName = Game1.currentLocation.DisplayName;

            if (Game1.currentLocation is Farm)
            {
                locationName = "On the Farm";
            }

            string details = $"{playerName} | {farmName}";
            string state = $"At {locationName}";

            client.SetPresence(new RichPresence()
            {
                Details = details,
                State = state,
                Timestamps = new Timestamps()
                {
                    Start = startTime
                },
                Assets = new Assets()
                {
                    LargeImageKey = "stardew_icon",
                    LargeImageText = "Stardew Valley",
                    SmallImageKey = "mods_count",
                    SmallImageText = $"Playing with {modCount} Mods!"
                }
            });
        }
    }
}
