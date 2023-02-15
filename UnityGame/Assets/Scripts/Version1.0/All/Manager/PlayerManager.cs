using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// ��ҹ���ϵͳ
/// </summary>
public class PlayerManager
{
    //����������Ƽ����󻺴�
    private Dictionary<string, GameObject> player_dic = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> Player_dic { get { return player_dic; } }



    // ��Ϸ��ɫԤ�Ƽ�
    private GameObject character_pre;


    //public Transform Spawn_player_pos { get { return spawn_player_pos; } set { spawn_player_pos = value; } }

    //ҡ��
    public DragController drag_controller;
    //Զ����ҽű�
    public RemoteCharacter remoteCharacter;

    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerManager();

            }
            return instance;
        }
    }

    public PlayerManager()
    {
        Init();
    }

    public void Init()
    {

        character_pre = Resources.Load("Version1.0/Player/Player") as GameObject;

        drag_controller = GameObject.Find("Canvas/TouchController/MainJoyStick").GetComponent<DragController>();
    }

    /// <summary>
    /// ʵ������ɫ,������ӵ��ֵ���
    /// </summary>
    /// <param name="pack">���ݷ������·��İ�</param>
    /// <param name="tf">��ɫ���ɵĵص�</param>
    public void InitPlayer(Mainpack pack, GameObject tf)
    {
        foreach (var item in player_dic)
        {
            Debug.Log(item);
        }
        int posIndex = 0;

        foreach (var item in pack.Playerpack)
        {
            if (player_dic.ContainsKey(item.PlayerName))
            {
                continue;
            }

            Vector3 pos = new Vector3(10, 0, 3);
       //    Vector3 pos = tf.transform.position;
            pos.x += posIndex++;
            GameObject obj = GameObject.Instantiate(character_pre, pos, Quaternion.identity);
            
            obj.name = item.PlayerName;
            Debug.Log("Ԥ������ɫ��" + obj + "Flag1----------");
            obj.transform.SetParent(tf.transform);
            Debug.Log("���ؽ�ɫ�����ƣ�" + GameLifeCycle.Instance.username + "Flag2----------");
            if (item.PlayerName.Equals(GameLifeCycle.Instance.username))
            {
                GameLifeCycle.Instance.obj = obj;
                //�������ؽ�ɫ
                drag_controller.characterController = obj.AddComponent<InputController>();
                obj.AddComponent<UpdateStateRequest>();
                obj.AddComponent<SyncPosition>();
                obj.AddComponent<CharacterMission>();
                obj.AddComponent<AutomaticPathfinding>();
                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.isLocal = true;
                characterRistic.username = item.PlayerName;

                if (GameObject.Find("CameraFollow") == null)
                {
                    GameLifeCycle.Instance.AddCamera(10, 2.5f, 0);
                }

                MoveViewController view_controller = GameObject.Find("CameraFollow").GetComponent<MoveViewController>();

                view_controller.player = obj.transform;
                view_controller.SetOffect(pos.x);
                view_controller.transform.rotation = Quaternion.Euler(15f, 0, 0);

            }
            else
            {
                //���������ͻ��˽�ɫ
                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.username = item.PlayerName;
                obj.AddComponent<RemoteCharacter>();

            }
            AddPlayer(item.PlayerName, obj);
        }


    }
    public void InitPlayer(Mainpack pack, Dictionary<string, GameObject> dic, GameObject tf)
    {

        int posIndex = 0;

        foreach (var item in pack.Playerpack)
        {
            
            Vector3 pos = tf.transform.position;
            pos.x = posIndex++;
            GameObject obj = GameObject.Instantiate(character_pre, pos, Quaternion.identity);

            obj.name = item.PlayerName;
            Debug.Log("���ؽ�ɫ��" + obj + "Flag1----------");
            obj.transform.SetParent(tf.transform);
            Debug.Log("���ؽ�ɫ�����ƣ�" + GameLifeCycle.Instance.username + "Flag2----------");
            if (item.PlayerName.Equals(GameLifeCycle.Instance.username))
            {
                //�������ؽ�ɫ
                drag_controller.characterController = obj.AddComponent<InputController>();
                obj.AddComponent<UpdateStateRequest>();
                obj.AddComponent<SyncPosition>();
                obj.AddComponent<CharacterMission>();
               obj.AddComponent<AutomaticPathfinding>();
                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.isLocal = true;
                characterRistic.username = item.PlayerName;


                GameLifeCycle.Instance.AddCamera(0, 2.5f, -3f);
                MoveViewController view_controller = GameObject.Find("CameraFollow").GetComponent<MoveViewController>();

                view_controller.player = obj.transform;
                view_controller.SetOffect(pos.x);
                view_controller.transform.rotation = Quaternion.Euler(15f, 0, 0);
            }
            else
            {
                //���������ͻ��˽�ɫ
                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.username = item.PlayerName;
                obj.AddComponent<RemoteCharacter>();

            }
            dic.Add(item.PlayerName, obj);
        }


    }

    public void PlayerGetBack(Mainpack pack, GameObject tf)
    {
        RemoveAllPlayer(Player_dic);
        int posIndex = 0;
        foreach (var item in pack.Playerpack)
        {
            Vector3 pos = new Vector3(10, 0, 3);
          //  Vector3 pos = tf.transform.position;
            pos.x += posIndex++;
            GameObject obj = GameObject.Instantiate(character_pre, pos, Quaternion.identity);

            obj.name = item.PlayerName;

            obj.transform.SetParent(tf.transform);

            if (item.PlayerName.Equals(GameLifeCycle.Instance.username))
            {
                //�������ؽ�ɫ
                drag_controller.characterController = obj.AddComponent<InputController>();
                obj.AddComponent<UpdateStateRequest>();
                obj.AddComponent<SyncPosition>();
                obj.AddComponent<CharacterMission>();
               obj.AddComponent<AutomaticPathfinding>();
                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.isLocal = true;
                characterRistic.username = item.PlayerName;
                Debug.Log(GameObject.Find("CameraFollow"));
                if (GameObject.Find("CameraFollow") == null)
                {
                    GameLifeCycle.Instance.AddCamera(10, 2.5f, 0);
                }
                GameObject.Find("CameraFollow").transform.position=new Vector3(10, 2.5f, 0);
                MoveViewController view_controller = GameObject.Find("CameraFollow").GetComponent<MoveViewController>();

                view_controller.player = obj.transform;
                view_controller.SetOffect(pos.x);
                view_controller.transform.rotation = Quaternion.Euler(15f, 0, 0);

            }
            else
            {
                //���������ͻ��˽�ɫ
                CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
                characterRistic.username = item.PlayerName;
                obj.AddComponent<RemoteCharacter>();

            }
            AddPlayer(item.PlayerName, obj);
        }
    }

    public void AddPlayer(string name, GameObject obj)
    {
        try
        {
            if (!player_dic.Keys.Contains(name))
            {
                player_dic.Add(name, obj);
            }
            else if (player_dic.ContainsKey(name) && player_dic[name] == null)
            {
                player_dic[name] = obj;
            }
        }
        catch (Exception e)
        {

            Debug.Log("��ӽ�ɫ����" + e.ToString());
        }


    }

    /// <summary>
    /// �����Լ���ɫ��������ͻ��˽�ɫ��ӽ�����
    /// </summary>
    /// <param name="player_Maincity_dic">���ػ���</param>
    /// <param name="pack"></param>
    //public void AddOtherPlayerMainCity(Dictionary<string, GameObject> player_Maincity_dic, PlayerPack player)
    //{

    //    int posIndex = 1;
    //    spawn_player_pos = GameObject.Find("SpawnPos").transform;
    //    Debug.Log("����Զ�̽�ɫ�ĸ��������꣺" + spawn_player_pos.position + "------------------");
    //    Vector3 pos = spawn_player_pos.position;
    //    pos.x = pos.x + posIndex++;
    //    GameObject obj = GameObject.Instantiate(character_pre, pos, Quaternion.identity);
    //    obj.name = player.PlayerName;
    //    obj.transform.SetParent(spawn_player_pos);
    //    Debug.Log("Զ�̽�ɫ�ĵ�ǰ���꣺" + obj.transform.position + "------------------");
    //    if (!player.PlayerName.Equals(GameLifeCycle.Instance.username))
    //    {
    //        //���������ͻ��˽�ɫ

    //        CharacterRistic characterRistic = obj.AddComponent<CharacterRistic>();
    //        characterRistic.username = player.PlayerName;
    //        obj.AddComponent<RemoteCharacter>();

    //    }
    //    player_Maincity_dic.Add(player.PlayerName, obj);


    //}

    public void RemoveAllPlayer(Dictionary<string, GameObject> dic)
    {
        //����һ��������
        //var enumerator = dic.GetEnumerator();
        //while (enumerator.MoveNext())
        //{

        //    GameObject.Destroy(enumerator.Current.Value);
        //    dic.Remove(enumerator.Current.Key);
        //}

        //����������ֵ��
        //foreach (KeyValuePair<string,GameObject> pair in dic)
        //{
        //    GameObject.Destroy(pair.Value);
        //}
        //dic.Clear();
        //dic = null;


        //��������ȡ��Ԫ��
        for (int i = 0; i < dic.Keys.Count; i++)
        {
            GameObject s = dic.ElementAt(i).Value;
            GameObject.Destroy(s);
        }
        dic.Clear();
        dic = null;

    }
    public void RemovePlayer(string name)
    {
        if (player_dic.TryGetValue(name, out GameObject result))
        {
            GameObject.Destroy(result);
            player_dic.Remove(name);
        }
        else
        {
            Debug.Log("�����ڸý�ɫ,�Ƴ���ɫ����");
        }
        player_dic.Remove(name);
    }


    /// <summary>
    /// �˳�ս��
    /// </summary>
    //public void BattleExit()
    //{
    //    foreach (var item in player_dic.Values)
    //    {
    //        GameObject.Destroy(item);
    //    }
    //    player_dic.Clear();
    //    UIManager.Instance.PopPanel();
    //    UIManager.Instance.PopPanel();

    //}
    public void BattleExit()
    {

    }


    /// <summary>
    /// Զ�˽�ɫλ��ͬ��
    /// </summary>
    /// <param name="pack"></param>
    public void SyncPosition(Mainpack pack, Dictionary<string, GameObject> dic)
    {
        try
        {
            foreach (var item in pack.Playerpack)
            {
                PosPack posPack = item.Pos;
                if (dic.TryGetValue(item.PlayerName, out GameObject result))
                {
                    Vector3 pos = new Vector3(posPack.PosX, posPack.PosY, posPack.PosZ);
                    float characterRot = posPack.RotY;
                    result.GetComponent<RemoteCharacter>().SetState(pos, characterRot);
                }
            }

            //if (player_dic.TryGetValue(pack.Playerpack[0].PlayerName, out GameObject result))
            //{
            //    Vector3 pos = new Vector3(posPack.PosX, posPack.PosY, posPack.PosZ);
            //    float characterRot = posPack.RotY;

            //    //result.transform.position = pos;
            //    //result.transform.eulerAngles=new Vector3(0,characterRot,0);
            //    result.GetComponent<RemoteCharacter>().SetState(pos, characterRot);
            //}
        }
        catch (System.Exception e)
        {

            Debug.Log("λ��ͬ�������쳣" + e.Message);
        }

    }
    public void SyncAnim(Mainpack pack)
    {
        try
        {
            if (SceneManager.GetActiveScene().name == "MainCity")
            {
                if (player_dic.TryGetValue(pack.User, out GameObject result))
                {
                    result.GetComponent<RemoteCharacter>().SetSkill(pack.Description);
                }
            }
            else if (SceneManager.GetActiveScene().name == "BattleScene")
            {
                if (GameLifeCycle.Instance.playersOnGame.TryGetValue(pack.User, out GameObject result))
                {
                    result.GetComponent<RemoteCharacter>().SetSkill(pack.Description);
                }
            }


        }
        catch (System.Exception e)
        {

            Debug.Log("����ͬ�������쳣" + e.Message);
        }
    }


    //public void GeneratePlayerIntoMaincity()
    //{

    //    Transform pos = new GameObject("SpawnPos").transform;
    //    pos.position = new Vector3(50, 0, 10);
    //    //  Vector3 pos = spawn_player_pos.position;
    //    GameObject obj = GameObject.Instantiate(character_pre, pos.position, Quaternion.identity);
    //    obj.transform.SetParent(pos);
    //    obj.name = GameLifeCycle.Instance.username;


    //    drag_controller.characterController = obj.AddComponent<InputController>();
    //    obj.AddComponent<UpdatePosRequest>();
    //    obj.AddComponent<SyncPosition>();
    //    obj.AddComponent<CharacterMission>();



    //    obj.AddComponent<AStarAlgorithm>();

    //    GameLifeCycle.Instance.obj = obj;
    //    GameLifeCycle.Instance.AddCamera(50, 3.58f, 3.62f);
    //    MoveViewController view_controller = GameObject.Find("CameraFollow").GetComponent<MoveViewController>();

    //    view_controller.player = obj.transform;
    //    view_controller.SetOffect(pos.position.x);

    //    if (!player_dic.Keys.Contains(obj.name))
    //    {
    //        player_dic.Add(obj.name, obj);
    //    }
    //    else if (player_dic[obj.name] == null)
    //    {
    //        player_dic[obj.name] = obj;
    //    }

    //}



    //�������߿ͻ���
    public void SetOfflineClinet(Mainpack mainpack)
    {
        List<string> l = new List<string>();
        foreach (var item in mainpack.Playerpack)
        {
            l.Add(item.PlayerName);
        }
        if (l.Count == 0)
        {
            RemoveAllPlayer(Player_dic);
        }

        for (int i = 0; i < Player_dic.Keys.Count; i++)
        {
            string a = Player_dic.ElementAt(i).Key;
            if (!l.Contains(a))
            {
                RemovePlayer(a);
            }
        }
    }

    public Dictionary<string, GameObject> UpdateBattlePlayer(Dictionary<string, GameObject> dic, Mainpack mainpack)
    {
        Dictionary<string, GameObject> result = new Dictionary<string, GameObject>();
        foreach (var item in mainpack.Playerpack)
        {
            dic.TryGetValue(item.PlayerName, out var player);
            result.Add(item.PlayerName, player);
        }
        GameLifeCycle.Instance.player_Battle_dic = result;
        return result;
    }



    public void OnDestroy()
    {
        instance = null;
        RemoveAllPlayer(player_dic);

    }
}
