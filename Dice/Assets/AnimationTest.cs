using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        leftStickInputAxis.x = Input.GetAxis("LHorizontal");
        leftStickInputAxis.y = Input.GetAxis("LVertical");


        triggerPress = 0;


        if (GetComponent<ProjectileFire>().projectileRight.tag != ("NotEquipped") && Input.GetAxis("RTrigger") > 0)
        {
            triggerPress += Input.GetAxis("RTrigger");
        }

        if (GetComponent<ProjectileFire>().projectileRight.tag != ("NotEquipped") && Input.GetAxis("LTrigger") > 0)
        {
            triggerPress += Input.GetAxis("LTrigger");
        }

        animator.SetFloat("TriggerPress", triggerPress);


        if (health > 0)
        {
            velocity = Mathf.Abs(leftStickInputAxis.x) + Mathf.Abs(leftStickInputAxis.y);
            leftStickInputAxis = CurrentDirection(leftStickInputAxis);

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
            deathDirection = CurrentDirection(deathDirection);
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


    private Vector2 CurrentDirection(Vector2 input)
    {


        var a = Vector3.SignedAngle(new Vector3(input.x, 0, input.y), transform.forward, transform.up);



        // Normalize the angle
        if (a < 0)
        {
            a *= -1;
        }
        else
        {
            a = 360 - a;
        }

        // Take into consideration the angle of the camera
        //a += Camera.main.transform.eulerAngles.y;

        var aRad = Mathf.Deg2Rad * a; // degrees to radians
        //Debug.Log(leftStickInputAxis);
        // If there is some form of input, calculate the new axis relative to the rotation of the model
        if (input.x != 0 || input.y != 0)
        {
            input = new Vector2(Mathf.Sin(aRad), Mathf.Cos(aRad));
        }


        if (input.x < -1)
        {
            input.x = -1;
        }
        else if (input.x > 1.6f)
        {
            input.x = 1.6f;
        }


        if (input.y < -1)
        {
            input.y = -1;
        }
        else if (input.y > 1.6f)
        {
            input.y = 1.6f;
        }

        return input;
    }




}
