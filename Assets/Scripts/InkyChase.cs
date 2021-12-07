using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyChase : GhostChase
{
    public Transform blinky;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled)
        {
            Vector2Int pacmanFront = ghost.grid.CoordinatesFromWorldPoint(ghost.target.position) + 2 * ghost.target.gameObject.GetComponent<Movement>().currentDirection;
            Vector2Int targetCoords = 2 * pacmanFront - ghost.grid.CoordinatesFromWorldPoint(blinky.position);
            ghost.SetTargetNode(targetCoords);
            ghost.SetDirectionFromTarget(other.tag == "NoUp");
        }
    }
}
