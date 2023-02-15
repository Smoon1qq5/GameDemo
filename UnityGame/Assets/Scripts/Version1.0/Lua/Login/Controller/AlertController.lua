

AlertController={}
AlertController.Page=nil
AlertController.Description=nil

function  AlertController:Start( )
	 ABManager:LoadFile("loginuiprefab")

   local  obj = ABManager:LoadAsset("loginuiprefab","Alert 1",typeof(CS.UnityEngine.Object))
   --加载预制体
   local uipage=prefab:Instantiate(obj)
   AlertController.Page=uipage
   --添加事件
   uipage.transform:Find("Window/Comfirm"):GetComponent(typeof(CS.UnityEngine.UI.Button)).onClick:AddListener(AlertController.Close)
   AlertController.Description=uipage.transform:Find("Window/Message"):GetComponent(typeof(CS.UnityEngine.UI.Text))


   AlertController.Page:SetActive(false)
end


function AlertController:Alert(mes)
  --添加描述
   AlertController.Description.text=mes
   AlertController.Page:SetActive(true)


end

function AlertController:Close()
   AlertController.Page:SetActive(false)
end

return AlertController