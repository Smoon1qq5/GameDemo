using System.Collections;
using System.Collections.Generic;
using ns;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class NoHealthTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.status.hp <= 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.NoHealth;
        }
    }
}
