using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Button enter;
    public Text title, number, state;
    [HideInInspector]
    public RoomListPanel room;
    void Start()
    {
        enter.onClick.AddListener(OnClickEnter);
    }

    private void OnClickEnter()
    {
        room.JoinRoom(title.text);
    }
    public void SetRoomInfo(string title,int min,int max,int state)
    {
        this.title.text = title;
        this.number.text = min + "/" + max;
        switch (state)
        {
           case 0:
                this.state.text = "�ȴ�����";
                break;
            case 1:
                this.state.text = "������������";
                break;
            case 2:
                this.state.text = "��Ϸ��";
                break;
        }
    }
   
}
