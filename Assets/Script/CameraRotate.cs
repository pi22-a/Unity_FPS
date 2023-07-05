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
        // 사용자의 입력에 따라
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        // X와 Y축의 값을 누적하고
        rx += my * rotSpeed * Time.deltaTime;
        ry += mx * rotSpeed * Time.deltaTime;
        // rx에 대해 각도를 제한하고싶다.
        rx = Mathf.Clamp(rx, -75, 75);
        // 그 누적값으로 회전을 하고싶다.
        transform.eulerAngles = new Vector3(-rx, ry, 0);
    }
}
