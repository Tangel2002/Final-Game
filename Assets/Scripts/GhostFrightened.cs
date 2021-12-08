using System.Collections;
using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    public bool eaten { get; private set; }

    public override void Enable(float aDuration)
    {
        base.Enable(aDuration);
        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), aDuration / 4f);
    }

    public override void Disable()
    {
        if (!eaten) {
            base.Disable();
        }

    }
    private void OnEnable()
    {
        ghost.movement.speedMultiplier = 0.5f;
        eaten = false;
    }
    private void OnDisable()
    {
        ghost.movement.speedMultiplier = 1f;
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
        if (ghost.chase.enabled)
        {
            ghost.chase.OnEnable();
        }
        else if (ghost.scatter.enabled) {
            ghost.scatter.OnEnable();
        }
        if (ghost.soul.enabled) {
            ghost.soul.OnEnable();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman") && !eaten)
        {
            if (this.enabled) {
                Eaten();
            }
        }
    }

    private void Flash() {
        if (!eaten) {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten() {
        eaten = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
        ghost.movement.speedMultiplier = 2.5f;
        ghost.gameObject.layer = LayerMask.NameToLayer("Eye Ghosts");
        StopAllCoroutines();
        ghost.SetTargetNode(ghost.grid.CoordinatesFromWorldPoint(ghost.movement.startingPosition));
        ghost.SetDirectionFromTarget();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (eaten) {
            if (other.gameObject.layer == LayerMask.NameToLayer("Nodes"))
            {
                ghost.SetDirectionFromTarget(other.transform.position);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Home")) {
                StartCoroutine(EnterHome());
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Nodes") && this.enabled)
        {
            ghost.SetDirectionRandom(other.transform.position);
        }
    }

    public IEnumerator EnterHome() {
        ghost.movement.SetDirection(Vector2Int.down, true);
        ghost.movement.body.isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        Vector3 newPosition = position;
        float duration = 0.2f;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            newPosition = Vector3.Lerp(position, ghost.home.inside.transform.position, timeElapsed / duration);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0.0f;
        if (ghost.home.homeVector != ghost.home.inside.transform.position) {
            while (timeElapsed < duration)
            {
                newPosition = Vector3.Lerp(ghost.home.inside.transform.position, ghost.home.homeVector, timeElapsed / duration);
                newPosition.z = position.z;
                ghost.transform.position = newPosition;
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        body.enabled = true;
        ghost.movement.body.isKinematic = false;
        ghost.gameObject.layer = LayerMask.NameToLayer("Ghosts");
        eaten = false;
        this.Disable();
        ghost.home.Enable(5);
    }
}
