# R.E.P.O Mod Template - Template Information

## What is this template?

This is a comprehensive, production-ready template for creating mods for the R.E.P.O game using BepInEx and Harmony. It provides everything you need to start developing R.E.P.O mods with best practices built-in.

## Template Contents

### Core Files
- **Plugin.cs** - Main plugin entry point with BepInEx setup
- **ArribaArribaMod.csproj** - Project configuration with all dependencies
- **manifest.json** - Metadata for mod distribution

### Code Organization
- **Patches/** - Harmony patches for modifying game behavior
  - `ExamplePatches.cs` - Comprehensive examples of all patch types
- **Utils/** - Utility classes and helpers
  - `ConfigManager.cs` - Configuration management
  - `ModHelpers.cs` - Common helper functions
- **lib/** - Game assembly references (populated by user)

### Documentation
- **README.md** - Main documentation and overview
- **QUICKSTART.md** - 5-minute quick start guide for beginners
- **DEVELOPMENT.md** - Advanced development techniques and patterns
- **INSTALLATION.md** - Detailed build and installation instructions
- **TEMPLATE_INFO.md** - This file

### Configuration
- **.gitignore** - Excludes build artifacts and game DLLs
- **.gitattributes** - Line ending configuration
- **LICENSE** - MIT License

## Features

### 1. Complete BepInEx Integration
- Proper plugin attributes and metadata
- Configuration system setup
- Logging infrastructure
- Lifecycle management (Awake, OnDestroy)

### 2. Harmony Patching Examples
- Prefix patches (run before original method)
- Postfix patches (run after original method)
- Transpiler patches (IL code modification)
- Property patching (getters/setters)
- State sharing between patches

### 3. Utility Functions
- Configuration validation
- GameObject finding and manipulation
- Component access helpers
- Debugging utilities
- Color conversion helpers

### 4. Comprehensive Documentation
- Step-by-step setup instructions
- Building and deployment guides
- Debugging techniques
- Best practices and patterns
- Troubleshooting guides
- Example code for common tasks

## Getting Started

### Quick Start (5 minutes)
1. Read `QUICKSTART.md`
2. Copy game DLLs to `lib/` folder
3. Run `dotnet build`
4. Copy output DLL to BepInEx plugins folder
5. Launch game and verify

### Complete Setup
1. Read `INSTALLATION.md` for detailed instructions
2. Follow prerequisites checklist
3. Setup project and dependencies
4. Build and test
5. Start customizing for your mod

### Learning to Mod
1. Start with `QUICKSTART.md` examples
2. Read `DEVELOPMENT.md` for advanced techniques
3. Study the example patches
4. Experiment with simple modifications
5. Check documentation when stuck

## Customization Checklist

Before publishing your mod, customize these items:

- [ ] Update namespace from `ArribaArribaMod` to your mod name
- [ ] Change plugin GUID in `Plugin.cs`
- [ ] Update plugin name and description
- [ ] Modify version number
- [ ] Update `ArribaArribaMod.csproj` properties
- [ ] Edit `manifest.json` with your mod info
- [ ] Update README.md with your mod's description
- [ ] Remove example patches and add your own
- [ ] Add your configuration options
- [ ] Update LICENSE if needed

## Technology Stack

### Required
- **.NET Standard 2.1** - Target framework
- **BepInEx 5.x** - Plugin framework for Unity
- **Harmony** - Runtime method patching library
- **Unity Engine 2022.3.21** - Game engine version

### Optional
- **dnSpy** - .NET decompiler for exploring game code
- **ILSpy** - Alternative decompiler
- **Visual Studio** - Full-featured IDE
- **VS Code** - Lightweight alternative

## Best Practices Included

1. **Null Safety** - Examples show proper null checking
2. **Error Handling** - Try-catch blocks in critical sections
3. **Logging** - Comprehensive logging for debugging
4. **Configuration** - User-configurable options
5. **Compatibility** - Checks for other mods
6. **Performance** - Efficient patching strategies
7. **Documentation** - Well-commented code

## File Structure Overview

```
ArribaArribaREPOModpack/
├── .gitignore              # Git exclusions
├── .gitattributes          # Git line ending config
├── ArribaArribaMod.csproj  # Project file
├── LICENSE                 # MIT License
├── Plugin.cs               # Main plugin
├── manifest.json           # Mod metadata
├── README.md               # Main documentation
├── QUICKSTART.md           # Quick start guide
├── DEVELOPMENT.md          # Advanced guide
├── INSTALLATION.md         # Build instructions
├── TEMPLATE_INFO.md        # This file
├── Patches/
│   └── ExamplePatches.cs   # Harmony patch examples
├── Utils/
│   ├── ConfigManager.cs    # Config management
│   └── ModHelpers.cs       # Helper functions
└── lib/
    └── README.md           # Instructions for game DLLs
```

## Common Use Cases

This template is suitable for:
- Gameplay modifications
- Quality of life improvements
- New features and content
- Bug fixes and patches
- UI enhancements
- Multiplayer tweaks
- Performance optimizations
- Custom configuration options

## Support and Resources

### Documentation
- All documentation files in this repository
- BepInEx docs: https://docs.bepinex.dev/
- Harmony docs: https://harmony.pardeike.net/

### Community
- R.E.P.O Modding: https://repomods.com/
- BepInEx Discord: https://discord.gg/MpFEDAg
- Arriba Arriba Discord: (see main README)

### Tools
- dnSpy: https://github.com/dnSpy/dnSpy
- ILSpy: https://github.com/icsharpcode/ILSpy
- BepInEx: https://github.com/BepInEx/BepInEx

## License

This template is provided under the MIT License. You are free to:
- Use it for any purpose
- Modify it as needed
- Distribute mods based on it
- Use it commercially

The only requirement is to include the license notice.

## Credits

This template is based on:
- Official R.E.P.O modding documentation (repomods.com)
- BepInEx plugin examples and documentation
- Harmony patching patterns and examples
- Community best practices from existing R.E.P.O mods

## Version History

- **v1.0.0** (2025-11-11)
  - Initial template release
  - Complete documentation suite
  - Example patches and utilities
  - Build and deployment instructions

## Contributing

If you improve this template:
1. Fork the repository
2. Make your improvements
3. Test thoroughly
4. Submit a pull request

Contributions welcome:
- Better examples
- More utility functions
- Documentation improvements
- Bug fixes
- New features

## Next Steps

1. **Customize** the template for your mod idea
2. **Learn** by reading the documentation
3. **Experiment** with simple patches
4. **Build** something amazing
5. **Share** with the community!

Happy modding! 🎮✨
