using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public enum FSMStateID
    {
        /// <summary>
        /// �����ڸ�״̬
        /// </summary>
        None,
        /// <summary>
        /// Ĭ��
        /// </summary>
        Default,
        /// <summary>
        /// ����
        /// </summary>
        Dead,
        /// <summary>
        /// ����
        /// </summary>
        Idle,
        /// <summary>
        /// ׷��
        /// </summary>
        Pursuit,
        /// <summary>
        /// ����
        /// </summary>
        Attacking,
        /// <summary>
        /// Ѳ��
        /// </summary>
        Patrolling
    }
}
