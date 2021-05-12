using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using Button.Utils;
using XInputDotNetPure;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Health health; 

    private Animator animator;

  

    public int idle = 0;
    public int oldIdle = 0;
    //private bool idleAnimation = false;

    public Vector2 deathDirection;
    private Vector2 newDeathDirection;

    
    PlayerController playerController;

    PlayerIndex playerIndex = 0;

    private bool doubleAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        //gameSettings = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSettings>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {  
        //if (health.GetHealth()  > 0)
        //{
        //    Move();

        //    if (!idleAnimation && !playerController.rightFire && !playerController.leftFire)
        //    {

        //        Idle();
        //    }
        //}    
    }

    public void DeathAnimation()
    {
        //Destroy(GetComponent<PlayerController>());
        GetComponent<PlayerController>().enabled = false;
        //Destroy(this);

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
        AnimationScript.StopAttack(animator);
    }
    

    public void Move(Vector2 direction, float velocity)
    {
        animator.SetFloat("XMove", direction.x);
        animator.SetFloat("YMove", direction.y);
        animator.SetFloat("Velocity", velocity);
    }

    public void Idle()
    {
        //idle = Random.Range(0, 2);
        animator.SetInteger("Idle", idle);
    }

    private void Death()
    {
       // isDead = true;
        animator.SetFloat("DeathX", newDeathDirection.x);
        animator.SetFloat("DeathY", newDeathDirection.y);
        animator.SetTrigger("Dead");
    }

    //private void IdleAnimationOn()
    //{
    //    idleAnimation = true;
    //}

    private void IdleAnimationOff()
    {
        while (idle == oldIdle)
        {
            idle = Random.Range(0, 4);
        }
        oldIdle = idle;
       // idleAnimation = false;
    }

    public void AttackOff()
    {
        AnimationScript.StopAttack(animator);
    }


    //Vibrations

    public void RightControllerVibration()
    {
        GamePad.SetVibration(playerIndex, 0, 0.2f);
    }

    public void LeftControllerVibration()
    {
        GamePad.SetVibration(playerIndex, 0.5f, 0);
    }

    public void BothControllerVibration()
    {
        GamePad.SetVibration(playerIndex, 0.5f, 0.5f);
    }

    public void VibrationOff()
    {
        GamePad.SetVibration(playerIndex, 0, 0);
    }



    public void DoubleAttackOn()
    {
        doubleAttack = false;
    }

    public void DoubleAttackOff()
    {
        doubleAttack = true;
    }
}
