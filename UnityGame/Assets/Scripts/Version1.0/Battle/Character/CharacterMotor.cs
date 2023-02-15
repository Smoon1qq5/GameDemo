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

        [Tooltip("��ת�ٶ�")]
        public float rotateSpeed = 20;
        [Tooltip("�ƶ��ٶ�")]
        public float moveSpeed = 2;
        private CharacterController controller;
        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }
        //��ת  ע�ӷ�����ת
        public void LookAtTarget(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            Quaternion lookDir = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, rotateSpeed * Time.deltaTime);

        }
        //�ƶ�
        public void Movement(Vector3 direction)
        {
            LookAtTarget(direction);
            //��ǰ�ƶ� CharacterController Move
            //
            Vector3 forward = transform.forward;
            forward.y = -1;//�൱������

            controller.Move(forward * Time.deltaTime * moveSpeed);

        }
    }
}
