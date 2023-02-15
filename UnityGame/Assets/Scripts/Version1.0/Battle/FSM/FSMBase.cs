using System;
using System.Collections;
using System.Collections.Generic;
using ns;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    ///<summary>
    ///
    ///</summary>
    public class FSMBase : MonoBehaviour
    {
        //״̬�б�
        private List<FSMState> states;
        [Tooltip("Ĭ��״̬���")]
        public FSMStateID defaultStateID;

        public FSMStateID test_CurrentStateID;
        //��ǰ״̬
        private FSMState currentState;
        private FSMState defaultState;

        [Tooltip("��ǰ״̬��ʹ�õ������ļ�")]
        public string fileName = "AI_01.txt";
        private void Start()
        {
            InitComponent();
            ConfigFSM();
            InitDefaultState();
            
        }
        private void InitDefaultState()
        {
            defaultState = states.Find(s => s.StateID == defaultStateID);
            currentState = defaultState;
            currentState.EnterState(this);
        }

        private void ConfigFSM()
        {
            states = new List<FSMState>();
           // var map = new AIConfigurationReader(fileName).Map;
            var map= AIConfigurationReaderFactory.GetMap(fileName);

            foreach (var state in map)
            {
                Type type = Type.GetType("FSM." + state.Key + "State");
                FSMState stateObj = Activator.CreateInstance(type) as FSMState;
                states.Add(stateObj);
                foreach (var item in state.Value)
                {
                    //string-->Enum
                    FSMTriggerID triggerID =(FSMTriggerID) Enum.Parse(typeof(FSMTriggerID), item.Key) ;
                    FSMStateID stateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), item.Value);
                    stateObj.AddMap(triggerID, stateID);
                }
            }
        }
        //����״̬��       
        //private void ConfigFSM()
        //{
        //    states = new List<FSMState>();
        //    //--����״̬������
        //    IdleState idle = new IdleState();
        //    //--����״̬��AddMap�����ӳ��
        //    idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    idle.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);

        //    states.Add(idle);

        //    DeadState dead = new DeadState();
        //    states.Add(dead);

        //    PursuitState pursuitState = new PursuitState();
        //    pursuitState.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    pursuitState.AddMap(FSMTriggerID.ReachTarget, FSMStateID.Attacking);
        //    pursuitState.AddMap(FSMTriggerID.LoseTarget, FSMStateID.Default);
        //    states.Add(pursuitState);

        //    AttackingState attacking = new AttackingState();
        //    attacking.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    attacking.AddMap(FSMTriggerID.WithoutAttackRange, FSMStateID.Pursuit);
        //    attacking.AddMap(FSMTriggerID.KilledTarget, FSMStateID.Default);

        //    states.Add(attacking);


        //    PatrollingState patrollingState = new PatrollingState();
        //    patrollingState.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        //    patrollingState.AddMap(FSMTriggerID.SawTarget, FSMStateID.Pursuit);
        //    patrollingState.AddMap(FSMTriggerID.CompletePatrol, FSMStateID.Idle);
        //    states.Add(patrollingState);
        //}

        //ÿ֡������߼�
        private void Update()
        {
            test_CurrentStateID = currentState.StateID;
            //�жϵ�ǰ״̬����������
            currentState.Reason(this);
            //ִ�е�ǰ״̬�߼�
            currentState.ActionState(this);

            SearchTarget();
        }

        //�л�״̬
        public void ChangeActiveState(FSMStateID stateID)
        {
            //���õ�ǰ״̬
            //currentState=?

            currentState.ExitState(this);
            currentState = stateID == FSMStateID.Default ? defaultState : states.Find(s => s.StateID == stateID);
            currentState.EnterState(this);
        }


        #region Ϊ״̬�������ṩ�ĳ�Ա
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public CharacterStatus status;
        [HideInInspector]
        public SkillSystem skillSystem;
        private void InitComponent()
        {
            anim = GetComponentInChildren<Animator>();
            status = GetComponent<CharacterStatus>();
            navAgent = GetComponent<NavMeshAgent>();
            skillSystem = GetComponent<SkillSystem>();
        }
        [HideInInspector]
        public Transform targetTF;
        [Tooltip("������ǩ")]
        public string[] targetTags = { "Player" };
        [Tooltip("��Ұ����")]
        public float sightDis = 10;
        private void SearchTarget()
        {
            SkillData data = new SkillData
            {
                attackTargetTags = targetTags,
                attackDistance = sightDis,
                attackAngle = 360,
                attackType = SkillAttackType.Single

            };
            Transform[] targetArr = new SectorAttackSelector().SelectTarget(data, transform);
            targetTF = targetArr.Length == 0 ? null : targetArr[0];
        }

        private NavMeshAgent navAgent;
        public float runSpeed = 3;
        public float walkSpeed = 1;
        public void MoveToTarget(Vector3 position, float distance, float moveSpeed)
        {
            //Ѱ·���ʵ��
            navAgent.SetDestination(position);
            navAgent.stoppingDistance = distance;
            navAgent.speed = moveSpeed;
        }
        public void StopMove()
        {
            navAgent.SetDestination(transform.position);
            //navAgent.enabled = false;
            //navAgent.enabled = true;
        }
        /*
      ����ִ�����̣�
     ״̬��ÿ֡��⵱ǰ״̬������------>״̬�����������������------>
     ���ĳ���������------>״̬���л���ǰ״̬

        */
        [Tooltip("Ѳ��ģʽ")]      
        public PatrolMode patrolMode;
        [Tooltip("·��")]
        public Transform[] wayPoints;
       // [HideInInspector]
        public bool isPatrolComplete;

        #endregion
    }
}
