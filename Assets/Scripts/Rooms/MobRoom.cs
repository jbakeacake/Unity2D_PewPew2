using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRoom : Room
{
    public GameObject navMesh;
    public int numberOfEnemies = 10;
    public int allowedToSpawnAtOnce = 3;
    public float spawnRate = 5.0f;
    public GameObject[] enemies, destructibles;

    void Start()
    {
        //TODO GenerateDestructibles();
        StartCoroutine(GenerateEnemies());
    }

    private void GenerateDestructibles()
    {
        throw new NotImplementedException();
    }

    private IEnumerator GenerateEnemies()
    {
        for (int currentEnemies = 0; currentEnemies < numberOfEnemies;)
        {
            for (int enemiesSpawnedIn = 0; enemiesSpawnedIn < allowedToSpawnAtOnce;)
            {
                GameObject enemyClone = RandomSpawner.trySpawnObject(roomOptions, enemies);
                enemyClone.transform.SetParent(navMesh.transform);
                enemiesSpawnedIn++;
                currentEnemies++;
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
