using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePickup : MonoBehaviour
{
    public Color color;
        private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectsOfType<GameManager>()[0];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SpriteRenderer>().color = color;
            gameManager.SetPlayerColor(GameManager.PlayerColor.PURPLE);
            Destroy(gameObject);
        }
    }
}
