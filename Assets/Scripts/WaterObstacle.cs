using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObstacle : MonoBehaviour
{
    public int FireCost;

    public int IceCost;

    private PowerManager powerManager;

    private SpriteRenderer spriteRenderer;

    private Color iceColor = new Color(134f/255f, 206f/255f, 235f/255f);


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        powerManager = FindObjectsOfType<PowerManager>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Power.PowerType? powerType = powerManager.GetActivePowerType();
        if (powerType == null)
        {
            Destroy(other.gameObject);
        }
        if (powerType == Power.PowerType.FIRE)
        {
            powerManager.UseActivePower(FireCost);
            Destroy(gameObject);
        }
        if (powerType == Power.PowerType.ICE)
        {
            powerManager.UseActivePower(IceCost);
            spriteRenderer.color = iceColor;
        }
    }
}
