using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  
    public float speed = 20.0f;
    
    private float totalPower = 200.0f;
    private Vector3 movement;
    public float powerTransforSpeed = 2.0f;
    private CannonController currentLaser = null;
    private LineRenderer lr;
    private Vector3 lrTextureOffset;
    private AudioSource transferSound;

    private void Start()
    {
        GameManager.instance.PowerChange += OnChangePower;

        GameManager.instance.TotalPower = totalPower;
        lrTextureOffset = new Vector3(0, 0, 0);
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        transferSound = GetComponent<AudioSource>();
    }

    public void OnChangePower()
    {
        totalPower = GameManager.instance.TotalPower;
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(movement);
        if (transform.position.x > 35.0f) transform.position = new Vector3(35.0f, transform.position.y, transform.position.z);
        if (transform.position.x < -30.0f) transform.position = new Vector3(-30.0f, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentLaser = other.gameObject.GetComponent<CannonController>();
        Debug.Log("Found cannon");
        transferSound.Play();
    }

    private void OnTriggerStay(Collider other)
    {


        if (currentLaser != null)
        {
            if (currentLaser.currentPower < currentLaser.maximumPower && totalPower > 0.0f)
            {
                lrTextureOffset.x -= Time.deltaTime * 10.0f;
                lr.enabled = true;
                lr.material.SetTextureOffset("_MainTex", lrTextureOffset);
                float powerTotransfor = powerTransforSpeed * Time.deltaTime;
                currentLaser.currentPower += powerTotransfor;
                GameManager.instance.ChangePower(-powerTotransfor);

            }
            else
            {
                transferSound.Stop();
                lr.enabled = false;
            }

        } else
        {
            transferSound.Stop();
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        lr.enabled = false;
        currentLaser = null;
        transferSound.Stop();
    }

}
