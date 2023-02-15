using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoItem : MonoBehaviour
{
    public Text username;
    public Slider hp;

    public void SetInfo(string name, int hp)
    {
        username.text = name;
        this.hp.value = hp;
    }
    public void UpdateInfo(int v)
    {
        hp.value = v;   
    }
}
