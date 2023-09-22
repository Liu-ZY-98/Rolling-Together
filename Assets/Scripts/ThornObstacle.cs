using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornObstacle : MonoBehaviour
{
    public int FireCost;

    private PowerManager powerManager;

    void Awake()
    {
        powerManager = FindObjectsOfType<PowerManager>()[0];
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Power.PowerType? powerType = powerManager.GetActivePowerType();
        if (powerType == Power.PowerType.FIRE)
        {
            powerManager.UseActivePower(FireCost);
            Destroy(gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
