using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationFunctions.Utils;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{

    private NavMeshAgent agent;
    private Animator animator;
    public GameObject target;
    public float health = 100;
    public bool isDead = false;
    private bool removeBody = false;
    private float removeSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.avoidancePriority = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && health < 0)
        {
            agent.ResetPath();
            isDead = true;
            animator.SetBool("Death", isDead);
            animator.SetTrigger("Dead");
            Destroy(GetComponent<Collider>());
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<Rigidbody>());
        }
        else if (!isDead)
        {
            agent.SetDestination(new Vector3(target.transform.position.x, 0, target.transform.position.z));
            Vector2 targetDirection;
            targetDirection.x = agent.velocity.x;
            targetDirection.y = agent.velocity.z;
            float velocity = Mathf.Abs(targetDirection.x) + Mathf.Abs(targetDirection.y);

            targetDirection = AnimationScript.CurrentDirection(targetDirection, gameObject);
            targetDirection.Normalize();


            animator.SetFloat("Velocity", velocity);
            animator.SetFloat("XMove", targetDirection.x);
            animator.SetFloat("YMove", targetDirection.y);



            if (target.GetComponent<PlayerHealth>() != null &&  Vector3.Distance(target.transform.position, transform.position) <= 30)
            {
                Vector3 targetDir = target.transform.position - transform.position;
                targetDir.y = transform.position.y;
                Quaternion rotation;

                //transform.rotation = Quaternion.Euler( Vector3.RotateTowards(transform.position, targetDir, removeSpeed * Time.deltaTime, 0f));
                rotation = Quaternion.LookRotation(targetDir);
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100 * Time.deltaTime);
                transform.LookAt(target.transform);

                GetComponent<EnemyFire>().EnemyShoot();


            }
        }

        if (removeBody)
        {
            float posY = transform.position.y - (removeSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }

    }

    public void RemoveBody()
    {
        removeBody = true;
        Destroy(gameObject, 5);
    }

}
