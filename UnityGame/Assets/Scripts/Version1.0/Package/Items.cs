using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Items
{

    public string itemName;

    public string itemCount;
    public bool isStack;
    [TextArea(0, 50)]
    public string itemInfo;
    public ItemType type;


    public Sprite itemImage;

    //精灵的名称
    public string sprite_Name;

   


    /*
    [HideInInspector]
    public byte[] sprite_res;
    //直接获得sprite
    public Sprite GetItemImage(byte[] res )
    {
      return   RebuildSprite(res);
    }
    public byte[]  GetRawSpritByte(Sprite Image)
    {
        return  GetSpriteSerializaInfo(Image);
    }

   

    private byte[] GetSpriteSerializaInfo(Sprite image)
    {
        return image.texture.GetRawTextureData();
    }
    private Sprite RebuildSprite(byte[] res)
    {
        Texture2D tex = new Texture2D(39, 39);
        tex.LoadImage(res);
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }

    /*如果真的想直接存储精灵的JSON，考虑序列化从Sprite.texture.GetRawTextureData()拍摄的原始纹理数据
     * （可能存储在base64 string二进制数据），并使用Sprite.Create()在运行时重新创建它。
     * ISerializationCallbackReceiver技巧也适用于此。作为一个附加说明，如果它符合您的要求，
     * 绝对考虑放弃基于JSON的对象数据库而倾向于使用Scriptable Objects。
     * 这样，可以轻松引用UnityEngine对象，而无需诉诸使用Resources文件夹（除非必要，否则不推荐）。


    /*重建
     * byte [] bytes = File.ReadAllBytes(path); 
      Texture2d tex = new Texture2D(4,4); 
      tex.LoadImage(bytes); 
      Sprite sp = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f,0.5f)); 
     */



}
[System.Serializable]
public enum ItemType
{
    Default,
    Food,
    Material


}