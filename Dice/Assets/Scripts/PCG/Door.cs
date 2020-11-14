using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public  Vector2Int doorLocation;
    public  int roomNumber;
    public bool connected= false;
    public FacingDirection facingDirection;

    public Door(Vector2Int location ,int RoomNumber)
    {
        doorLocation = location;
        roomNumber = RoomNumber;

    }
    public void UpdatePathStartingLocation()
    {
        if(facingDirection == FacingDirection.Down)
        {
            doorLocation+=new Vector2Int(0, -1 );
        }
        else if(facingDirection ==FacingDirection.Up)
        {

            doorLocation += new Vector2Int(0, 1 );

        }
        else if (facingDirection == FacingDirection.Left)
        {

            doorLocation +=new Vector2Int(-1,  0);

        }
        else if (facingDirection == FacingDirection.Right)
        {

            doorLocation +=new Vector2Int(-1, 0);

        }
 
    }
}
