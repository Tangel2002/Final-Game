using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    public float speedMultiplier;
    private float initialSpeed;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ghosts")) {
            initialSpeed = collision.gameObject.GetComponent<Ghost>().movement.speedMultiplier;
            collision.gameObject.GetComponent<Ghost>().movement.speedMultiplier = this.speedMultiplier;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ghosts"))
        {
            if (collision.gameObject.GetComponent<Ghost>().movement.speedMultiplier == speedMultiplier)
            {
                collision.gameObject.GetComponent<Ghost>().movement.speedMultiplier = initialSpeed;
            }
        }
       
    }
}
