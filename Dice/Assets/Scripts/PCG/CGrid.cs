using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGrid
{

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int y;
    }

    private int width;
    private int height;
    private CPathNode[,] gridArray;

    public void MakeNonTravesable(int x,int y)
    {
        gridArray[x, y].isWalkable = false;
    }
    public CGrid(int width, int height, CGrid g)
    {
        this.width = width;
        this.height = height;


        gridArray = new CPathNode[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = new CPathNode(this, x, y);
            }
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public void SetGridObjectCameFrom(int x, int y, Vector2Int CameFromNode)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y].cameFromNode = CameFromNode;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }



    public void SetGridObject(int x, int y, CPathNode value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }

    public void SetGridObject(Vector2 worldPosition, CPathNode value)
    {
       
        SetGridObject((int)worldPosition.x, (int)worldPosition.y ,value);
    }

    public CPathNode GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(CPathNode);
        }
    }

    public CPathNode GetGridObject(Vector2 worldPosition)
    {

        return GetGridObject((int)worldPosition.x, (int)worldPosition.y);
    }

}
