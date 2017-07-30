using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public float explosionScaleFactor = 0.5f;
    public float explosionSPeed = 1.0f;
    public Vector3 explosionScale;

    private void Start()
    {
        Debug.Log("Starting explosion");
        explosionScale = new Vector3(0.0f, 0.0f, 0.0f);
    }
    private void OnEnable()
    {
        explosionScale = new Vector3(0, 0, 0);
        this.transform.localScale = explosionScale;
        
    }
    private void LateUpdate()
    {

    }
    private void Update()
    {
        if (explosionScale.x > 3.0f && explosionSPeed > 0.0f)
        {
            explosionSPeed *= -2.0f;

        }
        Debug.Log("Testing");
        float addToScale = Time.deltaTime * explosionScaleFactor * explosionSPeed;
        explosionScale.x += addToScale;
        explosionScale.y += addToScale;
        transform.localScale = explosionScale;
        if (explosionScale.x < 0.1f && explosionSPeed < 0.0f) GameObject.Destroy(this.gameObject);
    }
}
