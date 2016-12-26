using UnityEngine;

namespace CheatMenuPlus
{
    class LocalPlayerCtrl : TheForest.Utils.LocalPlayer
    {
        protected bool LastFreeCam = false;
        protected float rotationY = 0f;

        void Update()
        {
            if (CheatMenuPlusCtrl.Options.Other.FreeCam && !LastFreeCam)
            {
                CamFollowHead.enabled = false;
                CamRotator.enabled = false;
                MainRotator.enabled = false;
                FpCharacter.enabled = false;
                LastFreeCam = true;
            }

            if (!CheatMenuPlusCtrl.Options.Other.FreeCam && LastFreeCam)
            {
                CamFollowHead.enabled = true;
                CamRotator.enabled = true;
                MainRotator.enabled = true;
                FpCharacter.enabled = true;
                LastFreeCam = false;
            }

            if (CheatMenuPlusCtrl.Options.Other.FreeCam)
            {
                bool button1 = TheForest.Utils.Input.GetButton("Crouch");
                bool button2 = TheForest.Utils.Input.GetButton("Run");
                bool button3 = TheForest.Utils.Input.GetButton("Jump");
                float multiplier = 0.1f;
                if (button2) multiplier = 2f;

                Vector3 vector3 = Camera.main.transform.rotation * (
                    new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"),
                    0f,
                    TheForest.Utils.Input.GetAxis("Vertical")
                ) * multiplier);
                if (button3) vector3.y += multiplier;
                if (button1) vector3.y -= multiplier;
                Camera.main.transform.position += vector3;

                float rotationX = Camera.main.transform.localEulerAngles.y + TheForest.Utils.Input.GetAxis("Mouse X") * 15f;
                rotationY += TheForest.Utils.Input.GetAxis("Mouse Y") * 15f;
                rotationY = Mathf.Clamp(rotationY, -80f, 80f);
                Camera.main.transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
        }
    }
}