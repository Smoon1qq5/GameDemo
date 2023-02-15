using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRequest : MonoBehaviour
{
    protected ActionCode actionCode;
    protected Request request;

    
    public ActionCode GetActionCode { get { return actionCode; } }
    

    private void Awake()
    {
        Init();
    }
    private void Start()
    {
     
      RequestManager.Instance.AddRequest(this);
       
    }
    
    private void OnDestroy()
    {

        //RequestManager.Instance.RemoveARequest(actionCode);
        //Debug.Log(" 移除的action：" + actionCode.ToString());
    }

    protected virtual void Init()
    {
        RequestManager.Instance.AddRequest(this);
    }

    protected void AddRequest()
    {
        RequestManager.Instance.AddRequest(this);
    }
    protected void RemoveRequest(ActionCode actionCode)
    {
        RequestManager.Instance.RemoveARequest(actionCode);
    }

    //发送请求
    public virtual void OnResponse(Mainpack pack)
    {

    }
    public virtual void SendRequest(Mainpack pack)
    {
        ClientManager.Instance.SendMessage(pack);
    }
    public virtual void SendRequestByUdp(Mainpack pack)
    {
        ClientManager.Instance.SendTo(pack);
    }

    private void OnApplicationQuit()
    {
        RequestManager.Instance.RemoveARequest(actionCode);
   // Debug.Log(" 移除的action：" + actionCode.ToString());
    }
}
