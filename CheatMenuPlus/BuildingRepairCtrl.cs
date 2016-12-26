using ModAPI;
using TheForest.Buildings.World;

namespace CheatMenuPlus
{
    public class BuildingRepairCtrl : BuildingRepair
    {
        protected override void Update()
        {
            if (_target == null || !CheatMenuPlusCtrl.Options.Player.InstantBuild)
            {
                base.Update();
                return;
            }

            _iconLog.color = _white;
            if (Input.GetButton("InstantBuild"))
            {
                for (var i = 0; i < _target.CalcMissingRepairLogs(); i++)
                {
                    _target.AddRepairMaterial(true);
                }
                for (var i = 0; i < _target.CalcMissingRepairMaterial(); i++)
                {
                    _target.AddRepairMaterial(false);
                }
            }
        }
    }
}