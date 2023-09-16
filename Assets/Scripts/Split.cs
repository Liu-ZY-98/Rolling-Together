using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Duplicate();
        }
    }

    void Duplicate()
    {
        Vector3 spawnPosition = playerPrefab.transform.position + new Vector3(2, 0, 0);
        transform.localScale *= 0.8f;
        GameObject newPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

        newPlayer.transform.localScale = transform.localScale;
    }
}
