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
    public Vector2Int targetNode { get; private set; }
    public Transform target;
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
    public void SetDirectionFromTarget(Vector3 aPosition = default, bool aSpecial = false)
    {
        Vector2Int currentCoords;
        if (aPosition == default)
        {
            currentCoords = grid.CoordinatesFromWorldPoint(this.transform.position);
        }
        else {
            currentCoords = grid.CoordinatesFromWorldPoint(aPosition);
        }
        
        float[] possibleDirections = new float[4];
        Vector2Int newCoords;
        float currentMin = 999;
        int minIndex = -1;
        foreach (KeyValuePair<int, Vector2Int> pair in Movement.directions)
        {
            if ((aSpecial && pair.Key == 0) || (pair.Value == -movement.currentDirection))
            {
                possibleDirections[pair.Key] = 999f;
                continue;
            }
            newCoords = currentCoords + pair.Value;
            if (grid.NodeFromCoordinates(newCoords).traversable)
            {
                possibleDirections[pair.Key] = Vector2Int.Distance(newCoords, targetNode);
            }
            else
            {
                possibleDirections[pair.Key] = 999f;
            }
        }
        for (int i = 0; i < possibleDirections.Length; i++) {
            if (possibleDirections[i] < currentMin) {
                currentMin = possibleDirections[i];
                minIndex = i;
            }
        }
        movement.SetDirection(minIndex);
    }

    public void SetDirectionRandom(Vector3 aPosition = default) {
        Vector2Int currentCoords;
        if (aPosition == default)
        {
            currentCoords = grid.CoordinatesFromWorldPoint(this.transform.position);
        }
        else
        {
            currentCoords = grid.CoordinatesFromWorldPoint(aPosition);
        }

        List<int> possibleDirections = new List<int>();
        Vector2Int newCoords;
        foreach (KeyValuePair<int, Vector2Int> pair in Movement.directions)
        {
            if (pair.Value == -movement.currentDirection)
            {
                continue;
            }
            newCoords = currentCoords + pair.Value;
            if (grid.NodeFromCoordinates(newCoords).traversable)
            {
                possibleDirections.Add(pair.Key);
            }
            movement.SetDirection(possibleDirections[Random.Range(0,possibleDirections.Count)]);
        }
    }
    public void ResetState() {
        this.gameObject.SetActive(true);
        movement.ResetState();
        targetNode = grid.CoordinatesFromWorldPoint(movement.startingPosition);
        chase.Disable();
        frightened.Disable();
        soul.Disable();
        scatter.Enable();
        home.Disable();
        
        if (initialBehavior != null) {
            initialBehavior.Enable();
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!frightened.eaten) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                if (frightened.enabled) { 
                    FindObjectOfType<GameManager>().GhostEaten(this);
                }
                else
                {
                    FindObjectOfType<GameManager>().PacmanEaten();
                }
            }
        }
            
    }
    public void SetTargetNode (Vector2Int nodeCoords){
        targetNode = nodeCoords;
    }
}
