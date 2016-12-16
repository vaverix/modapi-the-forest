using UnityEngine;

namespace CheatMenuPlus
{
    public class AtmosphereCtrl : TheForestAtmosphere
    {
        protected override void Update()
        {
            if (CheatMenuPlusCtrl.Options.World.CaveLight > 0f && InACave)
            {
                CaveAddLight1 = new Color(CheatMenuPlusCtrl.Options.World.CaveLight, CheatMenuPlusCtrl.Options.World.CaveLight, CheatMenuPlusCtrl.Options.World.CaveLight);
                CaveAddLight2 = new Color(CheatMenuPlusCtrl.Options.World.CaveLight, CheatMenuPlusCtrl.Options.World.CaveLight, CheatMenuPlusCtrl.Options.World.CaveLight);

                CaveAddLight1Intensity = CheatMenuPlusCtrl.Options.World.CaveLight;
                CaveAddLight2Intensity = CheatMenuPlusCtrl.Options.World.CaveLight;
            }
            else if (CheatMenuPlusCtrl.Options.World.NightLight > 0f && !InACave)
            {
                NightAddLight1 = new Color(CheatMenuPlusCtrl.Options.World.NightLight, CheatMenuPlusCtrl.Options.World.NightLight, CheatMenuPlusCtrl.Options.World.NightLight);
                NightAddLight2 = new Color(CheatMenuPlusCtrl.Options.World.NightLight, CheatMenuPlusCtrl.Options.World.NightLight, CheatMenuPlusCtrl.Options.World.NightLight);

                NightAddLight1Intensity = CheatMenuPlusCtrl.Options.World.NightLight;
                NightAddLight2Intensity = CheatMenuPlusCtrl.Options.World.NightLight;
            }
            base.Update();
        }
    }
}