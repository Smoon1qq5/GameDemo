using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
namespace ns
{
   ///<summary>
   ///
   ///</summary>
   public class EnemyStatus : CharacterStatus
   {
        public override void Damage(float val)
        {
            base.Damage(val);
            val -= baseDefence;
            if (val > 0)
            {
                hp -= val;
                Stun();
            }

            if (hp <= 0)
                Death();
        }

        private void Stun()
        {
            //GetComponentInChildren<Animator>().SetBool(chParams.stun, true);
        }

        private void Death()
        {
            GetComponentInChildren<Animator>().SetBool(chParams.death, true);
           
        }
    }
}
