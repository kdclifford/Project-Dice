using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
using UnityEngine.SocialPlatforms;

public class EnemyFire : MonoBehaviour
{
    [SerializeField, Header("Object Referneces")]
    private GameObject projectile;
    [SerializeField]
    private RandomColour meshRenderer;
    [SerializeField, Header("Projectile offsets"), Tooltip("Controls how high the projectile spawns")]
    private float yOffsetProgectile = 1;
    [SerializeField, Tooltip("Controls how far forward the projectile spawns")]
    private float zOffsetProgectile = 1;
    [SerializeField, Header("Projectile Settings")]
    private float fireRate = 1;
    [SerializeField]
    private float projectileSpeed = 10;

    private float fireCooldown = 0;
    private int playerHealth;
    private Animator animator;
    private SoundManager soundManager;


    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = fireRate;
        animator = GetComponent<Animator>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;
    }

    //Universal call to make enemies
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

    //Spider Attack
    public void Fire()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>() != null)
        {
            SpawnBullet(0, 0);
            SpawnBullet(10, 45);
            SpawnBullet(-10, -45);
        }
    }

    //Wisard attacks
    public void WizardFire()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>() != null)
        {
            SpawnBullet(0, 0);
        }
    }

    //Changes the staff colour while shooting
    public void StaffAttack()
    {
        meshRenderer.lerpOn = true;
    }

    //Spawns projectiles
    void SpawnBullet(float angleMin, float angleMAx)
    {
        Vector3 forward = transform.forward;
        forward.y = 0;

        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y + Random.Range(angleMin, angleMAx), 90);

        Vector3 firePos = transform.position;
        firePos.y += yOffsetProgectile;
        firePos += transform.forward * zOffsetProgectile;

        //playerRot.eulerAngles += 45;
        GameObject bullet = Instantiate(projectile, firePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileSpeed);
        bullet.tag = "EnemyProjectile";
        soundManager.Play(projectile.name, bullet);
        fireCooldown = fireRate;
    }
}
