using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 태어날 때 agnet에게 목적지(플레이어)를 알려주고싶다.
// 상태머신을 이용해서 Enemy를 제어하고싶다.
// 대기, 이동, 공격(공격대기)
// 상태머신이 바뀌면 애니메이션 상태도 같이 바뀌게 하고싶다.
public class Enemy : MonoBehaviour
{
    public Animator anim;
    public enum State
    {
        Idle,
        Move,
        Attack,
    }

    public State state;
    // 공격가능거리
    public float attackRange = 3;

    public float speed = 5;
    GameObject target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle: UpdateIdle(); break;
            case State.Move: UpdateMove(); break;
            case State.Attack: UpdateAttack(); break;
        }

    }

    private void UpdateIdle()
    {
        // 태어날 때 목적지(플레이어)를 찾고싶다.
        target = GameObject.Find("Player");
        // 만약 목적지를 찾았다면 
        if (target != null)
        {
            // 이동상태로 전이하고싶다.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }

    private void UpdateMove()
    {
        // agent야 너의 목적지는 target의 위치야
        agent.destination = target.transform.position;

        // 목적지와 나의 거리를 재고싶다.
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        // 만약 그 거리가 공격가능거리(attackRange)보다 작다면
        if (distance < attackRange)
        {
            // 공격상태로 전이하고싶다.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    // SubState를 만들고싶다(isAttackWait). 공격/공격대기
    // isAttackWait == true 공격대기
    // isAttackWait == false 공격중 
    enum AttackSubState
    {
        Attack,
        Wait,
    }

    AttackSubState attackSubState;
    bool isAttackHit;
    float currentTime;
    float attackHitTime = 0.91f;
    float attackFinishedTime = 2.2f;
    float attackWaittime = 2f;
    private void UpdateAttack()
    {
        // 시간이 흐르다가
        currentTime += Time.deltaTime;

        if (attackSubState == AttackSubState.Wait)
        {
            // 대기시간을 초과하면
            if (currentTime > attackWaittime)
            {
                // 공격상태로 전이하고싶다.
                attackSubState = AttackSubState.Attack;
                currentTime = 0;
                isAttackHit = false;
                anim.SetBool("bAttack", true);
            }
        }
        else if (attackSubState == AttackSubState.Attack)
        {
            // 타격시간을 초과하면
            if (currentTime > attackHitTime)
            {
                // 타격을 하고싶다.
                if (false == isAttackHit)
                {
                    // 타격!!!
                    isAttackHit = true;
                    anim.SetBool("bAttack", false);
                    print("Enemy -> Player Hit!!!");
                }
                // 공격끝시간이 초과하면
                if (currentTime > attackFinishedTime)
                {
                    // 공격대기 상태로 전이하고싶다.
                    attackSubState = AttackSubState.Wait;
                    currentTime = 0;
                }
            }
        }
    }
}