using AnimationFunctions.Utils;
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

       Debug.Log( Vector2.Distance(xVector, controller));

           // Debug.Log((Mathf.Abs(horizontalInput) - Mathf.Abs(facing.x)) - (Mathf.Abs(verticalInput) - Mathf.Abs(facing.y)));
        if (Mathf.Abs(horizontalInput + verticalInput) > 0)
        {
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
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime, Space.World);
    }
}
