using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{ 
    [SerializeField]
    private float MaxTimer = 4;
    private float curTimer;
    Quaternion projectileRotation;
    Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        curTimer = MaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (curTimer > 0)
        {
            curTimer -= Time.deltaTime;
        }
        else
        {


        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            SpellList.instance.spells[(int)GetComponent<SpellIndex>().spellIndex].death(transform.position, gameObject, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            //rigidbody.velocity = Vector3.zero;
            SpellList.instance.spells[(int)GetComponent<SpellIndex>().spellIndex].death(transform.position, gameObject, transform.rotation);
            Destroy(gameObject);
        }
    }


    public void setTimer(float timerTime)
    {
        MaxTimer = timerTime;
        curTimer = timerTime;

    }

}
