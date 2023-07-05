using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
// 사용자가 점프키를 입력시 점프를 뛰고싶다.
// 최대 점프 횟수를 정해서 여러번 점프하게 하고싶다.

public class PlayerMove : MonoBehaviour
{
    int jumpCount;
    public int maxJumpCount = 2;


    public float speed = 5;

    public float jumpPower = 10;
    public float gravity = -9.81f;
    float yVelocity;

    CharacterController cc;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //사용자의 입력에 따라
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 중력의 힘이 y속도에 작용
        yVelocity += gravity * Time.deltaTime;

        // 땅에 닿았다면 점프카운트를 초기화 하고싶다.
        if(cc.isGrounded)
        {
            jumpCount = 0;
            // 땅에 서있을때는 y속도가 변화하지 않게하고싶다.
            yVelocity = 0;
        }
        else
        {
            GetGravity();
        }

        // 만약 점프카운트가 최대보다 작다 그리고 사용자가점프버튼을 누르면
        if(jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            // JumpPower가 y속도에 작용해야한다.
            yVelocity = jumpPower;
            jumpCount++;
        }

        // 사용자가 점프키 입력시 jumpPower가 y속도에 작용
        if (Input.GetButtonDown("Jump"))
        {
            //JumpPower가 y속도에 작용해야한다.
            yVelocity = jumpPower;

        }
        //방향을 만들고
        //Vector3 dir = Vector3.right * h + Vector3.forward * v;
        Vector3 dir = new Vector3(h, 0, v);
        //현재 방향을 카메라의 앞방향을 기준으로 변환하고싶다.
        dir = cam.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize(); // 벡터의 정규화

        // 최종 결정된 y속도를 dir의 y항목에 반영되어야한다.
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        //그 방향으로 이동하고싶다.
        //transform.position += velocity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        /**/
        
    }
    //중력 작용
    void GetGravity()
    {
        //y 속도에 중력 만큼 작용
        yVelocity += gravity * Time.deltaTime;
    }

    //점프
    //사용자의 y 속도에 점프력 작용
    void Jump()
    {
        //점프 횟수가 있고 점프 키를 누르면 점프 함수 실행
        if (jumpCount > 0 && Input.GetButtonDown("Jump"))
        {
            jumpCount -= 1;
            //yVelocity = PlayerStat.instance.JUMP_POWER;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 from = transform.position;
        Vector3 to = from + Vector3.up * yVelocity;
        
        Gizmos.DrawLine(from, to);

    }
}