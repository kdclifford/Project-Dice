using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    private Vector3 _velocity = Vector3.zero;
    public float min;
    public float max;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");       
        GetComponent<Rigidbody>().velocity = Vector3.zero;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        GetComponent<Rigidbody>().velocity = _velocity;
       Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3(0, 1, 0)
            , ref _velocity, Time.deltaTime * Random.Range(min, max));


       // transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, 1, 0), 10 * Time.deltaTime);

    }


}
