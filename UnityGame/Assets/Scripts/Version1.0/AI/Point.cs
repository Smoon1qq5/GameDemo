using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Point 
{
    public Point Parent { get; set; }
    public float F { get; set; }
    public float G { get; set; }
    public float H { get; set; }
   // [SerializeField]
    public int X { get; set; }
   // [SerializeField]
    public int Y { get; set; }

    public bool IsWall { get; set; }

    public Point(int x, int y, Point parent = null)
    {
        X = x;
        Y = y;
        Parent = parent;
        IsWall = false;
    }
    /// <summary>
    /// 更新父节点的g值
    /// </summary>
    public void UpdateParent(Point parent, float g)
    {
        Parent = parent;
        G = g;
        F = G + H;
    }

}
