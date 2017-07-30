using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Vector3 movementVector;
    public PlayerController playerController; 

    private void Update()
    {
        transform.Translate(movementVector * Time.deltaTime);
        if (transform.position.y < -30.0f) gameObject.SetActive(false);
        if (transform.position.y < -23.0f)
        {
            //GameObject.Instantiate(SpawnManager.instance.explosion, transform);
            this.gameObject.SetActive(false);

            GameManager.instance.ChangePower(-5.0f); 
        }
    }
}
