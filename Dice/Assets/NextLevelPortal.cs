using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelPortal : MonoBehaviour
{
    private Room room;
    private List<GameObject> newEnemyList;
    public GameObject portalPrefab;
    private bool portalSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
      room =  transform.parent.GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = room.enemyList.Count - 1;
        newEnemyList = room.enemyList;
        for (int i = enemyCount; i >= 0; i--)
        {
            if(room.enemyList[i] != null)
            {
                break;
            }
            else
            {
                room.enemyList.RemoveAt(i);
            }
        }
        //room.enemyList = newEnemyList;

        if(!portalSpawned && room.enemyList.Count == 0)
        {
            GameObject tempEnemy = Instantiate(portalPrefab, transform.position, transform.rotation) as GameObject;
            portalSpawned = true;
        }

    }
}
