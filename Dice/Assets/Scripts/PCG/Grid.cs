using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    public int width;
    public  int height;
    public  PathNode[,] gridArray;
    public Grid(int width, int height,List<Room> Rooms)
    {
        this.width = width;
        this.height = height;
        gridArray = new PathNode[width, height];
        for(int x =0; x< width;x++)
        {
            for (int y = 0; y < width; y++)
            {
                gridArray[x, y] = new PathNode(x, y);
            }
        }

        for(int i = 0; i<Rooms.Count;i++)
        {

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