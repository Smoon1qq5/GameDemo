


--登录界面控制器
LoginController ={} 
LoginController.UserInput={}
LoginController.Page=nil
function LoginController:Start()

   ABManager:LoadFile("loginuiprefab")

   local  obj = ABManager:LoadAsset("loginuiprefab","Login 1",typeof(CS.UnityEngine.Object))
   --加载预制体
   local uipage=prefab:Instantiate(obj)
   LoginController.Page=uipage
   --绑定事件
   uipage.transform:Find("Background/Register"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener( 
    function() 
    RegisterController.Page:SetActive(true) 
    end
    )
   uipage.transform:Find("Background/Login"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(LoginController.LoginGame)
   LoginController.UserInput.UserNameText=LoginController.Page.transform:Find("Background/UserInfo/NameValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))
   LoginController.UserInput.PasswordText=LoginController.Page.transform:Find("Background/UserInfo/PwValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))


end

function LoginController:LoginGame()
  local  _loginInfo=CS.PBLogin.TcpLogin()
  _loginInfo.UserName=LoginController.UserInput.UserNameText.text
  _loginInfo.Password=LoginController.UserInput.PasswordText.text
  print("用户名为：".._loginInfo.UserName)
  local mes=CS.CSDataHandle.GetSendMessage(_loginInfo,CS.ProtoCommon.C2SID.TcpLogin)
  CS.ClientTCP.Instance:SendMessage(mes)
 -- print("整个包的长度："..#mes)
  CS.System.Threading.Thread.Sleep(500) --要确保收到服务器端发来的消息 否则接下来的判断一直是false   事件？？？？？
  print(CS.ClientTCP.Instance.isLogin)
  --print(CS.ClientTCP.Instance._logion_result.Result)
  if(CS.ClientTCP.Instance._logion_result~=nil)
    then
       if CS.ClientTCP.Instance._logion_result.Result==true
         then
    --跳转页面
          ServerInfoController.Page:SetActive(true) 
          CS.ClientTCP.Instance.isLogin=false  
          LoginController.Page:SetActive(false) 
        else 
    --弹窗失败
         AlertController:Alert("用户名和密码输入不正确！请重新输入")
       end
  end

end

return LoginController