# 修幹嘛？二手3C店的闇營業 修改 Mod

繁體中文 | [English](README.en.md)

**修幹嘛？二手3C店的闇營業**（Sex, Secrets & Used Tech）的遊戲內修改選單，以 BepInEx 6 插件製作。

## 安裝方法 Installation

> [!IMPORTANT]
> 本 Mod 只支援 **BepInEx 6 IL2CPP 版**（建議 `6.0.0-be.791`），不支援 BepInEx 5，也不支援 BepInEx 6 的 Mono 版。
> 判斷方式：`BepInEx/core` 裡有 `BepInEx.Unity.IL2CPP.dll` 才是對的版本。

- 從 [Releases](https://github.com/KazeNoYumeX/sex-secrets-used-tech-mod/releases/latest) 下載 `sex-secrets-used-tech-mod.zip`，內含 BepInEx 6 be.791（IL2CPP 版）
- 解壓縮到遊戲目錄（`steamapps/common/ElectronicStore`，裡面有 `ElectronicsThriftStore.exe`），遇到同名檔案請選擇覆蓋
- 從 Steam 啟動遊戲。第一次啟動時 BepInEx 要產生 `BepInEx/interop`，會停在黑畫面一段時間，之後就正常
- 自行安裝 BepInEx 的話，請下載 [BepInEx 6 be.791（Unity IL2CPP x64）](https://builds.bepinex.dev/projects/bepinex_be/791/BepInEx-Unity.IL2CPP-win-x64-6.0.0-be.791%2Bb782da4.zip) 解壓縮到遊戲目錄，再把 `SexSecretsUsedTechMod.dll` 放到 `BepInEx/plugins/SexSecretsUsedTechMod/`

## 疑難排解 Troubleshooting

- 按 `F3` 沒反應：打開 `BepInEx/LogOutput.log`，確認有 `Loading [修幹嘛？二手3C店的闇營業 修改選單 …]`
- `BepInEx/LogOutput.log` 沒有更新：完全結束 Steam 後重新開啟，再從 Steam 啟動遊戲。不要直接執行 `ElectronicsThriftStore.exe`

## 使用方法 Usage

遊戲中按 `F3` 開關選單。按鍵可以在 `BepInEx/config/SexSecretsUsedTechMod.cfg` 的 `MenuToggleKey` 修改。

| 選項 | 說明 |
|---|---|
| 金錢 | 顯示目前金錢。輸入數字按「設定」直接改成該金額，「+10000」一次加一萬 |
| 體力 | 顯示目前體力／上限。「補滿」補到上限；勾「鎖定」後體力一減少就自動補滿 |
| 遊戲速度 | 拖動滑桿調整 1～10 倍（每格 0.5 倍），「1x」恢復原速 |

- 金錢和體力要載入存檔後才能改，在標題畫面只會顯示「請先載入存檔」
- 選單打開時，點選單不會點到後面的遊戲

各版本的變更見 [CHANGELOG.md](CHANGELOG.md)。

## 使用效果 Screenshots

![修改選單](docs/screenshots/menu.webp)

## 建置 Build

需要 .NET SDK，以及已經裝好 BepInEx 並啟動過一次的遊戲（編譯時會參考 `BepInEx/interop` 裡的檔案）。

```sh
dotnet build -c Release
```

遊戲不在 Steam 預設路徑時，加上 `-p:GamePath=<遊戲目錄>`。編譯完會自動複製到遊戲的 `BepInEx/plugins/SexSecretsUsedTechMod/`。
