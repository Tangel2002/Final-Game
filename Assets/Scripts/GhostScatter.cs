using UnityEngine;

public class GhostScatter : GhostBehavior
{
    public Vector2Int scatterTarget;
    public void OnEnable()
    {
        ghost.SetTargetNode(scatterTarget);
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled && !this.ghost.frightened.enabled) {
            ghost.SetDirectionFromTarget(other.tag == "NoUp");
        }
    }
}
