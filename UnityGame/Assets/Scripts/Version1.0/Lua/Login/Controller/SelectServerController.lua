
SelectServerController={}
SelectServerController.Page=nil


function SelectServerController:Start()
   ABManager:LoadFile("loginuiprefab")

   local  obj = ABManager:LoadAsset("loginuiprefab","SelectServer 1",typeof(CS.UnityEngine.Object))
   --加载预制体
   local uipage=prefab:Instantiate(obj)
   SelectServerController.Page=uipage

   uipage.transform:Find("Window/Close"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(SelectServerController.Close)  
  
   uipage:SetActive(false)
end



function SelectServerController:Close( )
  SelectServerController.Page:SetActive(false)
end
function SelectServerController:ChannelInfo()
  --给服务器每个按钮添加事件  保存点击信息传递给选择服务器界面
  local Channel=SelectServerController.Page.transform:Find("Window/Servers/Viewport/Content")
 
  local ChannelCount = Channel.childCount
  for i=1,ChannelCount do
    Channel:GetChild(i-1):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(
      function  ()
        ServerInfoController.ServerName.text=Channel:GetChild(i-1):Find("Name").transform:GetComponent(typeof(CS.UnityEngine.UI.Text)).text
        SelectServerController.Page:SetActive(false)  
      end
        ) 
  end
end
return SelectServerController