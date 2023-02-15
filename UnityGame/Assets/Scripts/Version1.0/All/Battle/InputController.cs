using ns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private CharacterController cc;
    public float speed = 5.0f;
    public float rotateSpeed = 50f;

    DragController dragController;
    CustomButtons[] buttons;
    private OperateActoinRequest operateRequest;

    private SkillManager manager;
    private SkillSystem skillSystem;
    private CharacterStatus status;
    private Animator anim;
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        dragController = FindObjectOfType<DragController>();
        
        buttons = FindObjectsOfType<CustomButtons>();
        status = GetComponent<CharacterStatus>();
        anim = GetComponentInChildren<Animator>();
        manager = GetComponent<SkillManager>();
        skillSystem = GetComponent<SkillSystem>();


        operateRequest = GetComponent<OperateActoinRequest>();
    }
    void OnEnable()
    {
       // dragController = FindObjectOfType<DragController>();
        dragController.onMove.AddListener(OnJoysitckMove);
        dragController.onMoveStart.AddListener(OnJoystickMoveStart);
        dragController.onMoveEnd.AddListener(OnJoystickMoveEnd);

        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onDown.AddListener(OnButtonPressed);
        }
    }
    private void OnButtonPressed(string buttonName)
    {
        // if (IsAttacking()) return;
        int id = 0;
        Debug.Log("当前按钮的名称" + buttonName);
        switch (buttonName)
        {
            case "BaseSkill":
                id = 1001;
                break;
            case "CircleAttackSkill":
                id = 1003;
                break;
            case "SectorAttackSkill":
                id = 1002;
                break;
        }
        //SkillData skill = manager.PraperSkill(id);
        //if (skill != null) manager.GenerateSkill(skill);
         skillSystem.AttackUseSkill(id);
        operateRequest.SendRequest(buttonName);
    }
    
    private void OnJoystickMoveEnd()
    {
        Debug.Log("移动结束");
        anim.SetBool(status.chParams.run, false);
    }

    private void OnJoystickMoveStart()
    {
        anim.SetBool(status.chParams.run, true);
    }

    private void OnJoysitckMove(Vector2 arg0)
    {
        Movement(new Vector3(arg0.x, 0, arg0.y));
    }



    void OnDisable()
    {
        dragController.onMove.RemoveListener(OnJoysitckMove);
        dragController.onMoveStart.RemoveListener(OnJoystickMoveStart);
        dragController.onMoveEnd.RemoveListener(OnJoystickMoveEnd);
    }
    private void LookAtTarget(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        Quaternion lookDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookDir, rotateSpeed * Time.deltaTime);
    }
    public void Movement(Vector3 direction)
    {
        LookAtTarget(direction);
        Vector3 forword = transform.forward;
        forword.y = -1;
        cc.Move(forword * Time.deltaTime * speed);
    }
    // Update is called once per frame
    void Update()
    {

        Move();
    }
    private void Move()
    {
        Vector3 pos = Vector3.zero;

        float f = Input.GetAxis("Horizontal");//ws
        float v = Input.GetAxis("Vertical");//ad
        pos = new Vector3(f, 0, v);
        if (pos == Vector3.zero) return;


        Movement(pos);


    }



    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("是否在地面:" + cc.isGrounded);
    }
    private bool IsAttacking()
    {
        return anim.GetBool(status.chParams.attack1) || anim.GetBool(status.chParams.attack2) || anim.GetBool(status.chParams.attack3);
    }
}
