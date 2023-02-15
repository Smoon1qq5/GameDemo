using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns

{
   ///<summary>
   ///
   ///</summary>
   public class CharacterStatus : MonoBehaviour
   {
        [Tooltip("��������")]
        public CharacterAnimationParameter chParams;
        [Tooltip("Ѫ��")]
        public float hp;
        [Tooltip("���Ѫ��")]
        public float maxHp;
        [Tooltip("����")]
        public float sp;
        [Tooltip("�����")]
        public float maxSp;
        [Tooltip("����������")]
        public float baseATK;
        [Tooltip("��������")]
        public float baseDefence;
        [Tooltip("�������")]
        public float attackInterval;
        [Tooltip("��������")]
        public float attackDistance;

        public  virtual void Damage(float val) { }
    }
}
