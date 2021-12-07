using UnityEngine;

public abstract class GhostChase : GhostBehavior
{
    public virtual void OnEnable()
    {
        if (!ghost.frightened.enabled)
        {
            ghost.target = GameObject.Find("Pacman").transform;
        }
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
