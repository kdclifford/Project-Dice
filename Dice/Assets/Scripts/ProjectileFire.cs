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
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PowerPickup" && Input.GetKey(KeyCode.JoystickButton0))
        {
            Debug.Log("Working");
        }
    }
}
