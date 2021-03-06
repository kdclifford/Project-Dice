using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDungeonCheast : MonoBehaviour
{

    [SerializeField]
    private GameObject pivot;
    public GameObject spellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void openTheCheast()
    {
        pivot.transform.rotation = new Quaternion(-90,0 , 0, 0);
        //Vector3 pivotPoint = pivot.transform.position;
        //this.transform.RotateAround(pivotPoint, Vector3.down, -90);
        gameObject.tag = "Wall";
        Vector3 spawnPosition = transform.position;
        spawnPosition.x -= 5;
        Instantiate(spellPrefab, spawnPosition, Quaternion.identity);
    }
}
