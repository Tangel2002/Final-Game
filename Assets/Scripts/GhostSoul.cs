using UnityEngine;

public class GhostSoul : GhostBehavior 
{
    public Transform soul;
    public virtual void OnEnable()
    {
        try
        {
            if (!ghost.frightened.enabled)
            {
                ghost.target = GameObject.Find("Pacman").transform;
            }
        }
        catch (System.NullReferenceException) { 
        
        }
    }
    public void OnDisable()
    {
        try
        {
            if (!ghost.frightened.enabled)
            {
                if (ghost.chase.enabled)
                {
                    ghost.chase.OnEnable();
                }
                else if (ghost.scatter.enabled)
                {
                    ghost.scatter.OnEnable();
                }
            }
        }
        catch (System.NullReferenceException)
        {

        }
    }
}
