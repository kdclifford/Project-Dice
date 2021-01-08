using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<int> connectedRooms;
    public Vector2 Size;
    public List<Vector2Int> DoorLocations = new List<Vector2Int>();
    public List<FacingDirection> DoorFacingDirections = new List<FacingDirection>();
    public RoomType roomType;
    public Vector2Int location; 
    public int preFabNumber;
    public int roomNumber;
   // [HideInInspector]
    public int roomSpawnPoints;
    // Start is called before the first frame update

}
