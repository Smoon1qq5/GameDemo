using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveViewController : MonoBehaviour
{
    // 玩家
    [HideInInspector]
    public Transform player;
    // 控制相机的组件


    private Vector3 offect;
    private void Start()
    {
       
        //offect = player.position - Y_TF.position;
    }
    public void SetOffect(float x)
    {
        offect = player.position - transform.position;
        offect.x = 0;
    }
    private void LateUpdate()
    {
        //Debug.Log(offect);
        //Debug.Log(transform+"-----");
        //Debug.Log(player+"+++++++++");
        if (offect != Vector3.zero&& player!=null)
        {
        transform.position = Vector3.Lerp(transform.position, player.position - offect, Time.deltaTime * 10f);

        }
       
    }

}
