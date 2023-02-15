using System.Collections;
using System.Collections.Generic;
using ns;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class KilledTargetTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.targetTF.GetComponent<CharacterStatus>().hp <= 0;
        }

        public override void Init()
        {
            TriggerID = FSMTriggerID.KilledTarget;
        }
    }
}
