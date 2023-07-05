using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �¾ �� �չ������� ��������� �̵� �ϰ�ʹ�.
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
        // ���� �ε��� ��ü�� Rigidbody�� �ִٸ�
        var otherRB = collision.gameObject.GetComponent<Rigidbody>();
        // �� �չ������� ���� 10 ���ϰ�ʹ�.
        if (otherRB != null)
        {
            otherRB.AddForce(transform.forward * otherRB.mass * 20, ForceMode.Impulse);
        }
        Destroy(this.gameObject);
    }
}