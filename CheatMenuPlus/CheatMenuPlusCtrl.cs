using System;
using UnityEngine;

namespace CheatMenuPlus
{
    public class CheatMenuPlusCtrl : MonoBehaviour
    {
        public static bool visible = false;
        public static class Options
        {
            public static class Player
            {
                public static bool GodMode = false;
                public static bool FreezeNeeds = false;
                public static bool FlyMode = false;
                public static bool NoClip = false;
                public static bool InstantTree = false;
                public static bool InstantBuild = false;
                public static bool InstantDestroy = false;
                public static bool InstantKill = false;
                public static float SpeedMultiplier = 1;
                public static float JumpMultiplier = 1;
            }

            public static class World
            {
                public static bool FreezeTime = false;
                public static bool FreezeWeather = false;
                public static int ForceWeather = -1;
                public static float CaveLight = 0f;
                public static float NightLight = 0f;
            }

            public static class Other
            {
                public static bool FreeCam = false;
                public static bool InstantRevive = false;
                public static bool Aura = false;
                public static bool Sphere = false;
            }
        }

        public delegate void TickHandler(object sender, EventArgs args);
        public event TickHandler OnTick;

        protected GUIStyle labelStyle;
        protected int fontSize = 10;
        protected float offsetX = 20f;
        protected float offsetY = 30f;
        protected float paddingY = 50f;

        private string textContent;
        private float textPhase;
        private bool showText;

        [ModAPI.Attributes.ExecuteOnGameStart]
        static void AddMeToScene() 
        {
            GameObject GO = new GameObject("__CheatMenuPlus__");
            GO.AddComponent<CheatMenuPlusCtrl>();
            GO.AddComponent<RemoveBuildings>();
        }

        public void DisplayText(string text)
        {
            textContent = text;
            textPhase = 0f;
            showText = true;
        }

