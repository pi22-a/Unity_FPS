using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날 때 앞방향으로 물리기반의 이동 하고싶다.
public class Bullet : MonoBehaviour
{
    public float speed = 10;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = rb.velocity.normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 만약 부딪힌 물체에 Rigidbody가 있다면
        var otherRB = collision.gameObject.GetComponent<Rigidbody>();
        // 내 앞방향으로 힘을 10 가하고싶다.
        if (otherRB != null)
        {
            otherRB.AddForce(transform.forward * otherRB.mass * 20, ForceMode.Impulse);
        }
        Destroy(this.gameObject);
    }
}