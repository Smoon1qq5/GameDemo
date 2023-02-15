using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            CalculateTargets();

            ImpactTargets();
        }
    }
}
