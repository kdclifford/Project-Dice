using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTile : MonoBehaviour
{
    public Vector2Int position;
    public bool northWall;
    public bool southWall;
    public bool eastWall;
    public bool westWall;
    
    public CTile(Vector2Int Position ,bool NorthWall,bool SouthWall,bool EastWall,bool WestWall)
    {
        position = Position;
        northWall = NorthWall;
        southWall = SouthWall;
        eastWall  = EastWall;
        westWall = WestWall;
    }

}
