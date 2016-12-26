namespace CheatMenuPlus
{
    public class EnemyHealthCtrl : EnemyHealth
    {
        public override void HitReal(int damage)
        {
            if (CheatMenuPlusCtrl.Options.Player.InstantKill)
            {
                //base.HitReal(damage * 1000);
                base.Die();
            }
            else
            {
                base.HitReal(damage);
            }
        }
    }
}