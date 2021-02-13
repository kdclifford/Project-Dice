using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPlayer : MonoBehaviour
{
    private GameObject player;
    public float orbitRadius = 1;
    public float orbitSpeed = 1;
    public float orbitHeight = 1;
    public float followSpeed = 1;
    public float followDistance = 1;
    Vector3 desiredPosition;
    Vector3 tempPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position + new Vector3(0, orbitHeight, orbitRadius);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            transform.position += new Vector3(0, orbitHeight, 0);
        }
       // transform.position += new Vector3(0, orbitHeight, 0);

        if (Vector3.Distance(player.transform.position, transform.position) < followDistance)
        {
            transform.RotateAround(player.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
            desiredPosition = (transform.position - player.transform.position).normalized * orbitRadius + player.transform.position;
        }
        else
        {
            desiredPosition = player.transform.position + new Vector3(0, orbitHeight, 0);
        }
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition , Time.deltaTime * followSpeed);
    }
}
