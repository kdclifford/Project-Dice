using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.UI;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    [SerializeField]
    private GameObject projectileLeft;
    [SerializeField]
    private GameObject projectileRight;
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

    private float currRTFireCooldown = 0;
    private float currLTFireCooldown = 0;

    private GameObject attachedParticle;
    private Sprite attachedSprite;
    Collider pickupCollider;
    private bool pickupColliding;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SoundManager soundManager;

    //Bools to chek if the user is firing
    private bool rightFire = false;
    private bool leftFire = false;



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
        if (Input.GetAxis("RTrigger") > 0 && projectileRight.tag != ("NotEquipped"))
        {
            if (currRTFireCooldown <= 0)
            {
                if(!rightFire)
                {
                rightFire = true;
                }
                AnimationScript.RightAttack(animator);                
            }
        }
        
        if (Input.GetAxis("LTrigger") > 0 && projectileLeft.tag != ("NotEquipped"))
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

        if (!rightFire && !leftFire)
        {
            AnimationScript.Idle4(animator);
        }

        currRTFireCooldown -= Time.deltaTime;
        currLTFireCooldown -= Time.deltaTime;

        if (Input.GetAxis("HorizontalDpad") < 0 && pickupColliding == true && attachedParticle != null)
        {
            projectileLeft = attachedParticle;
            LeftUIIcon.sprite = attachedSprite;
            Destroy(pickupCollider.gameObject);
            interactPopup.enabled = false;
            EquipPopup.enabled = false;
            attachedParticle = null;
            pickupCollider = null;
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
        }

    }


    void OnTriggerStay(Collider Collision)
    {
        interactPopup.enabled = true;

        if (Collision.gameObject.tag == "PowerPickup" && Input.GetKey(KeyCode.JoystickButton0))
        {
            EquipPopup.enabled = true;
            attachedParticle = Collision.GetComponent<PickupParticleEffect>().ProjectilePickup;
            attachedSprite = Collision.GetComponent<PickupParticleEffect>().ProjectileUIIcon;
            pickupCollider = Collision;
            pickupColliding = true;
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

        Vector3 RightFirePos = transform.position; RightFirePos.x += 0.4f;
        GameObject bullet = Instantiate(projectileRight, RightFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        soundManager.Play(projectileRight.name, bullet);

        currRTFireCooldown = MaxFireCooldown;
    }

  public void LeftFire()
    {
        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 LeftFirePos = transform.position; LeftFirePos.x -= 0.4f;
        GameObject bullet = Instantiate(projectileLeft, LeftFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

        
        soundManager.Play(projectileLeft.name, bullet);

        currLTFireCooldown = MaxFireCooldown;
    }

}
