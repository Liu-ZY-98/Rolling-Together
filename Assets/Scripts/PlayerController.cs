using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables for moving
    public float speed;
    public float movement;

    public float jump;
    public bool isJumping;

    private Rigidbody2D rb;
    
    // Variables for Splitting and Changing Colors
    public enum ColorType { None, Blue, Green, Purple, Red }
    public ColorType color1 = ColorType.None;
    public ColorType color2 = ColorType.None;

    public GameObject playerPrefab;
    public CameraController cameraController;
    
    private GameObject splitObject;
    
    // Variables for Switching Playable Balls
    public bool isControllable = true;
    public PlayerController parentObject;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        // If not controllable, don't process the controls.
        if (!isControllable) return;
        
        // Horizontal movement
        movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * movement, rb.velocity.y);

        // Jump and its conditions
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
        
        // Press "N" to SPLIT when the player has two colors
        if (Input.GetKeyDown(KeyCode.N) && color1 != ColorType.None && color2 != ColorType.None)
            Split();
        
        // Press "M" to MERGE when there are two balls present
        if (Input.GetKeyDown(KeyCode.M) && (splitObject != null || parentObject != null))
            Merge();
        
        // Press "1" to switch camera location to Ball 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
            cameraController.SwitchControl(parentObject != null ? parentObject.gameObject : this.gameObject);
    
        // Press "2" to switch camera location to Ball 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && splitObject != null)
            cameraController.SwitchControl(splitObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
    
    void Split()
    {
        splitObject = Instantiate(playerPrefab, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
        PlayerController splitController = splitObject.GetComponent<PlayerController>();

        splitController.parentObject = this;
        
        splitController.color1 = color2;
        splitController.color2 = ColorType.None;
        splitController.isControllable = false;
        color2 = ColorType.None;
        UpdateColor();
        splitController.UpdateColor();
    }

    void Merge()
    {
        if (splitObject != null)
        {
            color2 = splitObject.GetComponent<PlayerController>().color1;
            Destroy(splitObject);
            splitObject = null;
            UpdateColor();
        }
        
        // Merging when the current object is a split object
        else
        {
            // This is a splitObject, so we should merge into the parentObject.
            parentObject.transform.position = this.transform.position; // Move parentObject to splitObject's location
            parentObject.color2 = parentObject.color1;
            parentObject.color1 = this.color1; // Transfer color from splitObject to parentObject
            parentObject.UpdateColor(); // Update the color of the parentObject
            parentObject.splitObject = null; // Clear the splitObject reference in parentObject
            cameraController.SwitchControl(parentObject.gameObject); // Switch control to parentObject
            Destroy(this.gameObject); // Destroy the splitObject
        }
    }

    void UpdateColor()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

        if (color1 == ColorType.None && color2 == ColorType.None)
        {
            renderer.color = Color.black; // If the player has no color, it's black.
        }
        else if (color1 != ColorType.None && color2 != ColorType.None)
        {
            renderer.color = Color.white; // If the player has two colors, it's white.
        }
        else
        {
            ColorType presentColor = color1 != ColorType.None ? color1 : color2;
            switch (presentColor)
            {
                case ColorType.Blue:
                    renderer.color = Color.blue;
                    break;
                case ColorType.Green:
                    renderer.color = Color.green;
                    break;
                case ColorType.Purple:
                    renderer.color = new Color(0.5f, 0, 0.5f); // RGB for Purple
                    break;
                case ColorType.Red:
                    renderer.color = Color.red;
                    break;
                default:
                    renderer.color = Color.black;
                    break;
            }
        }
    }
}
