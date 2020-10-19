using AnimationFunctions.Utils;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private GameObject cameraDummy;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float rayDist;
    [SerializeField]
    private LayerMask layerMask;
   
    public bool walking = false;

    Vector3 rayOffset = new Vector3(0, 0.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var angle = Mathf.Atan2(Input.GetAxis("RHorizontal"), Input.GetAxis("RVertical")) * Mathf.Rad2Deg;
        float horizontalInput = Input.GetAxis("LHorizontal"); float verticalInput = Input.GetAxis("LVertical");

      // Debug.Log("X " + Input.GetAxis("LHorizontal"));
       //Debug.Log("Y " + Input.GetAxis("LVertical"));
       
      // Debug.Log(transform.forward);
       //Debug.Log(angle);

        if ( angle > 1 || angle < -1)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            cameraDummy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        bool leftfire = GetComponent<ProjectileFire>().leftFire;
        bool rightfire = GetComponent<ProjectileFire>().rightFire;


        Vector2 facing;
        facing.x = transform.forward.x;
        facing.y = transform.forward.z;

        Vector2 xVector;
        xVector.x = transform.right.x;
        xVector.y = transform.right.z;

        Vector2 controller = new Vector2(horizontalInput, verticalInput);

        // Debug.Log( Vector2.Distance(xVector, controller));

           // Debug.Log((Mathf.Abs(horizontalInput) - Mathf.Abs(facing.x)) - (Mathf.Abs(verticalInput) - Mathf.Abs(facing.y)));
        if (Mathf.Abs(horizontalInput + verticalInput) > 0)
        {
            walking = true;
       // Debug.Log(Mathf.Abs(Mathf.Abs(facing.y) - Mathf.Abs(facing.x)) - Mathf.Abs(Mathf.Abs(verticalInput) - Mathf.Abs(horizontalInput)));

            if (!rightfire && !leftfire && Vector2.Distance(facing, controller) <= 0.7f)
            {
                AnimationScript.WalkFoward(animator);
            }

           else if (!rightfire && !leftfire && Vector2.Distance(facing, controller) >= 1.7f)
            {
                AnimationScript.WalkBack(animator);
            }

           else if (!rightfire && !leftfire && Vector2.Distance(xVector, controller) >= 1.0f)
            {
                AnimationScript.WalkLeft(animator);
            }

           else if (!rightfire && !leftfire && Vector2.Distance(xVector, controller) < 1.0f)
            {
                AnimationScript.WalkRight(animator);
            }


        }
        else
        {
            walking = false;
        }

        RaycastHit hit;

        //Vector3 newd = Vector3.Cross(new Vector3(horizontalInput, 1, verticalInput), transform.up).normalized;
        //     if(Physics.Raycast(transform.position, newd, out hit, rayDist / 2) || Physics.Raycast(transform.position, -newd, out hit, rayDist / 2))
        //{
        //    Debug.Log("Working xxxx col");
        //    GetComponent<Rigidbody>().velocity = Vector3.zero;
        //}
        // if (Physics.SphereCast(transform.position, 2, new Vector3(horizontalInput, 1, verticalInput), out hit, rayDist))

        

        if (Physics.Raycast(transform.position + rayOffset, Vector3.forward, out hit, rayDist, ~layerMask))
        {
            if (verticalInput > 0)
            {
                verticalInput = 0;
            }
        }


        if (Physics.Raycast(transform.position + rayOffset, Vector3.back, out hit, rayDist, ~layerMask))
        { 
            if (verticalInput < 0)
            {
                verticalInput = 0;
            }
        }


        if (Physics.Raycast(transform.position + rayOffset, Vector3.right, out hit, rayDist, ~layerMask))
        {
            if (horizontalInput > 0)
            {
                horizontalInput = 0;
            }
        }



        if (Physics.Raycast(transform.position + rayOffset, Vector3.left, out hit, rayDist, ~layerMask))
        {
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            ////transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * -moveSpeed * Time.deltaTime, Space.World);
            //Debug.Log("Working col");

            if(horizontalInput < 0)
            {
                horizontalInput = 0;
            }


        }
        

            transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime, Space.World);
        
    }

    

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
           
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


}
