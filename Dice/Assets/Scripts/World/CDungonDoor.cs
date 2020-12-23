using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDungonDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject pivot;
    public GameObject roof;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void openTheDoor()
    {
        Vector3 pivotPoint = pivot.transform.position;
        this.transform.RotateAround(pivotPoint, Vector3.up, -90);
        gameObject.tag = "Wall";
        if (roof != null)
        {
            roof.SetActive(false);
        }
    }

}
