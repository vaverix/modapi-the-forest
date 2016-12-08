using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace CheatMenuPlus
{
    class CStructure : TheForest.Buildings.Creation.Craft_Structure
    {
        protected override void AddIngredient(int ingredientNum)
        {
            if (CheatMenuPlusComponent.InstantBuild)
            {
                BuildIngredients ingredients = this._requiredIngredients[ingredientNum];
                //if (TheForest.Utils.LocalPlayer.Inventory.RemoveItem(ingredients._itemID, 1, false))
                TheForest.Utils.LocalPlayer.Inventory.RemoveItem(ingredients._itemID, 1, false);
                //{
                    //TheForest.Utils.LocalPlayer.Sfx.PlayHammer();
                    //TheForest.Utils.Scene.HudGui.AddIcon.SetActive(false);
                    if (BoltNetwork.isRunning)
                    {
                        //AddIngredient ingredient = AddIngredient.Raise(Bolt.GlobalTargets.OnlyServer);
                        //AddIngredient ingredient = new AddIngredient { Targets = (byte)Bolt.GlobalTargets.OnlyServer, TargetConnection = null, Reliability = Bolt.ReliabilityModes.ReliableOrdered };
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
                //}
            }
            else
                base.AddIngredient(ingredientNum);
        }
        
        protected override void Update()
        {            
            if (CheatMenuPlusComponent.InstantBuild && ModAPI.Input.GetButtonDown("InstantBuild"))
            {
                TheForest.Utils.LocalPlayer.Sfx.PlayHammer();
                TheForest.Utils.Scene.HudGui.AddIcon.SetActive(false);
                for (int i = 0; i < this._requiredIngredients.Count; i++)
                {
                    int left = (this._requiredIngredients[i]._amount - this._presentIngredients[i]._amount);
                    for (int n = 0; n < left; n++)
                        this.AddIngredient(i);
                }
            }
            else
            {
                ///////////// base.Update();
                if (this._initialized)
                {
                    this.CheckText();
                    this.CheckNeeded();
                    TheForest.Utils.Scene.HudGui.DestroyIcon.gameObject.SetActive(true);
                    if (TheForest.Utils.Input.GetButtonAfterDelay("Craft", 0.5f))
                    {
                        base.CancelBlueprint();
                    }
                    else
                    {
                        for (int i = 0; i < this._requiredIngredients.Count; i++)
                        {
                            if (this._requiredIngredients[i]._amount != this._presentIngredients[i]._amount)
                            {
                                BuildIngredients ingredients = this._requiredIngredients[i];
                                TheForest.Items.Craft.ReceipeIngredient ingredient = this._presentIngredients[i];
                                if (ingredients._amount > ingredient._amount)
                                {
                                    if (!CheatMenuPlusComponent.InstantBuild && !TheForest.Utils.LocalPlayer.Inventory.Owns(this._requiredIngredients[i]._itemID))
                                    {
                                        ingredients._icon.GetComponent<GUITexture>().color = this.ColorRed;
                                    }
                                    else
                                    {
                                        ingredients._icon.GetComponent<GUITexture>().color = this.ColorGrey;
                                        TheForest.Utils.Scene.HudGui.AddIcon.SetActive(true);
                                        if (TheForest.Utils.Input.GetButtonDown("Take"))
                                        {
                                            if (CheatMenuPlusComponent.InstantBuild)
                                            {
                                                TheForest.Utils.LocalPlayer.Sfx.PlayHammer();
                                                TheForest.Utils.Scene.HudGui.AddIcon.SetActive(false);
                                            }
                                            this.AddIngredient(i);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                /////////////
            }
        }
    }
}
