using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    ///<summary>
    ///脚本单例类
    ///</summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        //按需加载
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //在场景中根据类型查找引用
                    instance = FindObjectOfType<T>() as T;
                    if (instance == null)
                    {
                        //创建脚本对象(立即执行Awake)
                        instance = new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                    }
                    else
                    {

                    instance.Init();
                    }
                }
                return instance;
            }

        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
        }


        public virtual void Init()
        {

        }
    }
}
