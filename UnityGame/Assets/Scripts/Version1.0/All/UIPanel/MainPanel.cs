using Google.Protobuf.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����壨���泡���л�������/ɾ����
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
        �����������г��� ���һ��DontDestroyOnLoad�Ķ���
         */

        /*
         * ���Ǿ���Ҫ�õ�DontDestroyOnLoad��ʹһ��GameObject���л�������ʱ�򲻱�����
           ������������������Scene1��Scene2����Scene1��GameManager�ű���Start����
        �е�����DontDestroyOnLoad(gameObject)�������л���Scene2���ᷢ��GameManager
        ����û�б����٣�������Scene2�л���Scene1ʱ������һ��GameManager����
        ÿ�ν��뵽Scenen1ʱ�����һ��GameManager����
      ������Ϊÿ�ν��뵽Scene1ʱ������ִ��Start�����е�DontDestroyOnLoad������
        ��֮ǰ��GameManager����û�б��ͷţ����Ի��ֶ��һ��GameManager����

         */

        /*
        �������һ
          ����һ����ʼ���ĳ������ڳ�ʼ�����������ĳ����Ϸ�����ȫ�ֽű��У�
        ��������Ϸ����ȫ�����ó�DontDestroyOnLoad��Ҳ�����л�����ʱ�����٣�
        ���Ž��뵽��һ����Ϸ������Ҳ����˵�߼���Զ�����ٷ��س�ʼ��������
        Ҳ�Ͳ�����������г���DontDestroyOnLoadû��ɾ��������

         ���������
          �ж�һ�������Ƿ�Ϊ�գ����Ϊ��������ΪDontDestroyOnLoad�������Ϊ����ɾ������Ϸ����

         
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
        //��ʾ����������ui

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
            UIManager.Instance.ShouMessage("�������ݲ���Ϊ��");
            return;
        }
        request.SendRequest(input_text.text);

        chet_text.text += "�ң�" + input_text.text + "\n"; //���ҵ���Ϣ��ʾ��ȥ
        input_text.text = "";
    }
    public void ChatResonse(string str)
    {

        chet_text.text += str + "\n";

    }


    //������Ӱ�ť����Ӱ  ������Ϸ�����͹ر� 
    public void SetActiveSelf(bool s)
    {
        team_but.gameObject.SetActive(s);
    }


    //���Ǽ�����������Ϣ Ȼ��ֱ�Ӵ���Զ����ҵ�����ģ��
    public void OnlinePlayerInfo(Mainpack mainpack)
    {

        //��mainpack�е�playpack���ϲ�  �鿴�����Ϣ
        Debug.Log("�ӷ������·��İ��������������" + mainpack.Playerpack.Count);
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
                Debug.Log("�򿪱���");
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

                Debug.Log("�򿪱����쳣��" + e.Message);
            }

        }
    }

    private void OnDestroy()
    {

    }
}
