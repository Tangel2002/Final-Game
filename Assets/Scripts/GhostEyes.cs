using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public Sprite[] eyeSprites;
    public Movement movement { get; private set; }
    public SpriteRenderer eyeRenderer { get; private set; }
    public Dictionary<Vector2Int, Sprite> spriteDict = new Dictionary<Vector2Int, Sprite>();
    private void Awake()
    {
        eyeRenderer = this.GetComponent<SpriteRenderer>();
        movement = this.GetComponentInParent<Movement>();
        for (int i = 0; i < 4; i++)
        {
            spriteDict.Add(Movement.directions[i], eyeSprites[i]);
        }
        
    }
    private void Update()
    {
        Sprite newEyes;
        if (spriteDict.TryGetValue(movement.currentDirection, out newEyes))
        {
            eyeRenderer.sprite = newEyes;
        }
    }
}
