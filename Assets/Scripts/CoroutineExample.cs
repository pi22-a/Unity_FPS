using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 1초동안 우,상,좌,하의 순서로 이동하고 싶다. 
public class CoroutineExample : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEMoveManager());
    }

    IEnumerator IEMoveManager()
    {
        while (true)
        {
            yield return StartCoroutine(IEMove(Vector3.right));
            yield return StartCoroutine(IEMove(Vector3.up));
            yield return StartCoroutine(IEMove(Vector3.left));
            yield return StartCoroutine(IEMove(Vector3.down));
        }
    }

    // 이동하는 코루틴함수 제작하고싶다.
    IEnumerator IEMove(Vector3 direction)
    {
        for (float t = 0; t <= 1; t += Time.deltaTime)
        {
            transform.position += direction * speed * Time.deltaTime;
            yield return 0;
        }

    }







}
