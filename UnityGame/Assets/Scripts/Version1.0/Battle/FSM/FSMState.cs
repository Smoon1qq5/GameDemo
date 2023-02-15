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
            //添加映射
            map.Add(triggerID, stateID);
            //创建条件对象
            CreatTrigger(triggerID);
        }
        //检测当前状态的条件是否满足
        public void Reason(FSMBase fsm)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                if (triggers[i].HandleTrigger(fsm))
                {
                    //切换状态
                    FSMStateID stateID = map[triggers[i].TriggerID];
                    fsm.ChangeActiveState(stateID);
                    return;
                }
            }
        }

        private void CreatTrigger(FSMTriggerID triggerID)
        {
            //创建条件对象
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
