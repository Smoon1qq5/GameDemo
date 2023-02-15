using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageSlot : MonoBehaviour
{
    //存储每个格子的信息
   //[HideInInspector]
    public Items item;

    public Image slotImage;
    public Text count_text;

    
    public int slotID;

    public GameObject itemInSlot;

    private void Awake()
    {

    }

    public void UpdateSlot(Items obj)
    {
        if (obj.type.Equals(ItemType.Default))
        {
            itemInSlot.SetActive(false);
            return;
        }
        item = obj;
        slotImage.sprite = obj.itemImage;
        count_text.text = obj.itemCount;
    }

}
