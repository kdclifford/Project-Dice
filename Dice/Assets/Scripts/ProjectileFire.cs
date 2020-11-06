using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.UI;
using XInputDotNetPure;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    //[SerializeField]
    public GameObject projectileLeft;
    //[SerializeField]
    public GameObject projectileRight;
    [SerializeField]
    private float MaxFireCooldown = 1;
    [SerializeField]
    private int projectileSpeed = 500;
    [SerializeField]
    private Image LeftUIIcon;
    [SerializeField]
    private Image RightUIIcon;
    [SerializeField]
    private Image interactPopup;
    [SerializeField]
    private Image EquipPopup;
    [SerializeField]
    private Material mat1;
    [SerializeField]
    private Material mat2;

    private float currRTFireCooldown = 0;
    private float currLTFireCooldown = 0;

    //Used to set projectile distance from the player
    [SerializeField]
    private float projectileDistance;

    private GameObject attachedParticle;
    private Sprite attachedSprite;
    private Collider pickupCollider;
    private bool pickupColliding;

    private Animator animator;   
    private SoundManager soundManager;

    //Bools to chek if the user is firing
    [HideInInspector]
    public bool rightFire = false;
    [HideInInspector]
    public bool leftFire = false;

    private Color leftProjectileColour;
    private Color rightProjectileColour;
    public float yOffsetProgectile = 1;

    bool playerIndexSet = false;
    PlayerIndex playerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currRTFireCooldown = MaxFireCooldown;
        currLTFireCooldown = MaxFireCooldown;
        animator = GetComponent<Animator>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        interactPopup.enabled = false;
        EquipPopup.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GetComponent<MovementScript>().controller)
        {

            if (Input.GetAxis("RTrigger") > 0 && projectileRight.tag != ("NotEquipped") && Input.GetAxis("LTrigger") > 0 && projectileLeft.tag != ("NotEquipped"))
            {
                AnimationScript.DoubleAttack(animator);
            }
            else
            {

                if (Input.GetAxis("RTrigger") > 0 && projectileRight.tag != ("NotEquipped"))
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

                else if (Input.GetAxis("LTrigger") > 0 && projectileLeft.tag != ("NotEquipped"))
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


            if (Input.GetAxis("HorizontalDpad") < 0 && pickupColliding == true && attachedParticle != null)
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
        else if (Input.GetAxis("HorizontalDpad") > 0 && pickupColliding == true && attachedParticle != null)
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
    }
        else
        {
            if (Input.GetMouseButton(1) && projectileRight.tag != ("NotEquipped") && Input.GetMouseButton(0) && projectileLeft.tag != ("NotEquipped"))
            {
                AnimationScript.DoubleAttack(animator);
            }
            else
            {

                if (Input.GetMouseButton(0) && projectileRight.tag != ("NotEquipped"))
                {
                    Debug.Log("Right click");

                    if (currRTFireCooldown <= 0)
                    {
                        if (!rightFire)
                        {
                            rightFire = true;
                        }
                        AnimationScript.RightAttack(animator);
                    }
                }

                if (Input.GetMouseButton(1) && projectileLeft.tag != ("NotEquipped"))
                {
                    Debug.Log("Left click");
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


            if (Input.GetKey(KeyCode.Q) && pickupColliding == true && attachedParticle != null)
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
            else if (Input.GetKey(KeyCode.E) && pickupColliding == true && attachedParticle != null)
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

        if (GetComponent<MovementScript>().controller)
        {
            if (Collision.gameObject.tag == "PowerPickup" && Input.GetKey(KeyCode.JoystickButton0))
            {
                EquipPopup.enabled = true;
                attachedParticle = Collision.GetComponent<PickupParticleEffect>().ProjectilePickup;
                attachedSprite = Collision.GetComponent<PickupParticleEffect>().ProjectileUIIcon;
                pickupCollider = Collision;
                pickupColliding = true;
            }
            else if (Collision.gameObject.tag == "HealthPickup" && Input.GetKey(KeyCode.JoystickButton0))
            {
                PlayerHealth sn = gameObject.GetComponent<PlayerHealth>();

                if(sn.CurrentHearts < 7)
                {
                    sn.addHealth();
                    Destroy(Collision.gameObject);
                    interactPopup.enabled = false;
                }
            }
            else if (Collision.gameObject.tag == "ShieldPickup" && Input.GetKey(KeyCode.JoystickButton0))
            {
                PlayerHealth sn = gameObject.GetComponent<PlayerHealth>();

                if (sn.CurrentShield < 2)
                {
                    sn.addShield();
                    Destroy(Collision.gameObject);
                    interactPopup.enabled = false;
                }
            }
        }
        else
        {
            if (Collision.gameObject.tag == "PowerPickup" && Input.GetKey(KeyCode.Space))
            {
                EquipPopup.enabled = true;
                attachedParticle = Collision.GetComponent<PickupParticleEffect>().ProjectilePickup;
                attachedSprite = Collision.GetComponent<PickupParticleEffect>().ProjectileUIIcon;
                pickupCollider = Collision;
                pickupColliding = true;
            }
            else if (Collision.gameObject.tag == "HealthPickup" && Input.GetKey(KeyCode.Space))
            {
                PlayerHealth sn = gameObject.GetComponent<PlayerHealth>();

                if (sn.CurrentHearts < 7)
                {
                    sn.addHealth();
                    Destroy(Collision.gameObject);
                    interactPopup.enabled = false;
                }
            }
            else if (Collision.gameObject.tag == "ShieldPickup" && Input.GetKey(KeyCode.Space))
            {
                PlayerHealth sn = gameObject.GetComponent<PlayerHealth>();

                if (sn.CurrentShield < 2)
                {
                    sn.addShield();
                    Destroy(Collision.gameObject);
                    interactPopup.enabled = false;
                }
            }
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

    public void VibrationOff()
    {
        GamePad.SetVibration(playerIndex, 0, 0);
    }
}
