using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkySoul : GhostSoul
{
    //targets the space directly between pacman and the soul
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !ghost.frightened.enabled)
        {
            Vector2Int pacman = ghost.grid.CoordinatesFromWorldPoint(ghost.target.position);
            Vector3 pacVector = new Vector3(pacman.x, pacman.y, ghost.target.position.z);
            Vector2Int targetCoords = ghost.grid.CoordinatesFromWorldPoint(pacVector - (soul.position / 2));
            ghost.SetTargetNode(targetCoords);
            ghost.SetDirectionFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
