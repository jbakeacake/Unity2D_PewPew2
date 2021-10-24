using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomOptions
{
    public float spawnCircleRadius;
    public Transform spawnCircleCenter;
    public List<Transform> corridorOrigins;
    public LayerMask spawnLayers;
    public GameObject corridorPrefab, wallPrefab, doorPrefab;
}
