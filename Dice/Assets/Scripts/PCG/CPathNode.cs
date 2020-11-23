using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPathNode 
{
    private CGrid grid;
    public int x;
    public int y; 

    public int gCost;
    public int hCost;
    public int fCost;


    public bool isWalkable;
    public Vector2Int cameFromNode;

    public CPathNode(CGrid g,int x, int y)
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
