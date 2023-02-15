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
        [Tooltip("动画参数")]
        public CharacterAnimationParameter chParams;
        [Tooltip("血量")]
        public float hp;
        [Tooltip("最大血量")]
        public float maxHp;
        [Tooltip("法力")]
        public float sp;
        [Tooltip("最大法力")]
        public float maxSp;
        [Tooltip("基础攻击力")]
        public float baseATK;
        [Tooltip("基础防御")]
        public float baseDefence;
        [Tooltip("攻击间隔")]
        public float attackInterval;
        [Tooltip("攻击距离")]
        public float attackDistance;

        public  virtual void Damage(float val) { }
    }
}
