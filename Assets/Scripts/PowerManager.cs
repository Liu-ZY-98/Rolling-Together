using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PowerManager : MonoBehaviour
{
    public TextMeshProUGUI PowerIndicator;

    public TextMeshProUGUI PowerLevelIndicator;

    public SpriteRenderer playerRenderer;

    private string basePowerIndicatorText = "Power: ";

    private string basePowerLevelIndicatorText = "Power %: ";

    private List<Power> powers;

    private int powerIndex;

    void Awake()
    {
        powers = new List<Power>(2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            powerIndex = (powerIndex + 1) % Math.Max(1, powers.Count);
            SetActivePower(powerIndex);
        }
    }

    public Power.PowerType? GetActivePowerType()
    {
        if (powers.Count == 0) return null;
        return powers[powerIndex].Type;
    }

    public bool AddPower(Power power)
    {
        if (powers.Count >= 2) return false;

        powers.Add(power);
        SetActivePower(powers.Count - 1);

        return true;
    }

    public void UseActivePower(int cost)
    {
        powers[powerIndex].Level -= cost;
        SetActivePower(powerIndex);
    }

    private void SetActivePower(int powerIndex)
    {
        Power power = powers[powerIndex];
        PowerIndicator.text = $"{basePowerIndicatorText}{power.Name}";
        PowerLevelIndicator.text = $"{basePowerLevelIndicatorText}{power.Level}";
        playerRenderer.color = power.Color;
        Debug.Log(power.Color);
    }
}
