# Game Assembly References

This folder should contain the DLL files from your R.E.P.O game installation.

## Required DLLs

You need to copy the following files from your R.E.P.O installation to this folder:

1. **Assembly-CSharp.dll** - Main game code
2. **PhotonUnityNetworking.dll** - Networking library
3. **PhotonRealtime.dll** - Photon realtime library

## Where to find these files

These files are typically located in:
```
<Steam>/steamapps/common/REPO/REPO_Data/Managed/
```

Common Steam installation paths:
- **Windows**: `C:\Program Files (x86)\Steam\steamapps\common\REPO\REPO_Data\Managed\`
- **Linux**: `~/.steam/steam/steamapps/common/REPO/REPO_Data/Managed/`

## How to copy

1. Navigate to your R.E.P.O installation folder
2. Go to `REPO_Data/Managed/`
3. Copy the three DLL files listed above
4. Paste them into this `lib` folder

## Note

These files are **not** included in the repository because they are part of the game and should not be redistributed. Each developer needs to copy them from their own game installation.

Once you've copied the files, you can build the project!
