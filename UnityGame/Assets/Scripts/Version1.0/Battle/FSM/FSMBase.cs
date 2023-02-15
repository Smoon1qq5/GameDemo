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
        //状态列表
        private List<FSMState> states;
        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;

        public FSMStateID test_CurrentStateID;
        //当前状态
        private FSMState currentState;
        private FSMState defaultState;

        [Tooltip("当前状态机使用的配置文件")]
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
        //配置状态机       
        //private void ConfigFSM()
        //{
        //    states = new List<FSMState>();
        //    //--创建状态机对象
        //    IdleState idle = new IdleState();
        //    //--设置状态（AddMap）添加映射
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

        //每帧处理的逻辑
        private void Update()
        {
            test_CurrentStateID = currentState.StateID;
            //判断当前状态条件。。。
            currentState.Reason(this);
            //执行当前状态逻辑
            currentState.ActionState(this);

            SearchTarget();
        }

        //切换状态
        public void ChangeActiveState(FSMStateID stateID)
        {
            //设置当前状态
            //currentState=?

            currentState.ExitState(this);
            currentState = stateID == FSMStateID.Default ? defaultState : states.Find(s => s.StateID == stateID);
            currentState.EnterState(this);
        }


        #region 为状态与条件提供的成员
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
        [Tooltip("攻击标签")]
        public string[] targetTags = { "Player" };
        [Tooltip("视野距离")]
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
            //寻路组件实现
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
      程序执行流程：
     状态机每帧检测当前状态的条件------>状态类遍历所有条件对象------>
     如果某个条件达成------>状态机切换当前状态

        */
        [Tooltip("巡逻模式")]      
        public PatrolMode patrolMode;
        [Tooltip("路点")]
        public Transform[] wayPoints;
       // [HideInInspector]
        public bool isPatrolComplete;

        #endregion
    }
}
