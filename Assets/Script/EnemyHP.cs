using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 태어날 때 체력이 최대체력이 되게 하고싶다.
// 총에 맞으면 체력을 1 감소하고싶다. 
// 체력이 변경되면 UI로 표현하고 싶다.
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
            // 체력이 변경되면 UI로 표현하고 싶다.
            sliderHP.value = hp;
        }
    }
    void Start()
    {
        // 태어날 때 체력이 최대체력이 되게 하고싶다.
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }
}