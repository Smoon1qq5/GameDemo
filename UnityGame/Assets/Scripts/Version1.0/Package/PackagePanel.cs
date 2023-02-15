using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PackagePanel : BaseUIPanel
{
    //�����ֿ�
    public InventoryDatabase mybag;
    //�������Ӳ��
    public GameObject slotItem;
    //���Ӹ��������
    public Transform list;



    public List<GameObject> slots = new List<GameObject>();


    private void Awake()
    {
        PackageManager.Instance.packagePanel = this;
    }
    protected override void InitPanel()
    {
        base.InitPanel();
        
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        //  DisplayItem();
        RefreshItem();
    }
    //��database������ ��������
    private void RefreshItem()
    {
        for (int i = 0; i < list.childCount; i++)
        {

            if (list.childCount == 0) break;
            Destroy(list.GetChild(i).gameObject);
            slots.Clear();

        }
        for (int i = 0; i < mybag.items.Count; i++)
        {
            GameObject obj = Instantiate(slotItem, list);
            obj.name = slotItem.name;
            slots.Add(obj);
            obj.GetComponent<PackageSlot>().slotID = i;
            slots[i].transform.GetComponent<PackageSlot>().UpdateSlot(mybag.items[i]);

        }

    }
    //����ӵĶ�����ʾ��������
    //public void DisplayItem()
    //{
    //    for (int i = 0; i < mybag.items.Count; i++)
    //    {
    //        if (!mybag.items[i].type .Equals(ItemType.Default))
    //        {
    //         var obj=   list.GetChild(i).GetComponentInChildren<PackageSlot>();
    //            Debug.Log("��ǰitem�ǣ�" + mybag.items[i]);
    //            obj.UpdateSlot(mybag.items[i]);
    //        }

    //    }
    //}

    public void CollectObject(GameObject other)
    {
        AddNewItem2Database(other.GetComponent<ItemMaterial>().ma_item);

        Destroy(other.gameObject);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag != "Material") return;

    //    AddNewItem2Database(other.GetComponent<ItemMaterial>().ma_item);
    //    Destroy(other.gameObject);  
    //}


    //�����޸Ľṹ�����ֶε�ֵ

    void SetStructValue<T>(ref T scr, string field, object value)
    {
        object temp = scr as object;
        Type type = temp.GetType();
        FieldInfo fieldInfo = type.GetField(field);
        if (fieldInfo != null)
        {
            object v = Convert.ChangeType(value, fieldInfo.FieldType);
            fieldInfo.SetValue(temp, v);
        }
        scr = (T)temp;
    }

    private void AddNewItem2Database(Items obj)
    {
        for (int i = 0; i < mybag.items.Count; i++)
        {
            if (mybag.items[i].itemName.Equals(obj.itemName))
            {
                object t = mybag.items[i];
                Type type = t.GetType();
                FieldInfo fieldInfo = type.GetField("itemCount");

                if (fieldInfo != null)
                {
                    object v = Convert.ChangeType((int.Parse(mybag.items[i].itemCount) + 1).ToString(), fieldInfo.FieldType);

                    fieldInfo.SetValue(t, v);
                }
                mybag.items[i] = (Items)t;

                RefreshItem();
                InventoryDatabase.Instance.RefeshDatabase();
                return;
            }
            
        }


        for (int i = 0; i < mybag.items.Count; i++)
        {
            if (mybag.items[i].type.Equals(ItemType.Default))
            {
                mybag.items[i] = obj;
                break;
            }

        }


        RefreshItem();
        InventoryDatabase.Instance.RefeshDatabase();
    }



    //private void AddNewItem2Database(Items obj)
    //{
    //    if (!mybag.items.Contains(obj))
    //    {
    //        for (int i = 0; i < mybag.items.Count; i++)
    //        {
    //            if (mybag.items[i].type.Equals(ItemType.Default))
    //            {
    //                mybag.items[i] = obj;
    //                break;
    //            }

    //        }
    //    }
    //    else
    //    {
    //        obj.itemCount += 1;
    //    }
    //    RefreshItem();
    //}


}
