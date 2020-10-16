using System.Collections;
using System.Collections.Generic;
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
    private GameObject PlayerPointer;

    private float currRTFireCooldown = 0;
    private float currLTFireCooldown = 0;

    private GameObject attachedParticle;
    Collider pickupCollider;
    private bool pickupColliding;

    // Start is called before the first frame update
    void Start()
    {
        currRTFireCooldown = MaxFireCooldown;
        currLTFireCooldown = MaxFireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RTrigger") > 0 && projectileRight.tag != ("NotEquipped"))
        {
            if (currRTFireCooldown <= 0)
            {
                Quaternion playerRot = Quaternion.identity;
                playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

                Vector3 RightFirePos = transform.position; RightFirePos.x += 0.4f;
                GameObject bullet = Instantiate(projectileRight, RightFirePos, playerRot) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

                currRTFireCooldown = MaxFireCooldown;
            }
        }

        if (Input.GetAxis("LTrigger") > 0 && projectileLeft.tag != ("NotEquipped"))
        {
            if (currLTFireCooldown <= 0)
            {
                Quaternion playerRot = Quaternion.identity;
                playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

                Vector3 LeftFirePos = transform.position; LeftFirePos.x -= 0.4f;
                GameObject bullet = Instantiate(projectileLeft, LeftFirePos, playerRot) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

                currLTFireCooldown = MaxFireCooldown;
            }
        }

        currRTFireCooldown -= Time.deltaTime;
        currLTFireCooldown -= Time.deltaTime;

        if (Input.GetAxis("HorizontalDpad") < 0 && pickupColliding == true && attachedParticle != null)
        {
            projectileLeft = attachedParticle;
            Destroy(pickupCollider.gameObject);
            attachedParticle = null;
            pickupCollider = null;
        }
        else if (Input.GetAxis("HorizontalDpad") > 0 && pickupColliding == true)
        {
            projectileRight = attachedParticle;
            Destroy(pickupCollider.gameObject);
            attachedParticle = null;
            pickupCollider = null;
        }
    }


    void OnTriggerStay(Collider Collision)
    {
        if (Collision.gameObject.tag == "PowerPickup" && Input.GetKey(KeyCode.JoystickButton0))
        {
            attachedParticle = Collision.GetComponent<PickupParticleEffect>().ProjectilePickup;
            pickupCollider = Collision;
            pickupColliding = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        pickupColliding = false;
        attachedParticle = null;
        pickupCollider = null;
    }
}
