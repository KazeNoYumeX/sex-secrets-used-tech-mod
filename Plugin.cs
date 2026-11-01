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
        private const string Gold = "Player_Gold";
        private const string HP = "Player_HP";

        private bool _show;
        private Rect _rect = new Rect(20, 20, 320, 180);
        private string _goldInput = "";
        private float _speed = 1f;
        private bool _lockHP;

        // Il2CppInterop creates injected components through this constructor
        public Menu(IntPtr ptr)
            : base(ptr) { }

        private void Update()
        {
            // Only force the scale while sped up, so the game's own timeScale changes still work at 1x
            if (_speed != 1f)
            {
                Time.timeScale = _speed;
            }

            if (_lockHP && Loaded && SSOT_Player.GetVarInt(HP) < SSOT_Player.MaxHP())
            {
                SetVar(HP, SSOT_Player.MaxHP());
            }
        }

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

        // Before a save loads (title screen) the var table is empty and MaxHP() throws
        private static bool Loaded => SSOT_Player.VarData.ContainsKey("Player_MaxHP");

        private void DrawWindow(int id)
        {
            if (Loaded)
            {
                DrawStats();
            }
            else
            {
                GUI.Label(new Rect(10, 25, 300, 22), "請先載入存檔，才能修改金錢與體力");
            }

            GUI.Label(new Rect(10, 120, 300, 22), $"遊戲速度：{_speed:0.#}x");
            float speed = Mathf.Round(GUI.HorizontalSlider(new Rect(10, 145, 230, 22), _speed, 1f, 10f) * 2f) / 2f;
            if (GUI.Button(new Rect(250, 140, 60, 22), "1x"))
            {
                speed = 1f;
            }

            if (speed != _speed)
            {
                _speed = speed;
                Time.timeScale = speed;
            }

            GUI.DragWindow();
        }

        private void DrawStats()
        {
            GUI.Label(new Rect(10, 25, 300, 22), $"金錢：{SSOT_Player.GetVarInt(Gold)}");
            _goldInput = GUI.TextField(new Rect(10, 50, 160, 22), _goldInput);
            if (GUI.Button(new Rect(180, 50, 60, 22), "設定") && int.TryParse(_goldInput, out int gold) && gold >= 0)
            {
                SetVar(Gold, gold);
            }

            if (GUI.Button(new Rect(250, 50, 60, 22), "+10000"))
            {
                SetVar(Gold, SSOT_Player.GetVarInt(Gold) + 10000);
            }

            GUI.Label(new Rect(10, 85, 160, 22), $"體力：{SSOT_Player.GetVarInt(HP)} / {SSOT_Player.MaxHP()}");
            if (GUI.Button(new Rect(180, 85, 60, 22), "補滿"))
            {
                SetVar(HP, SSOT_Player.MaxHP());
            }

            _lockHP = GUI.Toggle(new Rect(250, 85, 60, 22), _lockHP, "鎖定");
        }

        private static void SetVar(string key, int value)
        {
            SSOT_Player.SetVar(key, value.ToString());
            // The top bar only redraws when told, as the game does after a shop purchase
            Singleton<UIRoot>.Instance?.RefreshStateUI();
        }
    }
}
