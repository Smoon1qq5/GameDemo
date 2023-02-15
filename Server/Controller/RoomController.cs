using System.Collections;

public class RoomController : BaseController
{
    public RoomController()
    {
        request = Request.Room;
    }
    public Mainpack Chat(Server server, Client client, Mainpack pack)
    {
        server.Chat(client, pack);
        return null;
    }
    //CreateRoom
    //SearchRoom
    //JoinRoom
    //ExitRoom
    //Startgame
    public Mainpack CreateRoom(Server server, Client client, Mainpack pack)
    {
        return server.CreateRoom(client, pack); 
    }
    public Mainpack SearchRoom(Server server, Client client, Mainpack pack)
    {
        return server.SearchRoom(); 
    }
    public Mainpack JoinRoom(Server server, Client client, Mainpack pack)
    {
        return server.JoinRoom(client, pack);
    }
    public Mainpack ExitRoom(Server server, Client client, Mainpack pack)
    {

        return server .ExitRoom(client, pack);  
    }
    public Mainpack Startgame(Server server, Client client, Mainpack pack)
    {

        pack.Respone = client.GetRoom.StartGame(client);
        
        System.Console.WriteLine("-------开始游戏："+pack.Respone.ToString());
         foreach (var item in pack.Playerpack)
        {
            System.Console.WriteLine("要进入游戏场景的ID："+item.PlayerName);
        }
        System.Console.WriteLine("发送的包玩家数量为：" + pack.Playerpack.Count);
        foreach (var item in client.GetRoom.GetPlayerInfo())
        {
            pack.Playerpack.Add(item);
        } 
        server.Bradcast(null, pack);
        //开始游戏需要把自己从主城的缓存中去除 (需要把房间里的人都带上嘛？)
        server.mainCity.GetOffMainCIty(client);

        for (int i = 0; i < server.clients_list.Count; i++)
        {
            foreach (var item in pack.Playerpack)
            {
                if (item.PlayerName == server.clients_list[i].GetUserInfo.UserName)
                {
                    server.mainCity.GetOffMainCIty(server.clients_list[i]);

                }
            }
        }
        server.Bradcast(null, server.mainCity.SetPlayerIntoCity(ActionCode.EnterGameCity), server.mainCity.Players);
        //server.Bradcast(null, pack,server.mainCity.players);



        //把进入游戏场景的人从主城中去除


        return null;
    }

    public void Online(Server server, Client client, Mainpack pack)
    {
        //需要 把在线的玩家从服务器的列表中 挑出来
        //单独放在主城列表里
        server.mainCity.ChosePlayer(server, pack);
       // server.mainCity.ChackCityAttendance();       
    }


    public void GetbackMaincity(Server server, Client client, Mainpack pack)
    {
        //从战斗场景返回主城    查询主城中的人并返回包
        //把自己添加到主城中的列表去
        System.Console.WriteLine("--------------------------------接收到离开战斗场景消息--------------");
        server.mainCity.BackofMainCity(server, pack);
    }

}
