using UnityEngine;

public abstract class GhostChase : GhostBehavior
{
    public virtual void OnEnable()
    {
        ghost.target = GameObject.Find("Pacman").transform;
    }
    public virtual void OnDisable()
    {
        try {
            ghost.movement.SetDirection(-ghost.movement.currentDirection, true);
            ghost.scatter.Enable();
        }
        catch (System.NullReferenceException) {
        }
    }
}
