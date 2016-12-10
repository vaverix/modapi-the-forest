using ModAPI;
using ModAPI.Attributes;
using System;
using UnityEngine;

namespace FPSCounter
{
    public class Counter : MonoBehaviour
    {
        private int frames;

        private float dt;

        private float fps;

        private bool _visible;

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            GameObject e0A = new GameObject("__FPSCounter__");
            e0A.AddComponent<Counter>();
            UnityEngine.Object.DontDestroyOnLoad(e0A);
        }

        private void OnGUI()
        {
            if (this._visible)
            {
                UnityEngine.GUI.color = UnityEngine.Color.yellow;
                UnityEngine.GUI.Label(new Rect(0f, 0f, 100f, 100f), "FPS: " + this.fps);
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("togglefpscounter"))
            {
                this._visible = !this._visible;
            }
            this.frames++;
            this.dt += Time.deltaTime;
            if (this.dt > 1f)
            {
                this.fps = (float)this.frames;
                this.dt -= 1f;
                this.frames = 0;
            }
        }
    }
}