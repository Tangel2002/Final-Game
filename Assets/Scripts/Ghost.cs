using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public StageGrid grid { get; private set; }
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostFrightened frightened { get; private set; }
    public GhostSoul soul { get; private set; }
    public GhostBehavior initialBehavior;
    public Vector2Int targetNode;
    private Transform target;
    public int points = 200;

    private void Awake()
    {
        grid = FindObjectOfType<StageGrid>();
        movement = this.GetComponent<Movement>();
        home = this.GetComponent<GhostHome>();
        chase = this.GetComponent<GhostChase>();
        scatter = this.GetComponent<GhostScatter>();
        frightened = this.GetComponent<GhostFrightened>();
        soul = this.GetComponent<GhostSoul>();
    }
    private void Start()
    {
        ResetState();
    }
    public void SetDirectionFromTarget()
    {
        Vector2Int currentCoords = grid.CoordinatesFromWorldPoint(this.transform.position);
        Vector2Int[] possibleDirections = new Vector2Int[4];
        Vector2Int newCoords;
        for (int i = 0; i < 4; i++) {
            newCoords = currentCoords + new Vector2Int(Mathf.RoundToInt(Mathf.Cos(Mathf.PI/(i+1))), Mathf.RoundToInt(Mathf.Sin(Mathf.PI/(i+1))));
            if (grid.NodeFromCoordinates(newCoords).traversable) {
                possibleDirections[i] = newCoords;
            }
        }
    }
    public void ResetState() {
        this.gameObject.SetActive(true);
        movement.ResetState();
        
        chase.Disable();
        frightened.Disable();
        soul.Disable();
        scatter.Enable();
        if (home != initialBehavior) {
            home.Disable();
        }
        if (initialBehavior != null) {
            initialBehavior.Enable();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            FindObjectOfType<GameManager>().GhostEaten(this);
        }
        else {
            FindObjectOfType<GameManager>().PacmanEaten();
        }
    }
}
