using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class Server
{
    private Socket socket;
    private int port = 13001;
    private bool _isrun = false;
    public List<Client> clients_list = new List<Client>();
    private List<Room> roomlist = new List<Room>();
    private ControllerManager controllerManager;

    private Thread listenThread;

    public MainCity mainCity;
    private UDPServer UDPServer;
    public Server()
    {
        mainCity = new MainCity(this);
        controllerManager = new ControllerManager(this);
        StartServer();
        UDPServer = new UDPServer(6667, this, controllerManager);

        dic_Client = new Dictionary<int, ClientInfo>(500);
    }

    Thread scanClient;
    ~Server()
    {
        if (listenThread != null)
        {
            listenThread.Abort();
            listenThread = null;
        }
        if (scanClient != null)
        {
            scanClient.Abort();
            scanClient = null;
        }
    }
    public void StartServer()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Any, port));
            socket.Listen(0);
            _isrun = true;

            listenThread = new Thread(ListenClientConnect);
            listenThread.Start();

            //检测客户端是否在线的线程
            scanClient = new Thread(ScanClient);
            scanClient.Start();


        }
        catch (Exception e)
        {
            Console.WriteLine("服务器启动异常" + e);
        }

    }

    //检测客户端连接情况,处理主城中玩家的在线情况
    private void ScanClient()
    {
        int count = mainCity.Players.Count;
        while (true)
        {
            Console.WriteLine("Maincity Members：" + mainCity.Players.Count);

            for (int i = 0; i < clients_list.Count; i++)
            {
                Console.WriteLine(String.Format("当前第{0}个客户端的连接状态：", i) + clients_list[i].IsConnected);
            }
            switch (clients_list.Count.CompareTo(mainCity.Players.Count))
            {
                case 0:
                    break;
                case 1:
                    //有客户端上线或者返回主城
                   // mainCity.OnlineProcessing(this);
                    count = mainCity.Players.Count;
                    break;
                case -1:
                    //有客户端下线
                    //if (count > mainCity.Players.Count)
                    //{
                        mainCity.OfflineProcessing(this);
                        count = mainCity.Players.Count;
                    //}
                    
                    break;

            }

            Console.WriteLine("当前在线客户端数量为：" + clients_list.Count + "\t\t" + "------flag_server");
            //Console.WriteLine(MissionDataBase.Instance.missionDatas.Count);
            //Console.WriteLine(MissionDataBase.filePath);

            Thread.Sleep(3000);
        }



    }

    private void ListenClientConnect()
    {

        while (_isrun)
        {
            try
            {
                Console.WriteLine("正在监听");
                Socket clientSocket = socket.Accept();
                clients_list.Add(new Client(clientSocket, this, UDPServer));


            }
            catch (Exception e)
            {

                Console.WriteLine("监听异常" + e);
            }
        }
    }

    public void HandleRequest(Mainpack mainpack, Client client)
    {
        controllerManager.HandleRequest(mainpack, client);
    }

    public void RemoveClient(Client client)
    {
        clients_list.Remove(client);
        client = null;
    }

    //广播
    public void Bradcast(Client client, Mainpack pack)
    {

        foreach (var item in clients_list)
        {
            if (item.Equals(client))
            {
                continue;
            }
            item.SendMessage(pack);

        }
    }

    public void Bradcast(Client client, Mainpack pack, List<Client> clients)
    {
        foreach (var item in clients)
        {
            if (item.Equals(client))
            {
                continue;
            }
            item.SendMessage(pack);

        }
    }

    public void BradcastByUdp(Client client, Mainpack pack)
    {

        foreach (var item in clients_list)
        {
            if (item.Equals(client))
            {
                continue;
            }

            item.SendMessageByUdp(pack);

        }
    }

    public void Chat(Client client, Mainpack pack)
    {
        pack.Description = client.GetUserInfo.UserName + ":" + pack.Description;

        Bradcast(client, pack);
    }


    public Client ClientFromUsername(string name)
    {

        foreach (var item in clients_list)
        {

            if (item.GetUserInfo.UserName == name)
            {
                return item;
            }
        }
        return null;
    }



    //关于组队房间的方法
    //CreateRoom
    //SearchRoom
    //JoinRoom
    //ExitRoom
    //Startgame
    public Mainpack CreateRoom(Client client, Mainpack pack)
    {
        try
        {
            Room room = new Room(client, pack.Roompack[0], this);
            roomlist.Add(room);
            foreach (var item in room.GetPlayerInfo())
            {
                if (pack.Roompack[0].Maxnum <= 1)
                {
                    pack.Roompack[0].State = 1;
                }
                pack.Playerpack.Add(item);

            }
            pack.Respone = Response.Succeed;

            return pack;
        }
        catch (Exception e)
        {
            Console.WriteLine("创建房间失败" + e.Message);
            pack.Respone = Response.Failed;
            return pack;

        }
    }
    public Mainpack SearchRoom()
    {
        Mainpack pack = new Mainpack();
        pack.Actioncode = ActionCode.SearchRoom;
        try
        {
            if (roomlist.Count == 0)
            {
                pack.Respone = Response.NotRoom;
                return pack;
            }
            foreach (var item in roomlist)
            {
                pack.Roompack.Add(item.GetRoomInfo);
            }
            pack.Respone = Response.Succeed;
        }
        catch (Exception e)
        {
            Console.WriteLine("查找房间失败" + e.Message);
            pack.Respone = Response.Failed;
            throw;
        }
        return pack;
    }
    public Mainpack JoinRoom(Client client, Mainpack pack)
    {
        foreach (var item in roomlist)
        {
            if (item.GetRoomInfo.Roomname.Equals(pack.Description))
            {
                if (item.GetRoomInfo.State == 0)
                {
                    item.Join(client);
                    pack.Roompack.Add(item.GetRoomInfo);
                    Console.WriteLine("--------111------------房间内部玩家数量：" + item.GetPlayerInfo().Count);
                    //先清空默认值，再添加玩家
                    pack.Playerpack.Clear();
                    foreach (PlayerPack pack1 in item.GetPlayerInfo())
                    {
                        pack.Playerpack.Add(pack1);
                    }
                    pack.Respone = Response.Succeed;
                    Console.WriteLine("--------222------------房间内部玩家数量：" + pack.Playerpack.Count);
                    return pack;
                }
                else
                {
                    //房间不可加入
                    pack.Respone = Response.Failed;
                    return pack;
                }
            }
        }
        pack.Respone = Response.Failed;
        return pack;
    }
    public Mainpack ExitRoom(Client client, Mainpack pack)
    {
        if (client.GetRoom == null)
        {
            pack.Respone = Response.Failed;
            return pack;
        }
        client.GetRoom.Exit(client);
        pack.Respone = Response.Succeed;
        return pack;
    }



    public void RemoveRoom(Room room)
    {
        roomlist.Remove(room);
    }



    #region  ---------------心跳包----------------------


    //客户端上线委托
    public delegate void ClientOnlineHandler(ClientInfo info);
    //客户端下线委托
    public delegate void ClientOfflineHandler(ClientInfo info);

    public void StartHeartbeatThread()
    {
        // 开启扫描离线线程  
        Thread t = new Thread(ScanOffline);
        t.IsBackground = true;
        t.Start();
    }

    public ClientOnlineHandler OnClientOnline;
    public ClientOfflineHandler OnClientOffline;

    //存储客户端ID和客户端信息  字典
    private Dictionary<int, ClientInfo> dic_Client;

    //需要开启离线扫描线程
    /// <summary>
    /// 扫描离线
    /// </summary>
    private void ScanOffline()
    {
        while (true)
        {
            lock (dic_Client)
            {
                foreach (int id in dic_Client.Keys)
                {
                    ClientInfo clientInfo = dic_Client[id];
                    //如果已经离线不用管
                    if (!clientInfo.State)
                    {
                        continue;
                    }
                    //判断最后心跳时间是否大于3秒
                    TimeSpan sp = DateTime.Now - clientInfo.LastHeartbeatTime;
                    if (sp.Seconds >= 3)
                    {

                        //离线，触发离线时间
                        if (OnClientOffline != null)
                            OnClientOffline(clientInfo);

                        //修改状态
                        clientInfo.State = false;
                    }
                }
            }
        }

    }

    /// <summary>
    /// 接受心跳包
    /// </summary>
    /// <param name="clientID"></param>
    private void ReceiveHeartbeat(int clientID)
    {
        lock (dic_Client)
        {
            ClientInfo clientInfo;

            if (dic_Client.TryGetValue(clientID, out clientInfo))
            {
                //客户端在线，更新最后心跳时间
                clientInfo.LastHeartbeatTime = DateTime.Now;
            }
            else
            {
                //新上线
                clientInfo = new ClientInfo();
                clientInfo.LastHeartbeatTime = DateTime.Now;
                clientInfo.ClientID = clientID;
                clientInfo.State = true;

                dic_Client.Add(clientID, clientInfo);

                //触发上线事件
                if (OnClientOnline != null)
                {
                    OnClientOnline(clientInfo);
                }
            }
        }




    }


    #endregion
}
