using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ClientManager
{

    private Socket socket;
    private bool _isRun = false;
    private static ClientManager instance;
    public static ClientManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ClientManager();
            }
            return instance;
        }
    }
    public void Init()
    {
        ConnectServer(NetConfig.ServerIP);
        InitUDP();
    }
   
    public void OnDestroy()
    {
        instance = null;
        CloseSocket();
    }
    public void ConnectServer(string _ip)
    {
        try
        {

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint _endpoint = new IPEndPoint(IPAddress.Parse(_ip), NetConfig.TcpSeverPort);

            socket.BeginConnect(_endpoint, RequestConnectCallback, socket);
        }
        catch (Exception e)
        {

            Debug.Log("连接出错！" + e.Message);
        }

    }
    private void CloseSocket()
    {
        if (socket.Connected && socket != null)
        {
            socket.Close();
            _isRun = false;
        }
    }
    private void RequestConnectCallback(IAsyncResult ar)
    {
        try
        {

            Socket client = (Socket)ar.AsyncState;
            client.EndConnect(ar);
            Debug.Log("连接成功！！");
            _isRun = true;
            Thread receiveThread = new Thread(ReceiveMessage);
            receiveThread.Start();
        }
        catch (Exception e)
        {

            Debug.Log("tcp连接异常:" + e.Message);
        }

    }

    private void ReceiveMessage()
    {

        try
        {
            socket.BeginReceive(Message.Instance.Buffer, Message.Instance.StartIndex, Message.Instance.ReminSize, SocketFlags.None, ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Debug.Log("接收服务端数据异常:" + e.Message);


        }


    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (socket == null || socket.Connected == false) return;
            int len = socket.EndReceive(ar);
            if (len == 0)
            {
                CloseSocket();
                return;
            }
            Message.Instance.RedaBuffer(len, HandleRequest);
            ReceiveMessage();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void HandleRequest(Mainpack obj)
    {
        try
        {
            RequestManager.Instance.HandleResponse(obj);
        }
        catch (Exception e)
        {

            Debug.Log(e);
        }
    }

    public void SendMessage(Mainpack pack)
    {
        Debug.Log(socket + "发送消息");
        socket.Send(Message.Instance.PackData(pack));
    }



    private Socket udpClient;
    private IPEndPoint ipEndPoint;
    private EndPoint EPoint;
    private Byte[] buffer = new Byte[1024];

    Thread receiveThread;



    private void InitUDP()
    {
        udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        ipEndPoint = new IPEndPoint(IPAddress.Parse(NetConfig.ServerIP), 6667);
        EPoint = ipEndPoint;
        try
        {
            udpClient.Connect(EPoint);
            Loom.RunAsync(() =>
            {
                receiveThread = new Thread(ReceiveMsg);
                receiveThread.Start();
            });

        }
        catch
        {
            Debug.Log("UDP连接失败！");
            return;
        }



    }

    private void ReceiveMsg(object obj)
    {
        while (true)
        {

            int len = udpClient.ReceiveFrom(buffer, ref EPoint);
            Mainpack pack = (Mainpack)Mainpack.Descriptor.Parser.ParseFrom(buffer, 0, len);
            //  HandleResponse(pack);

            Loom.QueueOnMainThread((s) => { HandleRequest(pack); }, null

                  );

        }


        //执行处理消息
    }



    public void SendTo(Mainpack pack)
    {
        pack.User = GameLifeCycle.Instance.username;

        Byte[] sendBuff = Message.Instance.PackDataUDP(pack);

        udpClient.Send(sendBuff, sendBuff.Length, SocketFlags.None);

    }
}
public class User
{
    public string userName;
    public string btnName;
}