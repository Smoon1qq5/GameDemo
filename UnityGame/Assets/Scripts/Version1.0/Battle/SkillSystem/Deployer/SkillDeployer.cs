using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;
        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                InitDeployer();
            }
        }

        private IAttackSector sector;
        private IImpactEffect[] impacts;
        //��ʼ���ͷ���
        private void InitDeployer()
        {
            //a.���ݼ��ܹ��������ݵ����� �����㷨���� �����ʱ�򲢲�֪��������ֵ��������Ҫ���䴴���㷨����
            //string className = string.Format("ns.{0}AttackSelector", skillData.selectorType);
            ////Type type = Type.GetType(className);
            ////IAttackSector sector = Activator.CreateInstance(type) as IAttackSector;
            //sector = CreateObject<IAttackSector>(className);
            //b.ѡ���㷨  ��ѡ��������������
            //skillData.selectorType

            sector = DeployerConfigFactory.CreateAttackSelector(SkillData);

            //c.Ӱ���㷨 Ӱ��Ч�������淶��
            //for (int i = 0; i < skillData.impactType.Length; i++)
            //{

            //    string classNameImpact = string.Format("ns.{0}Impact", skillData.impactType[i]);
            //    impacts[i] = CreateObject<IImpactEffect>(classNameImpact);
            //}
            impacts = DeployerConfigFactory.CreateImpactEffects(SkillData);
        }

        //private T CreateObject<T>(string name) where T : class
        //{
        //    Type type = Type.GetType(name);
        //    return Activator.CreateInstance(type) as T;
        //}






        //ִ���㷨����
        public void CalculateTargets()
        {
          skillData.attackTargets=  sector.SelectTarget(skillData, transform);
        }




        //�ͷŷ�ʽ
        public  void ImpactTargets()
        {
            for (int i = 0; i < impacts.Length; i++)
            {
                impacts[i].Execute(this);
            }
        }

        public abstract void DeploySkill();
    }
}

