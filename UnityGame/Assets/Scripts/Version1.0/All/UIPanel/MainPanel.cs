using Google.Protobuf.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主面板（不随场景切换而加载/删除）
/// </summary>
public class MainPanel : BaseUIPanel
{
    [SerializeField]
    private Button send_but, team_but, mission_but, package_but;
    [SerializeField]
    private InputField input_text;
    [SerializeField]
    private Text chet_text;
    [SerializeField]
    private ChatRequest request;
    [SerializeField]
    private OnlineRequest onlineRequest;

    [SerializeField]
    private Canvas canvas;

    private void Awake()
    {
        onlineRequest.SendRequest();
    }

    private void Start()
    {
        Init();

    }

    private void Init()
    {
        // DontDestroyOnLoad(canvas.transform);
        /*
        会遇到来回切场景 多出一个DontDestroyOnLoad的对象
         */

        /*
         * 我们经常要用到DontDestroyOnLoad来使一个GameObject在切换场景的时候不被销毁
           例如现在有两个场景Scene1和Scene2，在Scene1的GameManager脚本的Start方法
        中调用了DontDestroyOnLoad(gameObject)，接着切换到Scene2，会发现GameManager
        对象没有被销毁，但当从Scene2切换到Scene1时，会多出一个GameManager对象，
        每次进入到Scenen1时都会多一个GameManager对象
      这是因为每次进入到Scene1时，都会执行Start方法中的DontDestroyOnLoad方法，
        而之前的GameManager对象没有被释放，所以会又多出一个GameManager对象

         */

        /*
        解决方法一
          创建一个初始化的场景，在初始化场景里面的某个游戏对象的全局脚本中，
        把所有游戏对象全部设置成DontDestroyOnLoad，也就是切换场景时不销毁，
        接着进入到第一个游戏场景，也就是说逻辑永远不会再返回初始化场景，
        也就不会存在来回切场景DontDestroyOnLoad没有删除的问题

         解决方法二
          判断一下引用是否为空，如果为空再设置为DontDestroyOnLoad，如果不为空则删除此游戏物体

         
         */

        send_but.onClick.AddListener(OnSendClick);
        team_but.onClick.AddListener(OnClickTeatBut);
        mission_but.onClick.AddListener(OnClickMissionBut);
        package_but.onClick.AddListener(OnClickPackageBut);




    }



    private void OnClickPackageBut()
    {

        if (PackageManager.Instance.packagePanel.gameObject.activeSelf)
        {
            PackageManager.Instance.packagePanel.gameObject.SetActive(false);
        }
        else
        {
            PackageManager.Instance.packagePanel.gameObject.SetActive(true);
        }
    }

    private void OnClickMissionBut()
    {
        //显示和隐藏任务ui

        if (MissionManager.Instance.missionPanel.gameObject.activeSelf)
        {
            MissionManager.Instance.missionPanel.gameObject.SetActive(false);
        }
        else
        {
            MissionManager.Instance.missionPanel.gameObject.SetActive(true);
        }
    }

    private void OnClickTeatBut()
    {

        UIManager.Instance.PushPanel(PanelType.RoomList, true);
    }

    private void OnSendClick()
    {
        if (input_text.text == "")
        {
            //  UIManager.Instance.PushPanel(PanelType.Alert);
            UIManager.Instance.ShouMessage("发送内容不能为空");
            return;
        }
        request.SendRequest(input_text.text);

        chet_text.text += "我：" + input_text.text + "\n"; //把我的消息显示上去
        input_text.text = "";
    }
    public void ChatResonse(string str)
    {

        chet_text.text += str + "\n";

    }


    //设置组队按钮的显影  进入游戏场景就关闭 
    public void SetActiveSelf(bool s)
    {
        team_but.gameObject.SetActive(s);
    }


    //主城检测服务器的消息 然后直接创建远端玩家的人物模型
    public void OnlinePlayerInfo(Mainpack mainpack)
    {

        //把mainpack中的playpack集合拆开  查看玩家信息
        Debug.Log("从服务器下发的包里面玩家数量：" + mainpack.Playerpack.Count);
        if (mainpack.Playerpack.Count == 0) return;
        foreach (var item in mainpack.Playerpack)
        {
            Debug.Log(item);
        }
        PlayerManager.Instance.InitPlayer(mainpack, GameObject.Find("SpawnPostion"));
    }

    public void GetBackCity(Mainpack mainpack)
    {
        PlayerManager.Instance.PlayerGetBack(mainpack, GameObject.Find("SpawnPostion"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            try
            {
                Debug.Log("打开背包");
                if (PackageManager.Instance.packagePanel.gameObject.activeSelf)
                {
                    PackageManager.Instance.packagePanel.gameObject.SetActive(false);
                }
                else
                {
                    PackageManager.Instance.packagePanel.gameObject.SetActive(true);
                }
            }
            catch (Exception e)
            {

                Debug.Log("打开背包异常！" + e.Message);
            }

        }
    }

    private void OnDestroy()
    {

    }
}
