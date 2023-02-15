using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    ///<summary>
    ///�ű�������
    ///</summary>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        //�������
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //�ڳ����и������Ͳ�������
                    instance = FindObjectOfType<T>() as T;
                    if (instance == null)
                    {
                        //�����ű�����(����ִ��Awake)
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
