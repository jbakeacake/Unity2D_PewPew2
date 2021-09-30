using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomOptions
{
    public float spawnCircleRadius;
    public Transform spawnCircleCenter;
    public LayerMask spawnLayers;
    public GameObject corridorPrefab, wallPrefab;
    public Transform[] corridorPositions;
    public bool isInitialRoom;
    public bool hasExit;
}
