using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetbackMaincityRequest : BaseRequest
{
    Mainpack mainpack;
   public MainPanel panel;
    protected override void Init()
    {
       
        request = Request.Room;
        actionCode = ActionCode.GetbackMaincity;
       base.Init(); 
    }

   
    void Update()
    {
       //Debug.Log("11111");
     //   Debug.Log(mainpack==null);
        if (mainpack != null)
        {
         //   Debug.Log("222222");
            Debug.Log("�������Ǵӷ������·�����ҵ�������" + mainpack.Playerpack.Count);
            if (SceneManager.GetActiveScene().name == "MainCity")
            {
                panel.GetBackCity(mainpack);
            }
            
            
            mainpack = null;
        }
        
    }
    
    public override void OnResponse(Mainpack pack)
    {
       // Debug.Log("�յ��ظ�,�Ƿ�Ϊ�գ�"+pack==null);
        mainpack = pack;
    }

    public  void SendRequest()
    {
        Debug.Log("������������뿪ս�������������ǵ���Ϣ");
        Mainpack pack = new Mainpack();
        pack.Actioncode = actionCode;
        pack.Request = request;
        pack.Description = GameLifeCycle.Instance.username;
        
        base.SendRequest(pack);

       // Print();
    }

    public void Print()
    {
        //   RequestManager.Instance.Request_dic
        foreach (var item in RequestManager.Instance.Request_dic)
        {
            Debug.Log(item);
        }     
    }
}
