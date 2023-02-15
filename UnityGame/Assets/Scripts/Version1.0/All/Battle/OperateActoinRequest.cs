using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateActoinRequest : BaseRequest
{

    private Mainpack mainpack;
    
    User u ;
    protected override void Init()
    {
        actionCode = ActionCode.OperateActoin;
        request = Request.Game;
        base.Init();
    }
    private void OnEnable()
    {
        base.Init();
        // AddRequest();
    }

    public override void OnResponse(Mainpack pack)
    {
        Debug.Log("�·��İ��Ƿ�Ϊ��:" + pack == null);
        Debug.Log("��������" + pack.Description);
        GameLifeCycle.Instance.tempPack = pack;
      
    

    }
    private void Update()
    {
        Debug.Log(mainpack + "==================");
        if (GameLifeCycle.Instance.tempPack != null)
        {

            PlayerManager.Instance.SyncAnim(GameLifeCycle.Instance.tempPack);
            Debug.Log("����ͬ����ɡ���������������������������������������������������������������");
            GameLifeCycle.Instance.tempPack = null;
        }
       
    }

    public void SendRequest(string butName)
    {
        Mainpack pack = new Mainpack();
        pack.Actioncode = actionCode;
        pack.Request = request;
        pack.Description = butName;
        pack.User = GameLifeCycle.Instance.username;
        base.SendRequest(pack);
    }

    private void OnApplicationQuit()
    {
        RemoveRequest(actionCode);
    }
}
