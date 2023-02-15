using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public abstract class FSMState
    {


        private Dictionary<FSMTriggerID, FSMStateID> map;
        private List<FSMTrigger> triggers;
        public FSMStateID StateID { get; set; }
        public FSMState()
        {
            Init();
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            triggers = new List<FSMTrigger>();
        }

        public void AddMap(FSMTriggerID triggerID, FSMStateID stateID)
        {
            //���ӳ��
            map.Add(triggerID, stateID);
            //������������
            CreatTrigger(triggerID);
        }
        //��⵱ǰ״̬�������Ƿ�����
        public void Reason(FSMBase fsm)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                if (triggers[i].HandleTrigger(fsm))
                {
                    //�л�״̬
                    FSMStateID stateID = map[triggers[i].TriggerID];
                    fsm.ChangeActiveState(stateID);
                    return;
                }
            }
        }

        private void CreatTrigger(FSMTriggerID triggerID)
        {
            //������������
            Type type = Type.GetType("FSM." + triggerID + "Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            triggers.Add(trigger);
        }

        public abstract void Init();


        public virtual void ActionState(FSMBase fsm) { }

        public virtual void EnterState(FSMBase fsm) { }
        public virtual void ExitState(FSMBase fsm) { }

    }
}
