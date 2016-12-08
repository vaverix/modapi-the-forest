using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenuPlus
{
    class TCoopCut : TreeCutChunk
    {
        protected override void Hit(float amount)
        {
            if (!CheatMenuPlusComponent.InstantTree)
            {
                base.Hit(amount);
                return;
            }

            /*
            for (int i = 0; i < 20; i++)
            {
                this.Delay = false;
                base.Hit(amount);
            }
            */

            if (BoltNetwork.isRunning)
            {
                //UnityEngine.Object.Instantiate(this.WoodParticle, base.transform.position, base.transform.rotation);
                try
                {
                    for (int damageIndex = 1; damageIndex <= 4; damageIndex++)
                    {
                        for (int damage = 0; damage < 4; damage++)
                        {
                            DamageTree tree = DamageTree.Raise(Bolt.GlobalTargets.OnlyServer);
                            tree.TreeEntity = base.GetComponentInParent<TreeHealth>().LodEntity;
                            //tree.DamageIndex = int.Parse(base.transform.parent.gameObject.name);
                            tree.DamageIndex = damageIndex;
                            tree.Send();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                this.MyTree.SendMessage("CheatMenuPlusCutDown");
            }
        }
    }
}
