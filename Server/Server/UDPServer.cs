using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPServer
{
    Socket udpSocket;
    IPEndPoint bindEP;//本地监听ip
    EndPoint remoteEP;//远端ip

    Server server;
    ControllerManager controllerManager;
    byte[] buffer=new byte[1024];

    Thread receiveThread;
    bool is_run = false;

    public UDPServer(int port,Server server, ControllerManager controllerManager)
    {
        this.server = server;
        this.controllerManager = controllerManager;
        bindEP = new IPEndPoint(IPAddress.Any, port);
        remoteEP = bindEP;
        StartUDPSocket();
    }
    ~UDPServer()
    {
        if (receiveThread != null)
        {
            receiveThread.Abort();
            receiveThread = null;
        }
    }

    private void StartUDPSocket()
    {
        try
        {
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(bindEP);
            is_run=true;
            receiveThread = new Thread(ReceiveMsg);
            receiveThread.Start();  
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine("开启UDPServer出现异常: "+e.Message);
            throw;
        }
    }

    private void ReceiveMsg()
    {
        while (is_run)
        {
            try
            {
                int len = udpSocket.ReceiveFrom(buffer, ref remoteEP);
                Mainpack pack = (Mainpack)Mainpack.Descriptor.Parser.ParseFrom(buffer, 0, len);
                HandleRequest(pack, remoteEP);
            }
            catch (Exception e)
            {

                Console.WriteLine("udp接受数据异常："+e.Message);
            }
       
        }

    }

    private void HandleRequest(Mainpack pack, EndPoint remoteEP)
    {
        Client client = server.ClientFromUsername(pack.User);
        if (client.IEP == null)
        {
            client.IEP = remoteEP;  //设置Client里的ip  为客户端发送udp消息的时候用
        }

        controllerManager.HandleRequest(pack, client,true);
    }

    public void SendByUDP(Mainpack pack, EndPoint endPoint)
    {
        byte[] buff=Message.Instance.PackDataUDP(pack);
        udpSocket.SendTo(buff,buff.Length,SocketFlags.None, endPoint);
    }
}
