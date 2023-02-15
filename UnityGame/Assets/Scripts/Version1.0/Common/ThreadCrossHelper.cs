using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
   ///<summary>
   ///
   ///</summary>
   public class ThreadCrossHelper : MonoSingleton<ThreadCrossHelper>
    {
        class DelayItem
        {
            public Action currentAction { get; set; }
            public DateTime Delay { get; set; }
        }
        private  List<DelayItem> actionList;

        public override void Init()
        {
            base.Init();
            actionList = new List<DelayItem>();
        }
        private void Update()
        {
            lock (actionList)
            {
                for (int i = actionList.Count - 1; i >= 0; i--)
                {
                    if (actionList[i].Delay <= DateTime.Now)
                    {
                        actionList[i].currentAction();
                        actionList.RemoveAt(i);
                    }
                }
            }
        }
        public void ExecuteOnMainThread(Action action, float delay = 0)
        {
            lock (actionList)
            {
                var item = new DelayItem()
                {
                    currentAction = action,
                    Delay = DateTime.Now.AddSeconds(delay)
                };
                actionList.Add(item);
            }
        }

   }
}
