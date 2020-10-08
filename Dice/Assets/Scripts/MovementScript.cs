using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    [SerializeField]
    private GameObject cameraDummy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var angle = Mathf.Atan2(Input.GetAxis("RHorizontal"), Input.GetAxis("RVertical")) * Mathf.Rad2Deg;
        float horizontalInput = Input.GetAxis("LHorizontal");
        float verticalInput = Input.GetAxis("LVertical");

        if ( angle > 1 || angle < -1)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
            cameraDummy.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * moveSpeed * Time.deltaTime, Space.World);
    }
}
