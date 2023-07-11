using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 사용자가 마우스 왼쪽 버튼을 누르면
// 총알공장에서 총알을 만들고
// 그 총알을 총구위치에 배치하고싶다.
// 태어날 때 수류탄UI공장에서 수류탄UI를 만들어서 화면에 배치하고 싶다.
// 쿨타임이 1일 때 수류탄을 던질 수 있다. 던지면 쿨타임이 0이되게하고싶다.
public class PlayerFire : MonoBehaviour
{
    public GameObject grenadeUIFactory;
    public ScrollRect scrollRectSkill;

    enum BImpactName
    {
        Floor,
        Enemy
    }

    public GameObject grenadeFactory;
    public Transform firePosition;
    // 총알자국공장
    public GameObject[] bImpactFactorys;
    SkillItem item;
    private void Awake()
    {
        GameObject ui = Instantiate(grenadeUIFactory);
        item = ui.GetComponent<SkillItem>();
        ui.transform.parent = scrollRectSkill.content;
    }


    void Start()
    {
    }

    void Update()
    {
        UpdateFire();
        UpdageGrenade();
    }

    private void UpdateFire()
    {
        // 0. 사용자가 마우스 왼쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. 카메라에서 카메라의 앞방향으로 시선을 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo;
            // 2. 바라보고싶다.
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue))
            {
                bool isEnemy = false;
                // 3. 시선이 닿은 곳에 총알자국공장에서 총알자국을 만들어서 배치하고싶다.
                BImpactName biName = BImpactName.Floor;
                // 만약 hitInfo의 물체의 레이어가 Enemy라면
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    biName = BImpactName.Enemy;
                    isEnemy = true;
                }

                GameObject bulletImpact = Instantiate(bImpactFactorys[(int)biName]);
                bulletImpact.transform.position = hitInfo.point;
                // 방향을 회전하고싶다. 튀는방향(forward을 부딪힌 면의 Normal방향으로
                bulletImpact.transform.forward = hitInfo.normal;

                // 만약 총에 맞는것이 적이라면
                if (isEnemy)
                {
                    // 적(Enemy2)에게 너 총에 맞았어!(DamageProcess()) 라고 알려주고싶다.
                    Enemy2 enemy = hitInfo.transform.GetComponent<Enemy2>();

                    enemy.DamageProcess();

                }

            }
            else
            {
                // 허공
            }
        }
    }

    private void UpdageGrenade()
    {
        // 만약 스킬을 사용할 수 있다면 그리고 사용자가 G키를 누르면 폭탄을 던지고 싶다.
        if (item.CanDoIt() && Input.GetKeyDown(KeyCode.G))
        {
            item.DoIt();
            GameObject g = Instantiate(grenadeFactory);
            g.transform.position = firePosition.position;
            g.transform.forward = firePosition.forward;

            g.GetComponent<Grenade>().speed = 10;
        }
    }


}
