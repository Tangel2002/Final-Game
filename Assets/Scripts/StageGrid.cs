using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGrid : MonoBehaviour
{
    public Transform player;
    public LayerMask obstacleMask;
    public float nodeSize;
    public Vector2 gridSize;
    public Node[,] grid;
    private int gridX;
    private int gridY;

    private void Start()
    {
        gridX = Mathf.RoundToInt(gridSize.x);
        gridY = Mathf.RoundToInt(gridSize.y);
        CreateGrid();
    }
    private void CreateGrid() {
        grid = new Node[gridX, gridY];
        Vector3 bottomLeft = this.transform.position - (Vector3.right * gridSize.x/2 * nodeSize) - (Vector3.up * gridSize.y/2 * nodeSize);
        for (int x = 0; x < gridX; x++) {
            for (int y = 0; y < gridY; y++) {
                Vector3 currentPoint = bottomLeft + (Vector3.right * (float)(x + 0.5) * nodeSize) + (Vector3.up * (float)(y + 0.5) * nodeSize);
                bool walkable = !Physics.CheckBox(currentPoint, new Vector3(nodeSize / 2, nodeSize / 2, 0.5f), Quaternion.identity, obstacleMask);
                grid[x, y] = new Node(currentPoint, walkable);
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 aWorldPosition) {
        float percentX = Mathf.Clamp01((aWorldPosition.x + gridSize.x * nodeSize/2)/(gridSize.x * nodeSize));
        float percentY = Mathf.Clamp01((aWorldPosition.y + gridSize.y * nodeSize/2) /(gridSize.y * nodeSize));
        int x = Mathf.Clamp((int)(gridX * percentX), 0, (gridX - 1));
        int y = Mathf.Clamp((int)(gridY * percentY), 0, (gridY - 1));
        return grid[x, y];
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, new Vector3(gridSize.x * nodeSize, gridSize.y * nodeSize));
        if (grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node n in grid)
            {
                Gizmos.color = (n.traversable) ? Color.white : Color.red;
                if (n == playerNode) {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.position, Vector3.one * nodeSize * 0.9f);
            }
        }
    }
}
