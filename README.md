# ArribaArribaREPOModpack

Official Arriba Arriba discord server modpack for R.E.P.O game.

## 🎮 About

This is a comprehensive mod template for creating R.E.P.O mods using BepInEx and Harmony. The template provides a solid foundation with examples, utilities, and best practices for R.E.P.O mod development.

## 📋 Prerequisites

Before you start, you'll need:

1. **R.E.P.O Game** - Installed via Steam
2. **BepInEx** - Mod loader for Unity games
   - Download from: https://github.com/BepInEx/BepInEx/releases
   - Use BepInEx 5.x for R.E.P.O
3. **.NET SDK** - For building the project
   - Download from: https://dotnet.microsoft.com/download
4. **IDE** (Choose one):
   - Visual Studio 2022 (recommended)
   - Visual Studio Code
   - JetBrains Rider

## 🚀 Getting Started

### 1. Setup BepInEx

1. Download BepInEx 5.x from the releases page
2. Extract it to your R.E.P.O game folder
3. Run the game once to generate BepInEx folders
4. Your game folder should now have a `BepInEx` folder with `plugins` subfolder

### 2. Copy Game Assemblies

You need to copy game DLLs to the `lib` folder for the project to build:

1. Navigate to: `<Steam>/steamapps/common/REPO/REPO_Data/Managed/`
2. Copy these files:
   - `Assembly-CSharp.dll`
   - `PhotonUnityNetworking.dll`
   - `PhotonRealtime.dll`
3. Paste them into the `lib` folder in this project

See `lib/README.md` for more details.

### 3. Build the Mod

```bash
dotnet restore
dotnet build
```

The compiled mod will be in `bin/Debug/netstandard2.1/` or `bin/Release/netstandard2.1/`

### 4. Install the Mod

1. Copy `ArribaArribaMod.dll` from the build output
2. Paste it into `<R.E.P.O>/BepInEx/plugins/`
3. Launch R.E.P.O
4. Check BepInEx console (F5 in-game) to see if the mod loaded

## 📁 Project Structure

```
ArribaArribaREPOModpack/
├── Plugin.cs                 # Main plugin entry point
├── Patches/
│   └── ExamplePatches.cs     # Harmony patches (modify game behavior)
├── Utils/
│   ├── ConfigManager.cs      # Configuration management
│   └── ModHelpers.cs         # Utility helper functions
├── lib/
│   └── README.md             # Instructions for game DLLs
├── ArribaArribaMod.csproj    # Project configuration
└── README.md                 # This file
```

## 🔧 Development Guide

### Creating Patches

Patches use Harmony to modify existing game methods without changing game files.

**Example Prefix Patch** (runs before original method):
```csharp
[HarmonyPatch(typeof(PlayerController), "Jump")]
[HarmonyPrefix]
private static bool BeforeJump()
{
    Plugin.Log.LogInfo("Player is about to jump!");
    return true; // true = run original method, false = skip it
}
```

**Example Postfix Patch** (runs after original method):
```csharp
[HarmonyPatch(typeof(GameManager), "GetScore")]
[HarmonyPostfix]
private static void AfterGetScore(ref int __result)
{
    __result *= 2; // Double the score!
    Plugin.Log.LogInfo($"Score modified to: {__result}");
}
```

### Finding What to Patch

1. **Use dnSpy or ILSpy** to decompile `Assembly-CSharp.dll`
2. Browse the game's code to find classes and methods
3. Look for methods you want to modify
4. Create patches targeting those methods

### Configuration

Add configuration options in `Plugin.cs`:

```csharp
private void InitializeConfig()
{
    var myConfig = Config.Bind(
        "Section",           // Config section
        "SettingName",       // Setting name
        defaultValue,        // Default value
        "Description"        // Description shown to users
    );
}
```

Config file will be generated at: `<R.E.P.O>/BepInEx/config/ArribaArribaMod.cfg`

### Logging

Use the logger to output messages:

```csharp
Plugin.Log.LogInfo("Information message");
Plugin.Log.LogWarning("Warning message");
Plugin.Log.LogError("Error message");
Plugin.Log.LogDebug("Debug message");
```

View logs in:
- BepInEx console (F5 in-game)
- `<R.E.P.O>/BepInEx/LogOutput.log`

## 🛠️ Useful Tools

- **dnSpy** - Decompiler and debugger for .NET assemblies
  - https://github.com/dnSpy/dnSpy
- **ILSpy** - .NET decompiler
  - https://github.com/icsharpcode/ILSpy
- **Harmony Documentation** - Learn about patching
  - https://harmony.pardeike.net/
- **BepInEx Documentation**
  - https://docs.bepinex.dev/

## 📚 Resources

- **R.E.P.O Modding Documentation**: https://repomods.com/develop.html
- **BepInEx Discord**: https://discord.gg/MpFEDAg
- **Harmony GitHub**: https://github.com/pardeike/Harmony

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## ⚠️ Important Notes

- **Never redistribute game DLLs** - Users must copy them from their own installation
- **Test your mods thoroughly** - Ensure they don't break the game or other mods
- **Use version control** - Commit frequently during development
- **Check compatibility** - Test with other popular mods
- **Follow BepInEx guidelines** - Use proper plugin metadata

## 📝 License

This mod template is provided as-is for the R.E.P.O modding community. Feel free to use, modify, and distribute your mods based on this template.

## 🆘 Troubleshooting

### Mod doesn't load
- Check BepInEx console (F5) for errors
- Verify DLL is in the correct plugins folder
- Ensure BepInEx is properly installed

### Build errors
- Make sure game DLLs are in the `lib` folder
- Run `dotnet restore` to restore NuGet packages
- Check .NET SDK is installed

### Game crashes
- Check BepInEx logs for error messages
- Disable your mod to confirm it's the cause
- Review your patches for potential issues

## 🎯 Next Steps

1. **Customize** the namespace and plugin info in `Plugin.cs`
2. **Add your patches** in the `Patches` folder
3. **Configure** your mod settings
4. **Test** thoroughly in-game
5. **Share** your creation with the community!

## 💬 Support

Join the Arriba Arriba Discord server for support and discussions:
- Ask questions in the modding channel
- Share your creations
- Get help from other modders

Happy modding! 🎉
