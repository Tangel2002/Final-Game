using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeChase : GhostChase
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled)
        {
            if (Vector3.Distance(ghost.transform.position, ghost.target.position) > 8f) {
                ghost.SetTargetNode(ghost.grid.CoordinatesFromWorldPoint(ghost.target.position));
            }
            else {
                ghost.SetTargetNode(ghost.scatter.scatterTarget);
            }
            ghost.SetDirectionFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
