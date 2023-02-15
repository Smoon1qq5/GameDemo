using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

public class Client
{
    private const string str = "server=localhost;user=root;database=mygamedb;port=3306;password=123456";
    private Socket socket;
   
    private UserInfo userInfo;
    private Server server;
    private UserData userData;
    private PackageData packageData;
    private MySqlConnection mySqlConnection;

    private EndPoint remoteEP;//远端IP
    private UDPServer UDPServer;


   // public List<Items> items = new List<Items>(36); //记录玩家的背包格子内容


    public MySqlConnection GetMySqlConnection
    {
        get { return mySqlConnection; }
    }
    public UserInfo GetUserInfo { get { return userInfo; } }
   
    public UserData GetUserData { get { return userData; } }   
    public PackageData GetPackageData { get { return packageData; } }   
    public Room GetRoom { get; set; }   

    public EndPoint IEP { get { return remoteEP; } set { remoteEP = value; } }
   public bool IsConnected { get { return socket.Connected; } }   
    public Client(Socket socket, Server server, UDPServer uDPServer)
    {

        this.socket = socket;
      
        mySqlConnection = new MySqlConnection(str);

        userInfo = new UserInfo();
        userData = new UserData();
        packageData = new PackageData();    

        this.server = server;
        UDPServer = uDPServer;
        ReceiveMessage();
     


    }

    private void ReceiveMessage()
    {
        socket.BeginReceive(Message.Instance.Buffer, Message.Instance.StartIndex, Message.Instance.ReminSize, SocketFlags.None, ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult ar)
    {

        try
        {
            if (socket == null || socket.Connected == false)
            {

                return;
            }
            int len = socket.EndReceive(ar);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("消息长度为：" + len);
            if (len == 0)
            {
                Close();
                return;
            }
            Message.Instance.RedaBuffer(len, HandleRequestFunc);


            ReceiveMessage();
        }
        catch (Exception e)
        {
            Close();
            Console.WriteLine("接受消息异常：" + e.Message);
        }


    }

    private void HandleRequestFunc(Mainpack obj)
    {
        server.HandleRequest(obj, this);
    }

    internal void SendMessage(Mainpack result)
    {
        if (socket == null || socket.Connected == false) return;
        try
        {
            socket.Send(Message.Instance.PackData(result));
        }
        catch (Exception e)
        {

            Console.WriteLine("发送消息出现错误"+e.Message);
        }
      
    }

    public void SendMessageByUdp(Mainpack pack)
    {
        if (IEP == null) return;
        UDPServer.SendByUDP(pack,IEP);
    }



    public void UpdatePos(Mainpack pack)
    {
        GetUserInfo.Pos = pack.Playerpack[0].Pos;
    }



    private void Close()
    {
        if (GetRoom != null)
        {
            GetRoom.Exit(this);
        }
        socket.Close();
   
        server.RemoveClient(this);
    }


    /// <summary>
    /// 存储用户信息
    /// </summary>
    public class UserInfo
    {
        public string UserName { get; set; }
        public int HP { get; set; }
        public int Level { get;set; }   
        public PosPack Pos { get; set; }
    }
    
}
