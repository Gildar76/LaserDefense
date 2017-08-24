using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  
    public float speed = 20.0f;
    
    private float playerPower = 20.0f;
    private Vector3 movement;
    public float powerTransforSpeed = 2.0f;
    private CannonController currentLaser = null;
    private LineRenderer lr;
    private Vector3 lrTextureOffset;
    private AudioSource transferSound;
    bool recharging = false;
    public float maximumPower = 20.0f;


    private void Start()
    {
        GameManager.instance.PowerChange += OnChangePower;

        GameManager.instance.PlayerPower = playerPower;
        lrTextureOffset = new Vector3(0, 0, 0);
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        transferSound = GetComponent<AudioSource>();
    }

    public void OnChangePower()
    {
        playerPower = GameManager.instance.PlayerPower;
        Debug.Log("playerPower: " + playerPower);
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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Batteries")
        {
            Debug.Log("Found battery");
            recharging = true;
        } else
        {
            recharging = false;
            currentLaser = other.gameObject.GetComponent<CannonController>();
            //Debug.Log("Found cannon");

        }
        transferSound.Play();
    }

    private void OnTriggerStay(Collider other)
    {


        if (currentLaser != null)
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerPower > maximumPower / 2.0f)
            {
                //Supercharge cannon
                float powerTotransfer = playerPower;
                currentLaser.currentPower += powerTotransfer;
                currentLaser.powerBarController.UpdatePowerBar();
                GameManager.instance.ChangePlayerPower(-powerTotransfer);
            }
            recharging = false;
            if (currentLaser.currentPower < currentLaser.maximumPower && playerPower > 0.0f)
            {
                lrTextureOffset.x -= Time.deltaTime * 10.0f;
                lr.enabled = true;
                lr.material.SetTextureOffset("_MainTex", lrTextureOffset);
                float powerTotransfer = powerTransforSpeed * Time.deltaTime;
                currentLaser.currentPower += powerTotransfer;
                currentLaser.powerBarController.UpdatePowerBar();
                GameManager.instance.ChangePlayerPower(-powerTotransfer);

            } 
            else
            {
                transferSound.Stop();
                lr.enabled = false;
            }

        }
        else if (recharging && playerPower < maximumPower && GameManager.instance.BatteryPower > 0.0f)
        {
            lrTextureOffset.x += Time.deltaTime * 10.0f;
            lr.enabled = true;
            lr.material.SetTextureOffset("_MainTex", lrTextureOffset);
            float powerTotransfer = powerTransforSpeed * Time.deltaTime;
            GameManager.instance.ChangePlayerPower(powerTotransfer);
            GameManager.instance.ChangePower(-powerTotransfer);
            Debug.Log("Recharging!");

        }
        else
        {
            transferSound.Stop();
            //recharging = false;
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        lr.enabled = false;
        currentLaser = null;
        transferSound.Stop();
        recharging = false;
    }

}
