using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int enemyCount = 50;
    public  GameObject enemy;
    public static SpawnManager instance;
    private static List<GameObject> enemies;
    public  float spawnY;
    public  float minSpawnX;
    public  float maxSpawnX; 

    private void Awake()
    {
        enemies = new List<GameObject>();
        FillEnemyList();
        instance = this;
    }

    private void FillEnemyList()
    {
        while (enemies.Count <= enemyCount) {
            GameObject go = Instantiate(enemy);
            go.SetActive(false);
            enemies.Add(go);

        }
    }

    public GameObject FindInactiveEnemy()
    {
        foreach (GameObject go in enemies)
        {
            if (!go.activeInHierarchy) return go; 
        }
        return null;
    }

    public void SpawnEnemy(float ySpeed)
    {
        GameObject newEnemy = FindInactiveEnemy();
        if (newEnemy != null)
        {
            Vector3 position = new Vector3(0, spawnY, 0);
            position.x = Random.Range(minSpawnX, maxSpawnX);
            newEnemy.transform.position = position;
            newEnemy.GetComponent<EnemyController>().movementVector.y = ySpeed;
            newEnemy.SetActive(true);


        }
    }

    public List<GameObject>  getEnemyList()
    {
        return enemies;
    }


}
