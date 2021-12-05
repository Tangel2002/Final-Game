using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float defaultDuration = 10f;

    private void Awake()
    {
        ghost = this.GetComponent<Ghost>();
        this.enabled = false;
    }
    public void Enable()
    {
        Enable(defaultDuration);
    }
    public virtual void Enable(float aDuration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), aDuration);
    }
    public virtual void Disable() {
        this.enabled = false;
        CancelInvoke();
    }
}
