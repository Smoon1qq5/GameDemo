using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
   ///<summary>
   ///
   ///</summary>
   public class CharacterMotor : MonoBehaviour
   {

        [Tooltip("旋转速度")]
        public float rotateSpeed = 20;
        [Tooltip("移动速度")]
        public float moveSpeed = 2;
        private CharacterController controller;
        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }
        //旋转  注视方向旋转
        public void LookAtTarget(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            Quaternion lookDir = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, rotateSpeed * Time.deltaTime);

        }
        //移动
        public void Movement(Vector3 direction)
        {
            LookAtTarget(direction);
            //向前移动 CharacterController Move
            //
            Vector3 forward = transform.forward;
            forward.y = -1;//相当于重力

            controller.Move(forward * Time.deltaTime * moveSpeed);

        }
    }
}
