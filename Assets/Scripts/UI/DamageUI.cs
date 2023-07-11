using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날 때 수명을 1초로 정하고싶다.
// 1초동안 위로 이동하고싶다.
public class DamageUI : MonoBehaviour
{
    public float lifeSpan = 0.8f;
    public float speed = 1;
    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
