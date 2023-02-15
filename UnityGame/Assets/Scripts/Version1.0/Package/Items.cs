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

    //���������
    public string sprite_Name;

   


    /*
    [HideInInspector]
    public byte[] sprite_res;
    //ֱ�ӻ��sprite
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

    /*��������ֱ�Ӵ洢�����JSON���������л���Sprite.texture.GetRawTextureData()�����ԭʼ��������
     * �����ܴ洢��base64 string���������ݣ�����ʹ��Sprite.Create()������ʱ���´�������
     * ISerializationCallbackReceiver����Ҳ�����ڴˡ���Ϊһ������˵�����������������Ҫ��
     * ���Կ��Ƿ�������JSON�Ķ������ݿ��������ʹ��Scriptable Objects��
     * ������������������UnityEngine���󣬶���������ʹ��Resources�ļ��У����Ǳ�Ҫ�������Ƽ�����


    /*�ؽ�
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