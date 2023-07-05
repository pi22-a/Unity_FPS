using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �¾ �� ü���� �ִ�ü���� �ǰ� �ϰ�ʹ�.
// �ѿ� ������ ü���� 1 �����ϰ�ʹ�. 
// ü���� ����Ǹ� UI�� ǥ���ϰ� �ʹ�.
public class EnemyHP : MonoBehaviour
{
    int hp;
    public int maxHP = 2;
    public Slider sliderHP;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            // ü���� ����Ǹ� UI�� ǥ���ϰ� �ʹ�.
            sliderHP.value = hp;
        }
    }
    void Start()
    {
        // �¾ �� ü���� �ִ�ü���� �ǰ� �ϰ�ʹ�.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}