using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    public TextMeshProUGUI GameTimer;

    public GameObject PauseMenu;

    public GameObject PauseButton;

    public GameObject HelpText;

    public GameObject Player;

    public GameObject RedPlayer;

    public GameObject BluePlayer;

    public float MergeDistance;

    private float elapsedTime;

    private PlayerColor playerColor;

    private FollowPlayer followPlayer;

    void Awake()
    {
        playerColor = PlayerColor.BLACK;
        followPlayer = FindObjectsOfType<FollowPlayer>()[0];
    }

    void Update()
    {
        // Timer
        elapsedTime += Time.deltaTime;
        GameTimer.text = $"{elapsedTime.ToString("F2")}s";

        // Split and merge.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // We can't do anything.
            if (playerColor == PlayerColor.BLACK) return;
            if (playerColor == PlayerColor.PURPLE)
            {
                Split();
                int randomInt = Random.Range(0, 10);
                if (randomInt % 2 == 0)
                {
                    SetPlayerColor(PlayerColor.RED);
                }
                else
                {
                    SetPlayerColor(PlayerColor.BLUE);
                }

                SetActivePlayer(playerColor);
            }
            else
            {
                Merge();
            }
        }

        // Switch balls.
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerColor == PlayerColor.RED)
            {
                Debug.Log("Switching to blue");
                SetPlayerColor(PlayerColor.BLUE);
                SetActivePlayer(playerColor);
            }
            else if (playerColor == PlayerColor.BLUE)
            {

                Debug.Log("Switching to red");
                SetPlayerColor(PlayerColor.RED);
                SetActivePlayer(playerColor);
            }
            else
            {
                // Do nothing.
            }
        }
    }

    public void EndGame()
    {
        GameOverText.gameObject.SetActive(true);
        Pause();
    }

    public PlayerColor GetPlayerColor() => playerColor;

    public void SetPlayerColor(PlayerColor newPlayerColor)
    {
        playerColor = newPlayerColor;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
        HelpText.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void Restart()
    {
        // Get current scene name
        string scene = SceneManager.GetActiveScene().name;
        // Load it
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

    public void Help()
    {
        HelpText.SetActive(true);
    }

    public void Win()
    {
        GameOverText.text = $"Congratulations! You have completed the level in {elapsedTime.ToString("F2")} seconds!";
        GameOverText.gameObject.SetActive(true);
    }

    private void Split()
    {
        Player.SetActive(false);
        Player.GetComponent<PlayerController>().enabled = false;

        Vector3 playerPosition = Player.transform.position;
        Vector3 redPosition = playerPosition + Vector3.right;
        Vector3 bluePosition = playerPosition - Vector3.right;

        RedPlayer.SetActive(true);
        RedPlayer.transform.position = redPosition;
        RedPlayer.GetComponent<PlayerController>().enabled = false;

        BluePlayer.SetActive(true);
        BluePlayer.transform.position = bluePosition;
        BluePlayer.GetComponent<PlayerController>().enabled = false;
    }

    private void Merge()
    {
        Vector3 redPosition = RedPlayer.transform.position;
        Vector3 bluePosition = BluePlayer.transform.position;

        // We must not merge too distant balls.
        if (Vector3.Distance(redPosition, bluePosition) > MergeDistance) return;

        Vector3 newPlayerPosition = (redPosition + bluePosition) / 2;
        Player.transform.position = newPlayerPosition;

        RedPlayer.SetActive(false);
        BluePlayer.SetActive(false);

        Player.SetActive(true);
        Player.GetComponent<PlayerController>().enabled = true;

        SetPlayerColor(PlayerColor.PURPLE);
        SetActivePlayer(playerColor);
    }

    private void SetActivePlayer(PlayerColor activePlayerColor)
    {
        Player.GetComponent<PlayerController>().enabled = false;
        RedPlayer.GetComponent<PlayerController>().enabled = false;
        BluePlayer.GetComponent<PlayerController>().enabled = false;


        if (playerColor == PlayerColor.RED)
        {
            RedPlayer.GetComponent<PlayerController>().enabled = true;
            followPlayer.player = RedPlayer;
        }
        else if (playerColor == PlayerColor.BLUE)
        {
            BluePlayer.GetComponent<PlayerController>().enabled = true;
            followPlayer.player = BluePlayer;
        }
        else
        {
            Player.GetComponent<PlayerController>().enabled = true;
            followPlayer.player = Player;
        }
    }
}
