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
    private float rayDist;
    [SerializeField]
    private LayerMask layerMask;   

    public Vector3 rayOffset = new Vector3(0, 1.5f, 0);

   private Vector3 upRight = Vector3.forward + Vector3.right;
   private Vector3 upLeft = Vector3.forward + Vector3.left;
   private Vector3 downRight = Vector3.back + Vector3.right;
   private Vector3 downLeft = Vector3.back + Vector3.left;

    public float diagonalOffset = 0.9f;
    public float collisionForce = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var angle = Mathf.Atan2(Input.GetAxis("RHorizontal"), Input.GetAxis("RVertical")) * Mathf.Rad2Deg;
        float horizontalInput = Input.GetAxis("LHorizontal"); float verticalInput = Input.GetAxis("LVertical");

        if ( angle > 1 || angle < -1)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            cameraDummy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
           
        RaycastHit hit;


        if (UpRight() && DownRight())
        {
            if (Physics.Raycast(transform.position + rayOffset, Vector3.right, out hit, rayDist, ~layerMask))
            {
                //Vector3 newPos = new Vector3(transform.position.x - (rayDist - hit.distance), transform.position.y, transform.position.z);
                //transform.position = newPos;

                GetComponent<Rigidbody>().AddForce(Vector3.left * collisionForce);                
            }
        }

        if (DownLeft() && UpLeft())
        {
            if (Physics.Raycast(transform.position + rayOffset, Vector3.left, out hit, rayDist, ~layerMask))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.right * collisionForce);
            }
        }

        if (DownRight() && DownLeft())
        {
            if (Physics.Raycast(transform.position + rayOffset, Vector3.down, out hit, rayDist, ~layerMask))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.forward * collisionForce);
            }
        }

        if (UpLeft() && UpRight())
        {
            if (Physics.Raycast(transform.position + rayOffset, Vector3.forward, out hit, rayDist, ~layerMask))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.back * collisionForce);
            }
        }



        if (Physics.Raycast(transform.position + rayOffset, Vector3.forward, out hit, rayDist, ~layerMask))
        {
            if (verticalInput > 0)
            {
                verticalInput = 0;
            }
            Debug.DrawRay(transform.position, Vector3.forward * rayDist, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.forward * rayDist, Color.green);
        }


        if (Physics.Raycast(transform.position + rayOffset, Vector3.back, out hit, rayDist, ~layerMask))
        { 
            if (verticalInput < 0)
            {
                verticalInput = 0;
            }
            Debug.DrawRay(transform.position, Vector3.back * rayDist, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.back * rayDist, Color.green);
        }


        if (Physics.Raycast(transform.position + rayOffset, Vector3.right, out hit, rayDist, ~layerMask))
        {
            if (horizontalInput > 0)
            {
                horizontalInput = 0;
            }
            Debug.DrawRay(transform.position, Vector3.right * rayDist, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.right * rayDist, Color.green);
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

            Debug.DrawRay(transform.position, Vector3.left * rayDist, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.left * rayDist, Color.green);
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

    bool UpLeft()
    {
        if (Physics.Raycast(transform.position + rayOffset, upLeft, rayDist * diagonalOffset, ~layerMask))
        {

            //horizontalInput = -horizontalInput;
            //verticalInput = -verticalInput;

            //if (Mathf.Abs(verticalInput) > Mathf.Abs(horizontalInput))
            //{
            //    verticalInput = 0;
            //}
            //else if (Mathf.Abs(verticalInput) < Mathf.Abs(horizontalInput))
            //{
            //    horizontalInput = 0;
            //}
            Debug.DrawRay(transform.position, upLeft * (rayDist * diagonalOffset), Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, upLeft * (rayDist * diagonalOffset), Color.green);
            return false;
        }
    }


    bool UpRight()
    {
        if (Physics.Raycast(transform.position + rayOffset, upRight, rayDist * diagonalOffset, ~layerMask))
        {            
            Debug.DrawRay(transform.position, upRight * (rayDist * diagonalOffset), Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, upRight * (rayDist * diagonalOffset), Color.green);
            return false;
        }
    }

    bool DownRight()
    {
        if (Physics.Raycast(transform.position + rayOffset, downRight, rayDist * diagonalOffset, ~layerMask))
        {
            Debug.DrawRay(transform.position, downRight * (rayDist * diagonalOffset), Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, downRight * (rayDist * diagonalOffset), Color.green);
            return false;
        }
    }

    bool DownLeft()
    {
        if (Physics.Raycast(transform.position + rayOffset, downLeft, rayDist * diagonalOffset, ~layerMask))
        {
            Debug.DrawRay(transform.position, downLeft * (rayDist * diagonalOffset), Color.red);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, downLeft * (rayDist * diagonalOffset), Color.green);
            return false;
        }
    }



}
