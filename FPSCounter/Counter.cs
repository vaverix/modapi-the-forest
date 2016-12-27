using ModAPI.Attributes;
using UnityEngine;

namespace FPSCounter
{
    public class Counter : MonoBehaviour
    {
        private float deltaTime = 0.0f;
        private bool visible;

        [ExecuteOnGameStart]
        private static void AddMeToScene()
        {
            GameObject e0A = new GameObject("__FPSCounter__");
            e0A.AddComponent<Counter>();
            DontDestroyOnLoad(e0A);
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("togglefpscounter"))
            {
                visible = !visible;
            }
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            if (!visible)
            {
                return;
            }

            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.yellow;
            float msec = deltaTime * 1000.0f;
            float fps = Mathf.Clamp(1.0f / deltaTime, 0f, 999f);
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}