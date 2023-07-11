using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 상태머신으로 제어하고싶다.
// Agent를 이용해서 이동하고싶다.
public class Enemy2 : MonoBehaviour
{
    EnemyHP enemyHP;
    public enum State
    {
        Idle,
        Move,
        Attack,
        React,  // 데미지
        Die,    // 죽음
    }
    public State state;

    Animator anim;
    GameObject target;
    NavMeshAgent agent;
    public float attackRange = 3;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        enemyHP = GetComponent<EnemyHP>();
    }



    // Update is called once per frame
    void Update()
    {
        // state를 기준으로 분기처리 해보세요!
        // switch case 문으로!
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
            agent.isStopped = false;
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
            // agent야 멈춰!!
            agent.isStopped = true;
        }
    }

    private void UpdateAttack()
    {
        // 공격중에는 목적지를 바라보게 하고싶다.
        transform.LookAt(target.transform);
    }

    #region 애니메이션 이벤트 함수를 통해 호출되는 함수들
    public void OnAttack_Hit()
    {
        anim.SetBool("bAttack", false);
        // 타격할 수 있는 조건
        // 목적지와의 거리가 공격가능거리 이하일때 가능
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance <= attackRange)
        {
            print("Enemy -> Player Hit!!!");
            // HitManager의 DoHit함수를 호출하시오!
            HitManager.instance.DoHit();
        }
    }
    public void OnAttack_Finished()
    {
        // 대기상태로 가야하는가?
        // 목적지와의 거리가 공격가능거리 이상이라면(벗어났다면)
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance > attackRange)
        {
            // 이동상태로 전이하고싶다.
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }

    }

    public void OnAttackWait_Finished()
    {
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance > attackRange) // 공격 범위 벗어남
        {
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }
        else // 공격 가능한 거리
        {
            anim.SetBool("bAttack", true);
        }
    }

    internal void OnReact_Finished()
    {
        // 리액션이 끝났으니 Move상태로 전이하고싶다.
        state = State.Move;
        anim.SetTrigger("Move");
        agent.isStopped = false;
    }
    #endregion

    // 데미지를 입으면 데미지 UI를 내위치 위쪽으로 1M 위에 배치하고싶다.
    public GameObject damageUIFactory;
    internal void DamageProcess(int damage = 1)
    {
        // 만약 내상태가 죽음상태라면
        if (state == State.Die)
        {
            // 바로 반환하고싶다.
            return;
        }

        GameObject ui = Instantiate(damageUIFactory);
        ui.transform.position = transform.position + Vector3.up * 1.2f;

        agent.isStopped = true;

        // 적 체력을 damage만큼 감소하고싶다.
        enemyHP.HP -= damage;
        // 만약 적 체력이 0이하라면
        if (enemyHP.HP <= 0)
        {
            state = State.Die;
            // 파괴하고싶다.
            Destroy(gameObject, 5);
            anim.SetTrigger("Die");

            //// 콜라이더 컴포넌트를 가져오고싶다.
            //Collider col = GetComponent<Collider>();
            //// 콜라이더를 끄고싶다.
            //if (col)
            //    col.enabled = false;
        }
        else // 체력이 남아있으면 리액션 하고싶다.
        {
            state = State.React;
            anim.SetTrigger("React");
        }
    }


}
