using System.Collections;
using System.Collections.Generic;
using Button.Utils;
using AnimationFunctions.Utils;
using PlayerCollisionCheck.Utils;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private UIManager uIManager;
    private GameSettings gameSettings;
    private Animator animator;
    private SoundManager soundManager;
    private PlayerAnimations playerAnimations;

   [HideInInspector]
    public GameObject projectileLeft;
    [HideInInspector]
    public GameObject projectileRight;
    private Material leftColour;
    private Material rightColour;

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

    [SerializeField]
    private GameObject cameraDummy;

    [SerializeField]
    private float moveSpeed = 10;

    [SerializeField]
    private float rayDist;
    [SerializeField]
    private LayerMask layerMask;

    public Vector3 rayOffset = new Vector3(0, 1.5f, 0);
    public float diagonalOffset = 0.9f;
    public float collisionForce = 1.0f;

    private int globalVolume = 10;
    private int musicVolume = 10;
    private int miscVolume = 10;
    private int projectileVolume = 10;
    private bool volumeTriggered = false;
    private GameObject volumeObj;
    float volumeButton = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIManager>();
        gameSettings = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameSettings>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        animator = GetComponent<Animator>();
        rightColour = (Material)Resources.Load("Player/Weapon 1");
        leftColour = (Material)Resources.Load("Player/Weapon 2");
        playerAnimations = GetComponent<PlayerAnimations>();

        currRTFireCooldown = MaxFireCooldown;
        currLTFireCooldown = MaxFireCooldown;

        if (PlayerPrefs.HasKey("MasterVol")) { globalVolume = PlayerPrefs.GetInt("MasterVol"); }
        if (PlayerPrefs.HasKey("MusicVol")) { musicVolume = PlayerPrefs.GetInt("MusicVol"); }
    }

    // Update is called once per frame
    void Update()
    {
        if (volumeTriggered == true &&  volumeButton < 0 && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            VolumeChange Vol = volumeObj.GetComponent<VolumeChange>();

            if(volumeObj.transform.parent.name == "MasterVol")

            {
                globalVolume = Vol.ChangeVolume(globalVolume);
            }
            else if (volumeObj.transform.parent.name == "MusicVol")
            {
                musicVolume = Vol.ChangeVolume(musicVolume);
            }
            else if (volumeObj.transform.parent.name == "MiscVol")
            {
                miscVolume = Vol.ChangeVolume(miscVolume);
            }
            else if (volumeObj.transform.parent.name == "ProjectileVol")
            {
                projectileVolume = Vol.ChangeVolume(projectileVolume);
            }

            volumeTriggered = false;
            volumeButton = 0.1f;
        }

        volumeButton -= Time.deltaTime;

 
      

        leftStickInputAxis.x = ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.HorizontalMovement, transform.position);
        leftStickInputAxis.y = ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.VerticalMovement, transform.position);


        var angle = Mathf.Atan2(ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.HorizontalFacing, transform.position),
            ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.VerticalFacing, transform.position)) * Mathf.Rad2Deg;

       // horizontalInput = ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.HorizontalMovement, transform.position);
       // verticalInput = ButtonMapping.GetStick(gameSettings.controllerType, EStickMovement.VerticalMovement, transform.position);


        //rigidbody.AddRelativeForce(move);

        if (angle > 1 || angle < -1)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            cameraDummy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }



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
                    //if (!rightFire)
                    //{
                    //    rightFire = true;
                    //}
                   
                    AnimationScript.RightAttack(animator);
                }
            }

            else if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.LeftAttack) && projectileLeft != null)
            {
                if (currLTFireCooldown <= 0)
                {                  
                    
                    AnimationScript.LeftAttack(animator);
                }
            }
            else
            {
                AnimationScript.StopAttack(animator);
            }

        }


        leftStickInputAxis = CollisionCheck.RayCastCollisions(leftStickInputAxis, rayOffset, rayDist, layerMask, collisionForce, transform);


        velocity = Mathf.Abs(leftStickInputAxis.x) + Mathf.Abs(leftStickInputAxis.y);

        Vector3 move = new Vector3(leftStickInputAxis.x, 0f, leftStickInputAxis.y);
        move = move.normalized * Time.deltaTime * moveSpeed;
        transform.Translate(move, Space.World);

        leftStickInputAxis = AnimationScript.CurrentDirection(leftStickInputAxis, gameObject);
        playerAnimations.Move(leftStickInputAxis, velocity);

        //leftStickInputAxis.x *= velocity;
        //leftStickInputAxis.y *= velocity;

        if (velocity <= 0)
        {

            playerAnimations.Idle();
        }


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
                projectileLeft = (GameObject)Resources.Load("Player/" + FindSpell(Collision.GetComponent<ProjectileType>().projectileName));
                Destroy(Collision.gameObject);
                if (projectileLeft != null)
                {
                    leftColour.color = projectileLeft.GetComponent<ParticleSystem>().main.startColor.color;
                }
            }
            else if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.RightEquipt))
            {
                uIManager.ShowRightSpell(Collision.GetComponent<SpriteRenderer>().sprite);
                projectileRight = (GameObject)Resources.Load("Player/" + FindSpell(Collision.GetComponent<ProjectileType>().projectileName));
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
                soundManager.Play("Shield", gameObject);
                Destroy(Collision.gameObject);
            }
        }
        else if (Collision.gameObject.tag == "Door" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            openDoor sn = Collision.gameObject.GetComponent<openDoor>();
            sn.openTheDoor();
        }
        else if (Collision.gameObject.tag == "DungonDoor" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            CDungonDoor sn = Collision.gameObject.GetComponent<CDungonDoor>();
            sn.openTheDoor();
        }
        else if (Collision.gameObject.tag == "Portal" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            ScenePortal sn = Collision.gameObject.GetComponent<ScenePortal>();
            sn.TeleportToScene();
        }
        else if (Collision.gameObject.tag == "VolumeOption")
        {
            volumeObj = Collision.gameObject;
            volumeTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        uIManager.HideEquipPopUp();
        uIManager.HideInteractPopUp();
        volumeTriggered = false;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {

            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    //*******************
    //Animation Functions
    //*******************
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


    string FindSpell(Projectile number)
    {
        switch (number)
        {
            case Projectile.Bubble:
                return "Bubble";
            case Projectile.FireBall:
                return "FireBall";
            case Projectile.Electric:
                return "Electric";


        }
       throw new System.Exception("No Spell Found");
    }
}
