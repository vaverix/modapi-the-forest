using TheForest.Utils;
using TheForest.World;
using UnityEngine;

namespace CheatMenuPlus
{
    public class WeatherSystemCtrl : WeatherSystem
    {
        protected float ResetCloudTime;

        protected override void TryRain()
        {
            if (CheatMenuPlusCtrl.Options.World.FreezeWeather)
            {
                return;
            }
            base.TryRain();
        }

        protected override void Update()
        {
            if (ResetCloudTime > 0f)
            {
                ResetCloudTime -= Time.deltaTime;
                if (ResetCloudTime <= 0f)
                {
                    CloudSmoothTime = 20f;
                }
            }
            if (CheatMenuPlusCtrl.Options.World.ForceWeather >= 0)
            {
                Scene.RainFollowGO.SetActive(false);
                Scene.RainTypes.RainHeavy.SetActive(false);
                Scene.RainTypes.RainMedium.SetActive(false);
                Scene.RainTypes.RainLight.SetActive(false);
                //Scene.Clock.AfterStorm.SetActive(false);
                switch (CheatMenuPlusCtrl.Options.World.ForceWeather)
                {
                    case 0:
                        Raining = true;
                        RainDice = 1;
                        RainDiceStop = 2;
                        TryRain();
                        CloudSmoothTime = 1f;
                        ResetCloudTime = 2f;
                        break;
                    case 1:
                        Raining = false;
                        RainDice = 2;
                        RainDiceStop = 1;
                        TryRain();
                        CloudSmoothTime = 1f;
                        ResetCloudTime = 2f;
                        break;
                    case 2:
                        Raining = false;
                        RainDice = 3;
                        RainDiceStop = 1;
                        TryRain();
                        CloudSmoothTime = 1f;
                        ResetCloudTime = 2f;
                        break;
                    case 3:
                        Raining = false;
                        RainDice = 4;
                        RainDiceStop = 1;
                        TryRain();
                        CloudSmoothTime = 1f;
                        ResetCloudTime = 2f;
                        break;
                    case 4:
                        Raining = false;
                        RainDice = 5;
                        RainDiceStop = 1;
                        TryRain();
                        CloudSmoothTime = 1f;
                        ResetCloudTime = 2f;
                        break;
                }
                CheatMenuPlusCtrl.Options.World.ForceWeather = -1;
            }
            base.Update();
        }
    }
}