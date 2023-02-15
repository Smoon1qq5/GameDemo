using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class DemageImpact : IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
            deployer.StartCoroutine(RepeatDamage(deployer));
        }
        //重复伤害
        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            float atkTime = 0;
            do
            {
                OnceDamage(deployer.SkillData);
                yield return new WaitForSeconds(deployer.SkillData.atkInterval);
                atkTime += deployer.SkillData.atkInterval;

                deployer.CalculateTargets();
            } while (atkTime < deployer.SkillData.durationTime);

        }

        //单次伤害
        private void OnceDamage(SkillData data )
        {
            //技能攻击力= 攻击比率*基础攻击力
            float atk = data.atkRatio * data.owner.GetComponent<CharacterStatus>().baseATK;
            for (int i = 0; i < data.attackTargets.Length; i++)
            {
                var status = data.attackTargets[i].GetComponent<CharacterStatus>();
                status.Damage(atk);
            }
        }


    }
}
