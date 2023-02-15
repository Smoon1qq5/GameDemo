using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 玩家加入房间 系统发送消息给每个玩家  用来更新组队人数信息
/// </summary>
public class PlayerRequest : BaseRequest
{
    private Mainpack mainpack = null;
    public RoomPanel roomPanel;
    protected override void Init()
    {
        actionCode = ActionCode.UpdateRoomPlayer;
        base.Init();
    }
    private void Update()
    {
        if (mainpack != null)
        {
            roomPanel.UpdateRoomPlayerList(mainpack);
            mainpack = null;
        }
    }
   
    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }

}
