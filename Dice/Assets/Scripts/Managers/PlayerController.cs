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

    //[HideInInspector]
    public int rightSpell = -1;
   // [HideInInspector]
    public int leftSpell = -1;



    //[HideInInspector]
    //public GameObject projectileLeft;
    //[HideInInspector]
    //public GameObject projectileRight;
    private Material leftColour;
    private Material rightColour;

    [SerializeField]
    private float currRTFireCooldown = 0;
    [SerializeField]
    private float currLTFireCooldown = 0;
    [SerializeField]
    private float MaxRTFireCooldown = 1;
    [SerializeField]
    private float MaxLTFireCooldown = 1;

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

    private bool DoorTriggered = false;
    private GameObject DoorObj;
    private bool DungeonDoorTriggered = false;
    private GameObject DungeonDoorObj;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = UIManager.instance;
        uIManager.HideInteractPopUp();
        uIManager.HideEquipPopUp();
        gameSettings = GameSettings.instance;
        soundManager = SoundManager.instance;
        animator = GetComponent<Animator>();
        rightColour = (Material)Resources.Load("Player/Weapon 1");
        leftColour = (Material)Resources.Load("Player/Weapon 2");
        playerAnimations = GetComponent<PlayerAnimations>();

        leftSpell = -1;
        rightSpell = -1;

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

        if (DoorTriggered == true && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            openDoor Door = DoorObj.GetComponent<openDoor>();
            Door.openTheDoor();
            DoorTriggered = false;
        }
        if (DungeonDoorTriggered == true && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            CDungonDoor sn = DungeonDoorObj.GetComponent<CDungonDoor>();
            sn.openTheDoor();
            DungeonDoorTriggered = false;
        }


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
           rightSpell != -1 &&
           ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.LeftAttack) &&
           leftSpell != -1)
        {
            AnimationScript.DoubleAttack(animator);
        }
        else
        {
            if (currRTFireCooldown <= 0 && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.RightAttack) && rightSpell != -1)
            {  
                currRTFireCooldown = MaxRTFireCooldown;
                AnimationScript.RightAttack(animator);
            }
            else if (currLTFireCooldown <= 0 && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.LeftAttack) && leftSpell != -1)
            {
                currLTFireCooldown = MaxLTFireCooldown;
                AnimationScript.LeftAttack(animator);
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

        if (velocity <= 0)
        {

            playerAnimations.Idle();
        }


        currRTFireCooldown -= Time.deltaTime;
        currLTFireCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NextLevel")
        {
            LevelManager.instance.NextLevel();
        }
    }


    void OnTriggerStay(Collider Collision)
    {
        if (Collision.gameObject.layer == LayerMask.NameToLayer("PickUp") || Collision.gameObject.layer == LayerMask.NameToLayer("Door"))            
        {
            uIManager.ShowInteractPopUp();
        }

        if (Collision.gameObject.layer == LayerMask.NameToLayer("Spell") || Collision.gameObject.layer == LayerMask.NameToLayer("PickUp"))
        {
            if(Collision.gameObject.tag != "VolumeOption") Collision.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (Collision.gameObject.layer == LayerMask.NameToLayer("Spell"))
        {
            uIManager.ShowEquipPopUp();
        
            if(ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.LeftEquipt))
            {
                //Set the spell
                leftSpell = (int)Collision.gameObject.GetComponent<ProjectileType>().spellIndex;

                var baseSpell = SpellList.instance.spells[leftSpell];

                uIManager.ShowLeftSpell(baseSpell.UILogo);

                Destroy(Collision.gameObject);
                uIManager.HideEquipPopUp();
                Collision.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                SetCooldownTimer(ref MaxLTFireCooldown, leftSpell);
                //if (projectileLeft != null)
                //{
                leftColour.color = baseSpell.castingColour;
                //}
            }
            else if (ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.RightEquipt))
            {

                rightSpell = (int)Collision.gameObject.GetComponent<ProjectileType>().spellIndex;

                var baseSpell = SpellList.instance.spells[rightSpell];

                uIManager.ShowRightSpell(baseSpell.UILogo);

                Destroy(Collision.gameObject);
                uIManager.HideEquipPopUp();
                Collision.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                SetCooldownTimer(ref MaxRTFireCooldown, rightSpell);
                //if (projectileRight != null)
                //{
                rightColour.color = baseSpell.castingColour;

                //}
            }
        }
        else if (Collision.gameObject.tag == "HealthPickup" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            Health sn = gameObject.GetComponent<Health>();

            if (sn.GetHealth() < sn.maxHealth)
            {
                sn.AddHealth();
                Destroy(Collision.gameObject);
                Collision.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else if (Collision.gameObject.tag == "ShieldPickup" && ButtonMapping.GetButton(gameSettings.controllerType, EButtonActions.Interact))
        {
            Health sn = gameObject.GetComponent<Health>();

            if (sn.GetShield() < sn.maxShields)
            {
                uIManager.AddUIShield();
                sn.AddShield();
                soundManager.Play(ESoundClipEnum.Shield, gameObject);
                Collision.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                Destroy(Collision.gameObject);
            }
        }
        else if (Collision.gameObject.tag == "VolumeOption")
        {
            volumeObj = Collision.gameObject;
            volumeTriggered = true;
        }
        else if (Collision.gameObject.tag == "Door")
        {
            DoorObj = Collision.gameObject;
            DoorTriggered = true;
        }
        else if (Collision.gameObject.tag == "DungonDoor")
        {
            DungeonDoorObj = Collision.gameObject;
            DungeonDoorTriggered = true;
        }
        else if (Collision.gameObject.tag == "Light")
        {
            Collision.gameObject.GetComponent<ParticleSystem>().Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Spell") || other.gameObject.layer == LayerMask.NameToLayer("PickUp"))
        {
            other.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }

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
        // Cast Spell?

       

        Vector3 rightFirePos = transform.position + (transform.right * projectileDistance);// RightFirePos.x += 0.4f;
        rightFirePos.y += yOffsetProgectile;

        SpellList.instance.spells[rightSpell].CastSpell(rightFirePos, transform.eulerAngles.y, gameObject);
        
    }

    public void LeftFire()
    {

        Debug.Log(SpellList.instance.spells[(int)leftSpell].SpellName);
        Vector3 leftFirePos = transform.position + (transform.right * -projectileDistance);// LeftFirePos.x -= 0.4f;
        leftFirePos.y += yOffsetProgectile;

        SpellList.instance.spells[leftSpell].CastSpell(leftFirePos, transform.eulerAngles.y, gameObject);
    }

    public void RightFireToggle()
    {

        rightFire = false;
    }

    public void LeftFireToggle()
    {

        leftFire = false;
    }

    void SetCooldownTimer(ref float timer, int spell)
    {
        timer = SpellList.instance.spells[spell].coolDown;
    }



}
