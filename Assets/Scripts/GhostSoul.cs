using UnityEngine;

public class GhostSoul : GhostBehavior 
{
    public Transform soul;
    public virtual void OnEnable()
    {
        if (!ghost.frightened.enabled)
        {
            ghost.target = GameObject.Find("Pacman").transform;
        }
    }
}
