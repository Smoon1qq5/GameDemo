using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Common;
using Common;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class SkillManager : MonoBehaviour
    {
        public SkillData[] skills;
       
      

        private void Start()
        {
            for (int i = 0; i <skills.Length ; i++)
            {
                InitSkill(skills[i]);
            }
        }
        private void InitSkill(SkillData data)
        {
            //ͨ�����ֲ��Ҽ��ؼ���Ԥ�Ƽ�
            //data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);

            data.owner = gameObject;
        }

        public SkillData PraperSkill(int id)
        {
            //�����ͷ���������ȴ��������
            float sp = GetComponent<CharacterStatus>().sp;
            //����ID���Ҽ�������
            SkillData skillData = skills.Find(s => s.skillID == id);
            //�ж�����  ���ؼ�������
            if (skillData != null && sp >= skillData.costSP && skillData.coolRemain <= 0) return skillData;

            return null;
        }

        public void GenerateSkill(SkillData skill)
        {
            //��������Ԥ�Ƽ�
            //GameObject obj=   Instantiate(skill.skillPrefab, transform.position, transform.rotation);
            GameObject skillGo = GameobjectPool.Instance.CreateObject(skill.prefabName, skill.skillPrefab, transform.position, transform.rotation);

            //���ݼ������� 
            SkillDeployer skillDeployer = skillGo.GetComponent<SkillDeployer>();
            skillDeployer.SkillData = skill;
            skillDeployer.DeploySkill();//ִ���㷨
            
            
            //���ټ���
            //Destroy(obj, skill.durationTime);
            GameobjectPool.Instance.Collect(skillGo, skill.durationTime);
            //������ȴ
            StartCoroutine(SkillCoolDown(skill));
        }
        private IEnumerator SkillCoolDown(SkillData data)
        {
            data.coolRemain = data.coolTime;
            while (data.coolRemain > 0)
            {
            yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
        }
    }
}
