using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{
    public Vector2Int Position;
    public AStarNode Parent;
    public bool Walkable;

    public int GCost; // Distance from the start node
    public int HCost; // Heuristic cost to the target node
    public int FCost => GCost + HCost; // Total cost

    public AStarNode(Vector2Int position, bool walkable)
    {
        Position = position;
        Walkable = walkable;
    }
}
