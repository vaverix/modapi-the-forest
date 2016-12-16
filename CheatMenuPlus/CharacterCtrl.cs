using UnityEngine;
using Input = TheForest.Utils.Input;

namespace CheatMenuPlus
{
    public class CharacterCtrl : FirstPersonCharacter
    {
        protected float BaseWalkSpeed;
        protected float BaseRunSpeed;
        protected float BaseJumpHeight;
        protected float BaseCrouchSpeed;
        protected float BaseStrafeSpeed;
        protected float BaseSwimmingSpeed;
        protected float BaseGravity;
        protected float BaseMaxVelocityChange;
        protected float BaseMaximumVelocity;
        protected float BaseMaxSwimVelocity;

        protected void BaseValues()
        {
            BaseMaxSwimVelocity = maxSwimVelocity;
            BaseWalkSpeed = walkSpeed;
            BaseRunSpeed = runSpeed;
            BaseJumpHeight = jumpHeight;
            BaseCrouchSpeed = crouchSpeed;
            BaseStrafeSpeed = strafeSpeed;
            BaseSwimmingSpeed = swimmingSpeed;
            BaseMaxVelocityChange = maxVelocityChange;
            BaseMaximumVelocity = maximumVelocity;
            BaseGravity = gravity;
        }

        protected Collider[] AllChildColliders;
        protected Collider[] AllColliders;

        protected override void Start()
        {
            AllChildColliders = gameObject.GetComponentsInChildren<Collider>();
            AllColliders = gameObject.GetComponents<Collider>();
            base.Start();
            BaseValues();
        }

        protected bool LastNoClip;
        protected bool LastFlyMode;

        protected override void FixedUpdate()
        {
            walkSpeed = BaseWalkSpeed * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;
            runSpeed = BaseRunSpeed * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;
            jumpHeight = BaseJumpHeight * CheatMenuPlusCtrl.Options.Player.JumpMultiplier;
            crouchSpeed = BaseCrouchSpeed * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;
            strafeSpeed = BaseStrafeSpeed * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;
            swimmingSpeed = BaseSwimmingSpeed * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;
            maxSwimVelocity = BaseMaxSwimVelocity * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;

            if (CheatMenuPlusCtrl.Options.Player.FlyMode && !PushingSled)
            {
                rb.useGravity = false;
                if (CheatMenuPlusCtrl.Options.Player.NoClip)
                {
                    if (!LastNoClip)
                    {
                        foreach (var t in AllColliders)
                        {
                            t.enabled = false;
                        }
                        foreach (var t in AllChildColliders)
                        {
                            t.enabled = false;
                        }
                        LastNoClip = true;
                    }
                }
                else
                {
                    if (LastNoClip)
                    {
                        foreach (var t in AllColliders)
                        {
                            t.enabled = true;
                        }
                        foreach (var t in AllChildColliders)
                        {
                            t.enabled = true;
                        }
                        LastNoClip = false;
                    }
                }

                var button1 = Input.GetButton("Crouch");
                var button2 = Input.GetButton("Run");
                var button3 = Input.GetButton("Jump");
                var multiplier = BaseWalkSpeed;
                gravity = 0f;
                if (button2)
                {
                    multiplier = BaseRunSpeed;
                }

                var vector3 = Camera.main.transform.rotation * (
                    new Vector3(Input.GetAxis("Horizontal"),
                        0f,
                        Input.GetAxis("Vertical")
                        ) * multiplier * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier);
                var velocity = rb.velocity;
                if (button3)
                {
                    velocity.y -= multiplier * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;
                }
                if (button1)
                {
                    velocity.y += multiplier * CheatMenuPlusCtrl.Options.Player.SpeedMultiplier;
                }
                var force = vector3 - velocity;
                rb.AddForce(force, ForceMode.VelocityChange);
                LastFlyMode = true;
            }
            else
            {
                if (LastFlyMode)
                {
                    if (!IsInWater())
                    {
                        rb.useGravity = true;
                    }
                    gravity = BaseGravity;
                    if (LastNoClip)
                    {
                        foreach (var t in AllColliders)
                        {
                            t.enabled = true;
                        }
                        foreach (var t in AllChildColliders)
                        {
                            t.enabled = true;
                        }
                        LastNoClip = false;
                    }
                    LastFlyMode = false;
                }
                base.FixedUpdate();
            }
        }
    }
}