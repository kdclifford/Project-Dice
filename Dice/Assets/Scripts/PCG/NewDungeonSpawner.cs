 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDungeonSpawner : MonoBehaviour
{
    [SerializeField] private int numberOfRooms;
    public List<GameObject> RoomPrefabs;
    public Vector2Int WorldSize;

    public GameObject Player;

    [SerializeField]
    private GameObject floorPRefab;
    [SerializeField]
    private GameObject finalRoomPrefab;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject startingRoom;
    [SerializeField]
    private GameObject doorPrefab;

    public bool[,] worldSpace = new bool[100, 100];

    [SerializeField]
    private List<Room> roomsPrefabData = new List<Room>();

    [SerializeField]
    private List<Room> roomsData = new List<Room>();
    // Start is called before the first frame update
    [SerializeField]
    private List<CDoor> Doors = new List<CDoor>();

    public List<Vector2Int> pathPoints = new List<Vector2Int>();


    private bool UpdateWorldSpace(List<Vector2Int> pathPoints)
    {
        return true;
    }
    private bool UpdateWorldSpace(Room newRoom)
    {
        var tempWorldSpace = worldSpace;
        for(int i = 0; i< newRoom.Size.x;i++)
        {
            for(int j = 0; j< newRoom.Size.y;j++)
            {
                if(tempWorldSpace[newRoom.location.x + i, newRoom.location.y + j])
                {
                    return false;
                }
                else
                {
                    tempWorldSpace[newRoom.location.x + i, newRoom.location.y + j] = true;
                }
            }
        }
        worldSpace = tempWorldSpace;
        return true;
    }


    void GenerateFloor()
    {
        //Place A room

        //Place up to 5 floor tiles

        //Try to place a room or two;
        int roomNumber = 0;
        Room tempRoom = new Room();
        roomsData = new List<Room>();
        Doors = new List<CDoor>();

        tempRoom.location = new Vector2Int((int)Random.Range(2, WorldSize.x - 2), (int)Random.Range(2, WorldSize.y - 2));
        tempRoom.Size = startingRoom.GetComponent<Room>().Size;
        tempRoom.roomType = RoomType.Start;
        tempRoom.DoorLocations = startingRoom.GetComponent<Room>().DoorLocations;
        tempRoom.DoorFacingDirections = startingRoom.GetComponent<Room>().DoorFacingDirections;
        tempRoom.roomNumber = 0;
        tempRoom.connectedRooms = new List<int>();
        roomsData.Add(tempRoom);

        int doorNumber = -1;

        foreach (Vector2 doorPos in roomsData[roomNumber].DoorLocations)
        {
            doorNumber++;
            var numberOfFloors = Random.Range(4, 6);
            var CurrentFacingDirection = roomsData[roomNumber].DoorFacingDirections[doorNumber];
            for (int i = 0; i < numberOfFloors; i++)
            {
                if (CurrentFacingDirection == FacingDirection.Down)
                {

                }
                if (CurrentFacingDirection == FacingDirection.Left)
                {

                }
                if (CurrentFacingDirection == FacingDirection.Up)
                {

                }
                if (CurrentFacingDirection == FacingDirection.Right)
                {

                }




            }
        }

    }

}
