# Hot Lava Cheat

Open source mod for the Unity Mono version of Hot Lava.

This is most likely the first and last public version of this project. It contains some minor bugs. If you want them fixed, feel free to do it yourself since the project is open source.

## What this mod includes

- Movement Recorder
- Replay and rewind tools
- Bhop helper
- In-game marker and overlay
- Misc gameplay and utility features

## Installation

1. Open your Hot Lava game folder.
2. Go to the root folder used by the modded build:
   `Hot Lava\archive\build`
3. Extract the release archive directly into that folder.
4. Confirm file replacement if Windows asks.
5. Launch the game.

After extraction, the mod DLL should end up here:
`Hot Lava\archive\build\BepInEx\plugins\Hot Lava Cheat.dll`

## Config and logs

- Config file:
  `%AppData%\Furion HotLava\config.ini`
- Debug log:
  `%AppData%\Furion HotLava\debug.log`
- Movement Recorder saves:
  `%AppData%\Furion HotLava\Movement recorder\<WORLD>\<COURSE>\`

## Build

1. Open `Hot Lava Cheat.sln` in Visual Studio.
2. Check the local reference paths inside `Hot Lava Cheat.csproj`.
3. Build the project.