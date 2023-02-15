--AssetBundle是把一些资源文件，场景文件或二进制文件以某种紧密的方式保存在一起的一些文件
--AssetBundle内部不包含C#脚本，AssetBundle可以配合Lua实现资源和游戏逻辑代码的更新

--AB包管理
--加载步骤1：通过AB包文件路径，加载AB包文件
--加载步骤2：通过名称（资源的名称），加载内部资源 

--所有被加载过的AB文件
-- 总AB包的Manifest


ABManager={}
ABManager.Path=Config.ABFilePath

--所有被加载过的文件
ABManager.Files={}


--总的ab包
local allAB=CS.UnityEngine.AssetBundle.LoadFromFile(ABManager.Path.."/AssetBundle")
-- 总AB包的Manifest
ABManager.manifest=allAB:LoadAsset("AssetBundleManifest",typeof(CS.UnityEngine.AssetBundleManifest))
allAB:Unload(false)

--加载AB文件（加载依赖的AB文件）放入files里面
function ABManager:LoadFile(name)
	--如果files里面有文件 就不用加载往问里面放了
	  if(ABManager.Files[name]~=nil)
      then 
         return
    end
    --所需加载文件的依赖项
    local dependfile=ABManager.manifest:GetAllDependencies(name)

    for i=0,dependfile.Length-1
    do
      local file = dependfile[i]

      --如果依赖的文件没有被加载过，则调用递归的加载
      if(ABManager.Files[file]==nil)
      then
          ABManager:LoadFile(file)
      end
    end
   --将AB包加载，并放入管理器的Files表下
   ABManager.Files[name]=CS.UnityEngine.AssetBundle.LoadFromFile(ABManager.Path.."/"..name)

end

--卸载AB文件
function ABManager:UnloadFile( file )
	if(ABManager.Files[file]~=nil)
    then
        ABManager.Files[file]:Unload(false)
	end 
end



--加载资源
--参数1：AB包文件名
--参数2：资源名称
--参数3：资源的类型
function ABManager:LoadAsset(file,name,t )
	--判断AB包是否加载过
	if(ABManager.Files[file] == nil)
	then
	     return nil
	else
	     return  ABManager.Files[file]:LoadAsset(name,t)
	end
end


