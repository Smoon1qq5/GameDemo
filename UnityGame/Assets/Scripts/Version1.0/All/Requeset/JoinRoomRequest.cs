using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomRequest : BaseRequest
{
    private Mainpack mainpack = null;
    public RoomListPanel roomListPanel;
    protected override void Init()
    {
        actionCode = ActionCode.JoinRoom;
        request = Request.Room;
        base.Init();
    }

    private void Update()
    {
        if (mainpack != null)
        {
            Debug.Log("�������·��İ���������������" + mainpack.Playerpack.Count);
            foreach (var item in mainpack.Playerpack)
            {
                Debug.Log("������ƣ�"+item.PlayerName); 
            }
            
            roomListPanel.JoinRoomResponse(mainpack);
            mainpack = null;
        }
    }
   

    public  void SendRequest(string roomname)
    {
        PlayerPack playerPack = new PlayerPack();    
        Mainpack pack = new Mainpack() { Actioncode=actionCode,Request=request,Description=roomname};
        pack.Playerpack.Add(playerPack);    

        base.SendRequest(pack);
    }
    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }

}
