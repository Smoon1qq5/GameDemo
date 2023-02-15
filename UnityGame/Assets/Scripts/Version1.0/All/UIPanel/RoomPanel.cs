using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomPanel : BaseUIPanel
{
    public Button exit_room, start_game;
    public Transform user_list;
    public GameObject userItem;


    public RoomExitRequest roomExitRequest;
    public StartRequest startRequest;

    public Mainpack packTemp = new Mainpack();
    protected override void InitPanel()
    {
        exit_room.onClick.AddListener(OnClickExit);
        start_game.onClick.AddListener(OnClickStartGame);
        if (start_game.gameObject.activeInHierarchy != true)
        {

            start_game.gameObject.SetActive(true);
        }
        base.InitPanel();
    }

    //正式开始游戏的回应
    internal void BeginingGameResponse(Mainpack mainpack)
    {
        GamePanel gamePanel = ui_manager.PushPanel(PanelType.Game).GetComponent<GamePanel>();

        gamePanel.UpdatePlayerInfo(mainpack);
    }

    //点击开始游戏
    private void OnClickStartGame()
    {
        startRequest.SendRequest();

    }
    //对于开始游戏的回应
    internal void StartGameResponse(Mainpack mainpack)
    {
        switch (mainpack.Respone)
        {

            case Response.Succeed:

                ui_manager.ShouMessage("游戏已启动");
                //切换场景 切换到战斗场景

                StartCoroutine(EnterBattaleScene(mainpack));
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
             
                break;
            case Response.Failed:
                if (GameLifeCycle.Instance.username != user_list.GetChild(0).GetComponent<UserItem>().useranme.text)
                {

                    ui_manager.ShouMessage("开始游戏失败！你不是房主");
                }
                break;

            default:
                break;
        }
    }

    private AsyncOperation operation;
    private IEnumerator EnterBattaleScene(Mainpack mainpack)
    {
        start_game.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        GameLifeCycle.Instance.LoadScene(2);
        //去掉主城缓存中的自己(以及房间里的远端玩家)
        foreach (var item in mainpack.Playerpack)
        {
            Debug.Log(item);
            PlayerManager.Instance.RemovePlayer(item.PlayerName);
        }

        //PlayerManager.Instance.RemovePlayer(GameLifeCycle.Instance.username);

        Debug.Log("开始游戏的玩家数量：" + mainpack.Playerpack.Count + "------------roomPanel_Flag!");
        foreach (var item in mainpack.Playerpack)
        {
            Debug.Log("开始游戏的玩家名称：" + item.PlayerName + "roomPanel_Flag!");
        }
       

       
        StartCoroutine(WaitForSceneToLoad("BattleScene",mainpack));



    }
    private IEnumerator WaitForSceneToLoad(string name,Mainpack mainpack)
    {
        Debug.Log("当前加载场景名称：" + SceneManager.GetActiveScene().name + "----roomPanel_Flag1-----!");
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
        }
        Debug.Log("当前加载场景名称：" + SceneManager.GetActiveScene().name + "----roomPanel_Flag1+++++!");
       
        //在新场景添加玩家
        PlayerManager.Instance.InitPlayer(mainpack, GameLifeCycle.Instance.player_Battle_dic,GameObject.Find("SpawnPostion"));
        ////把新添加的玩家加入到新的缓存中(战斗场景的缓存)
        //PlayerManager.Instance.UpdateBattlePlayer(PlayerManager.Instance.Player_dic, mainpack);
      
       

        Debug.Log("场景加载完成！！！！！！！！！！！！！！！！！！！");
    }
   
    private void OnClickExit()
    {
        roomExitRequest.SendRequest();
    }
    internal void ExitRoomResponse()
    {
        ui_manager.PopPanel();
    }


    /// <summary>
    /// 更新房间内部玩家列表
    /// </summary>
    /// <param name="mainpack"></param>
    internal void UpdateRoomPlayerList(Mainpack mainpack)
    {
        for (int i = 0; i < user_list.childCount; i++)
        {
            Destroy(user_list.GetChild(i).gameObject);
        }
        foreach (var item in mainpack.Playerpack)
        {

            var user_item = Instantiate(userItem, Vector3.zero, Quaternion.identity).GetComponent<UserItem>();
            user_item.transform.SetParent(user_list);
            //展示玩家信息
            user_item.ShouUserInfo(item.PlayerName, item.Level);
        }
       
    }


    public override void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        gameObject.SetActive(false);
    }

    public override void OnRecovery()
    {
        gameObject.SetActive(true);
    }

}
