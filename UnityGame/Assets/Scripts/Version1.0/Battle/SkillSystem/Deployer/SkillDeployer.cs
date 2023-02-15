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
        //初始化释放器
        private void InitDeployer()
        {
            //a.根据技能管理器传递的数据 创建算法对象 编译的时候并不知道具体数值，所以需要反射创建算法对象
            //string className = string.Format("ns.{0}AttackSelector", skillData.selectorType);
            ////Type type = Type.GetType(className);
            ////IAttackSector sector = Activator.CreateInstance(type) as IAttackSector;
            //sector = CreateObject<IAttackSector>(className);
            //b.选区算法  （选区对象命名规则）
            //skillData.selectorType

            sector = DeployerConfigFactory.CreateAttackSelector(SkillData);

            //c.影响算法 影响效果命名规范）
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






        //执行算法对象
        public void CalculateTargets()
        {
          skillData.attackTargets=  sector.SelectTarget(skillData, transform);
        }




        //释放方式
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

