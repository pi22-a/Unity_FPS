using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PatrolNChase
{
    // 적의 상태를 만들고싶다.
    // 순찰, 추적
    public class Enemy : MonoBehaviour
    {
        public enum State
        {
            Patrol, // 순찰
            Chase,  // 추적
        }

        public State state;

        NavMeshAgent agent;
        public int targetIndex;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            switch (state)
            {
                case State.Patrol:
                    UpdatePatrol();
                    break;
                case State.Chase:
                    UpdateChase();
                    break;
            }
        }

        public float chaseFailedDistansce = 10;
        /// <summary>
        /// 1. 추적대상을 향해 계속 이동하고싶다.
        /// 2. 만약 추적대상과의 거리가 추적포기거리보다 커진다면
        /// 3. 순찰상태로 전이하고싶다.
        /// </summary>
        void UpdateChase()
        {
            Vector3 targetPosition = chaseTarget.transform.position;
            // 1. 추적대상을 향해 계속 이동하고싶다.
            agent.SetDestination(targetPosition);
            // 2. 만약 추적대상과의 거리가 추적포기거리보다 커진다면
            float distance = Vector3.Distance(transform.position, targetPosition);
            if (distance > chaseFailedDistansce)
            {
                // 3. 순찰상태로 전이하고싶다.
                state = State.Patrol;
            }
        }

        public float chaseDistance = 5;
        GameObject chaseTarget;
        /// <summary>
        /// 1. 순찰을 한다.
        /// 2. 플레이어가 내 감지거리 안에 있는지를 계속 확인하고 만약 들어왔다면 내 상태를 추적상태로 전이하고싶다.
        /// </summary>
        void UpdatePatrol()
        {
            Vector3 target = PathManager.instance.points[targetIndex].position;

            agent.SetDestination(target);

            // 만약 목적지에 도착했다면(두지점의 거리가 0.1M이하라면)
            target.y = transform.position.y;
            float dist = Vector3.Distance(transform.position, target);
            if (dist <= 0.1f)
            {
                // 인덱스를 1증가시키고싶다.
                targetIndex++;
                // 만약 인덱스가 points배열의 크기이상이되면 0으로 초기화 하고싶다.
                if (targetIndex >= PathManager.instance.points.Length)
                {
                    targetIndex = 0;
                }
            }

            // 2. 플레이어가 내 감지거리 안에 있는지를 계속 확인하고
            int layerMask = 1 << LayerMask.NameToLayer("Player");
            Collider[] cols = Physics.OverlapSphere(transform.position, chaseDistance, layerMask);
            // 3. 만약 cols의 길이가 0보다 크다면
            if (cols.Length > 0)
            {
                // 4. 만약  나의 앞방향 벡터와 플레이어와의 방향벡터를 내적해서 0.85이상라면
                Vector3 targetVector = cols[0].transform.position - transform.position;
                targetVector.Normalize();

                Vector3 forwardVector = transform.forward;

                float dot = Vector3.Dot(targetVector, forwardVector);
                if (dot >= Math.Cos(20))
                {
                    // 5. 검출된 녀석을 추적대상으로 하고싶다.
                    chaseTarget = cols[0].gameObject;
                    // 6. 내 상태를 추적상태로 전이하고싶다.
                    state = State.Chase;
                }
            }
        }
    }
}