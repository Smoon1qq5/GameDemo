using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlayerRequest : BaseRequest
{
    private SelectPlayerPanel playerPanel;
    private Mainpack mainpack;
    protected override void Init()
    {
        actionCode = ActionCode.ChosePlayer;
        request = Request.User;
        playerPanel = GetComponent<SelectPlayerPanel>();

        base.Init();
    }

    public void SendRequest()
    {
        LoginPack playerInfo = new LoginPack() { Playerindex = playerPanel.playerIndex ,Username=GameLifeCycle.Instance.username};
        Mainpack pack = new Mainpack() { Loginpack = playerInfo ,Actioncode=actionCode,Request=request};

       
        base.SendRequest(pack);
    }
    private void Update()
    {
        if (mainpack != null)
        {

            playerPanel.OnResponse(mainpack);
            mainpack = null;
        }
    }
  
    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }
}
