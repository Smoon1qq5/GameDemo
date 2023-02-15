using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGameCityRequest : BaseRequest
{
    Mainpack mainpack;
    [SerializeField]
    private MainPanel mainPanel;
    protected override void Init()
    {

        request = Request.Room;
        actionCode = ActionCode.EnterGameCity;
        base.Init();
    }


    public override void OnResponse(Mainpack pack)
    {
        mainpack = pack;
    }

    private void Update()
    {
        if (mainpack != null && SceneManager.GetActiveScene().name == "MainCity")
        {
            // Debug.Log("去掉进入游戏场景的！————Flag_EnterGameCity");
            for (int i = 0; i < PlayerManager.Instance.Player_dic.Keys.Count; i++)
            {
                
                foreach (var item in mainpack.Playerpack)
                {
                    if (item.PlayerName != PlayerManager.Instance.Player_dic.ElementAt(i).Key)
                    {
                        PlayerManager.Instance.RemovePlayer(PlayerManager.Instance.Player_dic.ElementAt(i).Key);
                    }
                }
              
            }

            mainpack = null;
        }
    }
}
