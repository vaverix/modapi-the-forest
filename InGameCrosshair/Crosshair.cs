using UnityEngine;

namespace InGameCrosshair
{
    class Crosshair : MonoBehaviour
    {
        static float crossSize = 24f;
        bool initialized = false;
        bool visible = false;
        Texture2D tex;

        [ModAPI.Attributes.ExecuteOnGameStart]
        static void AddMeToScene()
        {
            GameObject GO = new GameObject("__InGameCrosshair__");
            GO.AddComponent<Crosshair>();
        }

        private void OnGUI()
        {
            if (!initialized)
            {
                tex = ModAPI.Resources.GetTexture("cross.png");
                initialized = true;
            }

            if (visible)
            {
                GUI.DrawTexture(new Rect((Screen.width / 2) - (crossSize / 2), (Screen.height / 2) - (crossSize / 2), crossSize, crossSize), tex);
            }
        }

        private void Update()
        {
            if (ModAPI.Input.GetButtonDown("togglecrosshair"))
            {
                visible = !visible;
            }
        }
    }
}