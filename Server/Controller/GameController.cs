using System.Collections;

public class GameController : BaseController
{
    public GameController()
    {
        request = Request.Game;
    }

    public Mainpack ExitGame(Server server, Client client, Mainpack pack)
    {
        client.GetRoom.ExitGameRoom(client ,pack);
       
        return null;
    }
    public Mainpack UpdateState(Server server, Client client, Mainpack pack)
    {
        
        server.BradcastByUdp(client, pack);
        foreach (var item in pack.Playerpack)
        {
            System.Console.WriteLine("当前物体的名称："+item.PlayerName+"其坐标为："+"X坐标：" + item.Pos.PosX + "\t" + "Z坐标：" + item.Pos.PosZ);
        }
       
        client.UpdatePos(pack);       
       
        return null;
    }
    public Mainpack OperateActoin(Server server, Client client, Mainpack pack)
    {

      client.GetUserInfo.UserName= pack.User ;
        System.Console.WriteLine( "玩家{0}使用了{1}技能",pack.User,pack.Description);
        System.Console.WriteLine(pack==null);
        System.Console.WriteLine(pack.Actioncode);
        server.Bradcast(client, pack);
        System.Console.WriteLine("技能转发完毕");
        return null;
    }
}
