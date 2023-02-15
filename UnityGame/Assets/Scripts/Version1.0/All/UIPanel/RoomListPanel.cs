using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BaseUIPanel
{
    [SerializeField]
    private Button eixt_but, search_but, create_but;
    [SerializeField]
    private InputField input_text;
    [SerializeField]
    private Slider count_sld;
    [SerializeField]
    private GameObject room_item;
    [SerializeField]
    private Transform room_list;
    public Text playerCount;


    public SearchRoomRequest searchRoomRequest;
    public CreateRoomRequest createRoomRequest;
    public JoinRoomRequest joinRoomRequest;


    
    protected new void InitPanel()
    {
        eixt_but.onClick.AddListener(OnClickExit);
        search_but.onClick.AddListener(OnClickSearch);
        create_but.onClick.AddListener(OnClickCreate);       

    }

    private void OnClickSearch()
    {
        searchRoomRequest.SendRequest();
    }
    public void SearchRoomResponse(Mainpack mainpack)
    {
        switch (mainpack.Respone)
        {
            case Response.Succeed:

                ui_manager.ShouMessage("��ѯ�ɹ�!һ����" + mainpack.Roompack.Count + "������");
                break;
            case Response.Failed:

                ui_manager.ShouMessage("��ѯ����");
                break;
            case Response.NotRoom:

                ui_manager.ShouMessage("��ǰû�з���");
                break;
            default:

                ui_manager.ShouMessage("���䲻���ڣ�");
                break;
        }
        UpdateRoomList(mainpack);
    }
    private void UpdateRoomList(Mainpack mainpack)
    {
        for (int i = 0; i < room_list.childCount; i++)
        {
            Destroy(room_list.GetChild(i).gameObject);
        }
        if (mainpack.Roompack.Count == 0) return;
        foreach (var item in mainpack.Roompack)
        {
            RoomItem roomItem = Instantiate(room_item, Vector3.zero, Quaternion.identity).GetComponent<RoomItem>();
            roomItem.room = this;
            roomItem.gameObject.transform.SetParent(room_list);
            roomItem.SetRoomInfo(item.Roomname, item.Curnum, item.Maxnum, item.State);
        }
    }

    private void Update()
    {
        playerCount.text = String.Format("��������Ϊ��{0}��", count_sld.value.ToString());
    }
    private void OnClickCreate()
    {

        if (string.IsNullOrEmpty( input_text.text))
        {

            ui_manager.ShouMessage("����������Ϊ��");
            return;
        }

        createRoomRequest.SendRequest(input_text.text, (int)count_sld.value);

        input_text.text = "";
        count_sld.value = 1;
    }
    /// <summary>
    /// ��������ظ�
    /// </summary>
    /// <param name="mainpack"></param>
    public void CreateRoomResponse(Mainpack mainpack)
    {

        switch (mainpack.Respone)
        {
            case Response.ResponeNone:
                Debug.Log("��������");
                break;
            case Response.Succeed:
                //��ʾ�������
                RoomPanel roomPanel = ui_manager.PushPanel(PanelType.Room, true).GetComponent<RoomPanel>();
                //���·����б�
                roomPanel.UpdateRoomPlayerList(mainpack);

                break;
            case Response.Failed:

                ui_manager.ShouMessage("����ʧ��");
                break;


        }
    }


    internal void JoinRoom(string roomname)
    {
        joinRoomRequest.SendRequest(roomname);
    }
    public void JoinRoomResponse(Mainpack mainpack)
    {
        switch (mainpack.Respone)
        {
            case Response.ResponeNone:
                Debug.Log("default");
                break;
            case Response.Succeed:

                //   ui_manager.ShouMessage("���뷿��ɹ�");
                RoomPanel roomPanel = ui_manager.PushPanel(PanelType.Room, true).GetComponent<RoomPanel>();
                roomPanel.UpdateRoomPlayerList(mainpack);
                break;
            case Response.Failed:

                ui_manager.ShouMessage("���뷿��ʧ��");
                break;

        }
    }


    private void OnClickExit()
    {
        Debug.Log("����˳������б�UI");
        ui_manager.PopPanel();
    }


    public override void OnEnter()
    {      
        gameObject.SetActive(true);
        InitPanel();
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
