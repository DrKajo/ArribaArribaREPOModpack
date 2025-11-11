# Installation and Build Instructions

## Prerequisites

Before building this mod, ensure you have:

1. **.NET SDK 6.0 or later**
   - Download from: https://dotnet.microsoft.com/download
   - Verify installation: `dotnet --version`

2. **R.E.P.O Game installed**
   - Available on Steam

3. **BepInEx 5.x installed in R.E.P.O**
   - Download from: https://github.com/BepInEx/BepInEx/releases
   - Get the latest BepInEx 5.x version (NOT 6.x)
   - Extract to your R.E.P.O game folder
   - Run the game once to generate BepInEx folders

4. **Internet connection** for NuGet packages

## Setup Steps

### Step 1: Copy Game DLLs

The project requires game assemblies to build. Copy these files from your R.E.P.O installation to the `lib/` folder:

**Required files:**
- `Assembly-CSharp.dll`
- `PhotonUnityNetworking.dll`
- `PhotonRealtime.dll`

**Location:**
```
<Steam>/steamapps/common/REPO/REPO_Data/Managed/
```

**Common Steam paths:**
- Windows: `C:\Program Files (x86)\Steam\steamapps\common\REPO\REPO_Data\Managed\`
- Linux: `~/.steam/steam/steamapps/common/REPO/REPO_Data/Managed/`

### Step 2: Restore NuGet Packages

Open a terminal in the project directory and run:

```bash
dotnet restore
```

This will download:
- BepInEx.Core
- BepInEx.PluginInfoProps
- BepInEx.Analyzers
- UnityEngine.Modules
- HarmonyX (included with BepInEx)

### Step 3: Build the Project

```bash
# Debug build (default)
dotnet build

# Release build (optimized)
dotnet build -c Release
```

The compiled DLL will be in:
- Debug: `bin/Debug/netstandard2.1/ArribaArribaMod.dll`
- Release: `bin/Release/netstandard2.1/ArribaArribaMod.dll`

## Installation

### Installing the Mod

1. Locate the compiled DLL in the build output folder
2. Copy `ArribaArribaMod.dll` 
3. Paste it into: `<R.E.P.O>/BepInEx/plugins/`

### Verifying Installation

1. Launch R.E.P.O
2. Press **F5** to open the BepInEx console
3. Look for the message: `Plugin ArribaArribaMod is loaded!`
4. Check for any errors in the console

## Troubleshooting

### NuGet restore fails

**Issue:** Cannot download BepInEx packages

**Solutions:**
- Check your internet connection
- Try clearing NuGet cache: `dotnet nuget locals all --clear`
- Manually download BepInEx from GitHub and reference the DLLs directly

### Build fails with "Assembly-CSharp not found"

**Issue:** Game DLLs not in lib folder

**Solution:**
1. Verify the three required DLLs are in the `lib/` folder
2. Check the file names match exactly (case-sensitive on Linux)
3. Make sure they're from the correct game version

### Mod doesn't load in game

**Issue:** Mod not appearing in BepInEx

**Solutions:**
- Verify BepInEx is installed correctly (check for BepInEx folder)
- Ensure the DLL is in the `plugins` folder (not in a subfolder)
- Check BepInEx console (F5) for error messages
- Look at `BepInEx/LogOutput.log` for detailed errors

### Game crashes on startup

**Issue:** Mod causing game to crash

**Solutions:**
1. Remove the mod and verify game works
2. Check BepInEx logs for exceptions
3. Review your patches for errors
4. Try building in Debug mode for better error messages

## Alternative Build Method (Manual References)

If you cannot access the BepInEx NuGet repository, you can manually reference the DLLs:

1. **Get BepInEx DLLs manually:**
   - Download BepInEx 5.x from GitHub
   - Extract and find these files in `BepInEx/core/`:
     - `BepInEx.dll`
     - `0Harmony.dll`

2. **Update the .csproj file:**

Replace the PackageReference section with direct references:

```xml
<ItemGroup>
  <Reference Include="BepInEx">
    <HintPath>lib\BepInEx.dll</HintPath>
    <Private>false</Private>
  </Reference>
  <Reference Include="0Harmony">
    <HintPath>lib\0Harmony.dll</HintPath>
    <Private>false</Private>
  </Reference>
  <Reference Include="UnityEngine">
    <HintPath>lib\UnityEngine.dll</HintPath>
    <Private>false</Private>
  </Reference>
  <Reference Include="UnityEngine.CoreModule">
    <HintPath>lib\UnityEngine.CoreModule.dll</HintPath>
    <Private>false</Private>
  </Reference>
  <Reference Include="Assembly-CSharp">
    <HintPath>lib\Assembly-CSharp.dll</HintPath>
    <Private>false</Private>
  </Reference>
</ItemGroup>
```

3. **Copy additional DLLs to lib folder:**
   - From BepInEx: `BepInEx.dll`, `0Harmony.dll`
   - From game: `UnityEngine.dll`, `UnityEngine.CoreModule.dll`

## Development Workflow

1. **Make changes** to your code
2. **Build** the project: `dotnet build`
3. **Copy** the DLL to BepInEx/plugins
4. **Launch** the game and test
5. **Check logs** for errors
6. **Repeat!**

### Quick Build & Deploy (Windows PowerShell)

```powershell
dotnet build; Copy-Item "bin/Debug/netstandard2.1/ArribaArribaMod.dll" "C:\Path\To\REPO\BepInEx\plugins\" -Force
```

### Quick Build & Deploy (Linux/Mac)

```bash
dotnet build && cp bin/Debug/netstandard2.1/ArribaArribaMod.dll "/path/to/REPO/BepInEx/plugins/"
```

## Configuration

After the mod runs once, a configuration file will be created at:
```
<R.E.P.O>/BepInEx/config/ArribaArribaMod.cfg
```

Edit this file to customize mod settings. Changes take effect after restarting the game.

## Uninstalling

To remove the mod:
1. Delete `ArribaArribaMod.dll` from `BepInEx/plugins/`
2. (Optional) Delete the config file from `BepInEx/config/`

## Getting Help

If you encounter issues:

1. **Check logs:**
   - BepInEx console (F5 in-game)
   - `BepInEx/LogOutput.log`

2. **Common resources:**
   - BepInEx Discord: https://discord.gg/MpFEDAg
   - Harmony documentation: https://harmony.pardeike.net/
   - This project's DEVELOPMENT.md

3. **Report issues:**
   - Check if it's a known issue
   - Include error messages from logs
   - Describe steps to reproduce

## Next Steps

Once you have the mod built and installed:

1. Read **QUICKSTART.md** for basic modding tutorials
2. Read **DEVELOPMENT.md** for advanced techniques
3. Customize the template for your mod idea
4. Join the community and share your creation!

Happy modding! 🚀
