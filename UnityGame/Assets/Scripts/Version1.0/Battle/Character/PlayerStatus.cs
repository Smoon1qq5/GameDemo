using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class PlayerStatus : CharacterStatus
    {
        public override void Damage(float val)
        {
            base.Damage(val);
            val -= baseDefence;
            if (val > 0)
                hp -= val;
            if (hp <= 0)
                Death();
        }

        private void Death()
        {
            GetComponentInChildren<Animator>().SetBool(chParams.death, true);
        }
    }
}
