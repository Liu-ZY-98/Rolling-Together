using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectsOfType<GameManager>()[0];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            gameManager.EndGame();
        }
    }
}
