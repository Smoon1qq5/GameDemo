using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform orginal;
    private  InventoryDatabase myBag;

    private int currentID;


    private void Start()
    {
        myBag = InventoryDatabase.Instance;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;

        orginal = transform.parent;
        currentID = orginal.GetComponent<PackageSlot>().slotID;
        transform.SetParent(transform.parent.parent);
        //射线锁定
        transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        transform.position = eventData.position;
      Debug.Log(eventData.pointerCurrentRaycast.gameObject);

    }

    //交换
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name == "Item")
            {
                //设置落点的父物体
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.position;

                //交换list中的位置

                var temp = myBag.items[currentID];
                myBag.items[currentID] = myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<PackageSlot>().slotID];
                myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<PackageSlot>().slotID] = temp;


                eventData.pointerCurrentRaycast.gameObject.transform.position = orginal.position;
                eventData.pointerCurrentRaycast.gameObject.transform.SetParent(orginal);

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryDatabase.Instance.RefeshDatabase();
                return;
            }
            if (eventData.pointerCurrentRaycast.gameObject.name == "SlotItem")
            {


                //否则直接挂在检测到的Slot下面
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;


                var temp = eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0);
               
                temp.gameObject.SetActive(true);
                temp.transform.position=orginal.position;
                temp.transform.SetParent(orginal);
                temp.gameObject.SetActive(false);

                //itemlist的物品存储位置不变
                var temp02 = myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<PackageSlot>().slotID];
                myBag.items[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<PackageSlot>().slotID] = myBag.items[currentID];
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<PackageSlot>().slotID != currentID)
                    myBag.items[currentID] = temp02;
                Debug.Log("Flag1111111111--------------:" + currentID + "______________" + myBag.items[currentID].itemImage); ;
                transform.GetComponent<CanvasGroup>().blocksRaycasts = true;
                InventoryDatabase.Instance.RefeshDatabase();
                return;
            }
        }
        transform.SetParent(orginal);
        transform.position = orginal.position;

        transform.GetComponent<CanvasGroup>().blocksRaycasts = true;


    }





}
