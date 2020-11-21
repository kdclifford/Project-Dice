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

  

    private int idle = 0;
    private bool idleAnimation = false;

    public Vector2 deathDirection;
    private Vector2 newDeathDirection;

    bool playerIndexSet = false;

    bool isDead = false;
    PlayerController playerController;
    GameSettings gameSettings;

    PlayerIndex playerIndex = 0;

    private bool doubleAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        gameSettings = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSettings>();
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {  
        if (health.GetHealth()  > 0)
        {
            Move();

            if (!idleAnimation && !playerController.rightFire && !playerController.leftFire)
            {

                Idle();
            }
        }    
    }

    public void DeathAnimation()
    {                    
            Destroy(GetComponent<PlayerController>());
        
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
        }
    

    private void Move()
    {
        animator.SetFloat("XMove", playerController.leftStickInputAxis.x);
        animator.SetFloat("YMove", playerController.leftStickInputAxis.y);
        animator.SetFloat("Velocity", playerController.velocity);
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
