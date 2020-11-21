using System.Collections;
using System.Collections.Generic;
using Button.Utils;
using AnimationFunctions.Utils;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    UIManager uIManager;
    GameSettings gameSettings;
    private Animator animator;
    private SoundManager soundManager;
  //  [HideInInspector]
    public GameObject projectileLeft;
  //  [HideInInspector]
    public GameObject projectileRight;
    private Material leftColour;
    private Material rightColour;
    private float triggerPress = 0;

    private float currRTFireCooldown = 0;
    private float currLTFireCooldown = 0;
    [SerializeField]
    private float MaxFireCooldown = 1;

    //Used to set projectile distance from the player
    [SerializeField, Header("Projectile Settings")]
    private float projectileDistance;
    [SerializeField]
    private float yOffsetProgectile = 1;

    [SerializeField]
    private int projectileSpeed = 700;

    [HideInInspector]
    public bool rightFire = false;
    [HideInInspector]
    public bool leftFire = false;

    public float velocity;
    public Vector2 leftStickInputAxis;


    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        gameSettings = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSettings>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        animator = GetComponent<Animator>();
        rightColour = (Material)Resources.Load("Player/Weapon 1");
        leftColour = (Material)Resources.Load("Player/Weapon 2");

        currRTFireCooldown = MaxFireCooldown;
        currLTFireCooldown = MaxFireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        triggerPress = 0;

        leftStickInputAxis.x = ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.HorizontalMovement, transform.position);
        leftStickInputAxis.y = ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.VerticalMovement, transform.position);

        if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.RightAttack) &&
           projectileRight != null &&
           ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.LeftAttack) &&
           projectileLeft != null)
        {
            AnimationScript.DoubleAttack(animator);
          
        }
        else
        {

            if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.RightAttack) && projectileRight != null)
            {
                if (currRTFireCooldown <= 0)
                {
                    if (!rightFire)
                    {
                        rightFire = true;
                    }
                   
                    AnimationScript.RightAttack(animator);
                }
            }

            else if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.LeftAttack) && projectileLeft != null)
            {
                if (currLTFireCooldown <= 0)
                {
                    if (!leftFire)
                    {
                        leftFire = true;
                    }
                    
                    AnimationScript.LeftAttack(animator);
                }
            }
            else
            {
                AnimationScript.StopAttack(animator);
            }

        }




        



        velocity = Mathf.Abs(leftStickInputAxis.x) + Mathf.Abs(leftStickInputAxis.y);
        leftStickInputAxis = AnimationScript.CurrentDirection(leftStickInputAxis, gameObject);

        leftStickInputAxis.x *= velocity;
        leftStickInputAxis.y *= velocity;



        currRTFireCooldown -= Time.deltaTime;
        currLTFireCooldown -= Time.deltaTime;
    }


    void OnTriggerStay(Collider Collision)
    {
        if (Collision.gameObject.layer == LayerMask.NameToLayer("PickUp"))            
        {
            uIManager.ShowInteractPopUp();
            
        }
       

        if (Collision.gameObject.layer == LayerMask.NameToLayer("Spell"))
        {
            uIManager.ShowEquipPopUp();
        
            if(ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.LeftEquipt))
            {
                uIManager.ShowLeftSpell(Collision.GetComponent<SpriteRenderer>().sprite);
                projectileLeft = (GameObject)Resources.Load("Player/" + Collision.name);
                Destroy(Collision.gameObject);
                if (projectileLeft != null)
                {
                    leftColour.color = projectileLeft.GetComponent<ParticleSystem>().main.startColor.color;
                }
            }
            else if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.RightEquipt))
            {
                uIManager.ShowRightSpell(Collision.GetComponent<SpriteRenderer>().sprite);
                projectileRight = (GameObject)Resources.Load("Player/" + Collision.name);
                Destroy(Collision.gameObject);
                uIManager.HideEquipPopUp();
                if (projectileRight != null)
                {                    
                    rightColour.color = projectileRight.GetComponent<ParticleSystem>().main.startColor.color;
                }
            }
        }
        else if (Collision.gameObject.tag == "HealthPickup" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            Health sn = gameObject.GetComponent<Health>();

            if (sn.GetHealth() < sn.maxHealth - 1)
            {
                sn.AddHealth();
                Destroy(Collision.gameObject);               
                uIManager.AddUIHeart();
            }
        }
        else if (Collision.gameObject.tag == "ShieldPickup" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            Health sn = gameObject.GetComponent<Health>();

            if (sn.GetShield() < sn.maxShields)
            {
                uIManager.AddUIShield();
                sn.AddShield();
                Destroy(Collision.gameObject);
            }
        }
        else if (Collision.gameObject.tag == "Door" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            openDoor sn = Collision.gameObject.GetComponent<openDoor>();
            sn.openTheDoor();
        }
        else if (Collision.gameObject.tag == "Portal" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            ScenePortal sn = Collision.gameObject.GetComponent<ScenePortal>();
            sn.TeleportToScene();
        }
        
    



    }

    private void OnTriggerExit(Collider other)
    {
        uIManager.HideEquipPopUp();
        uIManager.HideInteractPopUp();
    }



    public void RightFire()
    {
        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 RightFirePos = transform.position + (transform.right * projectileDistance);// RightFirePos.x += 0.4f;
        RightFirePos.y += yOffsetProgectile;

        GameObject bullet = Instantiate(projectileRight, RightFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        soundManager.Play(projectileRight.name, bullet);

        currRTFireCooldown = MaxFireCooldown;
    }

    public void LeftFire()
    {
        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 LeftFirePos = transform.position + (transform.right * -projectileDistance);// LeftFirePos.x -= 0.4f;
        LeftFirePos.y += yOffsetProgectile;

        GameObject bullet = Instantiate(projectileLeft, LeftFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);


        soundManager.Play(projectileLeft.name, bullet);

        currLTFireCooldown = MaxFireCooldown;
    }

    public void MiddleFire()
    {
        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 LeftFirePos = transform.position;// LeftFirePos.x -= 0.4f;
        LeftFirePos.y += yOffsetProgectile;

        GameObject bullet = Instantiate(projectileLeft, LeftFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        soundManager.Play(projectileLeft.name, bullet);

        currLTFireCooldown = MaxFireCooldown;

        Vector3 RightFirePos = transform.position;// RightFirePos.x += 0.4f;
        RightFirePos.y += yOffsetProgectile;

        bullet = Instantiate(projectileRight, RightFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        soundManager.Play(projectileRight.name, bullet);

        currRTFireCooldown = MaxFireCooldown;


    }


    public void RightFireToggle()
    {

        rightFire = false;
    }

    public void LeftFireToggle()
    {

        leftFire = false;
    }




  
}
