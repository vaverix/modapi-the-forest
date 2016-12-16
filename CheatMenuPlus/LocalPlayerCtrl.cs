using System;
using UnityEngine;

namespace CheatMenuPlus
{
    class LocalPlayerCtrl : TheForest.Utils.LocalPlayer
    {
        protected GameObject removeClone;
        protected UITexture removeCloneTexture;
        protected bool ShowRemoveIcon = false;
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

            if (!CheatMenuPlusCtrl.Options.Player.InstantDestroy)
            {
                return;
            }

            try
            {
                if (removeClone == null)
                {
                    if (TheForest.Utils.Scene.HudGui != null && TheForest.Utils.Scene.HudGui.DestroyIcon != null && TheForest.Utils.Scene.HudGui.DestroyIcon.gameObject != null)
                    {
                        GameObject MainPanel = null;
                        for (int i = 0; i < TheForest.Utils.Scene.HudGui.PauseMenu.transform.childCount; i++)
                        {
                            Transform t = TheForest.Utils.Scene.HudGui.PauseMenu.transform.GetChild(i);
                            if (t.name == "Panel - Main")
                            {
                                MainPanel = t.gameObject;
                                break;
                            }
                        }

                        Transform window = MainPanel.transform.GetChild(0);
                        GameObject continueButton = null;
                        for (int i = 0; i < window.childCount; i++)
                        {
                            Transform t = window.GetChild(i);
                            if (t.name == "Button - Continue")
                            {
                                continueButton = t.gameObject;
                                break;
                            }
                        }

                        removeClone = NGUITools.AddChild(TheForest.Utils.Scene.HudGui.DestroyIcon.transform.parent.gameObject, TheForest.Utils.Scene.HudGui.DestroyIcon.gameObject);
                        Destroy(removeClone.transform.GetChild(0).gameObject);
                        removeClone.transform.localPosition = TheForest.Utils.Scene.HudGui.DestroyIcon.transform.localPosition;
                        removeCloneTexture = removeClone.GetComponent<UITexture>();
                        removeCloneTexture.alpha = 1f;
                        removeCloneTexture.mainTexture = ModAPI.Resources.GetTexture("RemoveBuilding.png");
                        GameObject newLabel = NGUITools.AddChild(removeClone, continueButton.transform.GetChild(0).gameObject);
                        newLabel.GetComponent<UILabel>().text = ModAPI.Input.GetKeyBindingAsString("RemoveBuilding");
                        newLabel.transform.localPosition += new Vector3(0f, -70f, 0f);
                    }
                }
                else
                {
                    Ray r = new Ray(_mainCam.transform.position, _mainCam.transform.forward);
                    RaycastHit hitInfo = new RaycastHit();
                    int mask = 0;
                    mask += 1 << 25;
                    mask += 1 << 21;
                    mask += 1 << 28;
                    mask = ~((1 << 18) | (1 << 29));

                    if (Physics.Raycast(r, out hitInfo, 4f, mask))
                    {
                        Transform t = hitInfo.collider.transform;
                        BoltEntity ent = null;
                        if (BoltNetwork.isRunning)
                        {
                            ent = t.GetComponentInParent<BoltEntity>();
                            if (ent == null || !ent.StateIs<IBuildingState>())
                            {
                                t = null;
                            }
                        }
                        else
                        {
                            if (t.GetComponentInParent<TheForest.Buildings.World.BuildingHealth>() == null && t.GetComponentInParent<TheForest.Buildings.World.FoundationHealth>() == null)
                            {
                                t = null;
                            }
                            else
                            {
                                while (t.parent != null)
                                {
                                    t = t.parent; // Find main object
                                }
                            }
                        }

                        if (t != null)
                        {
                            ShowRemoveIcon = true;
                            removeCloneTexture.gameObject.SetActive(true);
                            if (ModAPI.Input.GetButtonDown("RemoveBuilding"))
                            {
                                if (BoltNetwork.isRunning)
                                {
                                    if (ent != null)
                                    {
                                        DestroyBuilding building = DestroyBuilding.Create(Bolt.GlobalTargets.OnlyServer);
                                        //DestroyBuilding building = DestroyBuilding.Raise(Bolt.GlobalTargets.OnlyServer);
                                        building.BuildingEntity = ent;
                                        building.Send();
                                    }
                                }
                                else
                                {
                                    Destroy(t.gameObject);
                                }
                            }
                        }
                        else
                        {
                            ShowRemoveIcon = false;
                            removeCloneTexture.gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        removeCloneTexture.gameObject.SetActive(false);
                    }
                }
            }
            catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }
    }
}