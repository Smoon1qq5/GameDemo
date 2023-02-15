using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
   ///<summary>
   ///�����¼���Ϊ��
   ///</summary>
   public class AnimationEventBehaviour : MonoBehaviour
   {
        private Animator anim;

        public event Action attackHandler;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }


        private void OnCancelAnim(string animParam)
        {
            anim.SetBool(animParam, false);

        }

        private void OnAttack()
        {
            if (attackHandler != null)
            {
                attackHandler();
            }
        }
    }
}
