using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheatMenuPlus
{
    class Stats : PlayerStats
    {

        [ModAPI.Attributes.Priority(1000)]
        protected override void hitFallDown()
        {
            if (!CheatMenuPlusComponent.GodMode)
                base.hitFallDown();
        }


        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFire()
        {
            if (!CheatMenuPlusComponent.GodMode)
                base.HitFire();
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void hitFromEnemy(int getDamage)
        {
            if (!CheatMenuPlusComponent.GodMode)
                base.hitFromEnemy(getDamage);
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void HitShark(int damage)
        {
            if (!CheatMenuPlusComponent.GodMode)
                base.HitShark(damage);
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void Fell()
        {
            if (!CheatMenuPlusComponent.GodMode)
                base.Fell();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFromPlayMaker(int damage)
        {
            if (!CheatMenuPlusComponent.GodMode)
                base.HitFromPlayMaker(damage);
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void FallDownDead()
        {
            if (!CheatMenuPlusComponent.GodMode || this.Dead)
                base.FallDownDead();
        }

 /* This makes the mod dll generation to fail...
        [ModAPI.Attributes.Priority(1000)]
        public override void Hit(int damage, bool ignoreArmor, PlayerStats.DamageType type)
        {
            if (!CheatMenuPlusComponent.GodMode)
                base.Hit(damage, ignoreArmor, type);
        }
 */

        // ... So i use this
        [ModAPI.Attributes.Priority(1000)]
        protected override void CheckDeath()
        {
            if (!CheatMenuPlusComponent.GodMode && (this.Health <= 0f) && !this.Dead)
            {
                this.Dead = true;
                this.Player.enabled = false;
                this.FallDownDead();
            }
        }

        protected override void Update()
        {
            if (!CheatMenuPlusComponent.GodMode && ModAPI.Input.GetButtonDown("Suicide"))
            {
                this.Health = 0f;
                this.Dead = true;
                this.Player.enabled = false;
                base.FallDownDead();
            }
            else
            {
                if (CheatMenuPlusComponent.GodMode)
                {
                    this.Health = 100f;
                    this.Armor = 100;
                }
                if (CheatMenuPlusComponent.HeavyMode)
                {
                    this.IsBloody = false;
                    //this.Warm = true;
                    this.IsCold = false;
                    this.Fullness = 1f;
                    this.Stamina = 100f;
                    this.Energy = 100f;
                    this.Hunger = 0;
                    this.Thirst = 0;
                    this.Starvation = 0;
                }
                base.Update();
            }
        }
    }
}
