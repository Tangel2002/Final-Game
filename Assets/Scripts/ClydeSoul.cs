using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeSoul : GhostSoul 
{
    public override void OnEnable()
    {
        ghost.target = soul;
    }
    //Targets the soul if farther than 6 away, targets the corner nearest the soul if 6 or less
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled)
        {
            if (Vector3.Distance(ghost.transform.position, ghost.target.position) > 6)
            {
                ghost.SetTargetNode(ghost.grid.CoordinatesFromWorldPoint(ghost.target.position));
            }
            else
            {
                ghost.SetTargetNode(ghost.grid.CornerFromWorldPoint(ghost.target.position));
            }
            ghost.SetDirectionFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
