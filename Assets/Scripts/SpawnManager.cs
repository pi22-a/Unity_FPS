using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public float makeTime = 2;
    public GameObject enemyFactory;
    public Transform[] spawnList;
    public float minTime = 1;
    public float maxTime = 2;
    // 생성수를 최대 갯수로 제한하고 싶다.
    // 만약 생성된 녀석이 파괴되면 생성수를 1 감소하고 싶다.
    public int makeCount;
    public int maxMakeCount = 5;

    internal void 나죽었어(Enemy2 enemy2)
    {
        makeCount--;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 일정시간마다 적공장에서 적을 생성해서
        // 스폰목록중에 랜덤한 위치에서 배치하고 싶다.
        // 생성후에 생성시간을 랜덤으로 정하고싶다.
        
        while (true)
        {
            // 만약 생성수가 최대수 미만이라면 생성하고 싶다.
            if (makeCount < maxMakeCount)
            {
                GameObject enemy = Instantiate(enemyFactory);
                makeCount++;
                int index = Random.Range(0, spawnList.Length);
                enemy.transform.position = spawnList[index].position;
                yield return new WaitForSeconds(makeTime);

                makeTime = Random.Range(minTime, maxTime);
            }
            yield return 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
