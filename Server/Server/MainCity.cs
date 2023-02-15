using Google.Protobuf.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainCity
{
    private List<Client> players = new List<Client>();
    public List<Client> Players { get { return players; } }
    Server server = null;

    public MainCity(Server server)
    {
        this.server = server;

    }
    ~MainCity()
    {
        players.Clear();
    }


    //主城中需要存储所有玩家的信息

    /// <summary>
    /// 设置在主城中的玩家
    /// </summary>
    /// <returns></returns>
    public Mainpack SetPlayerIntoCity(ActionCode actionCode)
    {
        System.Console.WriteLine("当前主城中的的客户端数量：" + players.Count);
        Mainpack pack = new Mainpack();
        pack.Actioncode = actionCode;
        pack.Description = "str";
        if (players.Count != 0)
        {
            foreach (Client client in players)
            {
                if (client.GetUserInfo.UserName == null) break;
                PlayerPack playerPack = new PlayerPack();
                System.Console.WriteLine("当前client的名称：" + client.GetUserInfo.UserName);
                playerPack.PlayerName = client.GetUserInfo.UserName;
                pack.Playerpack.Add(playerPack);
            }
        }

        return pack;
    }


    public void ChosePlayer(Server server, Mainpack mainpack)
    {
        
        Console.WriteLine("Maincity Members：" + players.Count);
        foreach (var item in server.clients_list)
        {

            if (item.GetUserInfo.UserName == mainpack.Description /*&& !players.Contains(item)*/)
            {
                System.Console.WriteLine("当前客户端名称：" + item.GetUserInfo.UserName + "------包中客户端名称：" + mainpack.Description + "Flag02_MainCity");
                GetOnMainCIty(item);
              //  players.Add(item);


                break;
            }
        }
        server.Bradcast(null, SetPlayerIntoCity(ActionCode.Online));
        System.Console.WriteLine("--------------客户端数量：" + players.Count);
  



    }

    //下线处理
    public void OfflineProcessing(Server server)
    {
        for (int i = 0; i < players.Count; i++)
        {
            System.Console.WriteLine("服务器存在的客户端名称：" + players[i].GetUserInfo.UserName);

            
            if (!server.clients_list.Contains(players[i]))
            {
                GetOffMainCIty(players[i]);
               // players.Remove(players[i]);

            }

        }
        server.Bradcast(null, SetPlayerIntoCity(ActionCode.Offline));
    }
    public void OnlineProcessing(Server server)
    {


       
        for (int i = 0; i < server.clients_list.Count; i++)
        {
            System.Console.WriteLine("flag maincity-online服务器存在的客户端名称：" + server.clients_list[i].GetUserInfo.UserName);

            GetOnMainCIty(server.clients_list[i]);
            

        }
        server.Bradcast(null, SetPlayerIntoCity(ActionCode.Online));
    }





    public void BackofMainCity(Server server, Mainpack mainpack)
    {
        try
        {
            foreach (var item in server.clients_list)
            {
                if (item.GetUserInfo.UserName == mainpack.Description)
                {
                    Console.WriteLine("从服务器中查询现在的角色：" + item.GetUserInfo.UserName);
                    GetOnMainCIty(item);
                    //不应该给每服务器的玩家发送  应该给主城中的玩家发送（应当排除战斗场景中玩家接受此消息）
                    server.Bradcast(null, SetPlayerIntoCity(ActionCode.GetbackMaincity), players);
                    //由于该角色还没进入主城 所以还没收到上面的消息

                }

            }

            // server.Bradcast(null, GetbackIntoCity(mainpack));
        }
        catch (Exception e)
        {

            Console.WriteLine(e.Message);
        }

    }





    public void GetOffMainCIty(Client client)
    {
        if (players.Count != 0 && players.Contains(client))
        {

            players.Remove(client);
        }

    }



    public void GetOnMainCIty(Client client)
    {
        if (!players.Contains(client))
        {
            players.Add(client);
        }

    }
}