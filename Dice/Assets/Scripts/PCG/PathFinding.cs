using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    private CGrid grid;
    private List<CPathNode> openList;
    private List<CPathNode> closedList;

    public Pathfinding(int width, int height, List<Room> Rooms)
    {
        Instance = this;
        grid = new CGrid(width+5, height+5, grid);

        foreach(Room room in Rooms)
        {
            for(int x =0; x< + room.Size.x;x++)
            {
                for(int y=0; y< room.Size.y;y++)
                {
                    grid.MakeNonTravesable(room.location.x+x, room.location.y+y);
                }
            }
        }
    }

    public CGrid GetGrid()
    {
        return grid;
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        CPathNode startNode = grid.GetGridObject(start.x, start.y);
        CPathNode endNode = grid.GetGridObject(end.x, end.y);

        if (startNode == null  || endNode == null)
        {
            // Invalid Path
            return null;
        }

        openList = new List<CPathNode> { startNode };
        closedList = new List<CPathNode>();

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                CPathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = 99999999;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = new Vector2Int(-1,-1);
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();


        while (openList.Count > 0)
        {
            CPathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode.x == endNode.x && currentNode.y == endNode.y)
            {
                // Reached final node
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (CPathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if ( ListContains(closedList,neighbourNode)) continue;
                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = new Vector2Int(currentNode.x,currentNode.y);
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!ListContains(openList,neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    private bool ListContains(List<CPathNode> list , CPathNode Target)
    {
        foreach(CPathNode pathnode in list)
        {
            if(pathnode.x == Target.x && pathnode.y == Target.y)
            {
                return true;
            }
        }
        return false;
    }

    private List<CPathNode> GetNeighbourList(CPathNode currentNode)
    {
        List<CPathNode> neighbourList = new List<CPathNode>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
         
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
        }
        // Down
        if (currentNode.y - 1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
        // Up
        if (currentNode.y + 1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbourList;
    }

    public CPathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<Vector2Int> CalculatePath(CPathNode endNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        path.Add(new Vector2Int(endNode.x,endNode.y));
        CPathNode currentNode = endNode;
        while (currentNode.cameFromNode != new Vector2Int(-1,-1))
        { 
            


            path.Add(currentNode.cameFromNode);
            currentNode = grid.GetGridObject(currentNode.cameFromNode.x,currentNode.cameFromNode.y);
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(CPathNode a, CPathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private CPathNode GetLowestFCostNode(List<CPathNode> pathNodeList)
    {
        CPathNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

}
