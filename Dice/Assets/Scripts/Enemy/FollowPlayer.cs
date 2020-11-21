using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script moves projectiles towards a target
public class FollowPlayer : MonoBehaviour
{
    [Tooltip("Speed range of projectile ")]
    public Vector2Int speedRange;
    private GameObject target;
    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");       
        GetComponent<Rigidbody>().velocity = Vector3.zero;        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        GetComponent<Rigidbody>().velocity = _velocity;
       Vector3.SmoothDamp(transform.position, target.transform.position + new Vector3(0, 1, 0)
            , ref _velocity, Time.deltaTime * Random.Range(speedRange.x, speedRange.y));
       // transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, 1, 0), 10 * Time.deltaTime);
    }
}
