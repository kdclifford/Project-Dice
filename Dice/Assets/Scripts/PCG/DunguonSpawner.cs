using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DunguonSpawner : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    public List<GameObject> RoomPrefabs;
    public Vector2Int WorldSize;
    public float wallOffset = 12;
    public float wallPosOffset = 6.6f;

    //private GameObject wallParent;
    //private GameObject floorParent;
     //private GameObject lightHolder;

    public GameObject Player;

    [SerializeField]
    private GameObject floorPrefab;
    [SerializeField]
    private GameObject finalRoomPrefab;
    [SerializeField]
    private GameObject finalBossRoom;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject startingRoom;
    [SerializeField]
    private GameObject doorPrefab;
    [SerializeField]
    private GameObject chestPrefab;

    public Color[] worldColourMap;

    public int BossFloor = 2;

    [SerializeField]
    private List<Room> roomsPrefabData = new List<Room>();

    [SerializeField]
    private List<Room> roomsData = new List<Room>();
    // Start is called before the first frame update
    [SerializeField]
    private List<CDoor> Doors = new List<CDoor>();

    public List<Vector2Int> pathPoints = new List<Vector2Int>();

    public Pathfinding pathFinder;

    public List<GameObject> roomRef;

    public static DunguonSpawner instance;

    public Sprite miniMap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        for (int i = 0; i < RoomPrefabs.Count; i++)
            roomsPrefabData.Add(RoomPrefabs[i].GetComponent<Room>());


        GenerateFloor();

        while (!AcceptableFloortCheck())
        {
            GenerateFloor();
        }

        //Calcaute how many chests will be placed on that floor

        //Pick Rooms to put them in

        //Place the chests and rotate as needed and set dirction for enum 
        SetSceneLocation();

        SetPathModels(GenerateWalls(pathPoints, Doors));


        var numberOfChests = Random.Range(1, 3);
        //Pick Rooms to put them inw

        for (int i = 0; i < numberOfChests; i++)
        {
            //First 2 rooms in the list are the starting and ending Room
            var chosenRoom = Random.Range(2, roomsData.Count);
            var chosenSpawnLocation = Random.Range(0, roomsData[chosenRoom].chestSpawnLocations.Count);

            //Place the chests and rotate as needed and set dirction for enum 
            //north = 2.5 East = 5 South = 7.5  West = 10
            //Place The chest in the correct postion
            //Set the chest rotation after and CDungeon Script values 
            Vector3 chestPostion = new Vector3(12 * roomsData[chosenRoom].location.x, 0, 12 * roomsData[chosenRoom].location.y);
            chestPostion.x += (roomsData[chosenRoom].chestSpawnLocations[chosenSpawnLocation].x*3);
            chestPostion.z += (roomsData[chosenRoom].chestSpawnLocations[chosenSpawnLocation].z*3);




            var chest = Instantiate(chestPrefab, chestPostion, Quaternion.identity);

            if (roomsData[chosenRoom].chestSpawnLocations[chosenSpawnLocation].w == 2.5)
            {
                chest.transform.rotation = Quaternion.Euler(0, 0, 0);
                chest.GetComponent<CDungeonChest>().facingDirection = FacingDirection.Down;

            }
            else if (roomsData[chosenRoom].chestSpawnLocations[chosenSpawnLocation].w == 5)
            {
                chest.transform.rotation = Quaternion.Euler(0, 90, 0);
                chest.GetComponent<CDungeonChest>().facingDirection = FacingDirection.Right;

            }
            else if (roomsData[chosenRoom].chestSpawnLocations[chosenSpawnLocation].w == 7.5)
            {
                chest.transform.rotation = Quaternion.Euler(0, 180, 0);
                chest.GetComponent<CDungeonChest>().facingDirection = FacingDirection.Up;

            }
            else if (roomsData[chosenRoom].chestSpawnLocations[chosenSpawnLocation].w == 10)
            {
                chest.transform.rotation = Quaternion.Euler(0, 270, 0);

                chest.GetComponent<CDungeonChest>().facingDirection = FacingDirection.Left;
                //Pick Rooms to put them in

            }

            

        }


        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(12 * roomsData[1].location.x, 1f, 12 * roomsData[1].location.y);
        //Instantiate(Player, new Vector3(4 * roomsData[1].location.x, 0, 4 * roomsData[1].location.y), Quaternion.identity);


        //generate the colour map Set Background of colour map to be white 
        //Will then go over and paint and rooms to be black and the corriders to be grey
        var texture = GenerateMinMapTexture();
        miniMap =  Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public Texture2D GenerateStartingMiniMapTexture() 
    {
        worldColourMap = new Color[WorldSize.x * WorldSize.y];

        var tempColour = Color.white;
        for (int x = 0; x < WorldSize.x; x++)
        {
            for (int y = 0; y < WorldSize.y; y++)
            {
                worldColourMap[y * WorldSize.y + x] = tempColour;
            }
        }


        //Set The Starting Room Texture
        tempColour = Color.black;

           
        for (int x = 0; x < roomsData[1].Size.x; x++)
        {
            for (int y = 0; y < roomsData[1].Size.y; y++)
            {
                worldColourMap[(roomsData[1].location.y + y) * WorldSize.y + (roomsData[1].location.x + x)] = tempColour;
            }
        }
        


        Texture2D texture = new Texture2D(WorldSize.x, WorldSize.y);
        texture.SetPixels(worldColourMap);
        texture.Apply();
        return texture;

    }
    public Texture2D UpdateCorridorMinMapTexture(float x, float y)
    {

        var tempColour = Color.blue;

        Vector2Int worldMapSize = new Vector2Int((int)x/12, (int)y /12);

        worldColourMap[(worldMapSize.y ) * WorldSize.y + (worldMapSize.x )] = tempColour;

        Texture2D texture = new Texture2D(WorldSize.x, WorldSize.y);
        texture.SetPixels(worldColourMap);
        texture.Apply();
        return texture;
    }

    public Texture2D GenerateMinMapTexture()
    {
        worldColourMap = new Color[WorldSize.x * WorldSize.y];
        //generate the colour map Set Background of colour map to be white 
        //Will then go over and paint and rooms to be black and the corriders to be grey

        var tempColour = Color.white;
        for (int x = 0; x < WorldSize.x; x++)
        {
            for (int y = 0; y < WorldSize.y; y++)
            {
                worldColourMap[x * WorldSize.x + y] = tempColour;
            }
        }
        tempColour = Color.blue;

        foreach (Vector2Int point in pathPoints)
        {
            worldColourMap[(point.x * WorldSize.x-1) + point.y] = tempColour;
        }

        tempColour = Color.black;
        foreach (Room room in roomsData)
        {
            for (int x = 0; x < room.Size.x; x++)
            {
                for (int y = 0; y < room.Size.y; y++)   
                {
                    worldColourMap[((room.location.x + x) * WorldSize.x -1)+ (room.location.y + y)] = tempColour;
                }
            }
        }

        Texture2D texture = new Texture2D(WorldSize.x, WorldSize.y);
        texture.SetPixels(worldColourMap);
        texture.Apply();
        return texture;
    }


    void GenerateFloor()
    {
        GenerateRooms();
        pathFinder = new Pathfinding((int)WorldSize.x, (int)WorldSize.y, roomsData);
        GenerateCorridors();
    }

    public bool AcceptableFloortCheck()
    {
        //Start at the start room
        List<int> connectedRooms = new List<int>();
        List<int> searchedRooms = new List<int>();
        List<int> roomsToSearch = new List<int>();

        connectedRooms.Add(roomsData[0].roomNumber);
        roomsToSearch.Add(roomsData[0].connectedRooms[0]);
        searchedRooms.Add(roomsData[0].roomNumber);

        while (roomsToSearch.Count > 0)
        {
            connectedRooms.Add(roomsToSearch[0]);
            foreach (int connectedRoom in roomsData[roomsToSearch[0]].connectedRooms)
            {
                if (!connectedRooms.Contains(connectedRoom) && !searchedRooms.Contains(connectedRoom) && !roomsToSearch.Contains(connectedRoom))
                {
                    roomsToSearch.Add(connectedRoom);
                }
            }
            searchedRooms.Add(roomsToSearch[0]);
            roomsToSearch.RemoveAt(0);
        }

        if (searchedRooms.Count < roomsData.Count)
        {
            return false;
        }

        return true;
    }
    void GenerateRooms()
    {

        Room tempRoom = new Room();
        roomsData = new List<Room>();
        Doors = new List<CDoor>();

        int numberOfAttempts = 100;

        //Place the Start and final Room Both of these only have a single door way
        //Then add rooms to till the room count is reached

        if (SpawnManager.instance.currentFloor % BossFloor == 0)
        {
            tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x - 2), (int)Random.Range(2, WorldSize.y - 2));
            tempRoom.Size = finalBossRoom.GetComponent<Room>().Size;
            tempRoom.roomType = RoomType.Exit;
            tempRoom.DoorLocations = finalBossRoom.GetComponent<Room>().DoorLocations;
            tempRoom.DoorFacingDirections = finalBossRoom.GetComponent<Room>().DoorFacingDirections;
            tempRoom.roomNumber = 0;
            tempRoom.connectedRooms = new List<int>();
            roomsData.Add(tempRoom);
        }
        else
        {
            tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x - 2), (int)Random.Range(2, WorldSize.y - 2));
            tempRoom.Size = finalRoomPrefab.GetComponent<Room>().Size;
            tempRoom.roomType = RoomType.Exit;
            tempRoom.DoorLocations = finalRoomPrefab.GetComponent<Room>().DoorLocations;
            tempRoom.DoorFacingDirections = finalRoomPrefab.GetComponent<Room>().DoorFacingDirections;
            tempRoom.roomNumber = 0;
            tempRoom.connectedRooms = new List<int>();
            roomsData.Add(tempRoom);
        }

        bool validStart = false;
        tempRoom = new Room();
        while (!validStart)
        {
            tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x - 2), (int)Random.Range(2, WorldSize.y - 2)); // Needed to allow pathing to find doors
            tempRoom.Size = startingRoom.GetComponent<Room>().Size;
            tempRoom.roomType = RoomType.Start;
            tempRoom.DoorLocations = startingRoom.GetComponent<Room>().DoorLocations;
            tempRoom.DoorFacingDirections = startingRoom.GetComponent<Room>().DoorFacingDirections;
            tempRoom.roomNumber = 1;
            tempRoom.connectedRooms = new List<int>();

            validStart = ValidRoomLocation(tempRoom, roomsData);
        }

        roomsData.Add(tempRoom);

        //Then add The rest of the room
        while (roomsData.Count < numberOfRooms && numberOfAttempts > 0)
        {
            tempRoom = new Room();
            tempRoom.preFabNumber = Random.Range(0, RoomPrefabs.Count);
            tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x - 2), (int)Random.Range(2, WorldSize.y - 2));
            tempRoom.Size = roomsPrefabData[tempRoom.preFabNumber].Size;
            tempRoom.DoorLocations = roomsPrefabData[tempRoom.preFabNumber].DoorLocations;
            tempRoom.DoorFacingDirections = roomsPrefabData[tempRoom.preFabNumber].DoorFacingDirections;
            tempRoom.roomNumber = roomsData.Count;
            tempRoom.connectedRooms = new List<int>();
            tempRoom.chestSpawnLocations = RoomPrefabs[tempRoom.preFabNumber].GetComponent<Room>().chestSpawnLocations;

            if (ValidRoomLocation(tempRoom, roomsData))
            {
                roomsData.Add(tempRoom);
            }
            else
                numberOfAttempts--;
        }

        //Add Doors
        Vector2Int newDoorPosistion;
        CDoor tempDoor;
        foreach (Room room in roomsData)
        {
            for (int i = 0; i < room.DoorLocations.Count; i++)
            {
                tempDoor = new CDoor();
                newDoorPosistion = room.DoorLocations[i];
                newDoorPosistion += room.location;
                tempDoor = new CDoor(newDoorPosistion, room.roomNumber);
                tempDoor.facingDirection = room.DoorFacingDirections[i];
                tempDoor.UpdatePathStartingLocation();
                tempDoor.connected = false;
                Doors.Add(tempDoor);
            }
        }
    }
    void SetPathModels(List<CTile> PathNodes)
    {
        if (PathNodes != null)
        {
            //floorParent = new GameObject("FloorHolder");
            //wallParent = new GameObject("WallHolder");
            //lightHolder = new GameObject("LightHolder");
            GameObject wall;

            foreach (CTile pathPoint in PathNodes)
            {
                if (pathPoint.northWall)
                {
                    wall = Instantiate(wallPrefab, new Vector3(wallOffset * pathPoint.position.x, 0, (wallOffset * pathPoint.position.y) + wallPosOffset), Quaternion.Euler(new Vector3(0, 180, 0)));
                    //wall.transform.GetChild(0).parent = lightHolder.transform;
                }
                if (pathPoint.eastWall)
                {
                    wall = Instantiate(wallPrefab, new Vector3((wallOffset * pathPoint.position.x) + wallPosOffset, 0, wallOffset * pathPoint.position.y), Quaternion.Euler(new Vector3(0, 270, 0)));
                    //wall.transform.GetChild(0).parent = lightHolder.transform;
                }
                if (pathPoint.southWall)
                {
                    wall = Instantiate(wallPrefab, new Vector3(wallOffset * pathPoint.position.x, 0, (wallOffset * pathPoint.position.y) - wallPosOffset), Quaternion.identity);
                    //wall.transform.GetChild(0).parent = lightHolder.transform;
                }
                if (pathPoint.westWall)
                {
                    wall = Instantiate(wallPrefab, new Vector3((wallOffset * pathPoint.position.x) - wallPosOffset, 0, wallOffset * pathPoint.position.y), Quaternion.Euler(new Vector3(0, 90, 0)));
                    //wall.transform.GetChild(0).parent = null;
                }
                Instantiate(floorPrefab, new Vector3(wallOffset * pathPoint.position.x, 0, wallOffset * pathPoint.position.y), Quaternion.identity);
            }
            //wallParent.AddComponent<MeshRenderer>().materials = wallParent.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            ////wallParent.AddComponent<MergeMeshes>();
            //wallParent.AddComponent<MeshCollider>();
            //wallParent.tag = "Wall";
            //wallParent.layer = LayerMask.NameToLayer( "Obstacle");

            //floorParent.AddComponent<MeshRenderer>().materials = floorParent.transform.GetChild(0).GetComponent<MeshRenderer>().materials;
            ////floorParent.AddComponent<MergeMeshes>();
            //floorParent.layer = LayerMask.NameToLayer("Floor");

            //for (int i = 0; i < wallParent.transform.childCount; i++)
            //{
            //    Destroy(wallParent.transform.GetChild(i).gameObject);
            //}
        }
    }
    void SetSceneLocation()
    {


        var finalRoomObject = finalRoomPrefab;
        if (SpawnManager.instance.currentFloor % BossFloor == 0)
        {
            finalRoomObject = finalBossRoom;
        }

        var temp = Instantiate(finalRoomObject, new Vector3(12 * roomsData[0].location.x, 0, 12 * roomsData[0].location.y), Quaternion.identity);
        temp.GetComponent<Room>().location = roomsData[0].location;
        roomRef.Add(temp);

        temp = Instantiate(startingRoom, new Vector3(12 * roomsData[1].location.x, 0, 12 * roomsData[1].location.y), Quaternion.identity);
        temp.GetComponent<Room>().location = roomsData[1].location;
        roomRef.Add(temp);

        for (int i = 2; i < roomsData.Count; i++)
        {
            temp = Instantiate(RoomPrefabs[roomsData[i].preFabNumber], new Vector3(12 * roomsData[i].location.x, 0, 12 * roomsData[i].location.y), Quaternion.identity);
            temp.GetComponent<Room>().location = roomsData[i].location;
            roomRef.Add(temp);
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
            if (newRoom.location.x - 1 < currentRoom.location.x + currentRoom.Size.x + 2 &&
                newRoom.location.x - 1 + newRoom.Size.x + 2 > currentRoom.location.x &&
                newRoom.location.y - 1 < currentRoom.location.y + currentRoom.Size.y + 2 &&
                newRoom.location.y - 1 + newRoom.Size.y + 2 > currentRoom.Size.y)
            {
                return false;
            }
        }
        return true;
    }
    void GenerateCorridors()
    {
        pathPoints = new List<Vector2Int>();
        int count = 0;
        while (!AllDoorsConnected() && count < 50)
        {
            int currentDoor = PickDoor();
            int targetDoor = NearestDoor(currentDoor);

            List<Vector2Int> path = GeneratePath(Doors[currentDoor], Doors[targetDoor]);
            if (path != null)
            {
                //update Room connected Data 
                roomsData[Doors[currentDoor].roomNumber].connectedRooms.Add(Doors[targetDoor].roomNumber);
                roomsData[Doors[targetDoor].roomNumber].connectedRooms.Add(Doors[currentDoor].roomNumber);

                foreach (Vector2Int point in path)
                {
                    if (!pathPoints.Contains(point))
                        pathPoints.Add(point);
                }
                Doors[currentDoor].connected = true;
                Doors[targetDoor].connected = true;
            }


            count++;
        }

    }
    private List<CTile> GenerateWalls(List<Vector2Int> pathPoints, List<CDoor> doors)
    {
        List<CTile> pathTiles = new List<CTile>();
        List<Vector2Int> doorLocations = new List<Vector2Int>();
        foreach (CDoor door in doors)
        {
            doorLocations.Add(door.doorPoisitoin());
        }
        foreach (Vector2Int point in pathPoints)
        {
            bool northWall = true;
            bool southWall = true;
            bool eastWall = true;
            bool westWall = true;
            if (pathPoints.Contains(new Vector2Int(point.x, point.y + 1)) || doorLocations.Contains(new Vector2Int(point.x, point.y + 1)))
            {
                northWall = false;
            }
            if (pathPoints.Contains(new Vector2Int(point.x, point.y - 1)) || doorLocations.Contains(new Vector2Int(point.x, point.y - 1)))
            {
                southWall = false;
            }
            if (pathPoints.Contains(new Vector2Int(point.x + 1, point.y)) || doorLocations.Contains(new Vector2Int(point.x + 1, point.y)))
            {
                eastWall = false;
            }
            if (pathPoints.Contains(new Vector2Int(point.x - 1, point.y)) || doorLocations.Contains(new Vector2Int(point.x - 1, point.y)))
            {
                westWall = false;
            }
            CTile temp = new CTile(point, northWall, southWall, eastWall, westWall);
            pathTiles.Add(temp);
        }
        return pathTiles;
    }

    private List<Vector2Int> GeneratePath(CDoor startingDoor, CDoor targetDoor)
    {
        //var temp = pathFinder.FindPath(startingDoor.doorLocation, targetDoor.doorLocation);

        return (pathFinder.FindPath(startingDoor.location, targetDoor.location));

    }
    int NearestDoor(int currentDoor)
    {
        int nearestDoor = -1;
        float distance = 1000;
        for (int i = 0; i < Doors.Count; i++)
        {
            if (i != currentDoor && Doors[i].roomNumber != Doors[currentDoor].roomNumber) // Making sure doors in the same room dont connted 
            {
                bool validRoom = true;
                foreach (int conntedRoom in roomsData[Doors[i].roomNumber].connectedRooms) //FIX HERE
                {
                    if (Doors[i].roomNumber == conntedRoom)
                    {
                        validRoom = false;
                    }
                }
                if (validRoom && distance > Vector2.Distance(Doors[i].location, Doors[currentDoor].location))
                {
                    distance = Vector2.Distance(Doors[i].location, Doors[currentDoor].location);
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
        for (int i = 0; i < Doors.Count; i++)
        {
            if (!Doors[i].connected)
            {
                return false;
            }
        }
        return true;
    }
}
