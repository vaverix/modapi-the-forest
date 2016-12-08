using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenuPlus
{
    class FPController : FirstPersonCharacter
    {
        protected float baseWalkSpeed;
        protected float baseRunSpeed;
        protected float baseJumpHeight;
        protected float baseCrouchSpeed;
        protected float baseStrafeSpeed;
        protected float baseSwimmingSpeed;
        protected float baseGravity;
        protected float baseMaxVelocityChange;
        protected float baseMaximumVelocity;
        protected Boolean _noClip = false;


        protected void BaseValues()
        {
            this.baseWalkSpeed = this.walkSpeed;
            this.baseRunSpeed = this.runSpeed;
            this.baseJumpHeight = this.jumpHeight;
            this.baseCrouchSpeed = this.crouchSpeed;
            this.baseStrafeSpeed = this.strafeSpeed;
            this.baseSwimmingSpeed = this.swimmingSpeed;
            this.baseMaxVelocityChange = maxVelocityChange;
            this.baseMaximumVelocity = maximumVelocity;
            this.baseGravity = this.gravity;
        }

        protected override void Start()
        {
            base.Start();
            BaseValues();
        }

        protected override void FixedUpdate()
        {
            this.walkSpeed = baseWalkSpeed * CheatMenuPlusComponent.SpeedMultiplier;
            this.runSpeed = baseRunSpeed * CheatMenuPlusComponent.SpeedMultiplier;
            this.jumpHeight = baseJumpHeight * CheatMenuPlusComponent.JumpMultiplier;
            this.crouchSpeed = baseCrouchSpeed * CheatMenuPlusComponent.SpeedMultiplier;
            this.strafeSpeed = baseStrafeSpeed * CheatMenuPlusComponent.SpeedMultiplier;
            this.swimmingSpeed = baseSwimmingSpeed * CheatMenuPlusComponent.SpeedMultiplier;

            if (CheatMenuPlusComponent.NoClip)
            {
                if (!this._noClip)
                {
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<SphereCollider>().enabled = false;
                    this._noClip = true;
                }
            }
            else
            {
                if (this._noClip)
                {
                    GetComponent<CapsuleCollider>().enabled = true;
                    GetComponent<SphereCollider>().enabled = true;
                    this._noClip = false;
                }
            }

            if (CheatMenuPlusComponent.FlyMode && !PushingSled)
            {
                if (this.rb.useGravity)
                {
                    this.rb.useGravity = false;
                    this.gravity = 0f;

                    /*
                                    if (CheatMenuPlusComponent.NoClip)
                                    {
                                        GetComponent<CapsuleCollider>().enabled = false;
                                        GetComponent<SphereCollider>().enabled = false;
                                    }
                                    else
                                    {
                                        GetComponent<CapsuleCollider>().enabled = true;
                                        GetComponent<SphereCollider>().enabled = true;
                                    }
                     */
                }
                else
                {
                    bool button1 = TheForest.Utils.Input.GetButton("Crouch");
                    bool button2 = TheForest.Utils.Input.GetButton("Run");
                    bool button3 = TheForest.Utils.Input.GetButton("Jump");
                    float multiplier = baseWalkSpeed;
                    if (button2) multiplier = baseRunSpeed;

                    Vector3 vector3 = Camera.main.transform.rotation * (
                        new Vector3(TheForest.Utils.Input.GetAxis("Horizontal"),
                        0f,
                        TheForest.Utils.Input.GetAxis("Vertical")
                    ) * multiplier * CheatMenuPlusComponent.SpeedMultiplier);
                    Vector3 velocity = this.rb.velocity;
                    if (button3) velocity.y -= multiplier * CheatMenuPlusComponent.SpeedMultiplier;
                    if (button1) velocity.y += multiplier * CheatMenuPlusComponent.SpeedMultiplier;
                    Vector3 force = vector3 - velocity;
                    this.rb.AddForce(force, ForceMode.VelocityChange);
                }
            }
            else
            {
                if (!this.rb.useGravity)
                {
                    this.rb.useGravity = true;
                    /*GetComponent<CapsuleCollider>().enabled = true;
                    GetComponent<SphereCollider>().enabled = true;*/
                    this.gravity = baseGravity;
                }
                base.FixedUpdate();
            }
        }
    }
}