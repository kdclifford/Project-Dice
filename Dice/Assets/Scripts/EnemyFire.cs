using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

public class EnemyFire : MonoBehaviour
{
    //[SerializeField]
    public GameObject projectile;


    [SerializeField]
    private float MaxFireCooldown = 1;
    [SerializeField]
    private float projectileSpeed = 10;


    private float fireCooldown = 0;

    //Used to set projectile distance from the player
    [SerializeField]
    private float projectileDistance;

    private GameObject attachedParticle;
    private Sprite attachedSprite;

    private Animator animator;
    private SoundManager soundManager;

    public float yOffsetProgectile = 1;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = MaxFireCooldown;
        animator = GetComponent<Animator>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;
    }

    public void EnemyShoot()
    {
        if (projectile.tag != ("NotEquipped"))
        {
            if (fireCooldown <= 0)
            {

                AnimationScript.RightAttack(animator);
            }
        }
    }






    void OnTriggerStay(Collider Collision)
    {    
       
    }
    void OnTriggerExit(Collider other)
    {

    }

    public void Fire()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;

        Vector3 sideVector = new Vector3(Random.Range(0.5f, 1), 0, Random.Range(0.5f, 1));
        sideVector.Normalize();
        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 MiddleFirePos = transform.position;// LeftFirePos.x -= 0.4f;
        MiddleFirePos.y += yOffsetProgectile;

        GameObject bullet = Instantiate(projectile, MiddleFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce((forward + sideVector) * projectileSpeed);
        bullet.tag = "EnemyProjectile";
        soundManager.Play(projectile.name, bullet);

        playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 LeftFirePos = transform.position;// LeftFirePos.x -= 0.4f;
        LeftFirePos.y += yOffsetProgectile;
        sideVector.x = -sideVector.x;

        GameObject bullet2 = Instantiate(projectile, LeftFirePos, playerRot) as GameObject;
        bullet2.GetComponent<Rigidbody>().AddForce((forward + sideVector) * projectileSpeed);
        bullet2.tag = "EnemyProjectile";
        soundManager.Play(projectile.name, bullet2);

        playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 RightFirePos = transform.position;// LeftFirePos.x -= 0.4f;
        MiddleFirePos.y += yOffsetProgectile;

        GameObject bullet3 = Instantiate(projectile, MiddleFirePos, playerRot) as GameObject;
        bullet3.GetComponent<Rigidbody>().AddForce(forward * projectileSpeed);
        bullet3.tag = "EnemyProjectile";
        soundManager.Play(projectile.name, bullet3);



        fireCooldown = MaxFireCooldown;
    }


}
