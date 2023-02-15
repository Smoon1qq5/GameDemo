using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 消息处理
/// </summary>

public class Message
{
    private static Message instance;
    public static Message Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Message();
            }
            return instance;
        }
    }
    private byte[] buffer = new byte[1024];
    private int startIndex;
    public byte[] Buffer { get { return buffer; } }
    public int StartIndex { get { return startIndex; } }
    public int ReminSize
    {
        get
        {
            return buffer.Length - startIndex;
        }
    }

    //消息解析
    public void RedaBuffer(int length,Action<Mainpack> HandleRequest)
    {
        startIndex += length;
        if (startIndex <= 4) return;
        int count = BitConverter.ToInt32(buffer, 0);//消息的长度  放在包头的
        while (true)
        {
            if (startIndex >= (count + 4))
            {
                Mainpack pack = (Mainpack)Mainpack.Descriptor.Parser.ParseFrom(buffer, 4, count);
                HandleRequest(pack);
                Array.Copy(buffer,count+4, buffer, 0, startIndex-count-4);
                startIndex -= (count+4);
            }else
            {
                break;
            }
        }
    }

    public Byte[] PackData(Mainpack pack)
    {
        byte[] data = pack.ToByteArray();
        byte[] head = BitConverter.GetBytes(data.Length);

        return head.Concat(data).ToArray();
    }
    public  Byte[] PackDataUDP(Mainpack pack)
    {
        return pack.ToByteArray();
    }
}
