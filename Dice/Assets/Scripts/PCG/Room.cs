using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<int> connectedRooms;
    public List<GameObject> enemyList;
    public Vector2 Size;
    public List<Vector2Int> DoorLocations = new List<Vector2Int>();
    public List<FacingDirection> DoorFacingDirections = new List<FacingDirection>();
    public RoomType roomType;
    public Vector2Int location; 
    public int preFabNumber;
    public int roomNumber;
   // [HideInInspector]
    public int roomSpawnPoints;
    public List<Vector4> chestSpawnLocations; //4th value will be used to indidcate the rotation of the chest

    // Start is called before the first frame update

}
