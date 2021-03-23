using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDungeonChest : MonoBehaviour
{

    [SerializeField]
    private GameObject pivot;
    public GameObject spellPrefab;
    public FacingDirection facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void openTheChest()
    {
        //pivot.transform.localRotation = Quaternion.Euler(-90, 0,0);
        GetComponent<Animator>().SetInteger("Open", 1);
        gameObject.tag = "Wall";

        Vector3 spawnPosition = transform.position;

        if(facingDirection == FacingDirection.Up)
        {
            spawnPosition.z -= 2.5f;
        } 
        else if (facingDirection == FacingDirection.Right)
        {
            spawnPosition.x += 2.5f;

        }
        else if(facingDirection == FacingDirection.Down)
        {
            spawnPosition.z += 2.5f;

        }
        else if(facingDirection == FacingDirection.Left)
        {
            spawnPosition.x -=2.5f;

        }
        var quat = new Quaternion();
        spawnPosition.y += 2;
        quat = Quaternion.Euler(90, 0, 0);
        Instantiate(spellPrefab, spawnPosition, quat);
    }
}
