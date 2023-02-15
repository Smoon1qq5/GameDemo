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
            //�ٸ��ݼ��������еı�ǩ ��ȡ����Ŀ��
            //data.attackTargetTags --> GameObject
            List<Transform> targets = new List<Transform>();
            if (data.attackTargetTags == null) return null;
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {

                GameObject[] games = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
                targets.AddRange(games.Select(s => s.transform));
            }

            //��ɸѡ������Χ������ / Բ�Σ�
            //data.attackDistance
            //data.attackAngle 

            targets = targets.FindAll(g =>
               Vector3.Distance(g.position, skillTF.position) <= data.attackDistance &&
               Vector3.Angle(skillTF.forward, g.position - skillTF.position) <= data.attackAngle / 2);
            //��ɸѡ����Ľ�ɫ
            targets = targets.FindAll(s => s.GetComponent<CharacterStatus>().hp > 0);

            //�ܷ���Ŀ�꣨���� / Ⱥ����
            Transform []  result= targets.ToArray();
            if (data.attackType == SkillAttackType.Group||result.Length==0)
            {
                return result;
            }
            else //�ݾ��������С���ĵ���
            {
             Transform min = result.GetMin(g => Vector3.Distance(g.position, skillTF.position));
                return new Transform[] { min };
            }

        }
    }
}
