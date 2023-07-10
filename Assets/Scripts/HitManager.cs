using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Image Hit를 번쩍이는 느낌으로 껏다 켜고싶다. 
public class HitManager : MonoBehaviour
{
    public static HitManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject imageHit;

    void Start()
    {
    }

    // 번쩍이는 코루틴함수을 호출할 함수를 만들고싶다.
    public void DoHit()
    {
        if (crt != null)
            StopCoroutine(crt);
        crt = StartCoroutine(IEHit(0.1f));
        //crt = StartCoroutine("IEHit", 0.1f, 1);
    }
    Coroutine crt;

    // 번쩍이는 코루틴함수를 만들고싶다.
    IEnumerator IEHit(float time)
    {
        // 1. imageHit 를 보이게 하고싶다.
        imageHit.SetActive(true);
        // 2. 0.1초기다렸다가
        yield return new WaitForSeconds(time);
        // 3. imageHit 를 보이지 않게 하고싶다.
        imageHit.SetActive(false);
    }


}
