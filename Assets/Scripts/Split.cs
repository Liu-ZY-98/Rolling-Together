using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public GameObject playerPrefab;

    private List<GameObject> playerInstances = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerInstances.Add(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            var previousPlayers = new List<GameObject>(playerInstances);
            foreach (var player in previousPlayers)
            {
                Duplicate(player);
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Merge();
        }
    }

    void Duplicate(GameObject player)
    {   //the max spilt times is 3
        /*if (playerInstances.Count >= 3){ 
            return;
        }else{ */
        Vector3 spawnPosition = player.transform.position + new Vector3(2, 0, 0);
        Debug.Log($"Spawning at {spawnPosition}");
        player.transform.localScale *= 0.8f;
        GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        newPlayer.transform.localScale = player.transform.localScale;

        playerInstances.Add(newPlayer);
        
    }

    void Merge()
    {
        if (playerInstances.Count <= 1) return;
        var mergedPlayers = new List<GameObject>();
        var playersToDelete = new List<GameObject>();
        
        int halfSize = playerInstances.Count / 2;
        for (int i = 0; i < playerInstances.Count; ++i)
        {
            if (i < halfSize)
            {
                mergedPlayers.Add(playerInstances[i]);
            }
            else
            {
                playersToDelete.Add(playerInstances[i]);
            }
        }

        playerInstances = mergedPlayers;
        foreach (var playerToDelete in playersToDelete)
        {
            Destroy(playerToDelete);
        }

        foreach (var mergedPlayer in mergedPlayers)
        {
            mergedPlayer.transform.localScale *= 1.25f;
        }
    }
}
