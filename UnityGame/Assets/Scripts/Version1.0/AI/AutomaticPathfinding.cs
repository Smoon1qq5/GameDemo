using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AutomaticPathfinding : MonoBehaviour
{
    // private AStarAlgorithm astar;


    private int index = 0;

    [Tooltip("自动寻路移动速度")]
    public float speed = 8f;
    [Tooltip("自动寻路旋转速度")]
    public float damping = 10f;

    public bool _isPathfinding = false;
    public Point[] points;

    private AStarAlgorithm aster;

    private Animator animator;
    void Awake()
    {
        GameLifeCycle.Instance.ap = this;
        animator=GetComponentInChildren<Animator>();    
    }

    private void Start()
    {
        aster = GameLifeCycle.Instance.aa;
        
      
       
    }
    public void Init()
    {
        points = new Point[aster.points.Length];
        for (int i = 0; i < aster.points.Length; i++)
        {
            points[i] = aster.points[i];
        }
        _isPathfinding=true;
    }
    public void ResetPathfinding()
    {
        points = null;
        _isPathfinding = false;
    }

    void Update()
    {
       
      
        if (_isPathfinding)
        {           
            if (points != null)
            {
                Pathfinding();
            }
        }
       
    }

    private void Pathfinding()
    {
        try
        {
            if (points == null) return;

            if (index > points.Length - 1) return;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(points[index].X, 0f, points[index].Y), Time.deltaTime * speed);
            Quaternion rotation = Quaternion.LookRotation(new Vector3(points[index].X, 0f, points[index].Y) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, damping * Time.deltaTime);
            animator.SetBool("run", true);
            if (Vector3.Distance(transform.position, new Vector3(points[index].X, 0f, points[index].Y)) < 0.5f)
            {

                index++;
                if (index == points.Length)
                {
                    transform.localPosition = new Vector3(points[index - 1].X, 0f, points[index - 1].Y);
                    _isPathfinding = false;
                }
            }
        }
        catch (Exception e)
        {

            Debug.Log("寻路异常" + e.Message);
        }
        

    }
}
