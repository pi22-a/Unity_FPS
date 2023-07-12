using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PatrolNChase
{
    public class Enemy : MonoBehaviour
    {
        NavMeshAgent agent;
        public int targetIndex;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();


        }

        // Update is called once per frame
        void Update()
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
        }
    }
}