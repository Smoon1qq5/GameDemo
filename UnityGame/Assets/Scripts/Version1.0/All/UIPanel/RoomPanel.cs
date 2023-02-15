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

    //��ʽ��ʼ��Ϸ�Ļ�Ӧ
    internal void BeginingGameResponse(Mainpack mainpack)
    {
        GamePanel gamePanel = ui_manager.PushPanel(PanelType.Game).GetComponent<GamePanel>();

        gamePanel.UpdatePlayerInfo(mainpack);
    }

    //�����ʼ��Ϸ
    private void OnClickStartGame()
    {
        startRequest.SendRequest();

    }
    //���ڿ�ʼ��Ϸ�Ļ�Ӧ
    internal void StartGameResponse(Mainpack mainpack)
    {
        switch (mainpack.Respone)
        {

            case Response.Succeed:

                ui_manager.ShouMessage("��Ϸ������");
                //�л����� �л���ս������

                StartCoroutine(EnterBattaleScene(mainpack));
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
             
                break;
            case Response.Failed:
                if (GameLifeCycle.Instance.username != user_list.GetChild(0).GetComponent<UserItem>().useranme.text)
                {

                    ui_manager.ShouMessage("��ʼ��Ϸʧ�ܣ��㲻�Ƿ���");
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
        //ȥ�����ǻ����е��Լ�(�Լ��������Զ�����)
        foreach (var item in mainpack.Playerpack)
        {
            Debug.Log(item);
            PlayerManager.Instance.RemovePlayer(item.PlayerName);
        }

        //PlayerManager.Instance.RemovePlayer(GameLifeCycle.Instance.username);

        Debug.Log("��ʼ��Ϸ�����������" + mainpack.Playerpack.Count + "------------roomPanel_Flag!");
        foreach (var item in mainpack.Playerpack)
        {
            Debug.Log("��ʼ��Ϸ��������ƣ�" + item.PlayerName + "roomPanel_Flag!");
        }
       

       
        StartCoroutine(WaitForSceneToLoad("BattleScene",mainpack));



    }
    private IEnumerator WaitForSceneToLoad(string name,Mainpack mainpack)
    {
        Debug.Log("��ǰ���س������ƣ�" + SceneManager.GetActiveScene().name + "----roomPanel_Flag1-----!");
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
        }
        Debug.Log("��ǰ���س������ƣ�" + SceneManager.GetActiveScene().name + "----roomPanel_Flag1+++++!");
       
        //���³���������
        PlayerManager.Instance.InitPlayer(mainpack, GameLifeCycle.Instance.player_Battle_dic,GameObject.Find("SpawnPostion"));
        ////������ӵ���Ҽ��뵽�µĻ�����(ս�������Ļ���)
        //PlayerManager.Instance.UpdateBattlePlayer(PlayerManager.Instance.Player_dic, mainpack);
      
       

        Debug.Log("����������ɣ�������������������������������������");
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
    /// ���·����ڲ�����б�
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
            //չʾ�����Ϣ
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
