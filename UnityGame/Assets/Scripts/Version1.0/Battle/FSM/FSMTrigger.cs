using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
   ///<summary>
   ///
   ///</summary>
   public abstract class FSMTrigger : MonoBehaviour
   {
        public  FSMTriggerID TriggerID { get; set; }

       public  FSMTrigger()
        {
            Init();
        }

        public abstract void Init();
       

        //触发条件  逻辑处理
        public abstract bool HandleTrigger(FSMBase fsm);
   }
}
