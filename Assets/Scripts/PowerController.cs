using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerController : MonoBehaviour
{
    Text val;  
    private void Start()
    {
        GameManager.instance.BatteryPowerChange += OnPowerChange;
        val = GetComponent<Text>();


    }
    private void OnPowerChange()
    {
        val.text = Mathf.Floor(GameManager.instance.BatteryPower).ToString();
        if (GameManager.instance.BatteryPower > 150)
        {
            val.color = Color.green;
        }
        else if (GameManager.instance.BatteryPower > 75) 
        {
            val.color = Color.yellow;
        }
        else
        {
            val.color = Color.red;
        }

    }
}
