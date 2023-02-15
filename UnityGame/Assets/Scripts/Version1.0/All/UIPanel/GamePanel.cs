using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BaseUIPanel
{
    public Button exit_battle;
    public Text time_text;
    public Transform user_info_list;
    public GameObject userinfo_pre;


    public ExitGameRequest gameExitRequest;
    //�����Ϣ�б�
    private Dictionary<string, PlayerInfoItem> player_list = new Dictionary<string, PlayerInfoItem>();

    //����б�
    private Dictionary<string, GameObject> player_game_dic = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> Player_game_dic { get { return player_game_dic; } }
    private int num;
    protected override void InitPanel()
    {
        exit_battle.onClick.AddListener(OnClickExitBattle);

        base.InitPanel();
        GameLifeCycle.Instance.playersOnGame = Player_game_dic;
    }

    //�뿪ս������
    private void OnClickExitBattle()
    {
        // �˳�ս����������
        gameExitRequest.SendRequest();
        //�رյ�ǰUI
        UIManager.Instance.Pop();
        //�Ƴ��Լ���ս�������Ļ���
        RemovePlayer(GameLifeCycle.Instance.username);
        PlayerManager.Instance.BattleExit();
        //�������ǳ���
        GameLifeCycle.Instance.LoadScene(1);



        GameObject.Find("CameraFollow").SetActive(false);
        GameLifeCycle.Instance.ExecuteCoroutine(WaitForSceneToLoad("MainCity"));
        // ThreadCrossHelper.Instance.ExecuteOnMainThread(delegate { WaitForSceneToLoad("MainCity"); });

    }
    private IEnumerator WaitForSceneToLoad(string name)
    {
        Debug.Log("��ǰ���س������ƣ�" + SceneManager.GetActiveScene().name + "----gamePanel_Flag1-----!");
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
        }

        Debug.Log("��ǰ���س������ƣ�" + SceneManager.GetActiveScene().name + "----gamePanel_Flag1+++++!");
        // ���Լ� ��ս������ �ͼ������ǵ��ֵ���


        // �ٸ����������
        GameObject.Find("Canvas/MainPanel").transform.GetComponent<GetbackMaincityRequest>().SendRequest();

        //foreach (var item in collection)
        //{

        //}



        Debug.Log("����������ɣ�������������������������������������");
    }

    private IEnumerator Time()
    {
        while (num > 0)
        {
            time_text.text = String.Format("{0:d2}:{1:d2}", num / 60, num % 60);
            yield return new WaitForSeconds(1);
            num--;
        }

    }
    //�����������
    public void UpdatePlayerNum()
    {
        player_game_dic = GameLifeCycle.Instance.player_Battle_dic;
    }


    //������Ϸ��������б����Ϣ
    internal void UpdatePlayerInfo(Mainpack mainpack)
    {
        //��ɾ��ȫ�� �����¼���
        for (int i = 0; i < user_info_list.childCount; i++)
        {
            Destroy(user_info_list.GetChild(i).gameObject);
        }
        player_list.Clear();
        Debug.Log(mainpack.Playerpack.Count);
        foreach (var item in mainpack.Playerpack)
        {

            Debug.Log("gamepanel---flag-------:" + item.PlayerName);
            GameObject g = Instantiate(userinfo_pre, Vector3.zero, Quaternion.identity);
            g.transform.SetParent(user_info_list);
            var p_info = g.GetComponent<PlayerInfoItem>();
            p_info.SetInfo(item.PlayerName, item.Hp);
            player_list.Add(item.PlayerName, p_info);
        }
    }

    //������ҵ�Ѫ����Ϣ
    public void UpdatePlayerValue(string id, int v)
    {
        if (player_list.TryGetValue(id, out PlayerInfoItem item))
        {
            item.UpdateInfo(v);
        }
        else
        {
            Debug.Log("��ȡ������Ӧ��ɫ��Ϣ");
        }
    }
    public void SetOfflinePlayer(Mainpack mainpack)
    {
        List<string> l = new List<string>();
        foreach (var item in mainpack.Playerpack)
        {
            l.Add(item.PlayerName);
        }
        if (l.Count == 0)
        {
            PlayerManager.Instance.RemoveAllPlayer(player_game_dic);
        }
        for (int i = 0; i < player_game_dic.Keys.Count; i++)
        {
            string a = player_game_dic.ElementAt(i).Key;
            if (!l.Contains(a))
            {
                RemovePlayer(a);
            }
        }
    }
    //�Ƴ���ɫ
    public void RemovePlayer(string name)
    {
        if (player_game_dic.TryGetValue(name, out GameObject item))
        {
            GameObject.Destroy(item);
            player_game_dic.Remove(name);
        }
        else
        {
            Debug.Log("�����ڸý�ɫ,�Ƴ���ɫ����");
        }


    }
    //�����·��İ���ɾ�����ض���Ļ����ɫ�������ڰ��еģ�
    public void RemovePlayer(Mainpack mainpack)
    {
        // player_game_dic ��Ϸ�����Ļ���
        for (int i = 0; i < player_game_dic.Keys.Count; i++)
        {
            foreach (var item in mainpack.Playerpack)
            {
                if (player_game_dic.ElementAt(i).Key != item.PlayerName)
                {
                    RemovePlayer(player_game_dic.ElementAt(i).Key);
                }
            }
        }
    }

    public override void OnEnter()
    {
        gameObject.SetActive(true);
        num = 3600;
        StartCoroutine(Time());
        UpdatePlayerNum();
        GameLifeCycle.Instance.OnOffline += SetOfflinePlayer;
    }

    public override void OnExit()
    {
        gameObject.SetActive(false);
        GameLifeCycle.Instance.OnOffline -= SetOfflinePlayer;
        GameLifeCycle.Instance.OnOffline = null;
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
