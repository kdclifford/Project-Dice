using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DunguonSpawner : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    public List<GameObject> RoomPrefabs;
    public Vector2 WorldSize;

    [SerializeField]
    private GameObject floorPRefab;
    [SerializeField]
    private GameObject finalRoomPrefab;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject startingRoom;

    [SerializeField]
    private List<Room> roomsPrefabData = new List<Room>();

    [SerializeField]
    private List<Room> roomsData = new List<Room>();
    // Start is called before the first frame update
    [SerializeField]
    private List<Door> Doors = new List<Door>();


    public Pathfinding pathFinder;


    private void Start()
    {
        for (int i = 0; i < RoomPrefabs.Count; i++)
            roomsPrefabData.Add(RoomPrefabs[i].GetComponent<Room>());

        GenerateFloor();
        pathFinder = new Pathfinding((int)WorldSize.x, (int)WorldSize.y, roomsData);

    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GenerateCorridors();

        }
    }
    void GenerateFloor()
    {
        GenerateRooms();
        SetSceneLocation();
    }


    void GenerateRooms()
    {
        Room tempRoom = new Room();
        int numberOfAttempts = 50;

        //Place the Start and final Room Both of these only have a single door way
        //Then add rooms to till the room count is reached
        tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x - 2), (int)Random.Range(2, WorldSize.y - 2));
        tempRoom.Size = finalRoomPrefab.GetComponent<Room>().Size;
        tempRoom.roomType = RoomType.Exit;
        tempRoom.DoorLocations = finalRoomPrefab.GetComponent<Room>().DoorLocations;
        tempRoom.DoorFacingDirections = finalRoomPrefab.GetComponent<Room>().DoorFacingDirections;
        roomsData.Add(tempRoom);

        Vector2Int newDoorPosistion = finalRoomPrefab.GetComponent<Room>().DoorLocations[0];
        newDoorPosistion += tempRoom.location;
        Door tempDoor = new Door(newDoorPosistion, 0);
        tempDoor.facingDirection = tempRoom.DoorFacingDirections[0];
        tempDoor.UpdatePathStartingLocation();
        tempDoor.connected = false;
        Doors.Add(tempDoor);

        bool validStart = false;
        tempRoom = new Room();
        while (!validStart)
        {
            tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x - 2), (int)Random.Range(2, WorldSize.y - 2));
            tempRoom.Size = startingRoom.GetComponent<Room>().Size;
            tempRoom.roomType = RoomType.Start;
            tempRoom.DoorLocations = startingRoom.GetComponent<Room>().DoorLocations;
            tempRoom.DoorFacingDirections = startingRoom.GetComponent<Room>().DoorFacingDirections;
  
            newDoorPosistion = startingRoom.GetComponent<Room>().DoorLocations[0];
            newDoorPosistion += tempRoom.location;
            tempDoor = new Door(newDoorPosistion, 1);
            tempDoor.facingDirection = tempRoom.DoorFacingDirections[0];
            tempDoor.UpdatePathStartingLocation();
            tempDoor.connected = false;
            validStart = ValidRoomLocation(tempRoom, roomsData);
        }
        Doors.Add(tempDoor);
        roomsData.Add(tempRoom);

        //Then add The rest of the room
        while (roomsData.Count < numberOfRooms && numberOfAttempts > 0)
        {
                tempRoom = new Room();
                tempRoom.preFabNumber = Random.Range(0, RoomPrefabs.Count);
                tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x-2), (int)Random.Range(2, WorldSize.y -2));
                tempRoom.Size = roomsPrefabData[tempRoom.preFabNumber].Size;
                tempRoom.DoorLocations = roomsPrefabData[tempRoom.preFabNumber].DoorLocations;
                tempRoom.DoorFacingDirections = roomsPrefabData[tempRoom.preFabNumber].DoorFacingDirections;


                if (ValidRoomLocation(tempRoom, roomsData))
                {
                    roomsData.Add(tempRoom);
                    for (int i = 0; i < roomsPrefabData[tempRoom.preFabNumber].DoorLocations.Count; i++)
                    {
                        newDoorPosistion = roomsPrefabData[tempRoom.preFabNumber].DoorLocations[i];
                        newDoorPosistion += tempRoom.location;
                        tempDoor = new Door(newDoorPosistion, roomsData.Count - 1);
                        tempDoor.facingDirection = tempRoom.DoorFacingDirections[0];
                        tempDoor.UpdatePathStartingLocation();
                        tempDoor.connected = false;
                        Doors.Add(tempDoor);
                    }
                }
                else
                    numberOfAttempts--;

        }



    }

    void SetPathModels(List<CTile> PathNodes)
    {
        if (PathNodes != null)
        {
            foreach (CTile pathPoint in PathNodes)
            {
                Instantiate(floorPRefab, new Vector3(4 * pathPoint.position.x, 0, 4 * pathPoint.position.y), Quaternion.identity);
                if(pathPoint.northWall)
                {
                    Instantiate(wallPrefab, new Vector3(4 * pathPoint.position.x, 0, (4 * pathPoint.position.y) +2), Quaternion.identity);

                }
                if (pathPoint.eastWall)
                {
                    Instantiate(wallPrefab, new Vector3((4 * pathPoint.position.x) +2, 0, 4 * pathPoint.position.y), Quaternion.Euler(new Vector3(0,270,0)));

                }
                if (pathPoint.southWall)
                {
                    Instantiate(wallPrefab, new Vector3(4 * pathPoint.position.x, 0, (4 * pathPoint.position.y) - 2), Quaternion.identity);

                }
                if (pathPoint.westWall)
                {
                    Instantiate(wallPrefab, new Vector3((4 * pathPoint.position.x) -2, 0, 4 * pathPoint.position.y), Quaternion.Euler(new Vector3(0, 90, 0)));

                }
            }
        }
    }
    void SetSceneLocation()
    {

        var temp = Instantiate(finalRoomPrefab, new Vector3(4 * roomsData[0].location.x, 0, 4 * roomsData[0].location.y), Quaternion.identity);
        temp.GetComponent<Room>().location = roomsData[0].location;

        temp = Instantiate(startingRoom, new Vector3(4 * roomsData[1].location.x, 0, 4 * roomsData[1].location.y), Quaternion.identity);
        temp.GetComponent<Room>().location = roomsData[1].location;

        for (int i=2; i< roomsData.Count;i++)
        {

                temp = Instantiate(RoomPrefabs[roomsData[i].preFabNumber], new Vector3(4 * roomsData[i].location.x, 0, 4 * roomsData[i].location.y), Quaternion.identity);
                temp.GetComponent<Room>().location = roomsData[i].location;

            
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
    
    void GenerateCorridors()
    {
        List<Vector2Int> pathPoints = new List<Vector2Int>();
        int count =0;
       while(!AllDoorsConnected() && count < 50)
       {
            int currentDoor = PickDoor();
            int targetDoor =NearestDoor(currentDoor);
            
            List<Vector2Int> path = GeneratePath(currentDoor, targetDoor);
            if(path !=null)
            {

                foreach(Vector2Int point in path)
                {
                    if(!pathPoints.Contains(point))
                        pathPoints.Add(point);

                }
                Doors[currentDoor].connected = true;
                Doors[targetDoor].connected = true;
            }
            
            
            count++;
       }
       
        SetPathModels(GenerateWalls(pathPoints,Doors));
    }

    private List<CTile> GenerateWalls(List<Vector2Int> pathPoints, List<Door> doors)
    {
        List<CTile> pathTiles = new List<CTile>();
        List<Vector2Int> doorLocations = new List<Vector2Int>();
        foreach(Door door in  doors)
        {
            doorLocations.Add(door.doorPoisitoin());
        }
        foreach (Vector2Int point in pathPoints)
        {
            bool northWall = true;
            bool southWall = true;
            bool eastWall = true;
            bool westWall = true;
            if (pathPoints.Contains(new Vector2Int(point.x,point.y + 1)) || doorLocations.Contains(new Vector2Int(point.x, point.y + 1)))
            {
                northWall = false;
            }
            if (pathPoints.Contains(new Vector2Int(point.x, point.y - 1)) || doorLocations.Contains(new Vector2Int(point.x, point.y  - 1)))
            {
                southWall = false;
            }
            if (pathPoints.Contains(new Vector2Int(point.x +1, point.y)) || doorLocations.Contains(new Vector2Int(point.x + 1, point.y)))
            {
                eastWall = false;
            }
            if (pathPoints.Contains(new Vector2Int(point.x -1, point.y)) || doorLocations.Contains(new Vector2Int(point.x - 1, point.y)))
            {
                westWall = false;
            }
            CTile temp = new CTile(point, northWall, southWall, eastWall, westWall);
            pathTiles.Add(temp);
        }
        return pathTiles;
    }

    private List<Vector2Int> GeneratePath(int startingDoor,int targetDoor)
    {
        //PathFinding pathFinding = new PathFinding((int)WorldSize.x, (int)WorldSize.y);



        var temp = pathFinder.FindPath(Doors[startingDoor].doorLocation, Doors[targetDoor].doorLocation);

        return (pathFinder.FindPath(Doors[startingDoor].doorLocation, Doors[targetDoor].doorLocation));

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
