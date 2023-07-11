using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날 때 수명을 1초로 정하고싶다.
// 1초동안 위로 이동하고싶다.
public class DamageUI : MonoBehaviour
{
    public float lifeSpan = 0.8f;
    public float speed = 1;
    public AnimationCurve ac;
    // 태어날 때 내 위치를 기억하고싶다.
    Vector3 origin;
    void Start()
    {
        origin = transform.position;
        Destroy(gameObject, lifeSpan);
    }

    float currenTime;
    public float height = 1;

    void Update()
    {
        currenTime += Time.deltaTime / speed;
        float value = ac.Evaluate(currenTime);

        transform.position = origin + Vector3.up * height * value;
    }
}
