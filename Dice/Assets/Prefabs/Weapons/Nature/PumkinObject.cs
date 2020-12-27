using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumkinObject : MonoBehaviour
{

    public float timer = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        //Start the timer to deley
        
        //Set of partcile system

        //Avtive Collider 


    }

    // Update is called once per frame
    void Update()
    {
        var finished = false;
        timer -= Time.deltaTime;
        if (timer < 0 && !finished)
        {
            this.GetComponent<ParticleSystem>().Play();
            this.GetComponent<SphereCollider>().enabled = true;
            finished = true;
        }
        if(timer <-0.25)
        {
            GameObject.Destroy(this);
        }
    }
}
