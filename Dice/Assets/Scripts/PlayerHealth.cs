using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int currentHearts;
    private int maxHearts;
    public static int maxShield = 2;
    public int currentShield = 0;
    [SerializeField]
    public GameObject[] HPUIIcons;
    [SerializeField]
    public GameObject[] ShieldUIIcons;


    private Animator animator;

    private float velocity;
    private Vector2 leftStickInputAxis;

    private int idle = 0;
    private bool idleAnimation = false;

    public Vector2 deathDirection;
    private Vector2 newDeathDirection;

    float triggerPress = 0;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        maxHearts = (int)GetComponent<Health>().maxHealth;
        for(int i = 0; i < ShieldUIIcons.Length; i++)
        {
            ShieldUIIcons[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHearts = (int)GetComponent<Health>().currentHealth;

        if(Input.GetKeyDown(KeyCode.X))
        {
            playerHit();
        }        
   
        
        triggerPress = 0;

        if (currentHearts >= 0)
        {

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

                if (GetComponent<ProjectileFire>().projectileRight.tag != ("NotEquipped") && Input.GetMouseButton(1))
                {
                    triggerPress += 1;
                }

                if (GetComponent<ProjectileFire>().projectileLeft.tag != ("NotEquipped") && Input.GetMouseButton(0))
                {
                    triggerPress += 1;
                }
            }


            
                animator.SetFloat("TriggerPress", triggerPress);
            


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
            if (!isDead)
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
        }

        animator.SetFloat("Health", currentHearts);

    }

    public void playerHit()
    {
        if(currentShield > 0)
        {
            ShieldUIIcons[currentShield].gameObject.SetActive(false);
            currentShield--;
            return;
        }
        
        if (currentHearts > -1)
        {
            HPUIIcons[currentHearts].gameObject.GetComponent<Animator>().SetInteger("LoseHeart", 1);
            currentHearts--;
        }
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
}
