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
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.LookAt(player.transform);
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3(0, 1, 0)
            , ref _velocity, Time.deltaTime * Random.Range(min, max));
    }


}
