using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����ڰ� ���콺 ���� ��ư�� ������
// �Ѿ˰��忡�� �Ѿ��� �����
// �� �Ѿ��� �ѱ���ġ�� ��ġ�ϰ�ʹ�.
public class PlayerFire : MonoBehaviour
{
    enum BImpactName
    {
        Floor,
        Enemy
    }

    public GameObject bulletFactory;
    public Transform firePosition;
    // �Ѿ��ڱ�����
    public GameObject[] bImpactFactorys;


    void Start()
    {
    }

    void Update()
    {
        UpdateFire();
        // UpdageGrenade();
    }

    private void UpdateFire()
    {
        // 0. ����ڰ� ���콺 ���� ��ư�� ������
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. ī�޶󿡼� ī�޶��� �չ������� �ü��� �����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;
            // 2. �ٶ󺸰�ʹ�.
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue))
            {
                bool isEnemy = false;
                // 3. �ü��� ���� ���� �Ѿ��ڱ����忡�� �Ѿ��ڱ��� ���� ��ġ�ϰ�ʹ�.
                BImpactName biName = BImpactName.Floor;
                // ���� hitInfo�� ��ü�� ���̾ Enemy���
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    biName = BImpactName.Enemy;
                    isEnemy = true;
                }

                GameObject bulletImpact = Instantiate(bImpactFactorys[(int)biName]);
                bulletImpact.transform.position = hitInfo.point;
                // ������ ȸ���ϰ�ʹ�. Ƣ�¹���(forward�� �ε��� ���� Normal��������
                bulletImpact.transform.forward = hitInfo.normal;

                // ���� �ѿ� �´°��� ���̶��
                if (isEnemy)
                {
                    // ��(Enemy2)���� �� �ѿ� �¾Ҿ�!(DamageProcess()) ��� �˷��ְ�ʹ�.
                    Enemy2 enemy = hitInfo.transform.GetComponent<Enemy2>();

                    enemy.DamageProcess();

                }

            }
            else
            {
                // ���
            }
        }
    }

    private void UpdageGrenade()
    {
        // ���� ����ڰ� GŰ�� ������ ��ź�� ������ �ʹ�.
        if (Input.GetKeyDown(KeyCode.G))
        {
            // �Ѿ˰��忡�� �Ѿ��� �����
            GameObject bullet = Instantiate(bulletFactory);
            // �� �Ѿ��� �ѱ���ġ�� ��ġ�ϰ�ʹ�.
            bullet.transform.position = firePosition.position;
            bullet.transform.forward = firePosition.forward;
            // �Ѿ��� speed�� �ٲٰ�ʹ�.
            Bullet bulletComp = bullet.GetComponent<Bullet>();
            bulletComp.speed = 20;
        }
    }


}