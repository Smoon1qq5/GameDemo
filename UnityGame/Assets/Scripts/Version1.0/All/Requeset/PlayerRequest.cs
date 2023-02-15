using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��Ҽ��뷿�� ϵͳ������Ϣ��ÿ�����  �����������������Ϣ
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
