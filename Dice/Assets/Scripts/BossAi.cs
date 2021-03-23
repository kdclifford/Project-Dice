using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.AI;
using UnityEngine;

public class BossAi : MonoBehaviour
{
    EBossStates bossState = EBossStates.Idle;
    List<GameObject> enemyList;
    Health bossHealth;
    CDragon dragon;
    GameObject player;
    NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        bossHealth = GetComponent<Health>();
        dragon = new CDragon(gameObject, player, navAgent);
    }

    // Update is called once per frame
    void Update()
    {
        dragon.Action();
    }

    private void SpellCast(ESpellEnum spell)
    {
        Vector3 forward = transform.forward;
        forward.y = 0;

        Vector3 firePos = transform.position;
        //firePos.y += yOffsetProgectile;
        //firePos += transform.forward;

        //playerRot.eulerAngles += 45;
        SpellList.instance.spells[(int)spell].CastSpell(firePos, transform.eulerAngles.y, gameObject, "EnemyProjectile");
    }

    public void AttackOne()
    {
        SpellCast(dragon.SpellOne);
    }

    public void AttackTwo()
    {
        SpellCast(dragon.SpellTwo);
    }
    public void AttackThree()
    {
        SpellCast(dragon.SpellThree);
    }
}


public abstract class CBossBase
{
    public EBossStates bossState = EBossStates.Idle;
    public abstract void Action();

    public GameObject agent;
    public GameObject target;
    public Health healthComp;

    public void StateCheck(float health, float maxHealth, float shield)
    {
        if (shield > 0)
        {
            bossState = EBossStates.Idle;
        }
        else if (health > maxHealth * 0.6f)
        {
            bossState = EBossStates.PhaseOne;
        }
        else if (health > maxHealth * 0.3f)
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
    public ESpellEnum SpellOne = ESpellEnum.FireWork;
    public ESpellEnum SpellTwo = ESpellEnum.FireBall;
    public ESpellEnum SpellThree = ESpellEnum.FireWork;
    float attackCooldown = 3;
    //public ESpellEnum currentSpell;

    Animator anim;
    NavMeshAgent navMesh;
    public CDragon(GameObject agentRef, GameObject targetRef, NavMeshAgent nav)
    {
        agent = agentRef;
        target = targetRef;
        anim = agent.GetComponent<Animator>();
        healthComp = agent.GetComponent<Health>();
        navMesh = nav;
    }

    public override void Action()
    {

        StateCheck(healthComp.GetHealth(), healthComp.maxHealth, healthComp.GetShield());
        // Debug.Log(health.GetHealth());

        if (bossState == EBossStates.PhaseOne)
        {
            Debug.Log("PhaseOne");
            //currentSpell = SpellOne;
             PhaseOne();
        }
        else if (bossState == EBossStates.PhaseTwo)
        {
            Debug.Log("Phase2");
            //currentSpell = SpellTwo;
            PhaseTwo();
        }
        else if (bossState == EBossStates.PhaseThree)
        {
            Debug.Log("Phase3");
            // currentSpell = SpellThree;
            PhaseThree();
        }
        else if (bossState == EBossStates.Dead)
        {
            Debug.Log("Dead");
            if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                anim.SetTrigger("Die");
                // Avoid any reload.
            }
            DeadAnimation();
        }
        attackCooldown -= Time.deltaTime;
    }

    void Attack(float multiplier)
    {
        // Determine which direction to rotate towards
        Vector3 targetDi = target.transform.position - agent.transform.position;

        float targetAngle = Vector3.SignedAngle(agent.transform.forward, targetDi, Vector3.up);
        // Debug.Log(targetAngle);


        //Debug.Log(targetAngle);


        // Determine which direction to rotate towards
        Vector3 targetDirection = target.transform.position - agent.transform.position;


        // The step size is equal to speed times frame time.
        float singleStep = 0.5f * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(agent.transform.forward, targetDirection, singleStep, 0.0f);


        // Draw a ray pointing at our target in
        Debug.DrawRay(agent.transform.position, newDirection, Color.red);
        newDirection.y = agent.transform.forward.y;

        if (targetAngle > 3 || targetAngle < -3)
        {
            if (targetAngle > 3)
            {
                targetAngle = 1;
            }
            else if (targetAngle < -3)
            {
                targetAngle = 0;
            }
            // Calculate a rotation a step closer to the target and applies rotation to this object
            agent.transform.rotation = Quaternion.LookRotation(newDirection);


            anim.SetBool("Pan", true);
            AnimationScript.DragonPan(anim, targetAngle);
            if (attackCooldown < 0)
            {
                AnimationScript.DragonAttack(anim, 2);
                attackCooldown = 4 / multiplier;
            }
            else
            {
                AnimationScript.DragonAttack(anim, 0);
            }
        }
        else
        {
            anim.SetBool("Pan", false);
            if (attackCooldown < 0)
            {
                AnimationScript.DragonAttack(anim, 1);
                attackCooldown = 4 / multiplier;
            }
            else
            {
                AnimationScript.DragonAttack(anim, 0);
            }
        }
    }

    bool once = true;
    void PhaseOne()
    {
        Attack(1);
    }

    void PhaseTwo()
    {
        Attack(6);

        if (once)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("SpawnEnemy");
                Vector2 randPos = Random.insideUnitCircle * 20;
                Vector3 position = new Vector3(agent.transform.position.x + randPos.x, agent.transform.position.y, agent.transform.position.z + randPos.y);
                GameObject tempEnemy = MonoBehaviour.Instantiate(SpawnManager.instance.enemyList[(int)EnemyType.Bat * ((int)EElementalyType.AmountOfElements) + (int)EElementalyType.Fire], position, agent.transform.rotation) as GameObject;

            }
            once = false;
        }

        if (!navMesh.hasPath)
        {
            Debug.Log("Path");
            Vector3 dir = target.transform.position - agent.transform.position;

            RaycastHit hit;
            RaycastHit floorHit;

            if (Physics.Raycast(agent.transform.position, dir, out hit, 100f, LayerMask.GetMask("Obstacle")))
            {
                if (Physics.Raycast(hit.point, Vector3.down, out floorHit, 100f, LayerMask.GetMask("Floor")))
                {
                    navMesh.SetDestination(floorHit.point);
                }
            }


        }


    }

    void PhaseThree()
    {
        Attack(13);

       
            Debug.Log("Path");
            Vector3 dir = target.transform.position - agent.transform.position;

            RaycastHit hit;
            RaycastHit floorHit;

          
                    navMesh.SetDestination(target.transform.position);
                


        


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