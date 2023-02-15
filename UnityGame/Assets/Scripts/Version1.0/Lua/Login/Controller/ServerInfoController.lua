
ServerInfoController={}
ServerInfoController.Page=nil
ServerInfoController.ServerName=nil
function ServerInfoController:Start()
   ABManager:LoadFile("loginuiprefab")

   local  obj = ABManager:LoadAsset("loginuiprefab","ServerInfo 1",typeof(CS.UnityEngine.Object))
   --加载预制体
   local uipage=prefab:Instantiate(obj)
   ServerInfoController.Page=uipage

   uipage.transform:Find("Background/Info/SelectServer"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(ServerInfoController.Select) 
   uipage.transform:Find("EnterGame"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(ServerInfoController.EnterGame)

   uipage:SetActive(false)
end



function ServerInfoController:Select()
  SelectServerController.Page:SetActive(true)
  SelectServerController:ChannelInfo()
  --服务器信息
  ServerInfoController.ServerName=ServerInfoController.Page.transform:Find("Background/Info/Name"):GetComponent(typeof(CS.UnityEngine.UI.Text))
end

function ServerInfoController:EnterGame()
  --如果账号角色存在 则直接进入游戏
  --否则进入选择人物确认昵称界面
  CS.UnityEngine.Application.LoadLevelAsync(1)   
end

return ServerInfoController