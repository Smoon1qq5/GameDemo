using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

                                                                                                                                                  
class Program
{
    
    
    static void Main(string[] args)
    {
        Server server = new Server();


        //  Console.WriteLine(MissionDataBase.filePath);
       


        while (true)
        {

        }

        
    }

    /// <summary>
    /// 客户端离线提示
    /// </summary>
    /// <param name="clientInfo"></param>
    private static void ClientOffline(ClientInfo clientInfo)
    {
        Console.WriteLine(String.Format("客户端{0}离线，离线时间：\t{1}", clientInfo.ClientID, clientInfo.LastHeartbeatTime));
    }
    /// <summary>
    /// 客户端上线提示  
    /// </summary>
    /// <param name="clientInfo"></param>
    private static void ClientOnline(ClientInfo clientInfo)
    {
        Console.WriteLine(String.Format("客户端{0}离线，上线时间：\t{1}", clientInfo.ClientID, clientInfo.LastHeartbeatTime));

    }
}
