using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenuPlus
{
    class THealth : TreeHealth
    {
        public void CheatMenuPlusCutDown()
        {
            TreeCutChunk[] chunks = this.transform.GetComponentsInChildren<TreeCutChunk>();
            foreach (TreeCutChunk chunk in chunks)
                Destroy(chunk.transform.parent.gameObject);
            base.DoFallTree();

        }
        /*
        public override void DoSpawnCutTree()
        {
            base.DoSpawnCutTree();
        }
        */
    }
}