using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class CostSPImpact : IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
            var status = deployer.SkillData.owner.GetComponent<CharacterStatus>();
            status.sp -= deployer.SkillData.costSP;
        }
    }
}
