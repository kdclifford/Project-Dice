using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AnimationFunctions.Utils;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator animator;

    private float velocity;
    private Vector2 leftStickInputAxis;

    private int idle = 0;
    private bool idleAnimation = false;

    public float health = 10;
    public Vector2 deathDirection;

    float triggerPress = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        triggerPress = 0;



        if (GetComponent<MovementScript>().controller)
        {
            leftStickInputAxis.x = Input.GetAxis("LHorizontal");
            leftStickInputAxis.y = Input.GetAxis("LVertical");

            if (GetComponent<ProjectileFire>().projectileRight.tag != ("NotEquipped") && Input.GetAxis("RTrigger") > 0)
            {
                triggerPress += Input.GetAxis("RTrigger");
            }

            if (GetComponent<ProjectileFire>().projectileLeft.tag != ("NotEquipped") && Input.GetAxis("LTrigger") > 0)
            {
                triggerPress += Input.GetAxis("LTrigger");
            }
        }
        else
        {
            float verticalInput = 0;
            float horizontalInput = 0;
            if (Input.GetKey(KeyCode.W))
            {
                verticalInput = Vector2.up.y;
            }
            if (Input.GetKey(KeyCode.S))
            {
                verticalInput = Vector2.down.y;
            }
            if (Input.GetKey(KeyCode.A))
            {
                horizontalInput = Vector2.left.x;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horizontalInput = Vector2.right.x;
            }

            leftStickInputAxis.x = horizontalInput;
            leftStickInputAxis.y = verticalInput;

            if (GetComponent<ProjectileFire>().projectileRight.tag != ("NotEquipped") && Input.GetMouseButton(0))
            {
                triggerPress += 1;
            }

            if (GetComponent<ProjectileFire>().projectileLeft.tag != ("NotEquipped") && Input.GetMouseButton(1))
            {
                triggerPress += 1;
            }
        }



        animator.SetFloat("TriggerPress", triggerPress);


        if (health > 0)
        {
            velocity = Mathf.Abs(leftStickInputAxis.x) + Mathf.Abs(leftStickInputAxis.y);
            leftStickInputAxis = AnimationScript.CurrentDirection(leftStickInputAxis, gameObject);

            leftStickInputAxis.x *= velocity;
            leftStickInputAxis.y *= velocity;


            Move();

            if (!idleAnimation && !GetComponent<ProjectileFire>().rightFire && !GetComponent<ProjectileFire>().leftFire)
            {

                Idle();
            }
        }
        else
        {
            deathDirection = AnimationScript.CurrentDirection(deathDirection, gameObject);
            Death();
        }

        animator.SetFloat("Health", health);

    }

    private void Move()
    {
        animator.SetFloat("XMove", leftStickInputAxis.x);
        animator.SetFloat("YMove", leftStickInputAxis.y);
        animator.SetFloat("Velocity", velocity);
    }

    private void Idle()
    {
        //idle = Random.Range(0, 2);
        animator.SetInteger("Idle", idle);
    }

    private void Death()
    {

        animator.SetFloat("DeathX", deathDirection.x);
        animator.SetFloat("DeathY", deathDirection.y);
    }

    private void IdleAnimationOn()
    {
        idleAnimation = true;
    }

    private void IdleAnimationOff()
    {

        idle = Random.Range(0, 4);

        idleAnimation = false;
    }


  




}
