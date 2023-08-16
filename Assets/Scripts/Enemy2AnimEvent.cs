sing System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이벤트 함수를 제작하고싶다.
// Hit, AttackFinished 일 때
public class Enemy2AnimEvent : MonoBehaviour
{
    Enemy2 enemy2;
    // Start is called before the first frame update
    void Awake()
    {
        enemy2 = GetComponentInParent<Enemy2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttack_Hit()
    {
        enemy2.OnAttack_Hit();
    }

    public void OnAttack_Finished()
    {
        enemy2.OnAttack_Finished();
    }

    public void OnAttackWait_Finished()
    {
        enemy2.OnAttackWait_Finished();
    }

    public void OnReact_Finished()
    {
        enemy2.OnReact_Finished();
    }
   


}
