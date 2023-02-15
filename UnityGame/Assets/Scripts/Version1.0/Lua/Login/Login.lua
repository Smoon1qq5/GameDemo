
Login ={} 
Login.UIPrefab={}
Login.UserInput={}
--require("Register")
--加载LoginUI
function Login:Start()
    --print("开始加载UI!!!!!!!!!!!!")
  local login= ABManager:LoadAsset("Login 1",typeof(CS.UnityEngine.Object))
  local  uiLoginObj =  prefab:Instantiate(login)
  uiLoginObj.transform:Find("Background/Register"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(Login.Register)
  uiLoginObj.transform:Find("Background/Login"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(Login.LoginGame)
  Login.UIPrefab.uiLoginObj=uiLoginObj
  Login.UserInput.UserNameText=Login.UIPrefab.uiLoginObj.transform:Find("Background/UserInfo/NameValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))
	Login.UserInput.PasswordText=Login.UIPrefab.uiLoginObj.transform:Find("Background/UserInfo/PwValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))


	local  register= ABManager:LoadAsset("Register 1",typeof(CS.UnityEngine.Object))
  local  uiregister=prefab:Instantiate(register)
  uiregister.transform:Find("Window/Register"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(Register.Register)
  uiregister.transform:Find("Window/GoBack"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(Register.Destroy)
  Login.UIPrefab.uiregister=uiregister
	uiregister:SetActive(false)
 
  local  serverinfo= ABManager:LoadAsset("ServerInfo 1",typeof(CS.UnityEngine.Object))
  local  uiserverinfo=prefab:Instantiate(serverinfo)
  uiserverinfo.transform:Find("Background/Info/SelectServer"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(ServerInfo.Select) 
  uiserverinfo.transform:Find("EnterGame"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(ServerInfo.EnterGame) 
  Login.UIPrefab.uiserverinfo=uiserverinfo
  uiserverinfo:SetActive(false)


  local  selectserver= ABManager:LoadAsset("SelectServer 1",typeof(CS.UnityEngine.Object))
  local  uiselectserver=prefab:Instantiate(selectserver)
  uiselectserver.transform:Find("Window/Close"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(SelectServer.Close)  
  Login.UIPrefab.uiselectserver=uiselectserver
	uiselectserver:SetActive(false)
  

 
  

  local  alert= ABManager:LoadAsset("Alert 1",typeof(CS.UnityEngine.Object))
  local  uialert=prefab:Instantiate(alert) 
  uialert.transform:Find("Window/Comfirm"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(Alert.Close)
  Login.UIPrefab.uialert=uialert
  uialert:SetActive(false)
   
end
--注册  登录   用户名的输入和判断

--给注册和登录按钮添加事件



function Login:Register()
  Login.UIPrefab.uiregister:SetActive(true)
  --获得用户的输入 然后判断  
end



function Login:LoginGame()
  local mes=CS.ChetMessage(CS.MessageType.UserInfo,Login.UserInput.UserNameText.text,Login.UserInput.PasswordText.text)
  CS.ClientTCP.Instance:SendMessage(mes)
  CS.System.Threading.Thread.Sleep(50)
  if CS.ClientTCP.Instance.isLogin==true
    then
    --跳转页面
     Login.UIPrefab.uiserverinfo:SetActive(true) 
     CS.ClientTCP.Instance.isLogin=false  
     Login.UIPrefab.uiLoginObj:SetActive(false) 
    else 
    --弹窗失败
   Alert:Alert("用户名和密码输入不正确！请重新输入")
  end


end



Register={}
Alert={}
ServerInfo={}
ServerInfo.ServerName={}
SelectServer={}
SelectServer.ChannelName={}

Register.UserInput={}
Alert.Description={}
--注册弹窗
function  Register:Register()
  Register.UserInput.UserNameText=Login.UIPrefab.uiregister.transform:Find("Window/UserInfo/NameValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))
  Register.UserInput.PasswordText=Login.UIPrefab.uiregister.transform:Find("Window/UserInfo/PwValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))
  Register.UserInput.rePasswordText=Login.UIPrefab.uiregister.transform:Find("Window/UserInfo/RpwValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))

  local username=Register.UserInput.UserNameText.text
  local psword=Register.UserInput.PasswordText.text
  local rpsword=Register.UserInput.rePasswordText.text
 --向服务器发送 检查用户名是否重复
  local inputmes=CS.ChetMessage(CS.MessageType.UserInfo,username,psword)
   CS.ClientTCP.Instance:SendMessage(inputmes)
   CS.System.Threading.Thread.Sleep(50)
  if CS.ClientTCP.Instance.ExistUser==true    
    then
      Alert:Alert("账号ID已经存在！")
      CS.ClientTCP.Instance.ExistUser=false 
  elseif(username=="" or psword=="" or rpsword=="" )
    then
      Alert:Alert("请输入正确的账号密码！")  
  elseif(psword~=rpsword)
    then
      Alert:Alert("两次密码不一致！")     
  else
  --直接注册  把信息写入服务器
      local registerMessage=CS.ChetMessage(CS.MessageType.ExistUser,username,psword)
      CS.ClientTCP.Instance:SendMessage(registerMessage)
      Alert:Alert("注册成功！")
      --把数据给到登录窗口
      Login.UserInput.UserNameText.text=Register.UserInput.UserNameText.text
      Login.UserInput.PasswordText.text=Register.UserInput.PasswordText.text

       Register.UserInput.UserNameText.text=""
       Register.UserInput.PasswordText.text=""
       Register.UserInput.rePasswordText.text=""
      Register:Destroy( )
      
  end
end


function Register:Destroy( )
  
  Login.UIPrefab.uiregister:SetActive(false)
 
end


function Alert:Alert(mes)
  --添加描述
 Alert.Description=Login.UIPrefab.uialert.transform:Find("Window/Message"):GetComponent(typeof(CS.UnityEngine.UI.Text))
 Alert.Description.text=mes
 Login.UIPrefab.uialert:SetActive(true)
end

function Alert:Close( )
   Login.UIPrefab.uialert:SetActive(false)
end




function ServerInfo:Select()
  Login.UIPrefab.uiselectserver:SetActive(true)
  SelectServer:ChannelInfo()
  --服务器信息
  ServerInfo.ServerName=Login.UIPrefab.uiserverinfo.transform:Find("Background/Info/Name"):GetComponent(typeof(CS.UnityEngine.UI.Text))
end

function ServerInfo:EnterGame()
  CS.UnityEngine.Application.LoadLevelAsync(1)   
end

function SelectServer:Close( )
  Login.UIPrefab.uiselectserver:SetActive(false)
end
function SelectServer:ChannelInfo()
  --给服务器每个按钮添加事件  保存点击信息传递给选择服务器界面
  local Channel=Login.UIPrefab.uiselectserver.transform:Find("Window/Servers/Viewport/Content")
 
  local ChannelCount = Channel.childCount
  for i=1,ChannelCount do
    Channel:GetChild(i-1):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(
      function  ()
        ServerInfo.ServerName.text=Channel:GetChild(i-1):Find("Name").transform:GetComponent(typeof(CS.UnityEngine.UI.Text)).text
        Login.UIPrefab.uiselectserver:SetActive(false)  
      end
        ) 
  end
end

