using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;

public class UserData
{
    
    public bool Register(Mainpack mainpack, MySqlConnection sqlConnection)
    {
        string username = mainpack.Loginpack.Username;
        string password = mainpack.Loginpack.Password;
        int playerindex = mainpack.Loginpack.Playerindex;
        int playerlevel = mainpack.Loginpack.Playerlevel;
        try
        {
            sqlConnection.Open();

            string sql = "INSERT INTO `mygamedb`.`userdata` (`username`,`password`,`playerindex`,`playerlevel`) VALUES ('" + username + "','" + password + "','" + playerindex + "','" + playerlevel + "')";
            MySqlCommand mySqlCommand = new MySqlCommand(sql, sqlConnection);
            mySqlCommand.ExecuteNonQuery();
            Console.WriteLine("注册成功，返回结果！！");
            sqlConnection.Close();
            return true;
          
        }
        catch (Exception e)
        {

            Console.WriteLine("写入数据异常" + e.Message);

            return false;
        }


    }
    public bool Login(Mainpack mainpack, MySqlConnection sqlConnection)
    {
        string username = mainpack.Loginpack.Username;
        string password = mainpack.Loginpack.Password;
        try
        {
           if(sqlConnection.State==ConnectionState.Closed)
            sqlConnection.Open();
            Console.WriteLine("flag1---数据库开启！");
            string sql = "SELECT * FROM mygamedb.userdata WHERE username=@username AND password=@password";

            MySqlCommand command = new MySqlCommand(sql, sqlConnection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            MySqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("读取数据");
            if (!reader.Read())
            {
                sqlConnection.Close();
                return false;
            }


            bool result = false;
            Console.WriteLine("用户名为：" + reader["username"]);
            Console.WriteLine("密码为：" + reader["password"]);
            if (reader["username"].ToString() == username && reader["password"].ToString() == password)
            {
                result = true;

            }

            reader.Close();
            Console.WriteLine("登录查询结果结束，返回结果！！");
            Console.WriteLine(result);
            sqlConnection.Close();
            Console.WriteLine("flag1---数据库关闭完成！");
            return result;
        }
        catch (Exception e)
        {

            Console.WriteLine("数据异常flag1" + e.Message);
            return false;
        }


    }
    public bool ChosePlayer(Mainpack mainpack, MySqlConnection sqlConnection)
    {
        string username = mainpack.Loginpack.Username;
        int playerindex = mainpack.Loginpack.Playerindex;
        Console.WriteLine("要更改的角色名为："+username);
        try
        {
            sqlConnection.Open();
            string sql = "UPDATE mygamedb.userdata  SET  playerindex=@playerindex  WHERE username=@username ";

            MySqlCommand command = new MySqlCommand(sql, sqlConnection);
            command.Parameters.AddWithValue("@playerindex", playerindex);
            command.Parameters.AddWithValue("@username", username);
         int result=   command.ExecuteNonQuery();
            Console.WriteLine("选择角色，在服务器记录完成！！"+"更改的行数为："+result);
            Console.WriteLine("角色为："+playerindex);
            sqlConnection.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("数据异常flag2" + e.ToString());
            return false;
        }


    }
    public Mainpack CheckInfo(Mainpack mainpack, MySqlConnection sqlConnection)
    {

        string username = mainpack.Description;

        LoginPack loginpack = new LoginPack();

        try
        {
            sqlConnection.Open();
            string sql = "SELECT  playerindex,playerlevel FROM mygamedb.userdata WHERE username=@username";
            MySqlCommand command = new MySqlCommand(sql, sqlConnection);
            command.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = command.ExecuteReader();
            if (rdr.Read())
            {
                loginpack.Playerindex = (int)rdr[0];
                loginpack.Playerlevel = (int)rdr[1];
            }
            rdr.Close();
            Console.WriteLine("检查角色信息结束，返回结果！！");
            mainpack.Loginpack = loginpack;
            sqlConnection.Close();
            return mainpack;
        }
        catch (Exception e)
        {
            Console.WriteLine("数据异常flag3----------" + e.ToString());
            return null;
        }
    }

    public bool CheckIsExistPlayer(Mainpack mainpack, MySqlConnection sqlConnection)
    {
        string username = mainpack.Loginpack.Username;
        Console.WriteLine("flag0x001------------:" + username);
        try
        {
            sqlConnection.Open();
            string sql = "SELECT  playerindex FROM mygamedb.userdata WHERE username=@username";
            MySqlCommand command = new MySqlCommand(sql, sqlConnection);
            command.Parameters.AddWithValue("@username", username);
            MySqlDataReader rdr = command.ExecuteReader();
            Console.WriteLine("flag0x002----------------");
            int result = 0;
            if (rdr.Read())
            {
                result = (int)rdr[0];
            }
            rdr.Close();
            Console.WriteLine("检查角色信息结束，返回结果！！");
            Console.WriteLine("flag0x003----------------:" + result);
            sqlConnection.Close();
            return result == 0 ? false : true;
        }
        catch (Exception e)
        {
            Console.WriteLine("数据异常flag4" + e.ToString());
            return false;
        }
    }

    public void RecordLevel(Mainpack mainpack, MySqlConnection sqlConnection)
    {
        string username = mainpack.Description;
        int playerlevel = mainpack.Playerpack[0].Level;
        
        try
        {
            sqlConnection.Open();
            string sql = "UPDATE mygamedb.userdata  SET  playerlevel=@playerlevel  WHERE username=@username ";
            MySqlCommand command = new MySqlCommand(sql, sqlConnection);
            command.Parameters.AddWithValue("@playerlevel", playerlevel);
            command.Parameters.AddWithValue("@username", username);
            command.ExecuteNonQuery();
            sqlConnection.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine("数据异常flag5" + e.ToString());
         
        }
    }
}