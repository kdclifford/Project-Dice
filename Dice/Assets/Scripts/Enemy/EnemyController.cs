using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.AI;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    //Public
    //public EElementalyType element;
    [SerializeField, Header("Object Referneces")]
    private ESpellEnum projectile = 0;
    [SerializeField]
    private RandomColour meshRenderer;
    public GameObject target;
    public bool isDead = false;
    [SerializeField]
    private LayerMask layerMask;
    public LayerMask layerhit;
    public LayerMask layerFLoor;

    public GameObject HPPickup;

    public EAIStates currentState = EAIStates.RandomMove;
    private float randDrop;
    private bool HPdropped = false;

    //Private
    private bool removeBody = false;
    private float removeSpeed = 5;
    private NavMeshAgent agent;
    private Animator animator;

    [SerializeField, Header("Projectile offsets"), Tooltip("Controls how high the projectile spawns")]
    private float yOffsetProgectile = 1;
    [SerializeField, Tooltip("Controls how far forward the projectile spawns")]
    private float zOffsetProgectile = 1;
    [SerializeField, Header("Projectile Settings")]
    private float fireRate = 1;
    [SerializeField]
    private float projectileSpeed = 10;

    private float fireCooldown = 0;
    private int playerHealth;
    private SoundManager soundManager;
    private Health health;

    public LayerMask raycastMask;
    bool gotRandomPos = false;
    Vector3 dest;
    private GameObject lootDrop;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.avoidancePriority = 0;
        fireCooldown = fireRate;
        animator = GetComponent<Animator>();
        soundManager = SoundManager.instance;
        health = GetComponent<Health>();
        lootDrop = Resources.Load("Mana/ManaPS", typeof(GameObject)) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        if (target != null)
        {
            AnimationScript.StopAttack(animator);
            if (health.GetHealth() <= 0)
            {
                currentState = EAIStates.Dead;
            }
            else
            {
                if (currentState != EAIStates.Nothing)
                {
                    if (!CheckLineofSight(target.transform.position) && Vector3.Distance(target.transform.position, transform.position) <=
                        SpellList.instance.spells[(int)projectile].range)
                    {
                        currentState = EAIStates.Fire;

                        agent.ResetPath();
                    }
                    else if (!CheckLineofSight(target.transform.position) && Vector3.Distance(target.transform.position, transform.position) >=
                        SpellList.instance.spells[(int)projectile].range/* && target.GetComponent<PlayerAnimations>() != null*/)
                    {
                        if (currentState != EAIStates.MoveTowards)
                        {
                            currentState = EAIStates.MoveTowards;
                            agent.ResetPath();
                        }
                    }
                    else
                    {
                        currentState = EAIStates.RandomMove;
                    }
                }
            }

            if (currentState != EAIStates.Dead)
            {
                Vector2 targetDirection;
                targetDirection.x = agent.velocity.x;
                targetDirection.y = agent.velocity.z;
                float velocity = Mathf.Abs(targetDirection.x) + Mathf.Abs(targetDirection.y);

                targetDirection = AnimationScript.CurrentDirection(targetDirection, gameObject);
                targetDirection.Normalize();

                animator.SetFloat("Velocity", velocity);
                animator.SetFloat("XMove", targetDirection.x);
                animator.SetFloat("YMove", targetDirection.y);
            }


            if (currentState == EAIStates.Dead)
            {
                randDrop = UnityEngine.Random.Range(0.0f, 100.0f);

                if(randDrop <= 25.0f && HPdropped == false)
                {
                    Instantiate(HPPickup, transform.position, Quaternion.Euler(0,0,0));
                    HPdropped = true;
                }

              //  agent.ResetPath();
                isDead = true;

                animator.SetBool("Death", isDead);
                animator.SetTrigger("Dead");

                PlayerController.instance.increaseKills();
                currentState = EAIStates.Nothing;
                Destroy(GetComponent<Collider>());
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<NavMeshAgent>());
            }
            else if (currentState == EAIStates.MoveTowards)
            {
                agent.SetDestination(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));
            }
            else if (currentState == EAIStates.Fire)
            {
                Vector3 targetDir = target.transform.position - transform.position;
                targetDir.y = transform.position.y;
                Quaternion rotation;

                //transform.rotation = Quaternion.Euler( Vector3.RotateTowards(transform.position, targetDir, removeSpeed * Time.deltaTime, 0f));
                rotation = Quaternion.LookRotation(targetDir);
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
                transform.LookAt(target.transform);

                EnemyShoot();
            }
            else if (currentState == EAIStates.RandomMove)
            {
                RaycastHit hit;
                
                if (!gotRandomPos)
                {
                    while (dest == new Vector3())
                    {
                        dest = RandomNavSphere(transform.position, 30, layerFLoor);
                        //dest.y = transform.position.y - 8;
                    }
                    gotRandomPos = true;
                }

                if (Physics.Raycast(transform.forward, dest, out hit, 10, layerhit))
                {
                    dest = RandomNavSphere(transform.position, 30, layerFLoor);
                }
                agent.SetDestination(dest);

                if (Vector3.Distance(transform.position, dest) <= 3)
                {
                    gotRandomPos = false;
                    agent.ResetPath();
                    dest = new Vector3();
                }

            }

            if (removeBody)
            {
                float posY = transform.position.y - (removeSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            }
            fireCooldown -= Time.deltaTime;
        }
    }

    public void RemoveBody()
    {
        removeBody = true;
        Destroy(gameObject, 5);
    }

    //Universal call to make enemies
    private void EnemyShoot()
    {
        if ((int)projectile != -1)
        {
            if (playerHealth >= 0 && fireCooldown <= 0)
            {
                AnimationScript.EnemyAttack(animator);
            }
            else
            {
                AnimationScript.StopAttack(animator);
            }
        }
    }

    //Spider Attack
    public void Fire()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimations>() != null)
        {
            SpawnBullet();
            //SpawnBullet(10, 45);
            //SpawnBullet(-10, -45);
        }
    }

  

    //Changes the staff colour while shooting
    public void StaffAttack()
    {
        meshRenderer.lerpOn = true;
    }

    //Spawns projectiles
    void SpawnBullet()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;

        Vector3 firePos = transform.position;
        firePos.y += yOffsetProgectile;
        //firePos += transform.forward;

        //playerRot.eulerAngles += 45;
        SpellList.instance.spells[(int)projectile].CastSpell(firePos, transform.eulerAngles.y, gameObject, "EnemyProjectile");
       
        fireCooldown = fireRate;
    }

    bool CheckLineofSight(Vector3 playerPos)
    {
        RaycastHit hit;
        float distance = Vector3.Distance(playerPos, transform.position);
        Vector3 targetDir = playerPos - transform.position;
        targetDir.y = transform.position.y + 4;
            targetDir.Normalize();
        bool check = Physics.Raycast(transform.position, targetDir, out hit, distance, layerhit);
        return check;

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerhit)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, distance, 1);
       return hit.position;
    }

    public void LootDrop()
    {
        ParticleSystem.MainModule particle = lootDrop.GetComponent<ParticleSystem>().main;
        particle.startColor = SpellList.instance.spells[(int)projectile].castingColour;
        GameObject loot = MonoBehaviour.Instantiate(lootDrop, transform.position, Quaternion.identity) as GameObject;
       // loot.tag = ManaTag(SpellList.instance.spells[(int)projectile].element);
    }

    public string ManaTag(EElementalyType type)
    {
        switch (type)
        {
            case EElementalyType.Fire:
                return "Fire";
            case EElementalyType.Water:
                return "Water";
            case EElementalyType.Electricity:
                return "Electricity";
            case EElementalyType.Nature:
                return "Nature";
            case EElementalyType.Wind:
                return "Wind";
        }
        throw new Exception();
    }

}

public enum EAIStates
{
    Dead,
    RandomMove,
    Fire,
    MoveTowards,
    Nothing,
}

