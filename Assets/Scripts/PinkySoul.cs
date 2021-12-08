using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkySoul : GhostSoul
{
    public override void OnEnable()
    {
        ghost.target = soul;
    }
    //targets the direction away from the soul
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled)
        {
            Vector2Int targetCoords = ghost.grid.CoordinatesFromWorldPoint(ghost.target.position);
            ghost.SetTargetNode(targetCoords);
            ghost.SetDirectionAwayFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
