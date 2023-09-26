using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredPlatform : MonoBehaviour
{
    public GameManager.PlayerColor AllowedColor;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectsOfType<GameManager>()[0];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && (other.gameObject.GetComponent<PlayerController>().PlayerColor != AllowedColor || gameManager.GetPlayerColor() != AllowedColor))
        {
            Destroy(other.gameObject);
            gameManager.EndGame();
        }
    }
}
