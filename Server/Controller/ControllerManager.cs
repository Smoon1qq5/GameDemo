using System.Collections;
using System;
using System.Collections.Generic;
using System.Reflection;

public class ControllerManager
{
    private Dictionary<Request, BaseController> _controllers = new Dictionary<Request, BaseController>();
    private Server server;

    public ControllerManager(Server server)
    {
        this.server = server;
        UserController userController = new UserController();
        _controllers.Add(userController.GetRequest, userController);

        RoomController roomController = new RoomController();
        _controllers.Add(roomController.GetRequest, roomController);

        GameController gameController = new GameController();
        _controllers.Add(gameController.GetRequest, gameController);

        PackageController packageController = new PackageController();
        _controllers.Add(packageController.GetRequest, packageController);
    }

    public void HandleRequest(Mainpack mainpack, Client client,bool use_udp=false)
    {
        Console.WriteLine("开始处理消息");
        if (_controllers.TryGetValue(mainpack.Request, out BaseController controller))
        {
            Console.WriteLine("当前为"+controller);
            string methodName = mainpack.Actioncode.ToString();
            Console.WriteLine("方法名称:"+methodName);
            MethodInfo method = controller.GetType().GetMethod(methodName);
            Console.WriteLine("方法是否为空:"+(method == null).ToString()); 
            if (method == null)
            {
                Console.WriteLine("没有找到对应的处理方法");
                return;
            }
            object[] obj;
            if (use_udp) //udp
            {
                obj = new object[] { server, client, mainpack };
                method.Invoke(controller, obj);
            }else
            {
                obj = new object[] { server, client, mainpack };
                object result = method.Invoke(controller, obj);
                if (result != null)
                {
                    client.SendMessage((Mainpack)result);
                    Console.WriteLine("处理完成，发送消息成功！");
                }
            }


          
            
        }
        else
        {
            Console.WriteLine("没有找到对应的controller事件处理");
        }
    }
}
