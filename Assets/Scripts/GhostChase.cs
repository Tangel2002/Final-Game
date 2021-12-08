using UnityEngine;

public abstract class GhostChase : GhostBehavior
{
    public virtual void OnEnable()
    {
        try
        {
            if (!ghost.frightened.enabled && !ghost.soul.enabled)
            {
                ghost.target = GameObject.Find("Pacman").transform;
                if (!ghost.home.enabled)
                {
                    ghost.SetDirectionFromTarget();
                }
            }
        }
        catch (System.NullReferenceException) { 
        
        }
    }
    public virtual void OnDisable()
    {
        try {
            if (!ghost.soul.enabled) {
                ghost.movement.SetDirection(-ghost.movement.currentDirection, true);
            }
            ghost.scatter.Enable();
        }
        catch (System.NullReferenceException) {
        }
    }
}
