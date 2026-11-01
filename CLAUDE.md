# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A BepInEx 6 plugin (HarmonyX-based mod) for the Unity game **Sex, Secrets & Used Tech** (install folder `ElectronicStore`, exe `ElectronicsThriftStore.exe`, Steam app 3576350, Unity 6000.0.49, **IL2CPP**, built on Naninovel). It adds an in-game IMGUI cheat/debug menu toggled with `F3` (BepInEx config key `General.MenuToggleKey`). UI strings are Traditional Chinese. Sibling of `C:\Projects\legend-of-mortal-mod` (活俠傳, Mono), whose layout and conventions it follows (explicit types over `var`, `Traverse` for private members, CSharpier, Keep a Changelog once there is a release).

## Build

- SDK-style `SexSecretsUsedTechMod.csproj`, `net6.0` (BepInEx IL2CPP runs on .NET 6): `dotnet build -c Release`
- References come straight from the game: BepInEx/HarmonyX/Il2CppInterop from `<game>\BepInEx\core` and the generated `<game>\BepInEx\interop\*.dll` (UnityEngine, `Assembly-CSharp`, Naninovel, …). `interop` only exists after the game was launched once with BepInEx installed, and BepInEx regenerates it after game updates. `GamePath` defaults to the Steam install; the built DLL is copied to `<game>\BepInEx\plugins\SexSecretsUsedTechMod\`.
- Formatting: CSharpier 1.x — `dotnet tool restore` then `dotnet csharpier format .`.
- No tests. Verify by launching with `explorer.exe "steam://rungameid/3576350"` and reading `<game>\BepInEx\LogOutput.log` (check its timestamp first).

## Game/loader gotchas

- BepInEx `6.0.0-be.791` IL2CPP win-x64 is installed in the game folder (legend-of-mortal-mod's be.697 is the Mono build and predates Unity 6 support).
- The developer shipped `ElectronicsThriftStore_BackUpThisFolder_ButDontShipItWithYourGame\Managed`: the real Mono assemblies with method bodies. Decompile `Managed\Assembly-CSharp.dll` there to read game logic, but compile against `BepInEx\interop` (stubs), never those DLLs.
- IL2CPP: injected `MonoBehaviour`s need the `(IntPtr)` constructor; `BasePlugin.AddComponent<T>` registers them. IL2CPP collections are `Il2CppSystem.Collections.Generic.*`, not `System.*`.
- The game ships the Input System, so the menu reads its key from IMGUI events (`Event.current`) rather than `UnityEngine.Input`.
- Never start the exe directly — launch through Steam (see legend-of-mortal-mod's CLAUDE.md for the `DOORSTOP_INITIALIZED` trap).
