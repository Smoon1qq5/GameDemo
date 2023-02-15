using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Assets.Scripts.Common
{
    public interface IResetable
    {
        void OnReset();
    }

    public class GameobjectPool : MonoSingleton<GameobjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;
        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {
            GameObject obj = null;
            //1.查找是否存在禁用的物体 没有则创建对象
            obj = FindUseableObject(key);

            if (obj == null)
            {
                obj = AddObject(key, prefab);
            }


            //2.使用对象
            UseObject(pos, rotate, obj);

            return obj;
        }

        /// <summary>
        /// 使用对象
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rotate"></param>
        /// <param name="obj"></param>
        private static void UseObject(Vector3 pos, Quaternion rotate, GameObject obj)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rotate;
            obj.SetActive(true);
            //obj.GetComponent<IResetable>().OnReset();//需要被重置就实现接口
            foreach (var item in obj.GetComponents<IResetable>())
            {
                item.OnReset();
            }
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prefab"></param>
        /// <returns></returns>
        private GameObject AddObject(string key, GameObject prefab)
        {
            //创建对象
            GameObject obj = Instantiate(prefab);
            //加入池中
            //如果池中没有key 则添加记录
            if (!cache.ContainsKey(key)) cache.Add(key, new List<GameObject>());
            cache[key].Add(obj);
            return obj;
        }
        /// <summary>
        /// 查找指定类别中 可以使用的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private GameObject FindUseableObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);
            return null;

        }


        //回收对象
        public void Collect(GameObject go, float delay = 0)
        {
            StartCoroutine(CollectDelay(go, delay));
        }
        private IEnumerator CollectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }


        //关于清空删除等

        public void Clear(string key)
        {
            foreach (var item in cache[key])
            {
                Destroy(item);
            }

            cache.Remove(key);
        }


        public void ClearAll()
        {
            //foreach (var item in cache.Keys)
            //{
            //    Clear(item);  //不能遍历A删A  否则索引出错报异常
            //}

            foreach (var item in new List<string>(cache.Keys))
            {
                Clear(item);
            }
        }
    }
}
