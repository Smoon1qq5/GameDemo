using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class SawTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.targetTF != null;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.SawTarget;
        }

    }
}
