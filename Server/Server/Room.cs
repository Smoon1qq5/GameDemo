using Google.Protobuf.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Room
{
    RoomPack roominfo;
    Server server = null;
    List<Client> client_list = new List<Client>();
    public RoomPack GetRoomInfo
    {
        get
        {
            roominfo.Curnum = client_list.Count;
            return roominfo;
        }
    }

    public Room(Client client, RoomPack roomPack, Server server)
    {
        this.server = server;
        client_list.Add(client);
        roominfo = roomPack;
        client.GetRoom = this;

    }

    public RepeatedField<PlayerPack> GetPlayerInfo()
    {
        RepeatedField<PlayerPack> pack = new RepeatedField<PlayerPack>();

        foreach (var item in client_list)
        {
            PlayerPack playerPack = new PlayerPack();
            playerPack.PlayerName = item.GetUserInfo.UserName;
            playerPack.Level = item.GetUserInfo.Level;
            pack.Add(playerPack);
        }
        return pack;
    }


    public void Join(Client client)
    {
        client_list.Add(client);
        if (client_list.Count >= roominfo.Maxnum)
        {
            roominfo.State = 1;
        }
        client.GetRoom = this;
        Mainpack mainpack = new Mainpack();
        mainpack.Actioncode = ActionCode.UpdateRoomPlayer;
        foreach (var item in GetPlayerInfo())
        {
            mainpack.Playerpack.Add(item);
        }
        server.Bradcast(client, mainpack);
    }
    /// <summary>
    /// 离开组队房间
    /// </summary>
    /// <param name="client"></param>
    public void Exit(Client client)
    {
        Mainpack mainpack = new Mainpack();

        ////房主离开
        //if (client == client_list[0])
        //{ 
        //    mainpack.Actioncode = ActionCode.ExitRoom;
        //    server.Bradcast(client, mainpack);

        //    return;
        //}
        client_list.Remove(client);

        if (client_list.Count == 0)
        {
            client.GetRoom = null;
            server.RemoveRoom(this);
        }
        roominfo.State = 0;
        mainpack.Actioncode = ActionCode.UpdateRoomPlayer;
        foreach (var item in GetPlayerInfo())
        {
            mainpack.Playerpack.Add(item);
        }
        server.Bradcast(client, mainpack);
        //玩家离开战斗场景加入主城
        server.mainCity.Players.Add(client);    
    }

    /// <summary>
    /// 离开游戏房间
    /// </summary>
    /// <param name="client"></param>
    public void ExitGameRoom(Client client,Mainpack pack)
    {
        Mainpack mainpack = new Mainpack();
        //如果是房主 关闭这个房间 广播给房间的其他人
        //if (client == client_list[0])
        //{
        //    mainpack.Actioncode = ActionCode.ExitRoom;
        //    mainpack.Description = "r";
        //    server.Bradcast(client,mainpack);
        //    server.RemoveRoom(this);
        //    client.GetRoom = null;
        //}
        //else
        //{
        client_list.Remove(client);
        if (client_list.Count == 0)
        {
            client.GetRoom = null;
            server.RemoveRoom(this);
            return;
        }
        client.GetRoom = null;  
        mainpack.Actioncode = ActionCode.UpdatePlayerList;
        foreach (var item in client_list)
        {
            PlayerPack pack1 = new PlayerPack();
            pack1.PlayerName = item.GetUserInfo.UserName;
            pack1.Hp = item.GetUserInfo.HP;
            mainpack.Playerpack.Add(pack1);
        }
        mainpack.Description = client.GetUserInfo.UserName;
        server.Bradcast(client, mainpack);
        //}
    }


    public Response StartGame(Client client)
    {
        if (client != client_list[0])
        {

            return Response.Failed;
        }
        roominfo.State = 2;
        Thread startTime = new Thread(Time);
        startTime.Start();
        
        return Response.Succeed;
    }


    private void Time()
    {
        Mainpack mainpack = new Mainpack();
        mainpack.Actioncode = ActionCode.Chat;
        mainpack.Description = "房主已启动游戏";
        System.Console.WriteLine("倒计时中的server是否为空：" + server);
        server.Bradcast(null, mainpack);
        Thread.Sleep(1000);
        for (int i = 5; i > 0; i--)
        {
            System.Console.WriteLine("---倒计时：" + i.ToString());

            mainpack.Description = i.ToString();
            server.Bradcast(null, mainpack);
            Thread.Sleep(1000);
        }
        mainpack.Actioncode = ActionCode.Beginning;
        System.Console.WriteLine("游戏开始！！！！！！！！！！！！-------");
        foreach (var item in client_list)
        {
            PlayerPack playerPack = new PlayerPack();
            item.GetUserInfo.HP = 100;
            playerPack.PlayerName = item.GetUserInfo.UserName;
            playerPack.Hp = item.GetUserInfo.HP;

            mainpack.Playerpack.Add(playerPack);
        }
        server.Bradcast(null, mainpack);
        System.Console.WriteLine(" 游戏玩家的数量：" + mainpack.Playerpack.Count);
    }
}

