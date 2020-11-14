using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{
    private Grid grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;


    public bool isWalkable;
    public Vector2Int cameFromNode;

    public PathNode(Grid g,int x, int y)
    {
        this.grid = g;
        this.x = x;
        this.y = y;
        isWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
