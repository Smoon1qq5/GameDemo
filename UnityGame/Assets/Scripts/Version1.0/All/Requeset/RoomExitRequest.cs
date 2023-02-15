using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomExitRequest : BaseRequest
{
    private bool  is_exit = false;

    public RoomPanel roomPanel;
    protected override void Init()
    {
        actionCode = ActionCode.ExitRoom;
        request = Request.Room;
        base.Init();
    }
    private void Update()
    {
        if (is_exit != false)
        {

            roomPanel.ExitRoomResponse();
            is_exit = false;
        }
    }
    
    public override void OnResponse(Mainpack pack)
    {
        is_exit = true;
    }
    public  void SendRequest()
    {
        Mainpack pack = new Mainpack() { Actioncode = actionCode, Request= request, Description = "r" };

        base.SendRequest(pack);
    }
}
