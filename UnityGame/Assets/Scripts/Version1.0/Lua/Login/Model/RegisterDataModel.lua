

RegisterDataModel={}


function RegisterDataModel:Data(username,psword)
	local _registerInfo = CS.PBLogin.TcpRegister()
	  _registerInfo.UserName=username
	  _registerInfo.Password=psword
   local inputmes=CS.CSDataHandle.GetSendMessage(_registerInfo,CS.ProtoCommon.C2SID.TcpRegister)
   CS.ClientTCP.Instance:SendMessage(inputmes)
   CS.System.Threading.Thread.Sleep(50)
end

--function RegisterDataModel:SendData( username,psword)
    -- local _registerInfo = CS.PBLogin.PBLogin:PBLogin()
	 -- _registerInfo.UserName=username
	--  _registerInfo.Password=psword
	--local registerMessage=CS.CSDataHandle:GetSendMessage(_registerInfo,CS.ProtoCommon.C2SID.TcpRegister)
   -- CS.ClientTCP.Instance:SendMessage(registerMessage)
--end




	