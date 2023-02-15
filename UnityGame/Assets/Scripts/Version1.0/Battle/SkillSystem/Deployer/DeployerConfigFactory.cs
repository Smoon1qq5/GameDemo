using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class DeployerConfigFactory
    {
        //º”…œª∫¥Ê
        private static Dictionary<string, object> cache;

        static DeployerConfigFactory()
        {
            cache = new Dictionary<string, object>();
        }
        public static IAttackSector CreateAttackSelector(SkillData skillData)
        {
            string className = string.Format("ns.{0}AttackSelector", skillData.selectorType);
     
            return CreateObject<IAttackSector>(className);
        }


        public static IImpactEffect[] CreateImpactEffects(SkillData skillData)
        {
            IImpactEffect[] impacts = new IImpactEffect[skillData.impactType.Length];
            for (int i = 0; i < skillData.impactType.Length; i++)
            {

                string classNameImpact = string.Format("ns.{0}Impact", skillData.impactType[i]);
                impacts[i] = CreateObject<IImpactEffect>(classNameImpact);
            }
            return impacts;
        }
        private static T CreateObject<T>(string name) where T : class
        {
            if (!cache.ContainsKey(name))
            {

            Type type = Type.GetType(name);
            object  result= Activator.CreateInstance(type) ;
                cache.Add(name, result);
            }
            return cache[name] as T;
        }

    }
}