        private void OnGUI()
        {
            if (visible)
            {
                GUI.skin = ModAPI.GUI.Skin;

                Matrix4x4 bkpMatrix = GUI.matrix;

                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(GUI.skin.label);
                    labelStyle.fontSize = fontSize;
                }

                GUI.Box(new Rect(10, 10, 550, 530), "Cheat menu", GUI.skin.window);

                float cY = paddingY;
                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "God mode:", labelStyle);
                Options.Player.GodMode = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.GodMode, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Freeze Player needs:", labelStyle);
                Options.Player.FreezeNeeds = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.FreezeNeeds, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "FreeCam mode:", labelStyle);
                Options.Other.FreeCam = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Other.FreeCam, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Fly mode:", labelStyle);
                Options.Player.FlyMode = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.FlyMode, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "No clip:", labelStyle);
                Options.Player.NoClip = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.NoClip, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Instant Tree:", labelStyle);
                Options.Player.InstantTree = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.InstantTree, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Instant Build/Repair:", labelStyle);
                Options.Player.InstantBuild = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.InstantBuild, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Instant Destroy building:", labelStyle);
                Options.Player.InstantDestroy = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.InstantDestroy, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Instant Kill:", labelStyle);
                Options.Player.InstantKill = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Player.InstantKill, "");
                cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Instant Revive Friends:", labelStyle);
                Options.Other.InstantRevive = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Other.InstantRevive, "");
                cY += offsetY;

                //GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Magic Aura:", labelStyle);
                //Options.Other.Aura = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.Other.Aura, "");
                //cY += offsetY;

                GUI.Label(new Rect(offsetX, cY, 150f, 20f), "Freeze Weather:", labelStyle);
                Options.World.FreezeWeather = GUI.Toggle(new Rect(offsetX + 150f, cY, 20f, 30f), Options.World.FreezeWeather, "");
                cY += offsetY;

                if (GUI.Button(new Rect(offsetX, cY, 120f, 20f), "Clear weather"))
                {
                    Options.World.ForceWeather = 0;
                }
                cY += offsetY;

                if (GUI.Button(new Rect(offsetX, cY, 120f, 20f), "Cloudy"))
                {
                    Options.World.ForceWeather = 4;
                }
                cY += offsetY;

                if (GUI.Button(new Rect(offsetX, cY, 120f, 20f), "Light rain"))
                {
                    Options.World.ForceWeather = 1;
                }
                cY += offsetY;

                if (GUI.Button(new Rect(offsetX, cY, 120f, 20f), "Medium rain"))
                {
                    Options.World.ForceWeather = 2;
                }
                cY += offsetY;

                if (GUI.Button(new Rect(offsetX, cY, 120f, 20f), "Heavy rain"))
                {
                    Options.World.ForceWeather = 3;
                }
                cY = paddingY;

                GUI.Label(new Rect(offsetX + 220f, cY, 150f, 20f), "Speed:", labelStyle);
                Options.Player.SpeedMultiplier = GUI.HorizontalSlider(new Rect(offsetX + 320f, cY + 3f, 210f, 30f), Options.Player.SpeedMultiplier, 1f, 10f);
                cY += offsetY;

                GUI.Label(new Rect(offsetX + 220f, cY, 150f, 20f), "Jump power:", labelStyle);
                Options.Player.JumpMultiplier = GUI.HorizontalSlider(new Rect(offsetX + 320f, cY + 3f, 210f, 30f), Options.Player.JumpMultiplier, 1f, 10f);
                cY += offsetY;

                GUI.Label(new Rect(offsetX + 220f, cY, 150f, 20f), "Speed of time:", labelStyle);
                TheForestAtmosphere.Instance.RotationSpeed = GUI.HorizontalSlider(new Rect(offsetX + 320f, cY + 3f, 210f, 30f), TheForestAtmosphere.Instance.RotationSpeed, 0f, 10f);
                cY += offsetY;
                if (GUI.Button(new Rect(offsetX + 400f, cY, 100f, 20f), "Reset"))
                {
                    TheForestAtmosphere.Instance.RotationSpeed = 0.13f;
                }
                cY += offsetY;

                GUI.Label(new Rect(offsetX + 220f, cY, 150f, 20f), "Time:", labelStyle);
                TheForestAtmosphere.Instance.TimeOfDay = GUI.HorizontalSlider(new Rect(offsetX + 320f, cY + 3f, 210f, 30f), TheForestAtmosphere.Instance.TimeOfDay, 0f, 360f);
                cY += offsetY;

                GUI.Label(new Rect(offsetX + 220f, cY, 150f, 20f), "Cave light:", labelStyle);
                Options.World.CaveLight = GUI.HorizontalSlider(new Rect(offsetX + 320f, cY + 3f, 210f, 30f), Options.World.CaveLight, 0f, 1f);
                cY += offsetY;

                GUI.Label(new Rect(offsetX + 220f, cY, 150f, 20f), "Night light:", labelStyle);
                Options.World.NightLight = GUI.HorizontalSlider(new Rect(offsetX + 320f, cY + 3f, 210f, 30f), Options.World.NightLight, 0f, 1f);
                cY += offsetY;

                if (GUI.Button(new Rect(offsetX + 220f, cY + 5f, 130f, 20f), "Instant game save"))
                {
                    visible = false;
                    TheForest.Utils.LocalPlayer.Stats.JustSave();
                }
                cY += offsetY;

                GUI.matrix = bkpMatrix;
            }

            if (showText)
            {
                float a = 1f;
                if (textPhase > 1f)
                {
                    a = 2f - textPhase;
                }
                Color arg_A4_0 = UnityEngine.GUI.color;
                UnityEngine.GUI.color = new Color(1f, 1f, 1f, a);
                GUIContent content = new GUIContent(textContent);
                Vector2 vector = UnityEngine.GUI.skin.label.CalcSize(content);
                UnityEngine.GUI.Label(new Rect(((float)Screen.width - vector.x) / 2f, ((float)Screen.height - vector.y) / 2f, vector.x, vector.y), content);
                UnityEngine.GUI.color = arg_A4_0;
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("OpenMenu"))
            {
                if (visible)
                {
                    TheForest.Utils.LocalPlayer.Inventory.ShowAllEquiped();
                    TheForest.Utils.LocalPlayer.FpCharacter.UnLockView();
                }
                else
                {
                    TheForest.Utils.LocalPlayer.FpCharacter.LockView();
                    TheForest.Utils.LocalPlayer.Inventory.HideAllEquiped(true);
                }
                visible = !visible;
            }

            if (visible && TheForest.Utils.Input.GetButtonDown("Esc"))
            {
                visible = false;
            }

            if (ModAPI.Input.GetButtonDown("FreeCam"))
            {
                Options.Other.FreeCam = !Options.Other.FreeCam;
                DisplayText("FreeCam " + (Options.Other.FreeCam ? "Enabled" : "Disabled"));
            }

            if (ModAPI.Input.GetButtonDown("FlyMode"))
            {
                Options.Player.FlyMode = !Options.Player.FlyMode;
                DisplayText("FlyMode " + (Options.Player.FlyMode ? "Enabled" : "Disabled"));
            }

            if (showText && textPhase < 2f)
            {
                textPhase += Time.deltaTime;
                if (textPhase >= 2f)
                {
                    showText = false;
                }
            }

            if (ModAPI.Input.GetButton("Save"))
            {
                TheForest.Utils.LocalPlayer.Stats.JustSave();
            }

            if (OnTick != null)
            {
                try
                {
                    foreach (var action in OnTick.GetInvocationList())
                    {
                        try
                        {
                            action.DynamicInvoke(this, EventArgs.Empty);
                        }
                        catch (Exception)
                        {
                            ModAPI.Log.Write("Exception while notifying OnTick listener: " + action.GetType().Name);
                        }
                    }
                }
                catch (Exception)
                {
                    ModAPI.Log.Write("Exception while looping over OnTick listeners");
                }
            }
        }
    }
}