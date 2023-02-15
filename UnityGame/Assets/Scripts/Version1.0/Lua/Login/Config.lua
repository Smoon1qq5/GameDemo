Config={}
local assetFileName="AssetBundle"
local path=CS.UnityEngine.Application.dataPath

Config.ABFilePath=string.sub(path,1,#path-6)..assetFileName