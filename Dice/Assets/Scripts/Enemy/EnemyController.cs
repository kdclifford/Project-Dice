using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Public
    [SerializeField, Header("Object Referneces")]
    private GameObject projectile;
    [SerializeField]
    private RandomColour meshRenderer;
    public GameObject target;
    public bool isDead = false;
    [SerializeField]
    private LayerMask layerMask;
    public LayerMask layerhit;
    public LayerMask layerFLoor;

    public EAIStates currentState = EAIStates.RandomMove;
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

    bool gotRandomPos = false;
    Vector3 dest;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.avoidancePriority = 0;
        fireCooldown = fireRate;
        animator = GetComponent<Animator>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationScript.StopAttack(animator);
        if (health.GetHealth() <= 0)
        {
            currentState = EAIStates.Dead;
        }
        else if (!CheckLineofSight(target.transform.position) && Vector3.Distance(target.transform.position, transform.position) <= 30 && target.GetComponent<PlayerAnimations>() != null)
        {
            currentState = EAIStates.Fire;
            
            agent.ResetPath();
        }
        else if (!CheckLineofSight(target.transform.position) && Vector3.Distance(target.transform.position, transform.position) <= 50 && target.GetComponent<PlayerAnimations>() != null)
        {
            currentState = EAIStates.MoveTowards;
        }
        else
        {
            currentState = EAIStates.RandomMove;
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


        if (currentState == EAIStates.Dead && !removeBody)
        {
            agent.ResetPath();
            isDead = true;
            animator.SetBool("Death", isDead);
            animator.SetTrigger("Dead");            
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<Rigidbody>());
        }
        else if (currentState == EAIStates.MoveTowards)
        {


            agent.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
         


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
            if (!gotRandomPos)
            {
                while (dest == new Vector3())
                {
                    dest = RandomNavSphere(transform.position, 50, layerFLoor);
                    dest.y = transform.position.y;
                }
                gotRandomPos = true;
            }
            ////Debug.Log(dest);

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

    public void RemoveBody()
    {
        removeBody = true;
        Destroy(gameObject, 5);
    }

    //Universal call to make enemies
    public void EnemyShoot()
    {
        if (projectile.tag != ("NotEquipped"))
        {
            if (playerHealth >= 0 && fireCooldown <= 0)
            {
                AnimationScript.SpiderAttack(animator);
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
            SpawnBullet(0, 0);
            SpawnBullet(10, 45);
            SpawnBullet(-10, -45);
        }
    }

    //Wisard attacks
    public void WizardFire()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimations>() != null)
        {
            SpawnBullet(0, 0);
        }
    }

    //Changes the staff colour while shooting
    public void StaffAttack()
    {
        meshRenderer.lerpOn = true;
    }

    //Spawns projectiles
    void SpawnBullet(float angleMin, float angleMAx)
    {
        Vector3 forward = transform.forward;
        forward.y = 0;

        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y + Random.Range(angleMin, angleMAx), 90);

        Vector3 firePos = transform.position;
        firePos.y += yOffsetProgectile;
        firePos += transform.forward * zOffsetProgectile;

        //playerRot.eulerAngles += 45;
        GameObject bullet = Instantiate(projectile, firePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed);
        bullet.tag = "EnemyProjectile";
        ESoundClipEnum clipEnum = (ESoundClipEnum)System.Enum.Parse(typeof(ESoundClipEnum), projectile.name, true);
        soundManager.Play(clipEnum, bullet);
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
        //Debug.Log(hit.collider.gameObject.name);
        //Debug.DrawRay(transform.position, targetDir * distance, Color.red);
        //Destroy(hit.collider.gameObject);
        return check;

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerhit)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, distance, 1);
       return hit.position;
    }

}

public enum EAIStates
{
    Dead,
    RandomMove,
    Fire,
    MoveTowards,
}