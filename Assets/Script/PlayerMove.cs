using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������� �Է¿� ���� �յ��¿�� �̵��ϰ� �ʹ�.
// ����ڰ� ����Ű�� �Է½� ������ �ٰ�ʹ�.
// �ִ� ���� Ƚ���� ���ؼ� ������ �����ϰ� �ϰ�ʹ�.

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
        //������� �Է¿� ����
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // �߷��� ���� y�ӵ��� �ۿ�
        yVelocity += gravity * Time.deltaTime;

        // ���� ��Ҵٸ� ����ī��Ʈ�� �ʱ�ȭ �ϰ�ʹ�.
        if(cc.isGrounded)
        {
            jumpCount = 0;
            // ���� ���������� y�ӵ��� ��ȭ���� �ʰ��ϰ�ʹ�.
            yVelocity = 0;
        }
        else
        {
            GetGravity();
        }

        // ���� ����ī��Ʈ�� �ִ뺸�� �۴� �׸��� ����ڰ�������ư�� ������
        if(jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            // JumpPower�� y�ӵ��� �ۿ��ؾ��Ѵ�.
            yVelocity = jumpPower;
            jumpCount++;
        }

        // ����ڰ� ����Ű �Է½� jumpPower�� y�ӵ��� �ۿ�
        if (Input.GetButtonDown("Jump"))
        {
            //JumpPower�� y�ӵ��� �ۿ��ؾ��Ѵ�.
            yVelocity = jumpPower;

        }
        //������ �����
        //Vector3 dir = Vector3.right * h + Vector3.forward * v;
        Vector3 dir = new Vector3(h, 0, v);
        //���� ������ ī�޶��� �չ����� �������� ��ȯ�ϰ�ʹ�.
        dir = cam.transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize(); // ������ ����ȭ

        // ���� ������ y�ӵ��� dir�� y�׸� �ݿ��Ǿ���Ѵ�.
        Vector3 velocity = dir * speed;
        velocity.y = yVelocity;
        //�� �������� �̵��ϰ�ʹ�.
        //transform.position += velocity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
        /**/
        
    }
    //�߷� �ۿ�
    void GetGravity()
    {
        //y �ӵ��� �߷� ��ŭ �ۿ�
        yVelocity += gravity * Time.deltaTime;
    }

    //����
    //������� y �ӵ��� ������ �ۿ�
    void Jump()
    {
        //���� Ƚ���� �ְ� ���� Ű�� ������ ���� �Լ� ����
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