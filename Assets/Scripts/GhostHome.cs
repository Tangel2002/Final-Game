using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;
    public Vector3 homeVector;
    private void OnEnable()
    {
        ghost.movement.SetDirection(Vector2Int.up, true);
        ghost.movement.speedMultiplier = 0.6f;
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        if (this.gameObject.activeSelf) {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacles")) {
            ghost.movement.SetDirection(-ghost.movement.currentDirection, true);
        }
    }

    private IEnumerator ExitTransition() {
        ghost.movement.SetDirection(Vector2Int.up, true);
        ghost.movement.body.isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = this.transform.position;
        Vector3 newPosition = position;
        float duration = 0.5f;
        float timeElapsed = 0.0f;

        while (timeElapsed < duration) {
            newPosition = Vector3.Lerp(position, inside.transform.position, timeElapsed / duration);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timeElapsed = 0.0f;

        while (timeElapsed < duration)
        {
            newPosition = Vector3.Lerp(inside.transform.position, outside.transform.position, timeElapsed / duration);
            newPosition.z = position.z;
            ghost.transform.position = newPosition;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        ghost.SetDirectionFromTarget();
        ghost.movement.body.isKinematic = false;
        ghost.movement.enabled = true;
        if (!ghost.frightened.enabled)
        {
            ghost.movement.speedMultiplier = 1.0f;
        }
    }
}
