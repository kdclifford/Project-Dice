using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private Grid grid ;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public int gridWidth;
    public int gridHeight;

    public PathFinding(int width,int height,List<Room> rooms)
    {
        grid = new Grid(width,height,rooms);

    }

    public List<PathNode> FindPath(Vector2 start, Vector2 end, int width, int height)
    {
        PathNode startNode = new PathNode((int)start.x, (int)start.y);
        PathNode endNode = new PathNode((int)end.x, (int)end.y);
        gridWidth = width;
        gridHeight = height;

        openList = new List<PathNode>();
        closedList = new List<PathNode>();

        for (int x = 0; x< grid.width;x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                grid.gridArray[x,y].gCost = int.MaxValue;
                grid.gridArray[x, y].CalculateFCost();
                grid.gridArray[x, y].cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);

            if (currentNode == endNode)
            {
                CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }

            }
        }
        //Out of Nodes Meaning no path

        return null;


    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            //Left
            neighbourList.Add(new PathNode(currentNode.x - 1, currentNode.y));
            //Down Left
            if (currentNode.y - 1 >= 0)
            {
                neighbourList.Add(new PathNode(currentNode.x - 1, currentNode.y - 1));
            }
            //Up Left
            if (currentNode.y + 1 < gridHeight)
            {
                neighbourList.Add(new PathNode(currentNode.x - 1, currentNode.y + 1));
            }

        }

        if (currentNode.x + 1 < gridWidth)
        {
            //Right
            neighbourList.Add(new PathNode(currentNode.x + 1, currentNode.y));
            //Down Right
            if (currentNode.y - 1 >= 0)
            {
                neighbourList.Add(new PathNode(currentNode.x + 1, currentNode.y - 1));
            }
            //Up Right
            if (currentNode.y + 1 < gridHeight)
            {
                neighbourList.Add(new PathNode(currentNode.x + 1, currentNode.y + 1));
            }
        }

        //Down
        if (currentNode.y - 1 >= 0)
        {
            neighbourList.Add(new PathNode(currentNode.x, currentNode.y - 1));
        }
        //Up
        if (currentNode.y + 1 < gridHeight)
        {
            neighbourList.Add(new PathNode(currentNode.x, currentNode.y + 1));
        }

        return neighbourList;
    }



    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFcostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFcostNode.fCost)
            {
                lowestFcostNode = pathNodeList[i];
            }
        }
        return lowestFcostNode;
    }


    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDisnatece = (int)Mathf.Abs(a.x - b.x);
        int yDisnatece = (int)Mathf.Abs(a.y - b.y);

        int remaining = Mathf.Abs(xDisnatece - yDisnatece);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDisnatece, yDisnatece) + MOVE_STRAIGHT_COST * remaining;

    }
}
