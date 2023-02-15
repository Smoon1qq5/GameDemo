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
        //���س��������ǣ�
        GameLifeCycle.Instance.LoadScene(1);
        //�������س���Э��  ��ʼ�����������λ��
        //GameLifeCycle.Instance.ExecuteCoroutine(WaitForLoadScene("MainCity"));

        //GameLifeCycle.Instance.transform.GetComponent<OnlineRequest>().SendRequest();

        //�л�������Ҫ���ջȻ��������µ�����
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
       text.text ="��ɫ��Ϊ��"+pack.Description +"\r\n" + "��ҽ�ɫΪ��" + pack.Loginpack.Playerindex.ToString() + "��ɫ" + "\r\n"+ "��ҽ�ɫ�ȼ�Ϊ��" + pack.Loginpack.Playerlevel.ToString() + "��";
        GameLifeCycle.Instance.level = pack.Loginpack.Playerlevel;
    }

    public override void OnResponse(Mainpack pack)
    {
        ShowPlayerInfo(pack);
    }
}
