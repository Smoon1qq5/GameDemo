using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptMissionRequest : BaseRequest
{
    protected override void Init()
    {
        actionCode = ActionCode.MissionCode;
        request = Request.User;
        MissionManager.Instance.missionRequest = this;
        base.Init();
    }
    
    public override void OnResponse(Mainpack pack)
    {
        //回复完成任务
        if (pack.Respone == Response.Succeed)
        {
            MissionManager.Instance._Flag_isFinish = true;
        }
    }

    public void SendRequest(MissionData data, int count=0)
    {
        Mainpack pack = new Mainpack();
      
        MissionPack missionPack = new MissionPack()
        {
            Des = data.description,
            Missiongoodstype = (MissionGoodsType)data.require.goodsType,
            Count = count,
            MissionID = data.id
        };
        pack.Actioncode = actionCode;
        pack.Request = request;
        pack.Missionpack = missionPack;
    

        Debug.Log("已向服务器发送接受任务的请求");
        base.SendRequest(pack);
    }
}
