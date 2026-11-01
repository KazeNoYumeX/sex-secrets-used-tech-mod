using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using UnityEngine;

namespace SexSecretsUsedTechMod
{
    [BepInPlugin("SexSecretsUsedTechMod", "Sex, Secrets & Used Tech 修改選單", "0.1.0")]
    public class Plugin : BasePlugin
    {
        internal static ConfigEntry<KeyCode> MenuToggleKey;

        public override void Load()
        {
            MenuToggleKey = Config.Bind("General", "MenuToggleKey", KeyCode.F3, "開關修改選單的按鍵");
            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly);
            AddComponent<Menu>();
        }
    }

    public class Menu : MonoBehaviour
    {
        private bool _show;
        private Rect _rect = new Rect(20, 20, 400, 300);

        // Il2CppInterop creates injected components through this constructor
        public Menu(IntPtr ptr)
            : base(ptr) { }

        private void OnGUI()
        {
            // Read the key from IMGUI events rather than Input, which throws when the game uses only the Input System
            Event e = Event.current;
            if (e.type == EventType.KeyDown && e.keyCode == Plugin.MenuToggleKey.Value)
            {
                _show = !_show;
            }

            if (_show)
            {
                // GUI.Window, not GUILayout.Window: IL2CPP stripped the LayoutedWindow constructor the latter needs
                _rect = GUI.Window(
                    0x4D4F44,
                    _rect,
                    (GUI.WindowFunction)DrawWindow,
                    "Sex, Secrets & Used Tech 修改選單"
                );
            }
        }

        private void DrawWindow(int id)
        {
            GUI.Label(new Rect(10, 25, 300, 22), "尚無功能");
            GUI.DragWindow();
        }
    }
}
