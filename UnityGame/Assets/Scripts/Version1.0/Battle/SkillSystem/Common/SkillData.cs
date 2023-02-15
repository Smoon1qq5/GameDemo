using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns

{
   ///<summary>
   ///
   ///</summary>
    
    [Serializable]
   public class SkillData
   {
        public int skillID;
        public string name;
        public string description;
        public int coolTime;
        public int coolRemain;
        public int costSP;
        public float attackDistance;
        public float attackAngle;
        public string[] attackTargetTags = { "Enemy" };
        [HideInInspector]
        public Transform[] attackTargets;
        public string[] impactType = { "CostSP", "DamageImpact" };
        public int nextBatterId;
        public float atkRatio;
        public float durationTime;
        public float atkInterval;
        [HideInInspector]
        public GameObject owner;
        public string prefabName;
        [HideInInspector]
        public GameObject skillPrefab;
        public string animationName;
        public string hitFxPrefab;
        public int level;
        public SkillAttackType attackType;
        public SelectorType selectorType;
   }
}
