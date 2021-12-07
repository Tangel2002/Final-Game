using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform exitPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 newPosition = exitPoint.position;
        newPosition.z = other.transform.position.z;
        other.transform.position = newPosition;
    }
}
