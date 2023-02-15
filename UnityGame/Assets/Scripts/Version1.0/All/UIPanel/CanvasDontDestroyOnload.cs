using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasDontDestroyOnload : MonoBehaviour
{

    //****Unity中解决DontDestroyOnLoad导致的对象重复出现****
    /*
     大致分为三种：静态初始化、循环销毁、flag判断以及不再进入初始场景，但是三种方法多多少少会存在一些问题。
     */


    //新的解决方案
    //把所有需要保留的对象挂载在名为“Global”的空对象下，并设置tag为“Global”，
    //再把一下代码脚本挂载在Global上
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
    这里使用了一个概念：立即销毁=限制创建，当然，这样描述是不正确的，
    因为对象的创建事实上发生了 ，所以对于需要保留的对象有一个限制，就是不能在Awake里写入对游戏有明显影响的代码。
    这段代码只会在创建对象时调用，所以性能上完全是没有问题的，
    这样当再次进入第一个场景时，新生成的对象调用Awake函数，发现“Global”的数量大于1，于是立马销毁掉，
    在玩家看来基本上是看不出有任何问题的。当然你不能一上来就删几个对象或者生产几个对象这样子，作死多了肯定会露馅的。
    */


    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainCity")
        {
            //DontDestroyOnLoad()
        }
    }

}
