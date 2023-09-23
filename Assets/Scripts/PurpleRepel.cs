using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleRepel : MonoBehaviour
{
    public float Force;

    private RaycastHit2D hit;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectsOfType<GameManager>()[0];
    }
    
    // Update is called once per frame
    void Update()
    {
        // int layerMask = 1 << 6;
        Vector2 center = transform.position;
        Vector2 size = new Vector2(3f, 3f);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(center, size, 0f, Vector2.up, 2f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player") && gameManager.GetPlayerColor() == GameManager.PlayerColor.PURPLE)
            {
                hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Force);
            }
        }

    }
}
