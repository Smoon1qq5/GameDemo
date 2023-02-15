using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class ReachTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            if (fsm.targetTF == null) return false;
           return  Vector3.Distance(fsm.transform.position, fsm.targetTF.position) <= fsm.status.attackDistance;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.ReachTarget;
        }
    }
}
