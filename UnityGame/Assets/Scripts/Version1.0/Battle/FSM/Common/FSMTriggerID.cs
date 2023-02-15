using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
   ///<summary>
   ///
   ///</summary>
   public enum FSMTriggerID 
   {
        /// <summary>
        /// û������
        /// </summary>
        NoHealth,
        /// <summary>
        /// ����Ŀ��
        /// </summary>
        SawTarget,
        /// <summary>
        /// ����Ŀ��
        /// </summary>
        ReachTarget,
        /// <summary>
        /// Ŀ�걻��ɱ
        /// </summary>
        KilledTarget,
        /// <summary>
        /// ����������Χ
        /// </summary>
        WithoutAttackRange,
        /// <summary>
        /// ��ʧĿ��
        /// </summary>
        LoseTarget,
        /// <summary>
        /// ���Ѳ��
        /// </summary>
        CompletePatrol

    }
}
