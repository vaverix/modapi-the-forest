using System;
using Bolt;
using TheForest.Networking;
using TheForest.Utils;

namespace CheatMenuPlus
{
    public class BoltPlayerSetupCtrl : BoltPlayerSetup
    {
        protected override void Update()
        {
            try
            {
                if (CheatMenuPlusCtrl.Options.Other.InstantRevive && RespawnDeadTrigger != null && RespawnDeadTrigger.activeSelf && RespawnDeadTrigger.GetComponent<RespawnDeadTrigger>() != null)
                {
                    LocalPlayer.Tuts.HideReviveMP();
                    var playerHealed = PlayerHealed.Create(GlobalTargets.Others);
                    playerHealed.HealingItemId = RespawnDeadTrigger.GetComponent<RespawnDeadTrigger>()._healItemId;
                    playerHealed.HealTarget = entity;
                    playerHealed.Send();
                    RespawnDeadTrigger.SetActive(false);
                }
            }
            catch (Exception)
            {
                ModAPI.Log.Write("Exception while reviving player.");
            }

            base.Update();
        }
    }
}