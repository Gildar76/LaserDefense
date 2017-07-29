using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableController : MonoBehaviour
{
    public float maximumPower;
    public float currentPower;
    private LineRenderer lr;
    private Color color;

	void Start()
    {
        lr = GetComponent<LineRenderer>();
        color = lr.material.GetColor("_TintColor");

	}
	

	void Update()
    {
        transform.rotation = Quaternion.identity;
        lr.material.SetColor("_TintColor", Color.Lerp(Color.blue, color, currentPower / maximumPower));
//        beamLight.intensity = Mathf.Lerp(beamLight.intensity, 0.0f, fadeSpeed * Time.deltaTime);
    }
}
