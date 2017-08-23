using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterController : MonoBehaviour
{
    [Range(1,60)]
    public float thrusterFlickerSpeed;
    [Range(0.0f, 0.5f)]
    public float thrusterFlickerAmount;
    [Range(1,10)]
    public float maxLength;
    public bool changeWithSpeed;

    private Light thrusterLight;
    private LineRenderer thrusterRenderer;
    private Material thrusterMaterial;
    private float lightIntencity;
    private Vector3 previousPosition;
    private float speed;
    private float length;
    private Color color;
    private float timeSinceLastFlicker = 0;

    private void Start()
    {
        thrusterRenderer = GetComponent<LineRenderer>();
        thrusterLight = GetComponentInChildren<Light>();
        lightIntencity = thrusterLight.intensity;
        thrusterMaterial = thrusterRenderer.material;
        color = thrusterMaterial.GetColor("_TintColor");

        thrusterRenderer.SetPosition(1, Vector3.forward * length);

    }

    private void Update()
    {
        length = maxLength;
        if (changeWithSpeed)
        {
            float speed = (transform.position - previousPosition).magnitude;
            length = length * speed * 5.0f;
            previousPosition = transform.position;
        }

        timeSinceLastFlicker += Time.deltaTime;
        if (timeSinceLastFlicker >= 1 / thrusterFlickerSpeed)
        {
            Flicker();
            timeSinceLastFlicker = 0.0f;
        }
    }

    public void Flicker()
    {

        float currentNoise = Random.Range(1 - thrusterFlickerAmount, 1);
        Debug.Log(currentNoise);
        thrusterMaterial.SetColor("_TintColor", color * currentNoise);
        thrusterRenderer.SetPosition(1, Vector3.up * length * currentNoise);
        
        thrusterLight.intensity =  (Mathf.Clamp(lightIntencity, 0, 5)) * currentNoise;
    }

}
