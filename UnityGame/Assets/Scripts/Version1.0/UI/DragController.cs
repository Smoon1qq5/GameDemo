using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
/// <summary>
/// ��Ҫ��UI���趨һ�������򣬵����ҡ�˳��֣�̧���ҡ����ʧ
/// ���ݵ��λ���ƶ�����ҡ��
/// ��קʱ
/// </summary>
public class DragController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler,IEndDragHandler,IBeginDragHandler
{

    public GameObject dragbar;

    public Transform bar;

    public float r;//��ק�뾶
    private Vector2 localPos;
    //[HideInInspector]
    public InputController characterController;

    [System.Serializable] public class OnMoveStartHandle : UnityEvent { }
    [System.Serializable] public class OnMoveEndHandle : UnityEvent { }
    [System.Serializable] public class OnMoveHanedle : UnityEvent<Vector2> { }

    [SerializeField] public OnMoveStartHandle onMoveStart;
    [SerializeField] public OnMoveEndHandle onMoveEnd;
    [SerializeField] public OnMoveHanedle onMove;
    private void Start()
    {
        dragbar.SetActive(false);
        
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        dragbar.SetActive(true);
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform,
            eventData.position,eventData.pressEventCamera, out localPos);
        dragbar.transform.localPosition = localPos; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        dragbar.SetActive(false);
        bar.localPosition = Vector3.zero;
        Debug.Log("������ק1");
        onMoveEnd.Invoke();
        Debug.Log("������ק2,�����������");
    }

    private void Update()
    {

        if (localPos.x != 0 || localPos.y != 0)
        {
            
            onMove.Invoke(new Vector2(localPos.x,  localPos.y));
          
            if (bar.localPosition == Vector3.zero)
            localPos = Vector2.zero;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        

        // Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dragbar.transform as RectTransform,
            eventData.position, eventData.pressEventCamera, out localPos);
       
        //����ҡ�˾���
        if (localPos.magnitude > r)
        {
            localPos = localPos.normalized * r;
        }
        bar.transform.localPosition = localPos;
        
           
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onMoveStart.Invoke();
    }
}
