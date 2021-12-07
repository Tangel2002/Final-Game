using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodStain : Pellet
{
    protected void pickup()
    {
        FindObjectOfType<GameManager>().BloodStainPickUp(this);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            pickup();
        }
    }
}
