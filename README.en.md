# Sex, Secrets & Used Tech Mod

[繁體中文](README.md) | English

An in-game cheat menu for **Sex, Secrets & Used Tech (修幹嘛？二手3C店的闇營業)**, built as a BepInEx 6 plugin.

> [!NOTE]
> The menu itself is in Traditional Chinese only. The table below explains what each part does.

## Installation

> [!IMPORTANT]
> This mod only works with **BepInEx 6 for IL2CPP** (`6.0.0-be.791` recommended). It does not load on BepInEx 5 or on the Mono build of BepInEx 6.
> To check: `BepInEx/core` must contain `BepInEx.Unity.IL2CPP.dll`.

- Download [BepInEx 6 be.791 (Unity IL2CPP x64)](https://builds.bepinex.dev/projects/bepinex_be/791/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.791%2Bb782da4.zip) and extract it into the game folder (`steamapps/common/ElectronicStore`, the one with `ElectronicsThriftStore.exe`)
- Start the game once through Steam. On the first start BepInEx generates `BepInEx/interop` and the screen stays black for a while; close the game once it shows up
- Put `SexSecretsUsedTechMod.dll` in `BepInEx/plugins/SexSecretsUsedTechMod/`

## Troubleshooting

- `F3` does nothing: open `BepInEx/LogOutput.log` and check that it lists `Loading [修幹嘛？二手3C店的闇營業 修改選單 …]`
- `BepInEx/LogOutput.log` is not updated: fully exit Steam, reopen it and start the game through Steam. Don't run `ElectronicsThriftStore.exe` directly

## Usage

Press `F3` in game to open or close the menu. The key is `MenuToggleKey` in `BepInEx/config/SexSecretsUsedTechMod.cfg`.

| Option | What it does |
|---|---|
| 金錢 (money) | Shows your money. Type a number and press 設定 (set) to change it, or press +10000 to add ten thousand |
| 體力 (stamina) | Shows current / max stamina. 補滿 (refill) fills it up; with 鎖定 (lock) checked it refills whenever it drops |
| 遊戲速度 (game speed) | Drag the slider for 1x to 10x (0.5 steps); 1x resets it |

- Money and stamina can only be changed once a save is loaded; on the title screen the menu says 請先載入存檔 (load a save first)
- While the menu is open, clicks on it don't reach the game behind it

## Screenshots

![The menu](docs/screenshots/menu.webp)

## Build

Requires the .NET SDK and the game with BepInEx installed and started once (the build references `BepInEx/interop`).

```sh
dotnet build -c Release
```

Add `-p:GamePath=<game folder>` if the game is not in the default Steam location. The DLL is copied to the game's `BepInEx/plugins/SexSecretsUsedTechMod/` after building.
