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
            //通过名字查找加载技能预制件
            //data.skillPrefab = Resources.Load<GameObject>("Skill/" + data.prefabName);
            data.skillPrefab = ResourceManager.Load<GameObject>(data.prefabName);

            data.owner = gameObject;
        }

        public SkillData PraperSkill(int id)
        {
            //技能释放条件（冷却、法力）
            float sp = GetComponent<CharacterStatus>().sp;
            //根据ID查找技能数据
            SkillData skillData = skills.Find(s => s.skillID == id);
            //判断条件  返回技能数据
            if (skillData != null && sp >= skillData.costSP && skillData.coolRemain <= 0) return skillData;

            return null;
        }

        public void GenerateSkill(SkillData skill)
        {
            //创建技能预制件
            //GameObject obj=   Instantiate(skill.skillPrefab, transform.position, transform.rotation);
            GameObject skillGo = GameobjectPool.Instance.CreateObject(skill.prefabName, skill.skillPrefab, transform.position, transform.rotation);

            //传递技能数据 
            SkillDeployer skillDeployer = skillGo.GetComponent<SkillDeployer>();
            skillDeployer.SkillData = skill;
            skillDeployer.DeploySkill();//执行算法
            
            
            //销毁技能
            //Destroy(obj, skill.durationTime);
            GameobjectPool.Instance.Collect(skillGo, skill.durationTime);
            //技能冷却
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
