using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class openDoor : MonoBehaviour
{
    [SerializeField]
    private GameObject pivot;
    [SerializeField]
    private GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        //portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openTheDoor()
    {
        Vector3 pivotPoint = pivot.transform.position;
        this.transform.RotateAround(pivotPoint, Vector3.up, -90);
        gameObject.tag ="Wall";
        //portal.SetActive(true);

        ScenePortal sn = portal.gameObject.GetComponent<ScenePortal>();
        sn.TeleportToScene();
    }

}
