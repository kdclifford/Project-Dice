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
    private GameObject finalRoomPrefab;

    [SerializeField]
    private List<Room> roomsPrefabData = new List<Room>();

    [SerializeField]
    private List<Room> roomsData = new List<Room>();
    // Start is called before the first frame update
    [SerializeField]
    private List<Vector2> DoorPostions = new List<Vector2>();

    private void Start()          
    {
        for(int i =0; i<RoomPrefabs.Count;i++)
            roomsPrefabData.Add(RoomPrefabs[0].GetComponent<Room>());

        GenerateFloor();          
    }                             
    void GenerateFloor()          
    { 
        GenerateRooms();
        SetSceneLocation();
        //GenerateListOfDoors();
    }
    void GenerateRooms()
    {
        while(roomsData.Count<numberOfRooms)
        {
            Room tempRoom = new Room();
            if (roomsData.Count == 0)
            {
                tempRoom.location = new Vector2((int)Random.Range(0, WorldSize.x), (int)Random.Range(0, WorldSize.y));
                tempRoom.Size = finalRoomPrefab.GetComponent<Room>().Size;
                tempRoom.roomType = RoomType.Exit;
                roomsData.Add(tempRoom);
            }
            else
            { 
                
                tempRoom.preFabNumber= Random.Range(0, RoomPrefabs.Count);
                tempRoom.location = new Vector2((int)Random.Range(0, WorldSize.x), (int)Random.Range(0, WorldSize.y));
                tempRoom.Size = roomsPrefabData[tempRoom.preFabNumber].Size;
                if(ValidRoomLocation(tempRoom, roomsData))
                {
                    roomsData.Add(tempRoom);
                    for(int i = 0; i<roomsPrefabData[tempRoom.preFabNumber].DoorLocations.Count;i++)
                    {
                        Vector2 newDoorPosistion = roomsPrefabData[tempRoom.preFabNumber].DoorLocations[i];
                        newDoorPosistion += tempRoom.location;
                        DoorPostions.Add(newDoorPosistion);
                    }
                }
            }
        }


    }

    void SetSceneLocation()
    {
        for(int i=0; i< numberOfRooms;i++)
        {
            if (i == 0)
            {
                Instantiate(finalRoomPrefab, new Vector3(4 * roomsData[i].location.x, 0, 4 * roomsData[i].location.y), Quaternion.identity);
            }
            else
                Instantiate(RoomPrefabs[roomsData[i].preFabNumber],new Vector3(4 * roomsData[i].location.x, 0, 4 * roomsData[i].location.y),Quaternion.identity);
        }
    }
    bool ValidRoomLocation(Room RoomLocation, List<Room> Rooms)
    {
        var newRoom = RoomLocation;
        for (int i = 0; i < roomsData.Count; i++)
        {
            Room currentRoom = Rooms[i];
            //Saftey Cheack need so taht rooms dont spawn back to back and is a tile space
            //Instead of coroner of the room and its size, it is adjusted to have a ring of space around each room so corriodods can fit
            if (newRoom.location.x-1 < currentRoom.location.x + currentRoom.Size.x + 2 &&
                newRoom.location.x-1 + newRoom.Size.x +2 > currentRoom.location.x &&
                newRoom.location.y-1 < currentRoom.location.y + currentRoom.Size.y+2 &&
                newRoom.location.y - 1+ newRoom.Size.y +2 > currentRoom.Size.y)
            {
                return false;
            }
        }
        return true;   


}
    
    void GenerateListOfDoors()
    { 
        for(int i = 0; i<roomsData.Count;i++)
        {
            for(int j = 0; j<roomsData[i].DoorLocations.Count;j++)
            DoorPostions.Add(roomsData[i].DoorLocations[j]);
        }    


    }
  
}
