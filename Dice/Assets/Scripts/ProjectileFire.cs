using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float MaxFireCooldown = 1;
    [SerializeField]
    private int projectileSpeed = 500;

    private float currFireCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        currFireCooldown = MaxFireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("RTrigger") > 0)
        {
            if (currFireCooldown <= 0)
            {
                Quaternion playerRot = Quaternion.identity;
                playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

                GameObject bullet = Instantiate(projectile, transform.position, playerRot) as GameObject;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);

                currFireCooldown = MaxFireCooldown;
            }
        }
        currFireCooldown -= Time.deltaTime;
    }
}
