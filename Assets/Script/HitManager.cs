using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Image Hit�� ��½�̴� �������� ���� �Ѱ�ʹ�. 
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

    // ��½�̴� �ڷ�ƾ�Լ��� ȣ���� �Լ��� �����ʹ�.
    public void DoHit()
    {
        if (crt != null)
            StopCoroutine(crt);
        crt = StartCoroutine(IEHit(0.1f));
        //crt = StartCoroutine("IEHit", 0.1f);
    }
    Coroutine crt;

    // ��½�̴� �ڷ�ƾ�Լ��� �����ʹ�.
    IEnumerator IEHit(float time)
    {
        // 1. imageHit �� ���̰� �ϰ�ʹ�.
        imageHit.SetActive(true);
        // 2. 0.1�ʱ�ٷȴٰ�
        yield return new WaitForSeconds(time);
        // 3. imageHit �� ������ �ʰ� �ϰ�ʹ�.
        imageHit.SetActive(false);
    }


}
