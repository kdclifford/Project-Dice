using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{ 
    [SerializeField]
    private float MaxTimer = 4;
    private float curTimer;
    Quaternion projectileRotation;

    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            SpellList.instance.spells[(int)GetComponent<SpellIndex>().spellIndex].death(transform.position, gameObject, transform.rotation);
        }
    }

    public void setTimer(float timerTime)
    {
        MaxTimer = timerTime;
        curTimer = timerTime;

    }

}
