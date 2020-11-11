using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public  Vector2 doorLocation;
    public  int roomNumber;
    public bool connected= false;
    public FacingDirection facingDirection;

    public Door(Vector2 location ,int RoomNumber)
    {
        doorLocation = location;
        roomNumber = RoomNumber;

    }

}
