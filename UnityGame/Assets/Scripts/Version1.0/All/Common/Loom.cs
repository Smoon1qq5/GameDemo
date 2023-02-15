using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Loom : MonoBehaviour
{
    public static int maxThreads = 8;
    static int numThreads;

    private static Loom instance;
    public static Loom Instance
    {
        get
        {
            Initialize();
            return instance;
        }
    }


    private int count;
    static bool initialized = false;
    private void Awake()
    {
        instance = this;
        initialized = true;
    }

    private static void Initialize()
    {
        if (!initialized)
        {
            if (!Application.isPlaying) return;
            initialized = true;
            var g = new GameObject("Loom");
            instance = g.AddComponent<Loom>();
#if !ARTIST_BUILD
            UnityEngine.Object.DontDestroyOnLoad(g);
#endif

        }
    }

    public struct NoDelayedQueueItem
    {
        public Action<object> action;
        public object param;
    }

    private List<NoDelayedQueueItem> _actions = new List<NoDelayedQueueItem>();
      List<NoDelayedQueueItem> _currentactions = new List<NoDelayedQueueItem>();
    public struct DelayedQueueItem
    {
        public float time;
        public Action<object> action;
        public object param;
    }
    private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();

    List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();
    public static void QueueOnMainThread(Action<object> action,object param)
    {
        QueueOnMainThread(action, param,0f);
    }
    public static void QueueOnMainThread(Action<object> action, object param,float time)
    {
        if (time != 0)
        {
            lock (Instance._delayed)
            {
                Instance._delayed.Add(new DelayedQueueItem() { action = action, time = Time.time + time , param=param});
            }
        }
        else
        {
            lock (Instance._actions)
            {
                Instance._actions.Add(new NoDelayedQueueItem() { action=action,param=param});
            }
        }
    }

    public static Thread RunAsync(Action a)
    {
        Initialize();
        while (numThreads >= maxThreads)
        {
            Thread.Sleep(100);
        }
        //以原子操作的形式递增指定变量的值并存储结果 
        //所谓原子操作是指不会被线程调度机制打断的操作；
        //这种操作一旦开始，就一直运行到结束，中间不会有任何 context switch （切换到另一个线程）。
        Interlocked.Increment(ref numThreads);
        //将方法排入队列以便执行，并指定包含该方法所用数据的对象。 此方法在有线程池线程变得可用时执行。
        ThreadPool.QueueUserWorkItem(RunAction, a);
        return null;
    }

    private static void RunAction(object action)
    {
        try
        {
           ( action as Action)();
        }
        catch (Exception)
        {

            
        }
        finally
        {
            //以原子操作的形式递减指定变量的值并存储结果。
            Interlocked.Decrement(ref numThreads);  
        }
    }
    private void OnDisable()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
  

    private void Update()
    {
        if (_actions.Count > 0)
        {
            lock (_actions)
            {
                _currentactions.Clear();
                _currentactions.AddRange(_actions);
                _actions.Clear();
            }
            foreach (var a in _currentactions)
            {
                a.action(a.param);
            }
        }
       if (_delayed.Count > 0)
        {
            lock (_delayed)
            {
                _currentDelayed.Clear();
                _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
                foreach (var item in _currentDelayed)
                {
                    _delayed.Remove(item);
                }
            }
            foreach (var delayed in _currentDelayed)
            {
                delayed.action(delayed.param);
            }
        }
       
    }

}
