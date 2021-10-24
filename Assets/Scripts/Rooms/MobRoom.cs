using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using static Door;

public class MobRoom : Room
{
    public GameObject navMesh;
    public int allowedToSpawnAtOnce = 3;
    public float spawnRate = 5.0f;
    public GameObject[] enemies, destructibles;

    protected override void Awake()
    {
        base.Awake();
        this.activeEnemies = new List<EnemyController>();
    }
    protected override void onPlayerEnter(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasPlayerEntered)
            {
                hasPlayerEntered = true;
                StartCoroutine(GenerateEnemies());
            }
            roomManager.miniMapController.setActiveCoordinate(mapCoordinate);
        }
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
                activeEnemies.Add(enemyClone.GetComponent<EnemyController>());
                enemiesSpawnedIn++;
                currentEnemies++;
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
