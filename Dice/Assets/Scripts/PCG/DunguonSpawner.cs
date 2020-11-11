using System.Collections.Generic;
using TMPro;
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
    private List<Door> Doors = new List<Door>();

    private void Start()          
    {
        for(int i =0; i<RoomPrefabs.Count;i++)
            roomsPrefabData.Add(RoomPrefabs[0].GetComponent<Room>());

        GenerateFloor();          
    }

    private void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            GenerateCorridor();

        } 
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
            Room tempRoom = new Room();
            if (roomsData.Count == 0)
            {
                tempRoom.location = new Vector2((int)Random.Range(0, WorldSize.x), (int)Random.Range(0, WorldSize.y));
                tempRoom.Size = finalRoomPrefab.GetComponent<Room>().Size;
                tempRoom.roomType = RoomType.Exit;
                roomsData.Add(tempRoom);
               
                Vector2 newDoorPosistion = finalRoomPrefab.GetComponent<Room>().DoorLocations[0];
                newDoorPosistion += tempRoom.location;
                Door tempDoor = new Door(newDoorPosistion,0);
                Doors.Add(tempDoor);
                
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
                        Door tempDoor = new Door(newDoorPosistion, roomsData.Count-1);
                        Doors.Add(tempDoor);
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
    
    void GenerateCorridor()
    {
        //while(!AllDoorsConnected())
        //{
            int currentDoor = PickDoor();
            int targetDoor =NearestDoor(currentDoor);
            GeneratePath(currentDoor, targetDoor);
        //}
        //Generate Path Bewteen Avoiding Rooms
    }

    void GeneratePath(int startingDoor,int targetDoor)
    {
        PathFinding pathFinding = new PathFinding((int)WorldSize.x, (int)WorldSize.y,roomsData);
        List<PathNode> path = pathFinding.FindPath(Doors[startingDoor].doorLocation, Doors[targetDoor].doorLocation, (int)WorldSize.x, (int)WorldSize.y);
        int g = 5;

    }
     int NearestDoor(int currentDoor)
     {
        int nearestDoor=-1;
        float distance = 1000;
        for (int i = 0; i < Doors.Count; i++)
        {
            if (i != currentDoor && Doors[i].roomNumber != Doors[currentDoor].roomNumber)
            {
                if (distance > Vector2.Distance(Doors[i].doorLocation, Doors[currentDoor].doorLocation))
                {
                    distance = Vector2.Distance(Doors[i].doorLocation, Doors[currentDoor].doorLocation);
                    nearestDoor = i;
                }
            }
        }
        return nearestDoor;
    }

    int PickDoor()
    {
        for (int i = 0; i < Doors.Count; i++)
        {
            if (!Doors[i].connected)
            {
                return i;
            }
        }

        return -1;
    }
    bool AllDoorsConnected()
    {
        for(int i =0; i<Doors.Count;i++)
        {
            if(!Doors[i].connected)
            {
                return false;
            }
        }
        return true;
    }
}
