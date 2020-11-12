using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridObject>
{
    public int width;
    public  int height;
    private TGridObject[,] gridArray;
    public Grid(int width, int height, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        gridArray = new TGridObject[width, height];
        for(int x =0; x< width;x++)
        {
            for (int y = 0; y < width; y++)
            {
                gridArray[x, y] = createGridObject(this,x,y);
            }
        }

    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }   
}

//public void AddRooms(List<Room> Rooms)
//{
//    for (int i = 0; i < Rooms.Count; i++)
//    {
//        for (int x = 0; x < Rooms[i].Size.x; x++)
//        {
//            for (int y = 0; x < Rooms[i].Size.y; y++)
//            {
//                gridArray[(int)Rooms[i].location.x + x, (int)Rooms[i].location.y + y] = 0;
//            }
//        }
//    }
//}