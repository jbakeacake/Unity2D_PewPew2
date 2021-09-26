using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobRoom : MonoBehaviour
{
    public int numberOfEnemies = 10;
    public int allowedToSpawnAtOnce = 3;
    public float spawnRate = 5.0f;
    public RoomOptions roomOptions;
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
                RandomSpawner.trySpawnObject(roomOptions, enemies);
                enemiesSpawnedIn++;
                currentEnemies++;
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}