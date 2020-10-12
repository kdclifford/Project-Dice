using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    [SerializeField]
    private GameObject[] projectile;
    [SerializeField]
    private float MaxFireCooldown = 1;
    [SerializeField]
    private int projectileSpeed = 10000;

    private float currRTFireCooldown = 0;
    private int currRTProjectile = 1;

    private float currLTFireCooldown = 0;
    private int currLTProjectile = 1;

    // Start is called before the first frame update
    void Start()
    {
        currRTFireCooldown = MaxFireCooldown;
        currLTFireCooldown = MaxFireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RTrigger") > 0)
        {
            if (currRTFireCooldown <= 0 && currRTProjectile > 0)
            {
                Quaternion playerRot = Quaternion.identity;
                playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

                Vector3 RightFirePos = transform.position; RightFirePos.x += 0.4f;
                GameObject bullet = Instantiate(projectile[currRTProjectile], RightFirePos, playerRot) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

                currRTFireCooldown = MaxFireCooldown;
            }
        }

        if (Input.GetAxis("LTrigger") > 0)
        {
            if (currLTFireCooldown <= 0 && currLTProjectile > 0)
            {
                Quaternion playerRot = Quaternion.identity;
                playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

                Vector3 LeftFirePos = transform.position; LeftFirePos.x -= 0.4f;
                GameObject bullet = Instantiate(projectile[currLTProjectile], LeftFirePos, playerRot) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

                currLTFireCooldown = MaxFireCooldown;
            }
        }

        currRTFireCooldown -= Time.deltaTime;
        currLTFireCooldown -= Time.deltaTime;
    }
}
