using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasDontDestroyOnload : MonoBehaviour
{

    //****Unity�н��DontDestroyOnLoad���µĶ����ظ�����****
    /*
     ���·�Ϊ���֣���̬��ʼ����ѭ�����١�flag�ж��Լ����ٽ����ʼ�������������ַ���������ٻ����һЩ���⡣
     */


    //�µĽ������
    //��������Ҫ�����Ķ����������Ϊ��Global���Ŀն����£�������tagΪ��Global����
    //�ٰ�һ�´���ű�������Global��
    void Start()
    {
        //SetCanvas();
    }
    private void OnEnable()
    {
        SetCanvas();
    }

    public void SetCanvas()
    {
        if (GameObject.FindGameObjectsWithTag("Global").Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    /* 
    ����ʹ����һ�������������=���ƴ�������Ȼ�����������ǲ���ȷ�ģ�
    ��Ϊ����Ĵ�����ʵ�Ϸ����� �����Զ�����Ҫ�����Ķ�����һ�����ƣ����ǲ�����Awake��д�����Ϸ������Ӱ��Ĵ��롣
    ��δ���ֻ���ڴ�������ʱ���ã�������������ȫ��û������ģ�
    �������ٴν����һ������ʱ�������ɵĶ������Awake���������֡�Global������������1�������������ٵ���
    ����ҿ����������ǿ��������κ�����ġ���Ȼ�㲻��һ������ɾ����������������������������ӣ��������˿϶���¶�ڵġ�
    */


    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainCity")
        {
            //DontDestroyOnLoad()
        }
    }

}
