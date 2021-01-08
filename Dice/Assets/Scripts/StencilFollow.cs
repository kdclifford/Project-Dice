using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StencilFollow : MonoBehaviour
{
    public static StencilFollow instance;
    private GameObject player;
   // [SerializeField]
    private Vector3 spherePos = new Vector3(0f, -2f, 0.4f);

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            Vector3 newPos = player.transform.position + spherePos;


            transform.position = newPos;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }




    }
}
