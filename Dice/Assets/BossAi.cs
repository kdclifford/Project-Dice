using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    EBossStates bossState = EBossStates.Idle;
    List<GameObject> enemyList;
    Health bossHealth;
    CDragon dragon;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = GetComponent<Health>();
        dragon = new CDragon(gameObject, player);
    }

    // Update is called once per frame
    void Update()
    {
        dragon.Action();
    }

    public void SpawnBullet()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;

        Vector3 firePos = transform.position;
        //firePos.y += yOffsetProgectile;
        //firePos += transform.forward;

        //playerRot.eulerAngles += 45;
        SpellList.instance.spells[(int)dragon.currentSpell].CastSpell(firePos, transform.eulerAngles.y, gameObject, "EnemyProjectile");
    }

}


public abstract class CBossBase
{
    public EBossStates bossState = EBossStates.Idle;
    public abstract void Action();

    public GameObject agent;
    public GameObject target;
    public Health health;

    public void StateCheck(float health)
    {
        if (health == 101)
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
        else if (health > 0)
        {
            bossState = EBossStates.PhaseThree;
        }
        else
        {
            bossState = EBossStates.Dead;
        }
    }

    public void DeadAnimation()
    {

    }




}

public class CDragon : CBossBase
{
    ESpellEnum SpellOne = ESpellEnum.FireWork;
    ESpellEnum SpellTwo = ESpellEnum.FireBall;
    ESpellEnum SpellThree = ESpellEnum.FireWork;
    float attackCooldown = 3;
    public ESpellEnum currentSpell;

    Animator anim;

    public CDragon(GameObject agentRef, GameObject targetRef)
    {
        agent = agentRef;
        target = targetRef;
        anim = agent.GetComponent<Animator>();
        health = agent.GetComponent<Health>();
    }

    public override void Action()
    {

        StateCheck(health.GetHealth());
        Debug.Log(health.GetHealth());

        if (bossState == EBossStates.PhaseOne)
        {
            Debug.Log("PhaseOne");
            currentSpell = SpellOne;
            PhaseOne();
        }
        else if (bossState == EBossStates.PhaseTwo)
        {
            Debug.Log("Phase2");
            currentSpell = SpellOne;
            PhaseTwo();
        }
        else if (bossState == EBossStates.PhaseThree)
        {
            Debug.Log("Phase3");
            currentSpell = SpellOne;
            PhaseThree();
        }
        else if (bossState == EBossStates.Dead)
        {
            Debug.Log("Dead");
            DeadAnimation();
        }
        attackCooldown -= Time.deltaTime;
    }

    void PhaseOne()
    {
        //if(Vector3.SignedAngle(agent.transform.position, target.transform.position, Vector3.up) > 0)
        //{
        //    AnimationScript.DragonPan(anim, (Vector3.SignedAngle(agent.transform.position, target.transform.position, Vector3.up) - -180) / (180 - -180));
        //}
        //else if (Vector3.SignedAngle(agent.transform.position, target.transform.position, Vector3.up) < 0)
        //{

        //}
        float targetAngle = Vector3.SignedAngle(agent.transform.position, target.transform.position, Vector3.up);




        if (targetAngle > 10 || targetAngle < -10)
        {
            if (targetAngle > 10)
            {
                targetAngle = 1;
            }
            else if (targetAngle < -10)
            {
                targetAngle = 0;
            }

            // Determine which direction to rotate towards
            Vector3 targetDirection = target.transform.position - agent.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 0.5f * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(agent.transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(agent.transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            agent.transform.rotation = Quaternion.LookRotation(newDirection);


            anim.SetBool("Pan", true);
            AnimationScript.DragonPan(anim, targetAngle);
        
        }
        else
        {
            anim.SetBool("Pan", false);
        }

        //agent.transform.roa(target.transform);


        if (attackCooldown < 0)
        {
            AnimationScript.DragonAttack(anim, 1);
            attackCooldown = 10;
        }
        else
        {
            AnimationScript.DragonAttack(anim, 0);
        }




    }

    void PhaseTwo()
    {

    }

    void PhaseThree()
    {

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