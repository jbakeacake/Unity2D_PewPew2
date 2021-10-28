using System;
using System.Linq;
using UnityEngine;
using static UnityEngine.Object;

public class RandomSpawner
{
    public static GameObject trySpawnObject(RoomOptions options, GameObject[] objectPrefabs)
    {
        if (objectPrefabs == null || objectPrefabs.Length == 0)
        {
            throw new Exception("Missing object prefab(s) to spawn...");
        }

        GameObject objectToSpawn = objectPrefabs.Skip(Utils.getRandomInteger(0, objectPrefabs.Length)).First();
        Vector3 spawnLocation = getRandomSpawnLocation(options.spawnCircleRadius, options.spawnCircleCenter, options.spawnLayers);

        return Instantiate(objectToSpawn,
            spawnLocation,
            Quaternion.identity);
    }

    private static Vector2 getRandomSpawnLocation(float spawnCircleRadius, Transform spawnCircleCenter, LayerMask spawnLayers)
    {
        float minValue = -2;
        float maxValue = 2;
        float xDirection = Utils.getRandomFloat(minValue, maxValue);
        float yDirection = Utils.getRandomFloat(minValue, maxValue);

        // Get a random point in our room
        float randomDistance = Utils.getRandomFloat(1.0f, spawnCircleRadius);
        Vector3 direction = new Vector3(xDirection, yDirection, 0f).normalized * randomDistance;
        Vector3 origin = spawnCircleCenter.position + direction;

        float distanceAbove = -10f;

        origin.z = distanceAbove;

        return origin;
    }
}