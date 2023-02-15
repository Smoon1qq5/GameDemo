using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInfoPanel : BaseUIPanel
{
    private Button entergame;
    private Text text;
    private UserInfoRequest request;
    protected new void InitPanel()
    {
        entergame=GetComponentInChildren<Button>(); 
        text=GetComponentInChildren<Text>();
        request = GetComponent<UserInfoRequest>();
        entergame.onClick.AddListener(OnClickEnterGame);
        base.InitPanel();
        
    }

    private void OnClickEnterGame()
    {
        //加载场景（主城）
        GameLifeCycle.Instance.LoadScene(1);
        //开启加载场景协程  初始化生成物体的位置
        //GameLifeCycle.Instance.ExecuteCoroutine(WaitForLoadScene("MainCity"));

        //GameLifeCycle.Instance.transform.GetComponent<OnlineRequest>().SendRequest();

        //切换场景需要清除栈然后在添加新的内容
        UIManager.Instance.ClearPanelStack();

       // GameLifeCycle.Instance.ExecuteCoroutine(WaitForSceneToLoad("MainCity"));
    }

    

    private IEnumerator WaitForLoadScene(string v)
    {
        while (SceneManager.GetActiveScene().name != v)
        {
            yield return null;
        }
        GameLifeCycle.Instance.InitSpwanPosition(GameLifeCycle.Instance.Spawn_player_pos);
    }

    public override void OnEnter()
    {
        if (request == null)
        {
            InitPanel();
        }
       
        gameObject.SetActive(true);
      
        request.SendRequest();
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


    private void ShowPlayerInfo(Mainpack pack)
    {
       text.text ="角色名为："+pack.Description +"\r\n" + "玩家角色为：" + pack.Loginpack.Playerindex.ToString() + "角色" + "\r\n"+ "玩家角色等级为：" + pack.Loginpack.Playerlevel.ToString() + "级";
        GameLifeCycle.Instance.level = pack.Loginpack.Playerlevel;
    }

    public override void OnResponse(Mainpack pack)
    {
        ShowPlayerInfo(pack);
    }
}
