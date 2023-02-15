using System.Collections;

public class UserController : BaseController
{

    public UserController()
    {
        request = Request.User;
    }

    public Mainpack Register(Server server, Client client, Mainpack pack)
    {
        if (client.GetUserData.Register(pack, client.GetMySqlConnection))
        {
            pack.Respone = Response.Succeed;
        }
        else
        {
            pack.Respone = Response.Failed;
        }
        return pack;
    }
    public Mainpack Login(Server server, Client client, Mainpack pack)
    {
        if (client.GetUserData.Login(pack, client.GetMySqlConnection))
        {
            client.GetUserInfo.UserName = pack.Loginpack.Username;

            if (client.GetUserData.CheckIsExistPlayer(pack, client.GetMySqlConnection))
            {
                pack.Respone = Response.Succeed;
            }else
            {
                pack.Respone = Response.NeedChose;
            }
            
        }
        else
        {
            pack.Respone = Response.Failed;
        }
        return pack;
    }
    public Mainpack ChosePlayer(Server server, Client client, Mainpack pack)
    {
        if (client.GetUserData.ChosePlayer(pack, client.GetMySqlConnection))
        {

            pack.Respone = Response.Succeed;
        }
        else
        {
            pack.Respone = Response.Failed;

        }

        return pack;
    }

    public Mainpack CheckInfo(Server server, Client client, Mainpack pack)
    {
        pack.Description = client.GetUserInfo.UserName;
        Mainpack mainpack = client.GetUserData.CheckInfo(pack, client.GetMySqlConnection);
        client.GetUserInfo.Level = mainpack.Loginpack.Playerlevel;
        return mainpack;



    }

    public Mainpack MissionCode(Server server, Client client, Mainpack pack)
    {
        int requireCount = pack.Missionpack.Count;
        int count = 10000;
        System.Console.WriteLine("任务数量："+MissionDataBase.Instance.missionDatas.Count+"---------------------------");
        for (int i = 0; i < MissionDataBase.Instance.missionDatas.Count; i++)
        {
            if (MissionDataBase.Instance.missionDatas[i].id == pack.Missionpack.MissionID)
            {
                count = MissionDataBase.Instance.missionDatas[0].require.goodsCount;
            }
        }
        System.Console.WriteLine(count+"------------------------------");
        if (requireCount >= count)
        {
            pack.Respone = Response.Succeed;
            return pack;
        }
        return null;
    }
    public Mainpack LevelAction(Server server, Client client, Mainpack pack)
    {
        client.GetUserData.RecordLevel(pack, client.GetMySqlConnection);
        System.Console.WriteLine("当前角色等级为："+ pack.Playerpack[0].Level);
    
        return null;
    }

   
}
