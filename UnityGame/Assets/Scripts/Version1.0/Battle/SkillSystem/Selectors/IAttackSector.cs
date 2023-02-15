using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
   ///<summary>
   ///
   ///</summary>
   public interface IAttackSector 
   {
        Transform[] SelectTarget(SkillData data, Transform skillTF);
   }
}
