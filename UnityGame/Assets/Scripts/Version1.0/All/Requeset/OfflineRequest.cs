using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfflineRequest : BaseRequest
{
    Mainpack mainpack;
    [SerializeField]
    private MainPanel mainPanel;
    protected override void Init()
    {
        
        request = Request.Room;
        actionCode = ActionCode.Offline;
        base.Init();
    }


    private void Update()
    {
        if (mainpack != null && SceneManager.GetActiveScene().name == "MainCity")
        {
            Debug.Log("执行下线处理！————Flag_Offlinerequest");
            PlayerManager.Instance.SetOfflineClinet(mainpack);
            mainpack = null;
        }
       
      
    }
    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }

}
