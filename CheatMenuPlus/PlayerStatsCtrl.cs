namespace CheatMenuPlus
{
    class PlayerStatsCtrl : PlayerStats
    {

        [ModAPI.Attributes.Priority(1000)]
        protected override void hitFallDown()
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode)
                base.hitFallDown();
        }


        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFire()
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode)
                base.HitFire();
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void hitFromEnemy(int getDamage)
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode)
                base.hitFromEnemy(getDamage);
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void HitShark(int damage)
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode)
                base.HitShark(damage);
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void Fell()
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode)
                base.Fell();
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void HitFromPlayMaker(int damage)
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode)
                base.HitFromPlayMaker(damage);
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void FallDownDead()
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode || Dead)
                base.FallDownDead();
        }

        [ModAPI.Attributes.Priority(1000)]
        public override void Hit(int damage, bool ignoreArmor, PlayerStats.DamageType type)
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode)
                base.Hit(damage, ignoreArmor, type);
        }

        protected override void Update()
        {
            if (!CheatMenuPlusCtrl.Options.Player.GodMode && ModAPI.Input.GetButtonDown("Suicide"))
            {
                Health = 0f;
                Dead = true;
                Player.enabled = false;
                base.FallDownDead();
            }
            else
            {
                if (CheatMenuPlusCtrl.Options.Player.GodMode)
                {
                    Health = 100f;
                    Armor = 100;
                }
                if (CheatMenuPlusCtrl.Options.Player.FreezeNeeds)
                {
                    IsBloody = false;
                    IsCold = false;
                    FireWarmth = true;
                    SunWarmth = true;
                    Fullness = 1f;
                    Stamina = 100f;
                    Energy = 100f;
                    Hunger = 0;
                    Thirst = 0;
                    Starvation = 0;
                }
                base.Update();
            }
        }
    }
}
