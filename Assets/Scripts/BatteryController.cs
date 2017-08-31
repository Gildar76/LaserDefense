using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour, IPowerable
{

    public float batteryPower;
    public float transferRate;

    void Start()
    {
        batteryPower = GameManager.instance.BatteryPower;
    }

    private void OnEnable()
    {
        if (GameManager.instance != null) batteryPower = GameManager.instance.BatteryPower;
    }

    public float GetCurrentPower()
    {
        return batteryPower;
    }

    public float GetMaximumPower()
    {
        return 0.0f;
    }

    public void SetCurrentPower(float power)
    {
        batteryPower = power;
    }

    public void SetMaximumpower(float power)
    {
        return;
    }
}
