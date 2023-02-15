using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class PursuitState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Pursuit;
        }
        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            //Ѱ·ȥ׷�� 
            fsm.MoveToTarget(fsm.targetTF.position, fsm.status.attackDistance,fsm.runSpeed);
        }

        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.anim.SetBool(fsm.status.chParams.run, true);
        }
        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.status.chParams.run, false);
            fsm.StopMove();
        }
    }
}
