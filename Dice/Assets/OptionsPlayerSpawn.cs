using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPlayerSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.gameObject.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
