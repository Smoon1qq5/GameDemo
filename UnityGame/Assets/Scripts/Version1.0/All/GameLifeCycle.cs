using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLifeCycle : MonoSingleton<GameLifeCycle>
{
    public delegate void OfflineHandle(Mainpack mainpack);
    public OfflineHandle OnOffline;

    public string username;
    public int level;
    public Mainpack tempPack;
    void Start()
    {
        DontDestroyOnLoad(this);
        ClientManager.Instance.Init();

        UIManager.Instance.PushPanel(PanelType.Login);
        MissionManager.Instance.Init();
    }

    public AsyncOperation LoadScene(int index)
    {
        return SceneManager.LoadSceneAsync(index);
    }
    public void InitSpwanPosition(GameObject obj)
    {
        Debug.Log("开始处理初始化位置");

        obj = GameObject.Find("SpawnPostion");

        //50, 0, 10
        //tf.transform.position = vector;
        Debug.Log("处理初始化位置结束");
    }

    //场景玩家生成地点
    [SerializeField]
    
    public GameObject Spawn_player_pos;

    public Dictionary<string, GameObject> playersOnGame;

    public AStarAlgorithm aa;
    public AutomaticPathfinding ap;
    public GameObject obj;


    public void ExecuteCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    //添加第三人称相机
    public void AddCamera(float x, float y, float z)
    {
        GameObject obj = new GameObject("CameraFollow");

        obj.AddComponent<MoveViewController>();
        GameObject obj_camera = new GameObject("Camera");
        obj_camera.AddComponent<Camera>();
        obj_camera.transform.SetParent(obj.transform);

        obj.transform.position = new Vector3(x, y, z);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            Debug.Log("请求数量："+RequestManager.Instance.Request_dic.Keys.Count);
            Print();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            foreach (var item in PlayerManager.Instance.Player_dic)
            {
                Debug.Log(item);
            }
        }
    }
    private void OnApplicationQuit()
    {
        ClientManager.Instance.OnDestroy();
        PlayerManager.Instance.OnDestroy();
    }

    //战斗场景玩家缓存
    public Dictionary<string, GameObject> player_Battle_dic = new Dictionary<string, GameObject>();

    public void Print()
    {
        //   RequestManager.Instance.Request_dic
        foreach (var item in RequestManager.Instance.Request_dic)
        {
            Debug.Log(item);
        }
    }


    //A* 算法
    public AStarAlgorithm aStar;



    public AcceptMissionPanel accept;
}
