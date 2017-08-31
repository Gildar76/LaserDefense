using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarController : MonoBehaviour
{
    public Vector3 position;
    public IPowerable parent;
    public LineRenderer bar;



    private void Start()
    {
        position = transform.position;

        bar = GetComponent<LineRenderer>();
        bar.positionCount = GameManager.instance.defaulPowerBarPositions.Length;
        bar.SetPositions(GameManager.instance.defaulPowerBarPositions);
        parent = transform.parent.gameObject.GetComponent<CannonController>();
        UpdatePowerBar();
    }

    private void Update()
    {
        if (parent == null) parent = transform.parent.gameObject.GetComponent<IPowerable>();
        if (parent.GetType() == typeof(CannonController)) transform.position = position;
        transform.rotation = Quaternion.identity;
    }
    public virtual void UpdatePowerBar()
    {

        if (GameManager.instance.GameState != GameState.Running) return;
        if (parent == null) parent = transform.parent.gameObject.GetComponent<IPowerable>();
        if (bar == null) bar = GetComponent<LineRenderer>();
        
        if (parent.GetType() == typeof(PlayerController))Debug.Log(parent.GetCurrentPower());
        
        if (bar.positionCount != (int)parent.GetCurrentPower() + 1)
        {
            //Get a value to divide with to make sure we use one position for each 10 percent of power.
            float powerDivider = parent.GetMaximumPower() / 10;
            bar.positionCount = (int)parent.GetCurrentPower() + 1;
            for (int i = 0; i <= (int)parent.GetCurrentPower(); i++)
            {
                if ((int)(i / powerDivider) > (int)parent.GetMaximumPower()) break;
                int positionIndex = (int)Mathf.Clamp((i / powerDivider), 0.0f, parent.GetMaximumPower() / powerDivider);
                bar.SetPosition(positionIndex, GameManager.instance.defaulPowerBarPositions[positionIndex]);
            }
        }

        if (parent.GetCurrentPower() / parent.GetMaximumPower() > 0.67f)
        {
            bar.startColor = Color.green;
            bar.endColor = Color.green;
        } else if(parent.GetCurrentPower() / parent.GetMaximumPower() > 0.33f )
        {
            bar.startColor = Color.yellow;
            bar.endColor = Color.yellow;
        } else
        {
            bar.startColor = Color.red; // new Color(105, 25, 19);
            bar.endColor = Color.red;

        }


    }



}
