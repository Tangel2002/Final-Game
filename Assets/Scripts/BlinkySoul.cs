using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkySoul : GhostSoul
{
    //Targets space 1 in front of pacman
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled)
        {
            Vector2Int targetCoords = ghost.grid.CoordinatesFromWorldPoint(ghost.target.position) + ghost.target.gameObject.GetComponent<Movement>().currentDirection;
            ghost.SetTargetNode(targetCoords);
            ghost.SetDirectionFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
