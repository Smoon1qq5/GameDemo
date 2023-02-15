using ns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//远端角色 更新位置状态
public class RemoteCharacter : MonoBehaviour
{
    private Transform selfTF;

    private Vector3 selfPos;
    private Quaternion selfAngle;
    private Animator anim;
    private CharacterStatus status;
    private SkillSystem skillSystem;
    public void SetState(Vector3 vector, float angel)
    {
        selfPos = vector;
        selfAngle = Quaternion.Euler(0, angel, 0);
        SetAnim();
    }

    void Start()
    {
        selfTF = transform;
        selfAngle = selfTF.rotation;
        selfPos = selfTF.position;
        anim = GetComponentInChildren<Animator>();
        status = GetComponent<PlayerStatus>();
        PlayerManager.Instance.remoteCharacter = this;
        skillSystem=GetComponent<SkillSystem>();    
    }


    void Update()
    {
        selfTF.position = Vector3.Lerp(selfTF.position, selfPos, 0.05f);
        selfTF.rotation = Quaternion.Slerp(selfTF.rotation, selfAngle, 0.1f);
        
    }
    void LateUpdate()
    {
       // SetAnim();
    }

    //运动距离大于一个值就播放动画
    public void SetAnim()
    {
        if (Vector3.Distance(selfTF.position, selfPos) > 0.25)
        {
            //开启动画
            anim.SetBool(status.chParams.run, true);
        }
        else
        {
            //关闭动画
            anim.SetBool(status.chParams.run, false);
        }
    }
    public void SetSkill(string buttonName)
    {
        int id = 0;
        switch (buttonName)
        {
            case "BaseSkill":
                id = 1001;
                break;
            case "CircleAttackSkill":
                id = 1003;
                break;
            case "SectorAttackSkill":
                id = 1002;
                break;

        }
        skillSystem.AttackUseSkill(id); 
        //switch (buttonName)
        //{
        //    case "BaseSkill":
        //        anim.SetBool("attack01", true);
        //        break;
        //    case "SectorAttackSkill":
        //        anim.SetBool("attack02", true);

        //        break;
        //    case "CircleAttackSkill":
        //        anim.SetBool("attack03", true);
        //        break;

        //}

    }
}
