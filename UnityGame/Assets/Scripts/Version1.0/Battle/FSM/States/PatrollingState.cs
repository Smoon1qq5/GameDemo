using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class PatrollingState : FSMState
    {
        public override void Init()
        {
            StateID = FSMStateID.Patrolling;
        }
        public override void EnterState(FSMBase fsm)
        {
            base.EnterState(fsm);
            fsm.isPatrolComplete = false;
            
            fsm.anim.SetBool(fsm.status.chParams.run, true);
        }
        public override void ExitState(FSMBase fsm)
        {
            base.ExitState(fsm);
            fsm.anim.SetBool(fsm.status.chParams.run, false);
        }
        public override void ActionState(FSMBase fsm)
        {
            base.ActionState(fsm);
            //根据巡逻模式
            //--单次A B C
            //--循环A B C A B C
            //--往返A B C B A B C
            switch (fsm.patrolMode)
            {
                case PatrolMode.Once:
                    OncePatrolling(fsm);
                    break;
                case PatrolMode.Loop:
                    LoopPatrolling(fsm);
                    break;
                case PatrolMode.PingPang:
                    PingPangPatrolling(fsm);
                    break;

            }
        }
        private int index = 0;
        private void OncePatrolling(FSMBase fsm)
        {
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 0.5f)
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    fsm.isPatrolComplete = true;
                    return;
                }

                index++;
            }
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }
        private void LoopPatrolling(FSMBase fsm)
        {
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 0.5f)
            {

                index = (index + 1) % fsm.wayPoints.Length;
            }
            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);

        }


        private void PingPangPatrolling(FSMBase fsm)
        {
            if (Vector3.Distance(fsm.transform.position, fsm.wayPoints[index].position) < 0.5f)
            {
                if (index == fsm.wayPoints.Length - 1)
                {
                    Array.Reverse(fsm.wayPoints);
                    index++;
                }
                index = (index + 1) % fsm.wayPoints.Length;
            }

            fsm.MoveToTarget(fsm.wayPoints[index].position, 0, fsm.walkSpeed);
        }

    }
}
