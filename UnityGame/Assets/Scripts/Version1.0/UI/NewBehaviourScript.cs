using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform Y_Axis;
    public Transform X_Axis;
    public Transform Z_Axis;
    public Transform zoom_Axis;

    public Transform player;
    //旋转速度
    public float roSpeed = 180;
    //缩放速度
    public float scSpeed = 50;
    //限定角度
    public float limiAngle = 45;

    private float hor, ver, scrollview;
    float x = 0, sc = 10;

    public bool followFlag;
    public bool turnFlag;

    private Vector3 offect;

    private void Start()
    {
        offect =player.position-Y_Axis.position;
    }
    private void Update()
    {
        Y_Axis.position = Vector3.Lerp(Y_Axis.position, player.position - offect, Time.deltaTime * 10f);
        if (turnFlag && player != null)
        {
            transform.forward = new Vector3(player.forward.x, 0, player.forward.z);

        }
    }
    //private void LateUpdate()
    //{
    //    hor = Input.GetAxis("Mouse X");
    //    ver = Input.GetAxis("Mouse Y");


    //    scrollview = Input.GetAxis("Mouse ScrollWheel");
    //    //左右滑动
    //    if (hor != 0)
    //    {
    //        Y_Axis.Rotate(Vector3.up * roSpeed * hor * Time.deltaTime);
    //    }
    //    //上下滑动
    //    if (ver != 0)
    //    {
    //        x += -ver * Time.deltaTime * roSpeed;
    //        x = Mathf.Clamp(x, -limiAngle, limiAngle);
    //        Quaternion q = Quaternion.identity;
    //        q = Quaternion.Euler(new Vector3(x, X_Axis.eulerAngles.y, X_Axis.eulerAngles.z));
    //        X_Axis.rotation = Quaternion.Lerp(X_Axis.rotation, q, Time.deltaTime * roSpeed);
    //    }

    //    if (scrollview != 0)
    //    {
    //        sc -= scrollview * scSpeed;
    //        sc = Mathf.Clamp(sc, 3, 10);
    //        zoom_Axis.localPosition = new Vector3(0, 0, -sc);

    //    }
    //    if (followFlag && player != null)
    //    {
    //        Y_Axis.position = Vector3.Lerp(Y_Axis.position, player.position + Vector3.up, Time.deltaTime * 10f);

    //    }
    //    if (turnFlag && player != null)
    //    {
    //        player.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
    //    }
    //}



}
