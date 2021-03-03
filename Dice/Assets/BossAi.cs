using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    EBossStates bossState = EBossStates.Idle;
    List<GameObject> enemyList;
    Health bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        PhaseCheck(bossHealth.GetHealth());
        BossAction();

    }

    void PhaseCheck(float health)
    {
        if (bossHealth)
        {
            if(enemyList.Count > 0)
            {
                bossState = EBossStates.Idle;
            }
            else if (health > 66)
            {
                bossState = EBossStates.PhaseOne;
            }
            else if (health > 33)
            {
                bossState = EBossStates.PhaseTwo;
            }
            else if(health > 0)
            {
                bossState = EBossStates.PhaseThree;
            }
            else
            {
                bossState = EBossStates.Dead;
            }
        }
        else
        {
            Debug.Log("No Heath Component");
        }
    }

    void BossAction()
    {
        if (bossState == EBossStates.PhaseOne)
        {

        }
        else if (bossState == EBossStates.PhaseTwo)
        {

        }
        else if (bossState == EBossStates.PhaseThree)
        {

        }
        else if (bossState == EBossStates.Dead)
        {

        }
    }

}








public enum EBossStates
{
    Idle,
    PhaseOne,
    PhaseTwo,
    PhaseThree,
    Dead,
}