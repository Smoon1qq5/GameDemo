using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarAlgorithm : MonoBehaviour
{
    private int mapWidth = 100;
    private int mapHeight = 100;
   
    private Point[,] map;


    [HideInInspector]
    public List<Point> wayLines;

    public Point[] points;
    public int index = 0;
    [Tooltip("自动寻路移动速度")]
    public float speed = 8f;
    [Tooltip("自动寻路旋转速度")]
    public float damping = 10f;

   


    private Transform path;
    private GameObject obj;
    private void Awake()
    {
        path = GameObject.Find("Path").transform;
        //初始化地图和障碍物
        InitMap();
       
        GameLifeCycle.Instance.aa = this;

        
    }

    private void Update()
    {
       
    }
   
    
    public void Init()
    {
        try
        {
            obj = GameLifeCycle.Instance.obj;
            wayLines = new List<Point>();
           
            Point start = map[(int)obj.transform.position.x, (int)obj.transform.position.z];
            Point end = map[75, 75];
            FindPath(start, end);
            ShowPath(start, end);
            InitArray();

            
        }
        catch (Exception e)
        {

            Debug.Log("A*算法初始化异常" + e.Message);
        }

    }
   
    private void InitArray()
    {
        points = new Point[wayLines.Count];
        for (int i = 0; i < wayLines.Count; i++)
        {
            points[i] = wayLines[i];
        }

        Array.Reverse(points);
        wayLines.Clear();
    }


    //初始化地图
    private void InitMap()
    {
        map = new Point[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                map[x, y] = new Point(x, y);
            }
        }
        InitWall();


    }
    private void InitWall()
    {
        for (int i = 20; i < 50; i++)
        {
            map[50, i].IsWall = true;
        }
        for (int i = 50; i < 100; i++)
        {
            map[i, 30].IsWall = true;
        }
        for (int i = 5; i < 55; i++)
        {
            map[i, 60].IsWall = true;
        }

        for (int i = 40; i < 70; i++)
        {
            map[55, i].IsWall = true;
        }
    }

    //创建cube当作地图
    private void CreateCube(int x, int y, Color color)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.GetComponent<BoxCollider>().isTrigger = true;
        go.GetComponent<BoxCollider>().enabled = false;
        go.transform.position = new Vector3(x, 0, y);
        go.GetComponent<Renderer>().material.color = color;
        go.transform.SetParent(path);
    }
    //显示路径
    private void ShowPath(Point start, Point end)
    {
        Point temp = end;
        while (true)
        {
            Color color = Color.gray;
            if (temp == start)
            {
                color = Color.green;
            }
            if (temp == end)
            {
                color = Color.red;
            }
            CreateCube(temp.X, temp.Y, color);
            //添加路点 
            wayLines.Add(temp);
            if (temp.Parent == null)
            {
                break;
            }
            temp = temp.Parent;
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (map[x, y].IsWall)
                {
                    //  CreateCube(x, y, Color.blue);
                }
            }
        }
    }



    //查找路径
    private void FindPath(Point start, Point end)
    {
        List<Point> openList = new List<Point>();
        List<Point> closeList = new List<Point>();
        openList.Add(start);
        while (openList.Count > 0)
        {
            Point point = FindMinFOfPoint(openList);
            openList.Remove(point);
            closeList.Add(point);

            List<Point> surroundPointList = GetSurroundPoints(point);
            PointFilter(surroundPointList, closeList);
            foreach (Point surroundPoint in surroundPointList)
            {
                if (openList.IndexOf(surroundPoint) > -1)
                {
                    float nowG = CalculateG(surroundPoint, point);
                    if (nowG < surroundPoint.G)
                    {
                        surroundPoint.UpdateParent(point, nowG);
                    }
                }
                else
                {
                    surroundPoint.Parent = point;
                    CalculateF(surroundPoint, end);
                    openList.Add(surroundPoint);
                }
            }

            if (openList.IndexOf(end) > -1)
            {
                break;
            }
        }


       
    }




    /// <summary>
    /// 过滤掉关闭列表中的Point
    /// </summary>
    /// <param name="src"></param>
    /// <param name="closePoint"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void PointFilter(List<Point> src, List<Point> closePoint)
    {
        foreach (Point p in closePoint)
        {
            if (src.IndexOf(p) > -1)
            {
                src.Remove(p);
            }
        }
    }
    //获取周为的point
    private List<Point> GetSurroundPoints(Point point)
    {
        Point up = null, down = null, left = null, right = null;
        Point lu = null, ld = null, ru = null, rd = null;
        //获取上下左右的point
        if (point.Y < mapHeight - 1)
        {
            up = map[point.X, point.Y + 1];
        }
        if (point.Y > 0)
        {
            down = map[point.X, point.Y - 1];
        }
        if (point.X > 0)
        {
            left = map[point.X - 1, point.Y];
        }
        if (point.X < mapWidth - 1)
        {
            right = map[point.X + 1, point.Y];
        }

        //取得左上、左下、右上、右下的Point
        if (left != null && up != null)
        {
            lu = map[point.X - 1, point.Y + 1];
        }
        if (left != null && down != null)
        {
            ld = map[point.X - 1, point.Y - 1];
        }
        if (right != null && up != null)
        {
            ru = map[point.X + 1, point.Y + 1];
        }
        if (right != null && down != null)
        {
            rd = map[point.X + 1, point.Y - 1];
        }
        List<Point> pointList = new List<Point>();
        //周围的point不是墙就可以走
        if (up != null && up.IsWall == false)
        {
            pointList.Add(up);
        }
        if (down != null && down.IsWall == false)
        {
            pointList.Add(down);
        }
        if (left != null && left.IsWall == false)
        {
            pointList.Add(left);
        }
        if (right != null && right.IsWall == false)
        {
            pointList.Add(right);
        }

        //两边也不是墙才可以走
        if (lu != null && lu.IsWall == false && left.IsWall == false && up.IsWall == false)
        {
            pointList.Add(lu);
        }
        if (ld != null && ld.IsWall == false && left.IsWall == false && down.IsWall == false)
        {
            pointList.Add(ld);
        }
        if (ru != null && ru.IsWall == false && right.IsWall == false && up.IsWall == false)
        {
            pointList.Add(ru);
        }
        if (rd != null && rd.IsWall == false && right.IsWall == false && down.IsWall == false)
        {
            pointList.Add(rd);
        }
        return pointList;
    }

    /// <summary>
    /// 查找开启列表中的最小值
    /// </summary>
    /// <param name="openList"></param>
    /// <returns></returns>
    private Point FindMinFOfPoint(List<Point> openList)
    {
        float f = float.MaxValue;
        Point temp = null;
        foreach (Point point in openList)
        {
            if (point.F < f)
            {
                temp = point;
                f = point.F;
            }
        }
        return temp;
    }
    /// <summary>
    /// 计算F的值
    /// </summary>
    /// <param name="now">当前位置</param>
    /// <param name="end">目标位置</param>
    private void CalculateF(Point now, Point end)
    {
        //F=G+H
        float h = Mathf.Abs(end.X - now.X) + Mathf.Abs(end.Y - now.Y);
        float g = 0;
        if (now.Parent == null)
        {
            g = 0;
        }
        else
        {
            g = Vector2.Distance(new Vector2(now.X, now.Y), new Vector2(now.Parent.X, now.Parent.Y)) + now.Parent.G;
        }
        float f = g + h;
        now.F = f;
        now.G = g;
        now.H = h;
    }
    //计算G的值
    private float CalculateG(Point now, Point parent)
    {
        return Vector2.Distance(new Vector2(now.X, now.Y), new Vector2(parent.X, parent.Y)) + parent.G;
    }
}
