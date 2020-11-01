using System.Collections;
using System.Collections.Generic;
using AnimationFunctions.Utils;
using UnityEngine.UI;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    //[SerializeField]
    public GameObject projectile;


    [SerializeField]
    private float MaxFireCooldown = 1;
    [SerializeField]
    private int projectileSpeed = 500;


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
        Quaternion playerRot = Quaternion.identity;
        playerRot.eulerAngles = new Vector3(0, transform.eulerAngles.y, 90);

        Vector3 LeftFirePos = transform.position;// LeftFirePos.x -= 0.4f;
        LeftFirePos.y += yOffsetProgectile;

        GameObject bullet = Instantiate(projectile, LeftFirePos, playerRot) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * projectileSpeed);
        bullet.tag = "EnemyProjectile";
        soundManager.Play(projectile.name, bullet);

        fireCooldown = MaxFireCooldown;
    }


}
