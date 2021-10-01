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
    public CorridorPosition[] corridorPositions;

    public enum Direction
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT,
        NONE
    }
    [Serializable]
    public struct CorridorPosition
    {
        public Direction direction;
        public Transform transform;
        public bool isCorridor;
        public bool isInstantiated;
    }
}
