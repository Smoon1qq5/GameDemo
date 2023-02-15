using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserItem : MonoBehaviour
{

    public Image role_image;
    public Text useranme, userlevel;
   

   
    public void ShouUserInfo(string name, int level)
    {
        useranme.text = name;
        userlevel.text = level.ToString() ;
    }


}
