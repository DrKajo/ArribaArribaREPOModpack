# Quick Start Guide

Get your R.E.P.O mod up and running in 5 minutes!

## Step 1: Install Prerequisites (5 minutes)

1. **Install .NET SDK**
   - Download: https://dotnet.microsoft.com/download
   - Get .NET 6.0 or later
   - Verify: `dotnet --version`

2. **Install BepInEx in R.E.P.O**
   - Download BepInEx 5.x: https://github.com/BepInEx/BepInEx/releases
   - Extract to your R.E.P.O game folder
   - Run the game once to initialize BepInEx

## Step 2: Setup the Project (2 minutes)

1. **Copy Game DLLs**
   ```bash
   # Navigate to R.E.P.O Managed folder
   cd "C:\Program Files (x86)\Steam\steamapps\common\REPO\REPO_Data\Managed"
   
   # Copy these 3 files to your project's lib folder:
   # - Assembly-CSharp.dll
   # - PhotonUnityNetworking.dll
   # - PhotonRealtime.dll
   ```

2. **Restore and Build**
   ```bash
   cd path/to/ArribaArribaREPOModpack
   dotnet restore
   dotnet build
   ```

## Step 3: Install and Test (1 minute)

1. **Copy the compiled mod**
   ```bash
   # From: bin/Debug/netstandard2.1/ArribaArribaMod.dll
   # To: <R.E.P.O>/BepInEx/plugins/
   ```

2. **Launch R.E.P.O**
   - Press F5 to open BepInEx console
   - Look for: "Plugin ArribaArribaMod is loaded!"

## Step 4: Start Modding!

Now you're ready to create your mod. Here are the first things to customize:

### 1. Change Plugin Info

Edit `Plugin.cs` and update the namespace and info:

```csharp
namespace YourModName
{
    [BepInPlugin("author.yourmodname", "Your Mod Name", "1.0.0")]
    public class Plugin : BaseUnityPlugin
```

### 2. Create Your First Patch

Add a new file in `Patches/` folder:

```csharp
using HarmonyLib;

namespace YourModName
{
    [HarmonyPatch(typeof(GameClassName), "MethodName")]
    [HarmonyPostfix]
    private static void MyFirstPatch()
    {
        Plugin.Log.LogInfo("My first patch is working!");
    }
}
```

### 3. Add Configuration

In `Plugin.cs`, add config options:

```csharp
private void InitializeConfig()
{
    var mySetting = Config.Bind(
        "General",
        "MySetting", 
        true,
        "Description of my setting"
    );
}
```

### 4. Rebuild and Test

```bash
dotnet build
# Copy DLL to BepInEx/plugins/
# Launch game and test
```

## Common First Mods

### Example 1: Speed Boost
```csharp
[HarmonyPatch(typeof(PlayerController), "GetMoveSpeed")]
[HarmonyPostfix]
private static void SpeedBoost(ref float __result)
{
    __result *= 2.0f; // Double speed!
}
```

### Example 2: Infinite Health
```csharp
[HarmonyPatch(typeof(PlayerHealth), "TakeDamage")]
[HarmonyPrefix]
private static bool PreventDamage()
{
    return false; // Skip damage method
}
```

### Example 3: Custom Keybind
```csharp
[HarmonyPatch(typeof(PlayerController), "Update")]
[HarmonyPostfix]
private static void CustomKeybind(PlayerController __instance)
{
    if (Input.GetKeyDown(KeyCode.G))
    {
        Plugin.Log.LogInfo("G key pressed!");
        // Do something cool
    }
}
```

## Troubleshooting

### Build fails with missing references
- Make sure game DLLs are in `lib/` folder
- Run `dotnet restore`

### Mod doesn't load in game
- Check BepInEx console (F5) for errors
- Verify DLL is in `BepInEx/plugins/`
- Make sure BepInEx is installed correctly

### Patches don't work
- Use dnSpy to verify class/method names
- Check logs for Harmony errors
- Add debug logging to patches

## Next Steps

1. **Read DEVELOPMENT.md** - Learn advanced techniques
2. **Study existing mods** - Look at other R.E.P.O mods on GitHub
3. **Join the community** - Get help in Discord
4. **Experiment** - Try different patches and see what happens!

## Useful Commands

```bash
# Build in Debug mode (default)
dotnet build

# Build in Release mode (smaller DLL)
dotnet build -c Release

# Clean build artifacts
dotnet clean

# Restore packages
dotnet restore

# Build and copy to game (Windows PowerShell)
dotnet build; Copy-Item "bin/Debug/netstandard2.1/ArribaArribaMod.dll" "C:/Path/To/REPO/BepInEx/plugins/"
```

## Development Workflow

1. Write code
2. Build: `dotnet build`
3. Copy DLL to plugins folder
4. Launch game and test
5. Check logs
6. Repeat!

## Tips for Beginners

- Start with simple patches (Postfix is easiest)
- Use lots of logging to understand what's happening
- Don't be afraid to break things - that's how you learn!
- Save your work frequently
- Test one change at a time
- Read error messages carefully

Happy modding! 🚀
