using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PCG : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    public List<GameObject> RoomPrefabs;
    public Vector2 WorldSize;

    private List<GameObject> rooms;
    // Start is called before the first frame update
    void GenerateFloor()
    {
        PlaceRooms();   

    }
    void PlaceRooms()
    {
        for(int i = 0; i < numberOfRooms;i++)
        {
            GameObject tempRoom = RoomPrefabs[Random.Range(0, RoomPrefabs.Count)];

            tempRoom.GetComponent<Room>().location = new Vector2(Random.Range(0, WorldSize.x), Random.Range(0, WorldSize.y));

            if (i == 0)
            {
                rooms.Add(tempRoom);
            }
            ValidRoomLocation(tempRoom, rooms);



        }
    }

    void SetSceneLocation()
    {

    }
    bool ValidRoomLocation(GameObject RoomLocation, List<GameObject> Rooms)
    {
        for(int i = 0; i < rooms.Count;i++)
        {
            var rect1 = { x: 5, y: 5, width: 50, height: 50}
            var rect2 = { x: 20, y: 10, width: 10, height: 10}

            if (rect1.x<rect2.x + rect2.width && rect1.x + rect1.width> rect2.x && rect1.y<rect2.y + rect2.height && rect1.y + rect1.height> rect2.y)
    {
        // collision detected!
    }

}
        return true;
    }
    
    void GenerateCorriodors()
    {

    }
  
}
