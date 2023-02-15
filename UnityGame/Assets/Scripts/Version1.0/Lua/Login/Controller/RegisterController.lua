
--注册界面控制器
RegisterController ={} 
RegisterController.Page=nil
RegisterController.UserInput={}
require("Model/RegisterDataModel")
require("View/RegisterView")
function RegisterController:Start()

   ABManager:LoadFile("loginuiprefab")

   local  obj = ABManager:LoadAsset("loginuiprefab","Register 1",typeof(CS.UnityEngine.Object))
   --加载预制体
   local uipage=prefab:Instantiate(obj)
   RegisterController.Page=uipage
   --绑定事件
   uipage.transform:Find("Window/Register"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(RegisterView.Register)
   uipage.transform:Find("Window/GoBack"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(
   	function()uipage:SetActive(false) end)
    
   RegisterController.Page:SetActive(false)

end


return RegisterController