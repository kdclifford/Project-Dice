using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using Button.Utils;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Health health; 

    private Animator animator;

    private float velocity;
    private Vector2 leftStickInputAxis;

    private int idle = 0;
    private bool idleAnimation = false;

    public Vector2 deathDirection;
    private Vector2 newDeathDirection;

    float triggerPress = 0;

    bool isDead = false;
    ProjectileFire projectileFire;
    public EControllerType controllerType;

    private bool doubleAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        projectileFire = GetComponent<ProjectileFire>();
    }

    // Update is called once per frame
    void Update()
    {     
   
        
        triggerPress = 0;

        if (health.GetHealth()  > 0)
        {

            
                leftStickInputAxis.x = ButtonMapping.GetStick(controllerType, EStickMovement.HorizontalMovement, transform.position);
                leftStickInputAxis.y = ButtonMapping.GetStick(controllerType, EStickMovement.VerticalMovement, transform.position);

                

                if (projectileFire.projectileRight.tag != ("NotEquipped") && ButtonMapping.GetButton(controllerType, EButtonActions.RightAttack))
                {
                    triggerPress += 1;
                }

                if (projectileFire.projectileLeft.tag != ("NotEquipped") && ButtonMapping.GetButton(controllerType, EButtonActions.LeftAttack))
                {
                    triggerPress += 1;
                }
            
           


            
                animator.SetFloat("TriggerPress", triggerPress);
            


            velocity = Mathf.Abs(leftStickInputAxis.x) + Mathf.Abs(leftStickInputAxis.y);
            leftStickInputAxis = AnimationScript.CurrentDirection(leftStickInputAxis, gameObject);

            leftStickInputAxis.x *= velocity;
            leftStickInputAxis.y *= velocity;


            Move();

            if (!idleAnimation && !projectileFire.rightFire && !projectileFire.leftFire)
            {

                Idle();
            }
        }       

    }


    //public void UpdateHeartUI()
    //{
    //    animator.SetFloat("Health", health.GetHealth());
    //}


    public void DeathAnimation()
    {

        
            Destroy(GetComponent<MovementScript>());
            Destroy(GetComponent<ProjectileFire>());
            Destroy(this);

            if (deathDirection.y >= 0 && deathDirection.y < 0.1)
            {
                deathDirection.y = 0.1f;
            }
            else if (deathDirection.y <= 0 && deathDirection.y > -0.1)
            {
                deathDirection.y = -0.1f;
            }

            newDeathDirection = AnimationScript.CurrentDirection(deathDirection, gameObject);
            Death();
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
        isDead = true;
        animator.SetFloat("DeathX", newDeathDirection.x);
        animator.SetFloat("DeathY", newDeathDirection.y);
        animator.SetTrigger("Dead");
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

    public void AttackOff()
    {
        AnimationScript.StopAttack(animator);
    }
   

}
