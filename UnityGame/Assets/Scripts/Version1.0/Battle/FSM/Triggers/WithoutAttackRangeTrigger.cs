using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class WithoutAttackRangeTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return Vector3.Distance(fsm.transform.position, fsm.targetTF.position) > fsm.status.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.WithoutAttackRange;
        }
    }
}
