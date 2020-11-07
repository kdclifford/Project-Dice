using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
using UnityEngine.SocialPlatforms;

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

    private int playerHealth;

    private Animator animator;
    private SoundManager soundManager;

    public float yOffsetProgectile = 1;
    public float zOffsetProgectile = 1;

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
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().CurrentHearts;
    }

    public void EnemyShoot()
    {
        if (projectile.tag != ("NotEquipped"))
        {
            if (playerHealth >= 0 && fireCooldown <= 0)
            {
                AnimationScript.SpiderAttack(animator);
            }
            else
            {
                AnimationScript.StopAttack(animator);
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
        SpawnBullet(0, 0);
        SpawnBullet(10, 45);
        SpawnBullet(-10, -45);
    }

    public void WizardFire()
    {
        SpawnBullet(0, 0);
    }

    void SpawnBullet(float angleMin, float angleMAx)
    {
        Vector3 forward = transform.forward;
        forward.y = 0;

        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y + Random.Range(angleMin, angleMAx), 90);

        Vector3 firePos = transform.position;// LeftFirePos.x -= 0.4f;
        firePos.y += yOffsetProgectile;
        firePos += transform.forward * zOffsetProgectile;

        //playerRot.eulerAngles += 45;

        GameObject bullet = Instantiate(projectile, firePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed);
        bullet.tag = "EnemyProjectile";
        soundManager.Play(projectile.name, bullet);
        fireCooldown = MaxFireCooldown;
    }


}
