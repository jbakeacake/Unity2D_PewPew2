using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public float lerpSpeed;
    private Camera miniMapCamera;
    private Vector2Int activeCoordinate;
    void Start()
    {
        this.miniMapCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector2 xyPos = Vector2.Lerp(miniMapCamera.transform.position,
            activeCoordinate * RoomManager.DISTANCE_BETWEEN_ROOMS, Time.deltaTime * lerpSpeed);
        miniMapCamera.transform.position = new Vector3(xyPos.x, xyPos.y, -1f);
    }
    
    public void setActiveCoordinate(Vector2Int coordinate)
    {
        this.activeCoordinate = coordinate;
    }
}
