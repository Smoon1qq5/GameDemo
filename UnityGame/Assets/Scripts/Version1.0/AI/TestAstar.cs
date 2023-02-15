using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAstar : MonoBehaviour
{
    public GameObject obj;
    private CharacterController cc;
    private AStarAlgorithm astar;
    private int index = 0;
    Point[] points;

    public float speed = 1f;
    public float damping=10f;
    public bool _isPathfinding = false;
    void Start()
    {
       
        astar=GetComponent<AStarAlgorithm>();
        points=new Point[astar.wayLines.Count];
        for (int i = 0; i < astar.wayLines.Count; i++)
        {
            points[i] = astar.wayLines[i];
        }
     //  Array.Reverse(points);
    }
    
   
    // Update is called once per frame
    void Update()
    {
        if(_isPathfinding)
       TestA();
    
    }
    private void PrintPoint()
    {
        foreach (var item in points)
        {
            Debug.Log(item.X+","+item.Y);
        }
    }
    private void TestA()
    {
        if (index > points.Length-1) return;

        obj.transform.localPosition = Vector3.MoveTowards(obj.transform.localPosition, new Vector3(points[index].X, 1.08f, points[index].Y), Time.deltaTime * speed);
        Quaternion rotation = Quaternion.LookRotation(new Vector3(points[index].X, 1.08f, points[index].Y) - obj.transform.position);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotation, damping * Time.deltaTime);
                 
        if (Vector3.Distance(obj.transform.position, new Vector3(points[index].X, 1.08f, points[index].Y)) < 0.5f)
        {
           
            index++;
            if (index == points.Length)
            {
                obj.transform.localPosition = new Vector3(points[index-1].X, 1.08f, points[index-1].Y);
            }
        }
       
    }
}
