using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerBarController : MonoBehaviour
{
    public Vector3 position;
    public CannonController parent;
    public LineRenderer bar;



    private void Start()
    {
        position = transform.position;
        for (int i = 0; i < GameManager.instance.defaultPokwerBarPositions.Length; i++  )
        {
            //bar.SetPosition(i, GameManager.instance.defaultPokwerBarPositions[i]);


        }
        bar = GetComponent<LineRenderer>();
        bar.positionCount = GameManager.instance.defaultPokwerBarPositions.Length;
        bar.SetPositions(GameManager.instance.defaultPokwerBarPositions);
        parent = transform.parent.gameObject.GetComponent<CannonController>();
        UpdatePowerBar();
    }

    private void Update()
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
    }
    public void UpdatePowerBar()
    {

        if (GameManager.instance.GameState != GameState.Running) return;
        if (parent == null) parent = transform.parent.gameObject.GetComponent<CannonController>();
        if (bar == null) bar = GetComponent<LineRenderer>();
        if (bar.positionCount != (int)parent.currentPower + 1)
        {
            bar.positionCount = (int)parent.currentPower + 1;
            for (int i = 0; i <= (int)parent.currentPower; i++)
            {
                bar.SetPosition(i, GameManager.instance.defaultPokwerBarPositions[i]);
            }
        }

        if (parent.currentPower / parent.maximumPower > 0.67f)
        {
            bar.startColor = Color.green;
            bar.endColor = Color.green;
        } else if(parent.currentPower / parent.maximumPower > 0.33f )
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
