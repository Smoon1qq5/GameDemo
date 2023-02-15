using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///封装技能系统，提供简单的技能释放功能
    ///</summary>
    [RequireComponent(typeof(SkillManager))]
    public class SkillSystem : MonoBehaviour
    {
        private SkillManager manager;
        private Animator anim;

        private void Start()
        {
            manager = GetComponent<SkillManager>();
            anim = GetComponentInChildren<Animator>();
            GetComponentInChildren<AnimationEventBehaviour>().attackHandler += DelpoySkill;
        }

        private void DelpoySkill()
        {
            if(skill!=null)
            manager.GenerateSkill(skill);
        }

        //准备技能


        //播放动画  动画播放至某一时间 生成技能
        //如果单攻（朝向目标 选中目标）

        //动作队列
        private SkillData skill;
        public void AttackUseSkill(int skillID)
        {
             skill = manager.PraperSkill(skillID);
            if (skill == null) return;
                    

            //播放动画 圆形...点了普攻...生成普攻技能  播放动画普攻  生成技能普攻
            anim.SetBool(skill.animationName, true);
        }



        public void UseRandomSkill()
        {
            //从 管理器 中挑选出随机的技能
            //--先筛选出所有可以释放的技能，再产生随机数
          var useableSkills=  manager.skills.FindAll(s => manager.PraperSkill(s.skillID) != null);
            if (useableSkills.Length == 0) return;
            int index = UnityEngine.Random.Range(0, useableSkills.Length);
            AttackUseSkill(useableSkills[index].skillID);
        }

    }
}
