using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using XInputDotNetPure;
using Button.Utils;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    // [HideInInspector]
    public GameObject projectileLeft;
    [HideInInspector]
    public GameObject projectileRight;
    //Used to set projectile distance from the player
    [SerializeField, Header("Projectile Settings")]
    private float projectileDistance;
    [SerializeField]
    private float yOffsetProgectile = 1;
    [SerializeField]
    private float MaxFireCooldown = 1;
    [SerializeField]
    private int projectileSpeed = 700;
    [SerializeField, Header("UI Refereneces")]
    private Image LeftUIIcon;
    [SerializeField]
    private Image RightUIIcon;
    [SerializeField]
    private Image interactPopup;
    [SerializeField]
    private Image EquipPopup;
    [SerializeField]
    private TextMeshPro volume;
    private float currentVolume = 10.0f;

    //Hand charge colours
    private Material mat1;
    private Material mat2;

    private float currRTFireCooldown = 0;
    private float currLTFireCooldown = 0;



    private GameObject attachedParticle;
    private Sprite attachedSprite;
    private Collider pickupCollider;
    private bool pickupColliding;

    private Animator animator;

    //Managers
    private SoundManager soundManager;
    private LevelManager levelManager;

    //Bools to chek if the user is firing
    [HideInInspector]
    public bool rightFire = false;
    [HideInInspector]
    public bool leftFire = false;

    private Color leftProjectileColour;
    private Color rightProjectileColour;

    bool playerIndexSet = false;
    PlayerIndex playerIndex = 0;
    public EControllerType controllerType;

    // Start is called before the first frame update
    void Start()
    {
        currRTFireCooldown = MaxFireCooldown;
        currLTFireCooldown = MaxFireCooldown;
        animator = GetComponent<Animator>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        interactPopup.enabled = false;
        EquipPopup.enabled = false;
        projectileLeft = (GameObject)Resources.Load("Player/NoProjectile Variant Resource");
        projectileRight = (GameObject)Resources.Load("Player/NoProjectile Variant Resource");
        mat1 = (Material)Resources.Load("Player/Weapon 1");
        mat2 = (Material)Resources.Load("Player/Weapon 2");

        if (PlayerPrefs.HasKey("Volume")) currentVolume = PlayerPrefs.GetFloat("Volume");
        if (SceneManager.GetActiveScene().name == "Options") volume.text = currentVolume.ToString();
        AudioListener.volume = currentVolume;
    }

    // Update is called once per frame
    void Update()
    {
       

            if (Input.GetAxis(ButtonMapping.GetButton(controllerType, EButtonActions.RightAttack)) > 0 && 
            projectileRight.tag != ("NotEquipped") &&
            Input.GetAxis(ButtonMapping.GetButton(controllerType, EButtonActions.LeftAttack)) > 0 && 
            projectileLeft.tag != ("NotEquipped"))
            {
                AnimationScript.DoubleAttack(animator);
            }
            else
            {

                if (Input.GetAxis(ButtonMapping.GetButton(controllerType, EButtonActions.RightAttack)) > 0 && projectileRight.tag != ("NotEquipped"))
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

                else if (Input.GetAxis(ButtonMapping.GetButton(controllerType, EButtonActions.LeftAttack)) > 0 && projectileLeft.tag != ("NotEquipped"))
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
            }


            if (Input.GetAxis(ButtonMapping.GetButton(controllerType, EButtonActions.LeftEquipt)) < 0 && pickupColliding == true && attachedParticle != null)
            {
                projectileLeft = attachedParticle;
                LeftUIIcon.sprite = attachedSprite;
                Destroy(pickupCollider.gameObject);
                interactPopup.enabled = false;
                EquipPopup.enabled = false;
                attachedParticle = null;
                pickupCollider = null;
                leftProjectileColour = projectileLeft.GetComponent<ParticleSystem>().main.startColor.color;
                if (projectileLeft.tag != ("NotEquipped"))
                {
                    mat2.color = leftProjectileColour;
                }
            }
            else if (Input.GetAxis(ButtonMapping.GetButton(controllerType, EButtonActions.RightEquipt)) > 0 && pickupColliding == true && attachedParticle != null)
            {
                projectileRight = attachedParticle;
                RightUIIcon.sprite = attachedSprite;
                Destroy(pickupCollider.gameObject);
                interactPopup.enabled = false;
                EquipPopup.enabled = false;
                attachedParticle = null;
                pickupCollider = null;

                rightProjectileColour = projectileRight.GetComponent<ParticleSystem>().main.startColor.color;
                if (projectileRight.tag != ("NotEquipped"))
                {
                    mat1.color = rightProjectileColour;
                }
            }
        
        



        currRTFireCooldown -= Time.deltaTime;
        currLTFireCooldown -= Time.deltaTime;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "EnemyProjectile")
    //    {
    //        Vector2 death = new Vector2(
    //            collision.gameObject.GetComponent<Rigidbody>().velocity.x, collision.gameObject.GetComponent<Rigidbody>().velocity.z);
    //        death.Normalize();
    //        GetComponent<AnimationTest>().deathDirection = death;
    //        GetComponent<PlayerHealth>().playerHit();
    //        Destroy(collision.gameObject);
    //    }
    //}


    void OnTriggerStay(Collider Collision)
    {
        if (Collision.gameObject.tag == "PowerPickup")
            interactPopup.enabled = true;
        if (Collision.gameObject.tag == "HealthPickup")
            interactPopup.enabled = true;
        if (Collision.gameObject.tag == "ShieldPickup")
            interactPopup.enabled = true;
        if (Collision.gameObject.tag == "Door")
            interactPopup.enabled = true;
        if (Collision.gameObject.tag == "Portal")
            interactPopup.enabled = true;
        if (Collision.gameObject.tag == "VolumeOption")
            interactPopup.enabled = true;

       
            if (Collision.gameObject.tag == "PowerPickup" && Input.GetKey(ButtonMapping.GetButton(controllerType, EButtonActions.Interact)))
            {
                EquipPopup.enabled = true;
                attachedParticle = Collision.GetComponent<PickupParticleEffect>().ProjectilePickup;
                attachedSprite = Collision.GetComponent<PickupParticleEffect>().ProjectileUIIcon;
                pickupCollider = Collision;
                pickupColliding = true;
            }
            else if (Collision.gameObject.tag == "HealthPickup" && Input.GetKey(ButtonMapping.GetButton(controllerType, EButtonActions.Interact)))
            {
                Health sn = gameObject.GetComponent<Health>();

                if (sn.currentHealth < sn.maxHealth - 1)
                {
                    sn.AddHealth();                    
                    Destroy(Collision.gameObject);
                    interactPopup.enabled = false;
                    GetComponent<PlayerAnimations>().AddUIHeart();
                }
            }
            else if (Collision.gameObject.tag == "ShieldPickup" && Input.GetKey(ButtonMapping.GetButton(controllerType, EButtonActions.Interact)))
            {
                PlayerAnimations sn = gameObject.GetComponent<PlayerAnimations>();

                if (sn.currentShield < 2)
                {
                    //  sn.addShield();
                    Destroy(Collision.gameObject);
                    interactPopup.enabled = false;
                }
            }
            else if (Collision.gameObject.tag == "Door" && Input.GetKey(ButtonMapping.GetButton(controllerType, EButtonActions.Interact)))
            {
                openDoor sn = Collision.gameObject.GetComponent<openDoor>();
                sn.openTheDoor();
            }
            else if (Collision.gameObject.tag == "Portal" && Input.GetKey(ButtonMapping.GetButton(controllerType, EButtonActions.Interact)))
            {
                if (Collision.gameObject.name == "QuitPortal")
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                    Application.Quit();
                }
                else if (Collision.gameObject.name == "PlayPortal")
                {
                    levelManager.LoadLevel((int)LevelEnum.Level1);
                }
                else if (Collision.gameObject.name == "MenuPortal")
                {
                    levelManager.LoadLevel((int)LevelEnum.MainMenu);
                }
                else if (Collision.gameObject.name == "OptionsPortal")
                {
                    levelManager.LoadLevel((int)LevelEnum.Options);
                }
            }
            else if (Collision.gameObject.tag == "VolumeOption" && Input.GetKey(ButtonMapping.GetButton(controllerType, EButtonActions.Interact)))
            {
                if (Collision.gameObject.name == "VolumeUp")
                {
                    if (currentVolume < 10)
                    {
                        currentVolume++;
                    }
                }
                else if (Collision.gameObject.name == "VolumeDown")
                {
                    if (currentVolume > 0)
                    {
                        currentVolume--;
                    }
                }

                PlayerPrefs.SetFloat("Volume", currentVolume);
                volume.text = currentVolume.ToString();
                AudioListener.volume = currentVolume;
            }
        
        

    }
    void OnTriggerExit(Collider other)
    {
        interactPopup.enabled = false;
        EquipPopup.enabled = false;
        pickupColliding = false;
        attachedParticle = null;
        pickupCollider = null;
    }


    public void RightFireToggle()
    {

        rightFire = false;
    }

    public void LeftFireToggle()
    {

        leftFire = false;
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





}
