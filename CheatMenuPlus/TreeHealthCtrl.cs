namespace CheatMenuPlus
{
    public class TreeHealthCtrl : TreeHealth
    {
        protected override void Hit()
        {
            if (CheatMenuPlusCtrl.Options.Player.InstantTree)
            {
                Explosion(100f);
            }
            else
            {
                base.Hit();
            }
        }
    }
}