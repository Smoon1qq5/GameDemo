using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns

{
    ///<summary>
    ///
    ///</summary>

    [System.Serializable]//可以序列化
    public class CharacterAnimationParameter
    {
        public string run = "run";
        public string idle = "idle";
        public string death = "death";
        public string attack1 = "attack1";
        public string attack2 = "attack2";
        public string attack3 = "attack3";
        public string walk = "walk";
        public string stun = "stun";
    }
}