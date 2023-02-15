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
                this.state.text = "等待加入";
                break;
            case 1:
                this.state.text = "房间人数已满";
                break;
            case 2:
                this.state.text = "游戏中";
                break;
        }
    }
   
}
