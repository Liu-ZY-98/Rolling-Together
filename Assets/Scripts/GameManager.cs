using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum PlayerColor
    {
        BLACK,
        PURPLE,
        RED,
        BLUE
    }

    public TextMeshProUGUI GameOverText;

    private PlayerColor playerColor;

    void Awake()
    {
        playerColor = PlayerColor.BLACK;
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        GameOverText.gameObject.SetActive(true);
    }

    public PlayerColor GetPlayerColor() => playerColor;

    public void SetPlayerColor(PlayerColor newPlayerColor)
    {
        playerColor = newPlayerColor;
    }
}
