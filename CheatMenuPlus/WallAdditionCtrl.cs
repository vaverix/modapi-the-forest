using TheForest.Buildings.Creation;
using TheForest.Utils;
using TheForest.World;
using UnityEngine;

namespace CheatMenuPlus
{
    public class WallAdditionCtrl : WallAdditionTrigger
    {

        protected override void Update()
        {
            if (!CheatMenuPlusCtrl.Options.Player.InstantBuild)
            {
                base.Update();
                return;
            }

            if (!GetComponent<Craft_Structure>().enabled)
            {
                GetComponent<Craft_Structure>().GrabEnter();
            }
            transform.GetComponentInParent<WallChunkArchitect>().ShowToggleAdditionIcon();
            Scene.HudGui.DestroyIcon.gameObject.SetActive(true);
            if ((!TheForest.Utils.Input.IsGamePad && TheForest.Utils.Input.GetButtonDown("Rotate")) || (TheForest.Utils.Input.IsGamePad && TheForest.Utils.Input.GetAxisDown("Rotate") > 0f))
            {
                transform.GetComponentInParent<WallChunkArchitect>().ToggleSegmentAddition();
                LocalPlayer.Sfx.PlayWhoosh();
            }
            if (TheForest.Utils.Input.GetButtonAfterDelay("Craft", 0.5f))
            {
                GetComponent<Craft_Structure>().CancelBlueprint();
                transform.GetComponentInParent<WallChunkArchitect>().HideToggleAdditionIcon();
                return;
            }
        }
    }
}