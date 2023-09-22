using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    public GameObject PowerObject;

    private Power power;

    private PowerManager powerManager;

    void Awake()
    {
        powerManager = FindObjectsOfType<PowerManager>()[0];
        power = PowerObject.GetComponent<Power>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (powerManager.AddPower(power))
            {
                Destroy(gameObject);
            }
        }
    }
}
