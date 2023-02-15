using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRequest : BaseRequest
{
    private Mainpack mainpack = null;
    public RoomPanel roomPanel;

    protected override void Init()
    {
        actionCode = ActionCode.Startgame;
        request = Request.Room;
        base.Init();
    }

    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }
    public void SendRequest()
    {
        Mainpack pack = new Mainpack() { Request=request,Actioncode=actionCode,Description="r"};

        base.SendRequest(pack);
    }
    private void Update()
    {
        if (mainpack != null)
        {
            roomPanel.StartGameResponse(mainpack);
            mainpack = null;
        }
    }
  
}
