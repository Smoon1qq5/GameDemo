using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomRequest : BaseRequest
{
    Mainpack mainpack = null;
    public RoomListPanel roomListPanel;
    protected override void Init()
    {
        actionCode = ActionCode.CreateRoom;
        request = Request.Room;
        base.Init();
  
    }

    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }
    private void Update()
    {
        if (mainpack != null)
        {
            roomListPanel.CreateRoomResponse(mainpack);
            mainpack = null;
        }
    }
   

    internal void SendRequest(string text, int value)
    {
        RoomPack roomPack = new RoomPack() { Roomname=text, Maxnum=value };
        Mainpack pack = new Mainpack() { Actioncode=actionCode,Request=request};     
        pack.Roompack.Add(roomPack);
        base.SendRequest(pack);
    }
}
