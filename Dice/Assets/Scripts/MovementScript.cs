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

        if ( angle > 1 || angle < -1)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            cameraDummy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        if(Mathf.Abs( horizontalInput + verticalInput) > 0)
        {
            AnimationScript.WalkFoward(animator);
        }

        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime, Space.World);
    }
}
