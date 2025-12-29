# Stardew Valley Discord RPC Mod

This is a SMAPI mod for Stardew Valley that updates your Discord Rich Presence with your player name and current location.

## Prerequisites

1.  **Stardew Valley** installed.
2.  **SMAPI** (Stardew Modding API) installed. [Install Guide](https://stardewvalleywiki.com/Modding:Installing_SMAPI)
3.  **.NET 6.0 SDK** (or later) installed to build the mod. [Download .NET](https://dotnet.microsoft.com/download)

## Setup

### 1. Get a Discord Client ID

1.  Go to the [Discord Developer Portal](https://discord.com/developers/applications).
2.  Click **New Application** and give it a name (e.g., "Stardew Valley").
3.  Copy the **Application ID** (Client ID).
4.  Go to the **Rich Presence** -> **Art Assets** section.
5.  Upload an image for the large icon and name it `stardew_icon`. This will be the main icon shown in the RPC.

### 2. Configure the Mod

1.  Open `StardewDiscordRPC/ModEntry.cs`.
2.  Find the line:
    ```csharp
    private const string ClientId = "123456789012345678";
    ```
3.  Replace `"123456789012345678"` with your actual **Application ID** from step 1.

### 3. Build the Mod

1.  Open a terminal in the `StardewDiscordRPC` folder.
2.  Run the following command:
    ```bash
    dotnet build
    ```
3.  This will compile the mod.

### 4. Install the Mod

1.  Locate the build output. It is usually in `StardewDiscordRPC/bin/Debug/net6.0`.
2.  You should see a folder or a set of files including `StardewDiscordRPC.dll`, `DiscordRPC.dll`, `manifest.json`, etc.
3.  Copy the **entire folder** (or create a new folder named `StardewDiscordRPC` and put the files in it) into your Stardew Valley `Mods` folder.
    *   **Important:** Ensure `DiscordRPC.dll` is copied along with `StardewDiscordRPC.dll`.
    *   Windows: `C:\Program Files (x86)\Steam\steamapps\common\Stardew Valley\Mods`
    *   Mac/Linux: Check the SMAPI install guide for the path.

### 5. Run the Game

Launch Stardew Valley via SMAPI. Your Discord status should now update!
