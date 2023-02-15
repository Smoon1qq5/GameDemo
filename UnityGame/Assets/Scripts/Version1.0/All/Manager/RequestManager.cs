using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestManager 
{
    private static RequestManager instance;
    public static RequestManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RequestManager();
            }
            return instance;
        }
    }
    private Dictionary<ActionCode, BaseRequest> request_dic = new Dictionary<ActionCode, BaseRequest>();
    public Dictionary<ActionCode, BaseRequest> Request_dic { get
        {
            return request_dic;
        }
}
    public void AddRequest(BaseRequest baseRequest)
    {
       
        if (request_dic.ContainsKey(baseRequest.GetActionCode))
        {
         
            return;
        }
        

        request_dic.Add(baseRequest.GetActionCode, baseRequest);
    }
    public void RemoveARequest(ActionCode action)
    {
        request_dic.Remove(action);
    }

    public void HandleResponse(Mainpack pack)
    {
        try
        {
            if (request_dic.TryGetValue(pack.Actioncode, out BaseRequest request))
            {
           //   Debug.Log("当前action为："+ pack.Actioncode);  
                request.OnResponse(pack);
            }else
            {
                Debug.Log("没有找到Actioncode：" + pack.Actioncode);

            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e);
        }
    }

}
