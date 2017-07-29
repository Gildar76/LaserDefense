using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action ScoreChange;
    public event Action PowerChange;
    public float enemySpeed = -5.0f;
    public float enemySpeedChange = 0.5f;
    public static GameManager instance;
    public float spawnDelay = 10.0f;
    public float SpawnDelayChangeOverTime = 0.03f;
    public float minSpawenDelay = 0.1f;
    private int numEnemiesToSpawn = 2;
    private float timeSinceLastSpawnIncrease = 0.0f;
    private int score;
    private float totalPower;



    public float timeSinceLastSpawn = 0.0f;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            ScoreChange();
        }
    }

    public float TotalPower
    {
        get
        {
            return totalPower;
        }

        set
        {
            totalPower = value;
            PowerChange();
        }
    }

    private void Awake()
    {
        
        timeSinceLastSpawn = 0.0f;
        instance = this;
        Debug.Log("Instance created");
    }
    private void Update()
    {
        timeSinceLastSpawnIncrease += Time.deltaTime;
        //Debug.Log("Running update");
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnDelay)
        {
            enemySpeed -= enemySpeedChange;
            int i = 0;
            while (i < numEnemiesToSpawn) {
                SpawnManager.instance.SpawnEnemy(enemySpeed);
                i++;
            }
            if (timeSinceLastSpawnIncrease > 10.0f)
            {
                numEnemiesToSpawn++;
                timeSinceLastSpawnIncrease = 0.0f;
            }
            if (spawnDelay < minSpawenDelay) spawnDelay -= SpawnDelayChangeOverTime;
            timeSinceLastSpawn = 0.0f;


        }
    }

    public void ChangePower(float addPower)
    {
        totalPower += addPower;
        PowerChange();

    }

    public void AddScore(int addScore)
    {
        score += addScore;
        ScoreChange();

    }
}
