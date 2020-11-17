using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public GameObject teleportMesh;
    public float teleportTimer = 0;
    public float cooldownTimer = 10;
    bool secondPress = false;
    bool moveMesh = false;
    Vector3 meshNewPos;
    public float distance = 4;


    [SerializeField]
    private LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        teleportMesh.transform.position = transform.position;
        teleportMesh.transform.position += transform.forward * 4;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetAxis("Teleport") > 0 && !moveMesh && cooldownTimer < 0)
        //{
        //    if (Vector3.Distance(teleportMesh.transform.position, transform.position) == 0)
        //    {
        //        moveMesh = true;
        //    }

        //    if(!moveMesh)
        //    {
        //        cooldownTimer = 10;
        //        transform.position = teleportMesh.transform.position;
        //        teleportMesh.transform.position = transform.position;
        //    }





        //}

        //if(moveMesh)
        //{
        //    meshNewPos = transform.forward * 4;
        //    teleportMesh.transform.position += meshNewPos * Time.deltaTime;
        //    if(Vector3.Distance(teleportMesh.transform.position, transform.position) > 4)
        //    {
        //       // teleportMesh.transform.position = meshNewPos;
        //        moveMesh = false;
        //    }

        //}
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward * distance, out hit, 5, ~layerMask))
        {
            //Vector3 newPos = new Vector3(transform.position.x - (rayDist - hit.distance), transform.position.y, transform.position.z);
            //transform.position = newPos;
            teleportMesh.transform.position = transform.position;
            teleportMesh.transform.position += teleportMesh.transform.forward * (hit.distance - 1);
            Debug.Log("Ghost hitting");
        
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
    }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * distance, Color.green);
            teleportMesh.transform.position = transform.position;
            teleportMesh.transform.position += teleportMesh.transform.forward * distance;
        }





cooldownTimer -= Time.deltaTime;
        teleportTimer -= Time.deltaTime;
    }
}
