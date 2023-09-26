using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColoredButton : MonoBehaviour
{
    public GameManager.PlayerColor ActivatorColor;

    public bool IsPressed;

    public GameObject UnpressedButton;

    public GameObject PressedButton;

    public UnityEvent Action;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectsOfType<GameManager>()[0];
        if (IsPressed)
        {
            UnpressedButton.SetActive(false);
            PressedButton.SetActive(true);
        }
        else
        {
            UnpressedButton.SetActive(true);
            PressedButton.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsPressed && other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().PlayerColor == ActivatorColor)
        {
            IsPressed = true;
            UnpressedButton.SetActive(false);
            PressedButton.SetActive(true);
            Action.Invoke();
        }
    }
}
