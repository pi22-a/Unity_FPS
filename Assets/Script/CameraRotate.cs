using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    float rx = 0;
    float ry = 0;

    public float rotSpeed = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ������� �Է¿� ����
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        // X�� Y���� ���� �����ϰ�
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // rx�� ���� ������ �����ϰ�ʹ�.
        rx = Mathf.Clamp(rx, -75, 75);
        // �� ���������� ȸ���� �ϰ�ʹ�.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
