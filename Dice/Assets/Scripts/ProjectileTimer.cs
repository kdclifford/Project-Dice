using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTimer : MonoBehaviour
{
    [SerializeField]
    private float MaxTimer = 4;
    private float curTimer;

    // Start is called before the first frame update
    void Start()
    {
        curTimer = MaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(curTimer > 0)
        {
            curTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
