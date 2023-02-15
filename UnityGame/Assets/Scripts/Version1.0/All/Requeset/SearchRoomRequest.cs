using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchRoomRequest : BaseRequest
{
    public RoomListPanel roomListPanel;
    private Mainpack mainpack;
    protected override void Init()
    {
        base.Init();
        request = Request.Room;
        actionCode = ActionCode.SearchRoom;
       
    }
    public  void SendRequest()
    {
        Mainpack pack = new Mainpack() { Request=request,Actioncode=actionCode,Description="r"};


        base.SendRequest(pack);
    }
    private void Update()
    {
        if (mainpack != null)
        {
            roomListPanel.SearchRoomResponse(mainpack);
            mainpack = null;
        }
    }
   

    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }
}
