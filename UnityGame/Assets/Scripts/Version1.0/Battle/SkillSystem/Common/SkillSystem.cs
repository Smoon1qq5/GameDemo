using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///��װ����ϵͳ���ṩ�򵥵ļ����ͷŹ���
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

        //׼������


        //���Ŷ���  ����������ĳһʱ�� ���ɼ���
        //�������������Ŀ�� ѡ��Ŀ�꣩

        //��������
        private SkillData skill;
        public void AttackUseSkill(int skillID)
        {
             skill = manager.PraperSkill(skillID);
            if (skill == null) return;
                    

            //���Ŷ��� Բ��...�����չ�...�����չ�����  ���Ŷ����չ�  ���ɼ����չ�
            anim.SetBool(skill.animationName, true);
        }



        public void UseRandomSkill()
        {
            //�� ������ ����ѡ������ļ���
            //--��ɸѡ�����п����ͷŵļ��ܣ��ٲ��������
          var useableSkills=  manager.skills.FindAll(s => manager.PraperSkill(s.skillID) != null);
            if (useableSkills.Length == 0) return;
            int index = UnityEngine.Random.Range(0, useableSkills.Length);
            AttackUseSkill(useableSkills[index].skillID);
        }

    }
}
