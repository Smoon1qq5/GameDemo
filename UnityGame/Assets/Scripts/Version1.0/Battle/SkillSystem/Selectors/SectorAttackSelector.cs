using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class SectorAttackSelector : IAttackSector
    {
        public Transform[] SelectTarget(SkillData data, Transform skillTF)
        {
            //①根据技能数据中的标签 获取所有目标
            //data.attackTargetTags --> GameObject
            List<Transform> targets = new List<Transform>();
            if (data.attackTargetTags == null) return null;
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {

                GameObject[] games = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
                targets.AddRange(games.Select(s => s.transform));
            }

            //②筛选攻击范围（扇形 / 圆形）
            //data.attackDistance
            //data.attackAngle 

            targets = targets.FindAll(g =>
               Vector3.Distance(g.position, skillTF.position) <= data.attackDistance &&
               Vector3.Angle(skillTF.forward, g.position - skillTF.position) <= data.attackAngle / 2);
            //③筛选出活的角色
            targets = targets.FindAll(s => s.GetComponent<CharacterStatus>().hp > 0);

            //④返回目标（单攻 / 群攻）
            Transform []  result= targets.ToArray();
            if (data.attackType == SkillAttackType.Group||result.Length==0)
            {
                return result;
            }
            else //⑤距离最近（小）的敌人
            {
             Transform min = result.GetMin(g => Vector3.Distance(g.position, skillTF.position));
                return new Transform[] { min };
            }

        }
    }
}
