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
        /// ²»´æÔÚ¸Ã×´Ì¬
        /// </summary>
        None,
        /// <summary>
        /// Ä¬ÈÏ
        /// </summary>
        Default,
        /// <summary>
        /// ËÀÍö
        /// </summary>
        Dead,
        /// <summary>
        /// ÏÐÖÃ
        /// </summary>
        Idle,
        /// <summary>
        /// ×·Öð
        /// </summary>
        Pursuit,
        /// <summary>
        /// ¹¥»÷
        /// </summary>
        Attacking,
        /// <summary>
        /// Ñ²Âß
        /// </summary>
        Patrolling
    }
}
