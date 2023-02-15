using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ns
{
    ///<summary>
    ///
    ///</summary>
    public class CharacterInputController : MonoBehaviour
    {
        private CharacterMotor motor;

       
        private CharacterStatus status;
        private Animator anim;
        private DragController joystick;

        private SkillManager manager;
        private SkillSystem skillSystem;

        private void Awake()
        {
            InitComponent();

        }

        private void InitComponent()
        {
            motor = GetComponent<CharacterMotor>();
      
            status = GetComponent<CharacterStatus>();
            anim = GetComponentInChildren<Animator>();
        
            manager = GetComponent<SkillManager>();
            skillSystem = GetComponent<SkillSystem>();


            joystick=FindObjectOfType<DragController>();    
        }
        private void OnEnable()
        {

            joystick.onMove.AddListener(OnJoystickMove);
            joystick.onMoveStart.AddListener(OnJoystickMoveStart);
            joystick.onMoveEnd.AddListener(OnJoystickMoveEnd);
            //for (int i = 0; i < buttons.Length; i++)
            //{
            //    buttons[i].onDown.AddListener(OnButtonPressed);
            //}
        }

        private void OnButtonPressed(string buttonName)
        {
            if (IsAttacking()) return;
            int id = 0;
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
        }

        private void OnJoystickMove(Vector2 arg0)
        {
            motor.Movement(new Vector3(arg0.x, 0, arg0.y));
        }
        private void OnJoystickMoveStart()
        {
            anim.SetBool(status.chParams.run, true);
        }
        private void OnJoystickMoveEnd()
        {
            anim.SetBool(status.chParams.run, false);

        }
        private void OnDisable()
        {
            //joystick.onMove.RemoveListener(OnJoystickMove);
            //joystick.onMoveStart.RemoveListener(OnJoystickMoveStart);

            //joystick.onMoveEnd.RemoveListener(OnJoystickMoveEnd);

            //for (int i = 0; i < buttons.Length; i++)
            //{
            //    buttons[i].onDown.RemoveListener(OnButtonPressed);
            //}
        }



        private bool IsAttacking()
        {
           return  anim.GetBool(status.chParams.attack1) || anim.GetBool(status.chParams.attack2) || anim.GetBool(status.chParams.attack3);
        }

    }
}
