using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGrid : MonoBehaviour
{
    public Transform player;
    public LayerMask obstacleMask;
    public float nodeSize;
    public Vector2Int gridSize;
    public Node[,] grid;
    private int gridX;
    private int gridY;

    private void Start()
    {
        gridX = gridSize.x;
        gridY = gridSize.y;
        CreateGrid();
    }
    private void CreateGrid() {
        grid = new Node[gridX, gridY];
        Vector2 bottomLeft = this.transform.position - (Vector3.right * gridSize.x / 2 * nodeSize) - (Vector3.up * gridSize.y / 2 * nodeSize);
        for (int x = 0; x < gridX; x++) {
            for (int y = 0; y < gridY; y++) {
                Vector2 currentPoint = bottomLeft + (Vector2.right * (float)(x + 0.5) * nodeSize) + (Vector2.up * (float)(y + 0.5) * nodeSize);
                bool walkable = (Physics2D.OverlapBox(currentPoint, new Vector2(nodeSize / 2, nodeSize / 2), 0f, obstacleMask) == null);
                grid[x, y] = new Node(currentPoint, walkable);
            }
        }
    }
    public Vector2Int CoordinatesFromWorldPoint(Vector3 aWorldPosition) {
        Vector3 lLocalPosition = aWorldPosition - this.transform.position;
        float percentX = Mathf.Clamp01((lLocalPosition.x + gridSize.x * nodeSize / 2) / (gridSize.x * nodeSize));
        float percentY = Mathf.Clamp01((lLocalPosition.y + gridSize.y * nodeSize / 2) / (gridSize.y * nodeSize));
        int x = (int)(gridX * percentX);
        int y = (int)(gridY * percentY);
        return new Vector2Int(x, y);
    }
    public Node NodeFromWorldPoint(Vector3 aWorldPosition) {
        return NodeFromCoordinates(CoordinatesFromWorldPoint(aWorldPosition));
    }
    public Node NodeFromCoordinates(Vector2Int coords) {
        int x = Mathf.Clamp(coords.x, 0, (gridX - 1));
        int y = Mathf.Clamp(coords.y, 0, (gridY - 1));
        return grid[x, y];
    }

    public Vector2Int CornerFromWorldPoint(Vector3 aWorldPosition) {
        Vector2 coords = CoordinatesFromWorldPoint(aWorldPosition);
        int x = Mathf.RoundToInt(Mathf.Clamp01(coords.x / gridX));
        int y = Mathf.RoundToInt(Mathf.Clamp01(coords.y / gridY));
        return new Vector2Int(x * (gridX-1),y * (gridY-1));
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, new Vector3(gridSize.x * nodeSize, gridSize.y * nodeSize));
        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            Node n;
            Vector2Int coords;
            for (int x = 0; x < gridX; x++) {

                for(int y = 0; y < gridY; y++)
                {
                    coords = new Vector2Int(x, y);
                    n = NodeFromCoordinates(coords);
                    Gizmos.color = (n.traversable) ? Color.white : Color.red;
                    if (n == playerNode)
                    {
                        Gizmos.color = Color.cyan;
                    }
                    else if (coords == new Vector2Int(25, 35)) {
                        Gizmos.color = Color.blue;
                    }
                    Gizmos.DrawCube(n.position, Vector3.one * nodeSize * 0.9f);
                }
            }
        }
    }*/
}
