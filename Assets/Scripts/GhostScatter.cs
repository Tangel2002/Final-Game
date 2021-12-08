using UnityEngine;

public class GhostScatter : GhostBehavior
{
    public Vector2Int scatterTarget;
    public void OnEnable()
    {
        if (!ghost.frightened.enabled && !ghost.soul.enabled) {
            ghost.SetTargetNode(scatterTarget);
        }
    }
    public void OnDisable()
    {
        try {

            ghost.movement.SetDirection(-ghost.movement.currentDirection, true);
            this.ghost.chase.Enable();
        }
        catch (System.NullReferenceException) { 
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !this.ghost.frightened.enabled && !this.ghost.soul.enabled) {
            ghost.SetDirectionFromTarget(other.transform.position, other.tag == "NoUp");
        }
    }
}
