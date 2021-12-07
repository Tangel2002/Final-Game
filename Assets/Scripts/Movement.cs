using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    public static Dictionary<int, Vector2Int> directions = new Dictionary<int, Vector2Int>{
        {0,Vector2Int.up},
        {1,Vector2Int.left},
        {2,Vector2Int.down},
        {3, Vector2Int.right}
    };

    public float speed = 8f;
    public float speedMultiplier = 1f;
    public int initialDirection;
    public LayerMask obstacles;

    public Rigidbody2D body { get; private set; }
    public Vector2Int currentDirection { get; private set; }
    public Vector2Int nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        body = this.GetComponent<Rigidbody2D>();
        startingPosition = this.transform.position;
        ResetState();
    }
    public void ResetState() {
        speedMultiplier = 1f;
        nextDirection = Vector2Int.zero;
        SetDirection(initialDirection);
        this.transform.position = startingPosition;
        body.isKinematic = false;
        this.enabled = true;
    }
    private void Update()
    {
        if (nextDirection != Vector2Int.zero) {
            SetDirection(nextDirection);
        }
    }
    private void FixedUpdate()
    {
        Vector2 position = body.position;
        Vector2 translation = (Vector2)currentDirection * speed * speedMultiplier * Time.fixedDeltaTime;
        body.MovePosition(position + translation);
    }
    public void SetDirection(int directionKey, bool forced = false) {
        Vector2Int direction;
        if (directions.TryGetValue(directionKey, out direction))
        {
            SetDirection(direction, forced);
        }
        else {
            currentDirection = directions[0];
        }
    }
    public void SetDirection(Vector2Int aDirection, bool forced = false)
    {
        if (!Occupied(aDirection) || forced)
        {
            currentDirection = aDirection;
            nextDirection = Vector2Int.zero;
        }
        else
        {
            nextDirection = aDirection;
        }
    }
    public bool Occupied(Vector2Int aDirection) {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0, aDirection, 1.5f, obstacles);
        return hit.collider != null;
    }
}
