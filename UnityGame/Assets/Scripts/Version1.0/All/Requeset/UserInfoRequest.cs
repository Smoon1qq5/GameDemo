using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoRequest : BaseRequest
{
    private Mainpack mainpack;
    UserInfoPanel userinfoPanel;    
    protected override void Init()
    {
        actionCode = ActionCode.CheckInfo;
        request = Request.User;
        userinfoPanel = GetComponent<UserInfoPanel>();
        base.Init();
    }


    private void Update()
    {
        if (mainpack != null)
        {
            userinfoPanel.OnResponse(mainpack);
            mainpack = null;
        }
    }
   

    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
        Debug.Log("����ID��" + pack.Loginpack.Playerindex + "����ȼ�" + pack.Loginpack.Playerlevel);
    }
    public  void SendRequest()
    {
     
        Mainpack pack = new Mainpack();
        pack.Actioncode = actionCode;
        pack.Request = request;
        pack.Description = GameLifeCycle.Instance.username;
        base.SendRequest(pack);
    }
}
