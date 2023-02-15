
Bootstrap={}
Bootstrap.Controllers={}

Bootstrap.Start=function ()
  CS.NetworkGlobal.Instance()
  CS.ClientTCP.Instance:Start(CS.NetworkGlobal.Instance().serverIP)
  	Bootstrap.LoadController("LoginController")
    Bootstrap.LoadController("RegisterController")
    Bootstrap.LoadController("ServerInfoController")
    Bootstrap.LoadController("SelectServerController")
    Bootstrap.LoadController("AlertController")

end

Bootstrap.Update=function ()
  	 for k, v in pairs(Bootstrap.Controllers)
  	 do 
  	    if(v.Update~=nil)
  	    then
  	        v:Update()
  	    end
  	 end
end

Bootstrap.OnDestroy=function ()
  	-- body
end

         

-- 提供一个核心table函数用于加载页面控制器
-- 参数是脚本的名称
Bootstrap.LoadController=function(name)
    --加载Controller目录下对应的脚本
    local c = require("Controller/"..name)
    --注册给核心的table，这样生命周期函数就会正常调用
    Bootstrap.Controllers[name]=c
    --加载页面时，同时运行Start
    c:Start()
end

require("Config")
require("ABManager")
require("Prefab")