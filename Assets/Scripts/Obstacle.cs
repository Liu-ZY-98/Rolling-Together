using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle: MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        var otherRenderer = col.GetComponent<SpriteRenderer>();
        bool fullyCovers = CheckIfFullyCovers(spriteRenderer, otherRenderer);
        Debug.Log($"Covered: {fullyCovers}");
        if (fullyCovers)
        {
            gameObject.SetActive(false);
        }
    }

    private bool CheckIfFullyCovers(SpriteRenderer spriteRenderer1, SpriteRenderer spriteRenderer2)
    {
        Bounds bounds1 = spriteRenderer1.bounds;
        Bounds bounds2 = spriteRenderer2.bounds;

        return bounds2.size.x >= bounds1.size.x && bounds2.size.y >= bounds2.size.y;
    }
       
}
