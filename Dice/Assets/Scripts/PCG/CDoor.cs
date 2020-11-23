using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDoor 
{
    // Start is called before the first frame update
    public  Vector2Int location;
    public  int roomNumber;
    public bool connected= false;
    public FacingDirection facingDirection;

    public CDoor()
    {

    }
    public CDoor(Vector2Int location ,int RoomNumber)
    {
        this.location = location;
        roomNumber = RoomNumber;

    }
    public Vector2Int doorPoisitoin()
    {
        if (facingDirection == FacingDirection.Down)
        {
            return (location + new Vector2Int(0, 1));
        }
        else if (facingDirection == FacingDirection.Up)
        {
            return (location + new Vector2Int(0, -1));
        }
        else if (facingDirection == FacingDirection.Left)
        {
            return (location + new Vector2Int(1, 0));
        }
        else if (facingDirection == FacingDirection.Right)
        {
            return (location + new Vector2Int(-1, 0));
        }
        return new Vector2Int();
    }

    public void UpdatePathStartingLocation()
    {
        if(facingDirection == FacingDirection.Down)
        {
            location+=new Vector2Int(0, -1 );
        }
        else if(facingDirection ==FacingDirection.Up)
        {

            location += new Vector2Int(0, 1 );

        }
        else if (facingDirection == FacingDirection.Left)
        {

            location +=new Vector2Int(-1,  0);

        }
        else if (facingDirection == FacingDirection.Right)
        {

            location +=new Vector2Int(1, 0);

        }

 
    }
}
