using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public AStarPathfinding pathfinding;
    public Transform player;
    public float speed = 5f;
    private List<Vector3> path;
    private int targetIndex;

    void Update()
    {
        path = pathfinding.FindPath(transform.position, player.position);
        if (path != null && path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    void MoveAlongPath()
    {
        if (targetIndex < path.Count)
        {
            Vector3 targetPosition = path[targetIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                targetIndex++;
            }
        }
    }
}
