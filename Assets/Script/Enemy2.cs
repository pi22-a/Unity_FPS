using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ���¸ӽ����� �����ϰ�ʹ�.
// Agent�� �̿��ؼ� �̵��ϰ�ʹ�.
public class Enemy2 : MonoBehaviour
{
    EnemyHP enemyHP;
    public enum State
    {
        Idle,
        Move,
        Attack,
        React,  // ������
        Die,    // ����
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
        // state�� �������� �б�ó�� �غ�����!
        // switch case ������!
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
            agent.isStopped = false;
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
            // agent�� ����!!
            agent.isStopped = true;
        }
    }

    private void UpdateAttack()
    {
        // �����߿��� �������� �ٶ󺸰� �ϰ�ʹ�.
        transform.LookAt(target.transform);
    }



    public void OnAttack_Hit()
    {
        anim.SetBool("bAttack", false);
        // Ÿ���� �� �ִ� ����
        // ���������� �Ÿ��� ���ݰ��ɰŸ� �����϶� ����
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance <= attackRange)
        {
            print("Enemy -> Player Hit!!!");
            // HitManager�� DoHit�Լ��� ȣ���Ͻÿ�!
            HitManager.instance.DoHit();
        }
    }
    public void OnAttack_Finished()
    {
        // �����·� �����ϴ°�?
        // ���������� �Ÿ��� ���ݰ��ɰŸ� �̻��̶��(����ٸ�)
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance > attackRange)
        {
            // �̵����·� �����ϰ�ʹ�.
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }

    }

    public void OnAttackWait_Finished()
    {
        float distance = Vector3.Distance(this.transform.position, target.transform.position);
        if (distance > attackRange) // ���� ���� ���
        {
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false;
        }
        else // ���� ������ �Ÿ�
        {
            anim.SetBool("bAttack", true);
        }
    }

    internal void DamageProcess()
    {
        // �� ü���� 1 �����ϰ�ʹ�.
        enemyHP.HP -= 1;
        // ���� �� ü���� 0���϶��
        if (enemyHP.HP <= 0)
        {
            state = State.Die;
            agent.isStopped = true;
            // �ı��ϰ�ʹ�.
            Destroy(gameObject, 5);
            anim.SetTrigger("Die");
        }
        else
        {

        }
    }
}