using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// �¾ �� agnet���� ������(�÷��̾�)�� �˷��ְ�ʹ�.
// ���¸ӽ��� �̿��ؼ� Enemy�� �����ϰ�ʹ�.
// ���, �̵�, ����(���ݴ��)
// ���¸ӽ��� �ٲ�� �ִϸ��̼� ���µ� ���� �ٲ�� �ϰ�ʹ�.
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
    // ���ݰ��ɰŸ�
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
        // �¾ �� ������(�÷��̾�)�� ã��ʹ�.
        target = GameObject.Find("Player");
        // ���� �������� ã�Ҵٸ� 
        if (target != null)
        {
            // �̵����·� �����ϰ�ʹ�.
            state = State.Move;
            anim.SetTrigger("Move");
        }
    }

    private void UpdateMove()
    {
        // agent�� ���� �������� target�� ��ġ��
        agent.destination = target.transform.position;

        // �������� ���� �Ÿ��� ���ʹ�.
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        // ���� �� �Ÿ��� ���ݰ��ɰŸ�(attackRange)���� �۴ٸ�
        if (distance < attackRange)
        {
            // ���ݻ��·� �����ϰ�ʹ�.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
    }

    // SubState�� �����ʹ�(isAttackWait). ����/���ݴ��
    // isAttackWait == true ���ݴ��
    // isAttackWait == false ������ 
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
        // �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;

        if (attackSubState == AttackSubState.Wait)
        {
            // ���ð��� �ʰ��ϸ�
            if (currentTime > attackWaittime)
            {
                // ���ݻ��·� �����ϰ�ʹ�.
                attackSubState = AttackSubState.Attack;
                currentTime = 0;
                isAttackHit = false;
                anim.SetBool("bAttack", true);
            }
        }
        else if (attackSubState == AttackSubState.Attack)
        {
            // Ÿ�ݽð��� �ʰ��ϸ�
            if (currentTime > attackHitTime)
            {
                // Ÿ���� �ϰ�ʹ�.
                if (false == isAttackHit)
                {
                    // Ÿ��!!!
                    isAttackHit = true;
                    anim.SetBool("bAttack", false);
                    print("Enemy -> Player Hit!!!");
                }
                // ���ݳ��ð��� �ʰ��ϸ�
                if (currentTime > attackFinishedTime)
                {
                    // ���ݴ�� ���·� �����ϰ�ʹ�.
                    attackSubState = AttackSubState.Wait;
                    currentTime = 0;
                }
            }
        }
    }
}