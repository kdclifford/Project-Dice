using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DunguonSpawner : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    public List<GameObject> RoomPrefabs;
    public Vector2 WorldSize;

    [SerializeField]
   // private List<GameObject> rooms = new List<GameObject>();

    public List<Room> roomsData = new List<Room>();
    // Start is called before the first frame update

    private void Start()          
    {                             
        GenerateFloor();          
    }                             
    void GenerateFloor()          
    { 
        GenerateRooms();
        SetSceneLocation();

    }
    void GenerateRooms()
    {
        while(roomsData.Count<numberOfRooms)
        {
            Room tempRoom = new Room() ;
            tempRoom.preFabNumber= Random.Range(0, RoomPrefabs.Count);

            tempRoom.location = new Vector2((int)Random.Range(0, WorldSize.x), (int)Random.Range(0, WorldSize.y));

            if (roomsData.Count == 0)
            {
                roomsData.Add(tempRoom);
            }
            else
            {
                //while(ValidRoomLocation(tempRoom, rooms))
                //{
                //    tempRoom.GetComponent<Room>().location = new Vector2((int)Random.Range(0, WorldSize.x), (int)Random.Range(0, WorldSize.y));
                //}
                roomsData.Add(tempRoom);

            }

        }
    }

    void SetSceneLocation()
    {
        for(int i=0; i< numberOfRooms;i++)
        {
            Instantiate(RoomPrefabs[roomsData[i].preFabNumber],new Vector3(4 * roomsData[i].location.x, 0, 4 * roomsData[i].location.y),Quaternion.identity);

        }
    }
    bool ValidRoomLocation(Room RoomLocation, List<Room> Rooms)
    {
        var newRoom = RoomLocation;
        for (int i = 0; i < roomsData.Count; i++)
        {
            Room currentRoom = Rooms[i];

            if (newRoom.location.x < currentRoom.location.x + currentRoom.Size.x &&
                newRoom.location.x + newRoom.Size.x > currentRoom.location.x &&
                newRoom.location.y < currentRoom.location.y + currentRoom.Size.y &&
                newRoom.location.y + newRoom.Size.y > currentRoom.Size.y)
            {
                return false;
            }
        }
        return true;   
    //        if (rect1.x<rect2.x + rect2.width && rect1.x + rect1.width> rect2.x && rect1.y<rect2.y + rect2.height && 
    //        rect1.y + rect1.height> rect2.y)
    //{
    //    // collision detected!
    //}

}
    
    void GenerateCorriodors()
    {

    }
  
}
