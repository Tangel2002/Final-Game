using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScript : MonoBehaviour
{
    public Movement movement { get; private set; }

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.movement.SetDirection(Vector2Int.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.movement.SetDirection(Vector2Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.movement.SetDirection(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.movement.SetDirection(Vector2Int.right);
        }
        //this changes rotation
        float angle = Mathf.Atan2(this.movement.currentDirection.y, this.movement.currentDirection.x);
        this.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

        //this is the stamina drain
        if (Input.GetKey(KeyCode.Space))
        {
            Stamina.instance.UseStamina(1);

            if(Stamina.instance.currentStamina >= 0)
            {
                this.movement.speedMultiplier = 1.5f;
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.movement.speedMultiplier = 1.0f;
        }
        if (Stamina.instance.currentStamina == 0)
        {
            this.movement.speedMultiplier = 1.0f;
        }
    }
    public void ResetState()
    {
        this.enabled = true;
        this.movement.ResetState();
        this.gameObject.SetActive(true);
    }
}
