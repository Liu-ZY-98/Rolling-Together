using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float movement;

    public float jump;
    public bool isJumping;

    private Rigidbody2D rb;
    
    public enum ColorType { None, Blue, Green, Purple, Red }
    public ColorType color1 = ColorType.None;
    public ColorType color2 = ColorType.None;

    public GameObject playerPrefab;
    public CameraController cameraController;
    
    private GameObject splitObject;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement
        movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * movement, rb.velocity.y);

        // Jump and its conditions
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
        
        // Press "J" to SPLIT when the player has two colors
        if (Input.GetKeyDown(KeyCode.N) && color1 != ColorType.None && color2 != ColorType.None)
            Split();
        
        // Press "K" to MERGE when there are two balls present
        if (Input.GetKeyDown(KeyCode.M) && splitObject != null)
            Merge();
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
        splitObject.GetComponent<PlayerController>().color1 = color2;
        splitObject.GetComponent<PlayerController>().color2 = ColorType.None;
        color2 = ColorType.None;
    }

    void Merge()
    {
        color2 = splitObject.GetComponent<PlayerController>().color1;
        Destroy(splitObject);
        splitObject = null;
    }
}
