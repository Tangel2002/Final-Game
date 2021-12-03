using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2 position;
    public bool traversable;
    public Node(Vector2 aPosition, bool aTraversable) {
        position = aPosition;
        traversable = aTraversable;
    }
}
