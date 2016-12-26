using System.Reflection;
using UnityEngine;

namespace CheatMenuPlus
{
    class CraftStructureCtrl : TheForest.Buildings.Creation.Craft_Structure
    {
        [ModAPI.Attributes.Priority(1000)]
        protected override void AddIngredient(int ingredientNum)
        {
            if (CheatMenuPlusCtrl.Options.Player.InstantBuild)
            {
                BuildIngredients ingredients = _requiredIngredients[ingredientNum];
                if (BoltNetwork.isRunning)
                {
                    AddIngredient ingredient = new AddIngredient();
                    typeof(Bolt.Event).GetField("Targets", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ingredient, (byte)Bolt.GlobalTargets.OnlyServer);
                    typeof(Bolt.Event).GetField("TargetConnection", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ingredient, null);
                    typeof(Bolt.Event).GetField("Reliability", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ingredient, Bolt.ReliabilityModes.ReliableOrdered);

                    ingredient.IngredientNum = ingredientNum;
                    ingredient.Construction = base.GetComponentInParent<BoltEntity>();
                    ingredient.Send();
                }
                else
                {
                    base.AddIngrendient_Actual(ingredientNum, true);
                }
            }
            else
            {
                base.AddIngredient(ingredientNum);
            }
        }

        [ModAPI.Attributes.Priority(1000)]
        protected override void Update()
        {
            if (CheatMenuPlusCtrl.Options.Player.InstantBuild && ModAPI.Input.GetButtonDown("InstantBuild"))
            {
                TheForest.Utils.LocalPlayer.Sfx.PlayHammer();
                TheForest.Utils.Scene.HudGui.AddIcon.SetActive(false);
                for (int i = 0; i < _requiredIngredients.Count; i++)
                {
                    int left = (_requiredIngredients[i]._amount - _presentIngredients[i]._amount);
                    for (int n = 0; n < left; n++)
                    {
                        AddIngredient(i);
                    }

                }
                return;
            }
            base.Update();
            if (_initialized && CheatMenuPlusCtrl.Options.Player.InstantBuild)
            {
                for (int i = 0; i < _requiredIngredients.Count; i++)
                {
                    if (_requiredIngredients[i]._amount != _presentIngredients[i]._amount)
                    {
                        BuildIngredients ingredients = _requiredIngredients[i];
                        TheForest.Items.Craft.ReceipeIngredient ingredient = _presentIngredients[i];
                        if (ingredients._amount > ingredient._amount)
                        {
                            if (!CheatMenuPlusCtrl.Options.Player.InstantBuild && !TheForest.Utils.LocalPlayer.Inventory.Owns(_requiredIngredients[i]._itemID))
                            {
                                ingredients._icon.GetComponent<GUITexture>().color = ColorRed;
                            }
                            else
                            {
                                ingredients._icon.GetComponent<GUITexture>().color = ColorGrey;
                                TheForest.Utils.Scene.HudGui.AddIcon.SetActive(true);
                                if (TheForest.Utils.Input.GetButtonDown("Take"))
                                {
                                    if (CheatMenuPlusCtrl.Options.Player.InstantBuild)
                                    {
                                        TheForest.Utils.LocalPlayer.Sfx.PlayHammer();
                                        TheForest.Utils.Scene.HudGui.AddIcon.SetActive(false);
                                    }
                                    AddIngredient(i);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
