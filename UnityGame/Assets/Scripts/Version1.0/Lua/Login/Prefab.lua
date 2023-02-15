

prefab={}

function prefab:Instantiate( prefab)
    local canvas=CS.UnityEngine.GameObject.Find("/Canvas").transform
	--根据预制件实例化object
	local obj= CS.UnityEngine.Object.Instantiate(prefab)
    obj.name=prefab.name
    

    local trs =obj.transform
    trs:SetParent(canvas)
    trs.localPosition=CS.UnityEngine.Vector3.zero
    trs.localRotation=CS.UnityEngine.Quaternion.identity
    trs.localScale=CS.UnityEngine.Vector3.one

    trs.offsetMin=CS.UnityEngine.Vector2.zero
    trs.offsetMax=CS.UnityEngine.Vector2.zero
 
    return obj
end