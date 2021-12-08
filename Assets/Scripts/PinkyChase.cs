using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChase : GhostChase
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled && !ghost.soul.enabled)
        {
            Vector2Int targetCoords = ghost.grid.CoordinatesFromWorldPoint(ghost.target.position) + 4 * ghost.target.gameObject.GetComponent<Movement>().currentDirection;
            ghost.SetTargetNode(targetCoords);
            ghost.SetDirectionFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
