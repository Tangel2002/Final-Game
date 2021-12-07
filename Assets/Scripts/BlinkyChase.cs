using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChase : GhostChase
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled)
        {
            ghost.SetTargetNode(ghost.grid.CoordinatesFromWorldPoint(ghost.target.position));
            ghost.SetDirectionFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
