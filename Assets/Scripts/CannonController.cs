using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 2f;
    public string enemyTag;
    public GameObject currentTarget;
    public float firingPowerCost = 1f;
    public float currentPower = 5f;
    public float maximumPower = 10f;
    private float firingDelay;
    private float timeSinceLastFire;
    private LaserController laserController;
    private CableController cableController;
    private float lookRotationTime = 0.5f;
    public powerBarController powerBarController;

    private void Start()
    {
        powerBarController = GetComponentInChildren<powerBarController>();
        timeSinceLastFire = 0.0f;
        firingDelay = (currentPower > 0.0f) ? 10.0f / currentPower : 9999;
        cableController = GetComponentInChildren<CableController>();
        laserController = GetComponentInChildren<LaserController>();
        cableController.maximumPower = maximumPower;

    }

    private void OnEnable()
    {
        if (powerBarController  == null) powerBarController = GetComponentInChildren<powerBarController>();
        currentPower = 1.0f;
        powerBarController.UpdatePowerBar();
    }
    private void Update()
    {
        firingDelay = (currentPower > 1.0f) ? 10.0f / currentPower : 999999.0f;
        timeSinceLastFire += Time.deltaTime;
        if (timeSinceLastFire > firingDelay)
        {
            Fire();
            powerBarController.UpdatePowerBar();


        }

        cableController.currentPower = currentPower;

    }

    private void TargetEnemy()
    {
        if (timeSinceLastFire < lookRotationTime) return;
        if (currentTarget.transform.position.y < -5) return;
        Vector3 direction = new Vector3();
        
        direction = (currentTarget.transform.position - transform.position).normalized;
        direction.z = 0.0f;
        //Debug.Log("dir" + direction);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
        //Quaternion q2 = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotationSpeed);
        ////direction = Vector3.RotateTowards(transform.up, direction, rotationSpeed, 0.0f);
        //q2.z = q2.y;
        //q2.y = 0.0f;
        //transform.rotation = q2;
        ////Quaternion lookatRotation = Quaternion.LookRotation(direction);
        ////Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, lookatRotation, rotationSpeed);

        ////transform.rotation = lookatRotation;
        transform.up = direction;

    }

    private void Fire()
    {

        if (currentTarget == null)
        {
            FindClosestEnemy();
        }
        else
        {
            if (!currentTarget.activeInHierarchy)
            {
                FindClosestEnemy();
            }
            TargetEnemy();
        }
        laserController.Fire();
        timeSinceLastFire = 0;
        currentPower -= firingPowerCost;
    }

    private void FindClosestEnemy()
    {
        float closestDistance = 99999f;
        
        foreach (GameObject enemy in SpawnManager.instance.getEnemyList())
        {
            if (enemy.activeInHierarchy)
            {
                float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if (targetDistance < closestDistance)
                {
                    currentTarget = enemy;
                    closestDistance = targetDistance;
                }
            }
        }
    }

}
