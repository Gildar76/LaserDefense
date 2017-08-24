using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
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

}
