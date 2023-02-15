
RegisterView={}
RegisterView.UserInput={}

function  RegisterView:Register()
  RegisterView.UserInput.UserNameText=RegisterController.Page.transform:Find("Window/UserInfo/NameValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))
  RegisterView.UserInput.PasswordText=RegisterController.Page.transform:Find("Window/UserInfo/PwValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))
  RegisterView.UserInput.rePasswordText=RegisterController.Page.transform:Find("Window/UserInfo/RpwValue"):GetComponent(typeof(CS.UnityEngine.UI.InputField))

  local username=RegisterView.UserInput.UserNameText.text
  local psword=RegisterView.UserInput.PasswordText.text
  local rpsword=RegisterView.UserInput.rePasswordText.text
 --向服务器发送 检查用户名是否重复
  RegisterDataModel:Data(username,psword)
  if CS.ClientTCP.Instance.isExisteUser==true    
    then
      AlertController:Alert("账号ID已经存在！")
      
  elseif(username=="" or psword=="" or rpsword=="" )
    then
      AlertController:Alert("请输入正确的账号密码！")  
  elseif(psword~=rpsword)
    then
      AlertController:Alert("两次密码不一致！")     
  else
  --直接注册  把信息写入服务器
      RegisterDataModel:Data( username,psword)
      AlertController:Alert("注册成功！")
      --把数据给到登录窗口
      LoginController.UserInput.UserNameText.text=RegisterView.UserInput.UserNameText.text
      LoginController.UserInput.PasswordText.text=RegisterView.UserInput.PasswordText.text

       RegisterView.UserInput.UserNameText.text=""
       RegisterView.UserInput.PasswordText.text=""
       RegisterView.UserInput.rePasswordText.text=""
     RegisterController.Page:SetActive(false)
      
  end
end