using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerball; // Reference to the player ball's Transform
    private GameObject follower;

    void Start()
    {
        // Initialize the follower to "purple" at the start
        SetFollower("purple");
    }

    void Update()
    {
        // Check for input to change the follower
        if (Input.GetKeyDown("1"))
        {
            SetFollower("purple");
        }
        if (Input.GetKeyDown("2"))
        {
            SetFollower("blue");
        }
        if (Input.GetKeyDown("3"))
        {
            SetFollower("red");
        }

        // Update the camera's position to follow the selected follower
        if (playerball != null)
        {
            transform.position = new Vector3(playerball.position.x, playerball.position.y, -10f);
        }
    }

    // Function to set the follower by name
    private void SetFollower(string followerName)
    {
        // Find the GameObject by name
        GameObject newFollower = GameObject.Find(followerName);

        // Check if the GameObject was found
        if (newFollower != null)
        {
            // Get the Transform component of the GameObject
            playerball = newFollower.transform;
            follower = newFollower;
        }
        else
        {
            Debug.LogError("GameObject with the name '" + followerName + "' not found.");
        }
    }
}

