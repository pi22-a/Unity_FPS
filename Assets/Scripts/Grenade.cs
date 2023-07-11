using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 물리로 힘을 받아서 이동하고
// 어딘가에 부딪히면 파괴되고싶다.
// 그 때 만약 부딪힌것이 적이라면
// 데미지를 2점 주고싶다.
[RequireComponent(typeof(Rigidbody))]
public class Grenade : MonoBehaviour
{
    public GameObject expFactory;
    Rigidbody rb;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);

        rb.AddTorque(Vector3.right * 50, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 어딘가에 부딪히면 3초 후에 파괴되고싶다.
    // 그 때 반경 3M 안의 충돌체중에 적이있다면
    // 데미지를 2점 주고싶다
    bool isCollisionCheck = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (true == isCollisionCheck)
            return;
        
        isCollisionCheck = true;

        StartCoroutine(IEBoom());

    }

    IEnumerator IEBoom()
    {
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 삐소리내기
        yield return new WaitForSeconds(1);
        // 삐소리내기
        yield return new WaitForSeconds(1);

        
        // 펑소리내기
        // 반경 3M 안의 충돌체중에 적이있다면
        int layer = 1 << LayerMask.NameToLayer("Enemy");
        Collider[] cols = Physics.OverlapSphere(transform.position, 3, layer);
        for (int i = 0; i < cols.Length; i++)
        {
            // 데미지를 2점 주고싶다
            cols[i].GetComponent<Enemy2>().DamageProcess(2);
        }
        // 수류탄도 파괴하고싶다.
        Destroy(this.gameObject);

        GameObject explosion = Instantiate(expFactory);
        explosion.transform.position = transform.position;
    }



}
